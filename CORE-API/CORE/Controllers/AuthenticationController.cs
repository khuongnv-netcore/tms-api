using AutoMapper;
using CORE_API.CORE.Controllers;
using CORE_API.CORE.Helpers.Abstract;
using CORE_API.CORE.Helpers.Attributes;
using CORE_API.CORE.Models.Configuration;
using CORE_API.CORE.Models.Views;
using CORE_API.CORE.Models.Entities;
using CORE_API.CORE.Services.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Linq.Expressions;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Security.Claims;
using System.Linq;
using System.IO;
using Newtonsoft.Json;
using CORE_API.CORE.Exceptions;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using LinqKit;
using Auth0.ManagementApi;
using Auth0.AuthenticationApi;
using Auth0.AuthenticationApi.Models;
using UserAuth0 = Auth0.ManagementApi.Models;

namespace CORE_API.CORE
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : GenericEntityController<Authentication, AuthenticationInputResource, AuthenticationOutputResource>
    {
        private readonly IGenericEntityService<User> _userEntityService;
        private readonly AuthenticationApiClient _authClient;
        private readonly ManagementApiClient _managementClient;

        public AuthenticationController(IGenericEntityService<User> userEntityService, IControllerHelper controllerHelper, IGenericEntityService<Authentication> entityService,

            IOptions<CoreConfigurationOptions> coreConfigurationOptions, IMapper mapper)
            : base(controllerHelper, entityService, coreConfigurationOptions, mapper)
        {
            _userEntityService = userEntityService;
            _authClient = new AuthenticationApiClient(new System.Uri(_coreConfigurationOptions.AuthenticationOptions.Domain));
            _managementClient = new ManagementApiClient(_coreConfigurationOptions.AuthenticationOptions.TokenUrl, new System.Uri(_coreConfigurationOptions.AuthenticationOptions.ManagementAudience));
        }

        [HttpPost]
        [Authorize, Authorize(Roles = "Administrator,Integration")]
        [SwaggerSummary("Create AuthenticationController")]
        public async Task<AuthenticationOutputResource> CreateToken()
        {
            var userFromToken = GetCurrentUser();

            var user = _userEntityService.FindById(userFromToken.Id);
            if (user == null) {
                throw new ApiDatabaseException("User Not Found!");
            }

            Guid tokenId = Guid.NewGuid();
            // Create Jwt token
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, tokenId.ToString()),
                new Claim(ClaimTypes.AuthenticationMethod, _coreConfigurationOptions.CustomAuthenticationOptions.AuthenticationMethod),
                new Claim(ClaimTypes.Email, user.EmailAddress)
            };
            SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_coreConfigurationOptions.CustomAuthenticationOptions.Secret));
            SigningCredentials creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(_coreConfigurationOptions.CustomAuthenticationOptions.CustomTokenExpirationDay),
                SigningCredentials = creds
            };
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);

            // Save Token into database
            var entity = new Authentication();
            entity.Token = tokenHandler.WriteToken(token);
            entity.Id = tokenId;
            entity.IsExpired = true;
            entity.ExpiredDate = tokenDescriptor.Expires;
            entity.UserId = user.Id;
            var result = await _entityService.AddAsync(entity);

            var output = _mapper.Map<Authentication, AuthenticationOutputResource>(result.Entity);
            return output;
        }

        [HttpGet]
        [Authorize, Authorize(Roles = "Administrator,Integration")]
        [SwaggerSummary("List AuthenticationController")]
        public override async Task<CoreListOutputResource<AuthenticationOutputResource>> List(int skip = 0, int count = 20)
        {

            var user = GetCurrentUser();
            var where = PredicateBuilder.New<Authentication>(true);

            if(!user.UserRoles.ToList().Select(r => r.Role.DisplayName).Contains("Administrator")){
                where = PredicateBuilder.New<Authentication>(
                    m => m.UserId == user.Id);
            }

            var results = _entityService.FindQueryableList(skip, count, where);
            var total = await _entityService.Count(where);

            var output = new CoreListOutputResource<AuthenticationOutputResource>
            {
                Entities = _mapper.Map<IEnumerable<Authentication>, IList<AuthenticationOutputResource>>(results.ToList()),
                TotalEntities = total
            };

            return output;
        }

        [HttpGet("{id}")]
        [Authorize, Authorize(Roles = "Administrator,Integration")]
        [SwaggerSummary("Read One AuthenticationController")]
        public override AuthenticationOutputResource Read(Guid id)
        {
            return base.Read(id);
        }

        [HttpPut]
        [Authorize, Authorize(Roles = "Administrator,Integration")]
        [SwaggerSummary("Update AuthenticationController")]
        public override Task<AuthenticationOutputResource> Update(Guid id, AuthenticationInputResource resource)
        {
            return base.Update(id, resource);
        }

        [HttpDelete]
        [Authorize, Authorize(Roles = "Administrator,Integration")]
        [SwaggerSummary("Delete AuthenticationController")]
        public override Task<AuthenticationOutputResource> Delete(Guid id)
        {
            return base.Delete(id);
        }

        [HttpPost("login")]
        [SwaggerSummary("Login AuthenticationController")]
        public async Task<LoginResponse> Login([FromBody] LoginRequest loginRequest)
        {
           var loginResponse = new LoginResponse
            {
                ResponseCode = "401",
                message = "Fail",
                UserInfo = null 
            };
            var result = await _authClient.GetTokenAsync(new ResourceOwnerTokenRequest
            {
                ClientId = _coreConfigurationOptions.AuthenticationOptions.ApiClientId,
                ClientSecret = _coreConfigurationOptions.AuthenticationOptions.ManagementClientSecret,
                Scope = "openid profile email",
                Username = loginRequest.Username,
                Password = loginRequest.Password
            });

            if (result == null)
            {
                return loginResponse;
            }

            var userInfo = await _authClient.GetUserInfoAsync(result.AccessToken);

            var existingUser = _userEntityService.FindOne(m => m.EmailAddress == userInfo.Email);

            if (existingUser != null) {
                var userOutputResource = _mapper.Map<User, UserOutputResource>(existingUser);
                loginResponse.ResponseCode = "200";
                loginResponse.message = "Success";
                loginResponse.UserInfo = new UserInfo
                {
                    token = result.AccessToken,
                    UserId = userOutputResource.Id,
                    FullName = existingUser.GetFullName(),
                    UserRoles = userOutputResource.Roles
                };
            }
            return loginResponse;
        }
    }

    public class LoginRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }

    public class LoginResponse
    {
        public string message { get; set; }
        public string ResponseCode { get; set; }
        public UserInfo? UserInfo { get; set; }
    }

    public class UserInfo
    {
        public string token { get; set; }
        public Guid UserId { get; set; }
        public string FullName { get; set; }
        public Guid EmployeeId { get; set; }
        public List<RoleOutputResource> UserRoles { get; set; }
    }
}
