using System;
using CORE_API.CORE.Models.Entities.Abstract;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using CORE_API.CORE.Models.Enums;
using CORE_API.Tms.Models.Entities;

namespace CORE_API.CORE.Models.Entities
{
    public class User : CoreEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public string EmailVerified { get; set; }
        public string AuthId { get; set; }
        public string StripeCustomerId { get; set; }
        public string SubscriptionProduct { get; set; }
        public DateTime? SubscriptionEndDate { get; set; }
        public string PhoneNumber { get; set; }
        public EUserType? UserType { get; set; }
        public Guid? EmployeeId { get; set; }

        public virtual IList<UserRole> UserRoles { get; set; }
        public virtual IList<UserOrganization> UserOrganizations { get; set; }

        public string GetFullName()
        {
            return FirstName + " " + LastName;
        }

        internal static void OnModelCreating(ModelBuilder builder)
        {
            //Config
            string tableName = "User";

            //Generic
            builder.Entity<User>().ToTable(tableName);
            builder.Entity<User>().HasKey(m => m.Id);
            builder.Entity<User>().Property(m => m.Id).IsRequired().ValueGeneratedOnAdd();

            //Entity Specific
            builder.Entity<User>().HasIndex(m => m.EmailAddress).IsUnique();
            builder.Entity<User>().HasIndex(m => m.AuthId).IsUnique();
            builder.Entity<User>().HasIndex(m => m.Created);
            builder.Entity<User>().HasIndex(m => m.Modified);
            builder.Entity<User>().HasIndex(m => m.StripeCustomerId);

            // QueryFilter for SoftDelete
            builder.Entity<User>().HasQueryFilter(m => m.DeletedAt == null);

            //Relationships
            builder.Entity<User>().HasMany(m => m.UserRoles).WithOne(m => m.User).HasForeignKey(m => m.UserId);
            builder.Entity<User>().HasMany(m => m.UserOrganizations).WithOne(m => m.User).HasForeignKey(m => m.UserId);
        }

        public override async Task OnSoftDeleteAsync(SoftDeletes.Core.DbContext context,
            CancellationToken cancellationToken = default)
        {
            var taskList = new List<Task> {
                context.RemoveRangeAsync(UserRoles, cancellationToken),
                context.RemoveRangeAsync(UserOrganizations, cancellationToken)
            };

            await Task.WhenAll(taskList);
        }

        public override void OnSoftDelete(SoftDeletes.Core.DbContext context)
        {
            context.RemoveRange(UserRoles);
            context.RemoveRange(UserOrganizations);
        }

        public override async Task LoadRelationsAsync(SoftDeletes.Core.DbContext context,
            CancellationToken cancellationToken = default)
        {
            var taskList = new List<Task> {
                context.Entry(this)
                    .Collection(user => user.UserRoles)
                    .LoadAsync(cancellationToken),
                context.Entry(this)
                    .Collection(user => user.UserOrganizations)
                    .LoadAsync(cancellationToken)
            };

            await Task.WhenAll(taskList);
        }

        public override void LoadRelations(SoftDeletes.Core.DbContext context)
        {
            context.Entry(this)
                .Collection(user => user.UserRoles)
                .Load();

            context.Entry(this)
                .Collection(user => user.UserOrganizations)
                .Load();
        }
    }
}
