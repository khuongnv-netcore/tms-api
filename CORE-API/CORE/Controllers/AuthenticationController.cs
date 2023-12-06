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

namespace CORE_API.CORE
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize, Authorize(Roles = "Administrator,Integration")]
    public class AuthenticationController : GenericEntityController<Authentication, AuthenticationInputResource, AuthenticationOutputResource>
    {
        private readonly IGenericEntityService<User> _userEntityService;
        public AuthenticationController(IGenericEntityService<User> userEntityService, IControllerHelper controllerHelper, IGenericEntityService<Authentication> entityService, IOptions<CoreConfigurationOptions> coreConfigurationOptions, IMapper mapper)
            : base(controllerHelper, entityService, coreConfigurationOptions, mapper)
        {
            _userEntityService = userEntityService;
        }

        [HttpPost]
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
        [SwaggerSummary("Read One AuthenticationController")]
        public override AuthenticationOutputResource Read(Guid id)
        {
            return base.Read(id);
        }

        [HttpPut]
        [SwaggerSummary("Update AuthenticationController")]
        public override Task<AuthenticationOutputResource> Update(Guid id, AuthenticationInputResource resource)
        {
            return base.Update(id, resource);
        }

        [HttpDelete]
        [SwaggerSummary("Delete AuthenticationController")]
        public override Task<AuthenticationOutputResource> Delete(Guid id)
        {
            return base.Delete(id);
        }
    }
}
