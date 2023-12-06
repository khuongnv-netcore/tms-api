using AutoMapper;
using CORE_API.CORE.Controllers;
using CORE_API.CORE.Exceptions;
using CORE_API.CORE.Helpers.Abstract;
using CORE_API.CORE.Helpers.Attributes;
using CORE_API.CORE.Models.Configuration;
using CORE_API.CORE.Models.Entities;
using CORE_API.CORE.Models.Views;
using CORE_API.CORE.Services.Abstract;
using LinqKit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CORE_API.CORE.Models.Enums;
using System.Linq.Expressions;

namespace CORE_API.CORE
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : GenericEntityController<User, UserInputResource, UserOutputResource>
    {

        private readonly IAuth0ManagementService _auth0ManagementService;
        private readonly IGenericEntityService<UserRole> _userRoleEntityService;

        public UserController(IAuth0ManagementService auth0ManagementService, IControllerHelper controllerHelper, IGenericEntityService<User> entityService, IGenericEntityService<UserRole> userRoleEntityService, IOptions<CoreConfigurationOptions> coreConfigurationOptions, IMapper mapper) 
            : base (controllerHelper, entityService, coreConfigurationOptions, mapper)
        {
            _auth0ManagementService = auth0ManagementService;
            _userRoleEntityService = userRoleEntityService;
        }

        [HttpGet("Filter")]
        [SwaggerSummary("Filter Users")]
        public async Task<CoreListOutputResource<UserOutputResource>> Filter(string SearchQuery, string byEmail, string byName, EUserOrderField? orderBy, bool isDesc = true, int skip = 0, int count = 20)
        {
            Expression<Func<User, object>> order;

            switch (orderBy)
            {
                case EUserOrderField.EmailAddress:
                    {
                        order = o => o.EmailAddress;
                        break;
                    }
                case EUserOrderField.FirstName:
                    {
                        order = o => o.FirstName;
                        break;
                    }
                case EUserOrderField.LastName:
                    {
                        order = o => o.LastName;
                        break;
                    }
                default:
                    {
                        order = o => o.Created;
                        break;
                    }
            }

            var where = PredicateBuilder.New<User>(true);

            if (SearchQuery != null)
            {
                where = PredicateBuilder.New<User>(
                    m => m.FirstName.Contains(SearchQuery) ||
                    m.LastName.Contains(SearchQuery) ||
                    m.EmailAddress.Contains(SearchQuery)
                    );
            }

            if (!string.IsNullOrEmpty(byEmail)) {
                where = where.And(m => m.EmailAddress.Contains(byEmail));
            }

            if (!string.IsNullOrEmpty(byName))
            {
                where = where.And(m => (m.FirstName + " " + m.LastName).Contains(byName));
            }

            var results = _entityService.FindQueryableList(skip, count, where, order, isDesc);

            if(orderBy == EUserOrderField.FullName) {
                if (isDesc)
                {
                    results = results.OrderByDescending(x => x.FirstName).ThenByDescending(x => x.LastName);
                }
                else {
                    results = results.OrderBy(x => x.FirstName).ThenBy(x => x.LastName);
                }
            }

            var total = await _entityService.Count(where);

            var output = new CoreListOutputResource<UserOutputResource>
            {
                Entities = _mapper.Map<IEnumerable<User>, IList<UserOutputResource>>(results.ToList()),
                TotalEntities = total
            };

            return output;
        }

        [HttpPut("ChangeEmailAddress")]
        [SwaggerSummary("Update users email address used for sign in")]
        public async Task<bool> ChangeEmailAddress(Guid id, string EmailAddress)
        {
            var user = _entityService.FindById(id);

            var result = await _auth0ManagementService.ChangeUserEmailAddress(user, EmailAddress);

            return result;
        }

        [HttpGet("CurrentUser")]
        [SwaggerSummary("Get current user from token")]
        public UserOutputResource GetCurrentUserFromToken()
        {
            var user = GetCurrentUser();

            var result = _mapper.Map<User, UserOutputResource>(user);

            return result;
        }

        [HttpGet("{id}")]
        public override UserOutputResource Read(Guid id)
        {
            return base.Read(id);
        }

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public override Task<UserOutputResource> Create(UserInputResource resource)
        {
            return base.Create(resource);
        }

        [HttpPut]
        public override Task<UserOutputResource> Update(Guid id, UserInputResource resource)
        {
            return base.Update(id, resource);
        }

        [HttpPut("SetRoles")]
        //[Authorize(Roles = "Administrator")]
        public async Task<bool> SetRoles(SetRolesResource resource)
        {
            var user = _entityService.FindById(resource.UserId);
            if(user == null)
            {
                throw new ApiNotFoundException();
            }

            var removeUserRoles = user.UserRoles.Where(m => !resource.RoleIds.Contains(m.RoleId)).ToList();

            var newRolesToAdd = resource.RoleIds.Where(m => !user.UserRoles.Select(f => f.RoleId).Contains(m));

            List<UserRole> addUserRoles = new List<UserRole>();

            foreach(Guid roleId in newRolesToAdd)
            {
                addUserRoles.Add(new UserRole
                {
                    UserId = user.Id,
                    RoleId = roleId
                });
            }

            var removeResult = await _userRoleEntityService.DeleteMany(removeUserRoles);
            var saveResult = await _userRoleEntityService.AddManyAsync(addUserRoles);


            return true;

        }
    }
}
