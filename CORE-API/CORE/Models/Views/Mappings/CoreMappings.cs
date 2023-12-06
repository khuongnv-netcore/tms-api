using AutoMapper;
using CORE_API.CORE.Models.Entities;
using System.Collections.Generic;

namespace CORE_API.CORE.Models.Views.Mappings
{
    public class CoreMappings : Profile
    {
        public CoreMappings()
        {

            #region User
            // Resource Input to Entity
            CreateMap<UserInputResource, User>();

            //Entity Output to Resource
            CreateMap<User, UserOutputResource>()
                .ForMember(dest => dest.Roles, opt => opt.MapFrom<UserRolesToList>())
                .ForMember(dest => dest.Organizations, opt => opt.MapFrom<UserOrganizationsToList>())
                ;
            #endregion

            #region UserRole
            // Resource Input to Entity
            CreateMap<UserRoleInputResource, UserRole>();

            //Entity Output to Resource
            CreateMap<UserRole, UserRoleOutputResource>();
            #endregion
            
            #region Role
            // Resource Input to Entity
            CreateMap<RoleInputResource, Role>();

            //Entity Output to Resource
            CreateMap<Role, RoleOutputResource>();
            #endregion

            #region Authentication
            CreateMap<AuthenticationInputResource, Authentication>();

            CreateMap<Authentication, AuthenticationOutputResource>();
            #endregion

            #region Organizations
            // Resource Input to Entity
            CreateMap<OrganizationInputResource, Organization>();

            //Entity Output to Resource
            CreateMap<Organization, OrganizationOutputResource>();
            #endregion

            #region UserOrganization
            // Resource Input to Entity
            CreateMap<UserOrganizationInputResource, UserOrganization>();

            //Entity Output to Resource
            CreateMap<UserOrganization, UserOrganizationOutputResource>();
            #endregion

        }
    }

    internal class UserRolesToList : IValueResolver<User, UserOutputResource, List<RoleOutputResource>>
    {
        public List<RoleOutputResource> Resolve(User source, UserOutputResource destination, List<RoleOutputResource> destMember, ResolutionContext context)
        {
            destMember = new List<RoleOutputResource>();
            foreach(var userRole in source.UserRoles)
            {
                destMember.Add(new RoleOutputResource { Id = userRole.RoleId, Created = userRole.Role.Created, DisplayName = userRole.Role.DisplayName, Modified = userRole.Role.Modified });
            }

            return destMember;
        }
    }

    internal class UserOrganizationsToList : IValueResolver<User, UserOutputResource, List<OrganizationOutputResource>>
    {
        public List<OrganizationOutputResource> Resolve(User source, UserOutputResource destination, List<OrganizationOutputResource> destMember, ResolutionContext context)
        {
            destMember = new List<OrganizationOutputResource>();
            foreach (var userOrganization in source.UserOrganizations)
            {
                destMember.Add(new OrganizationOutputResource { 
                    Id = userOrganization.OrganizationId, 
                    Created = userOrganization.Organization.Created,
                    Modified = userOrganization.Organization.Modified,
                    Name = userOrganization.Organization.Name
                });
            }

            return destMember;
        }
    }
}
