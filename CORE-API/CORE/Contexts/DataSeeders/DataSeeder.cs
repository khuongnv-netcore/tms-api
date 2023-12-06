using System;
using Microsoft.EntityFrameworkCore;
using CORE_API.CORE.Models.Entities;
using System.Threading.Tasks;
using CORE_API.CORE.Contexts;

namespace CORE_API.Contexts.DataSeeders
{
    public class DataSeeder
    {
        private readonly CoreContext _context;

        public DataSeeder(CoreContext context)
        {
            _context = context;
        }

        public async Task SeedData()
        {
            await InitialAdminUser();
            await InitialRoles();
        }

        private async Task InitialAdminUser()
        {
            var adminUser = await _context.Set<User>().FirstOrDefaultAsync(r => r.EmailAddress == "khuongnv@myfriends.com");
            if (adminUser == null)
            {
                adminUser = new User();
                adminUser.Id = Guid.NewGuid();
                adminUser.FirstName = "Khuong";
                adminUser.LastName = "Nguyen";
                adminUser.EmailAddress = "khuongnv1979@gmail.com";
                adminUser.EmailVerified = "true";
                adminUser.Created = DateTime.UtcNow;
                adminUser.Modified = DateTime.UtcNow;
                await _context.Set<User>().AddAsync(adminUser);
            }

            var adminRole = await _context.Set<Role>().FirstOrDefaultAsync(r => r.DisplayName == "Administrator");
            if (adminRole == null)
            {
                adminRole = new Role();
                adminRole.Id = Guid.NewGuid();
                adminRole.DisplayName = "Administrator";
                adminRole.Created = DateTime.UtcNow;
                adminRole.Modified = DateTime.UtcNow;
                await _context.Set<Role>().AddAsync(adminRole);
            }

            var adminUserRole = await _context.Set<UserRole>().FirstOrDefaultAsync(r => r.UserId == adminUser.Id && r.RoleId == adminRole.Id);
            if (adminUserRole == null)
            {
                adminUserRole = new UserRole();
                adminUserRole.Id = Guid.NewGuid();
                adminUserRole.UserId = adminUser.Id;
                adminUserRole.RoleId = adminRole.Id;
                adminUserRole.Created = DateTime.UtcNow;
                adminUserRole.Modified = DateTime.UtcNow;
                await _context.Set<UserRole>().AddAsync(adminUserRole);
            }

            await _context.SaveChangesAsync();
        }

        private async Task InitialRoles()
        {

            var integrationRole = await _context.Set<Role>().FirstOrDefaultAsync(r => r.DisplayName == "Integration");
            if (integrationRole == null)
            {
                integrationRole = new Role();
                integrationRole.Id = Guid.NewGuid();
                integrationRole.DisplayName = "Integration";
                integrationRole.Created = DateTime.UtcNow;
                integrationRole.Modified = DateTime.UtcNow;
                await _context.Set<Role>().AddAsync(integrationRole);
            }

            await _context.SaveChangesAsync();
        }
    }
}
