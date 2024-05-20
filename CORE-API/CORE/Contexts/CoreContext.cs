﻿using CORE_API.CORE.Models.Entities;
using CORE_API.Tms.Models.Entities;
using CORE_API.CORE.Models.Entities.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace CORE_API.CORE.Contexts
{
    public class CoreContext : SoftDeletes.Core.DbContext
    {
        public DbSet<AuditLog> AuditLogSet { get; set; }
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CoreContext(DbContextOptions<CoreContext> options, IHttpContextAccessor httpContextAccessor) : base(options)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public CoreContext(DbContextOptions<CoreContext> options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.UseCollation("SQL_Latin1_General_CP1_CS_AS");
            //builder.HasCharSet("utf8mb4");
            //builder.UseGuidCollation("latin1_swedish_ci");

            base.OnModelCreating(builder);

            User.OnModelCreating(builder);

            Role.OnModelCreating(builder);
            
            Schedule.OnModelCreating(builder);

            UserRole.OnModelCreating(builder);

            Authentication.OnModelCreating(builder);

            AuditLog.OnModelCreating(builder);

            Organization.OnModelCreating(builder);

            UserOrganization.OnModelCreating(builder);

            DistributedLock.OnModelCreating(builder);

            Booking.OnModelCreating(builder);

            BookingContainer.OnModelCreating(builder);

            BookingCharge.OnModelCreating(builder);

            BookingContainerDetail.OnModelCreating(builder);

            Driver.OnModelCreating(builder);

            FixedAsset.OnModelCreating(builder);

            Customer.OnModelCreating(builder);

            Location.OnModelCreating(builder);
            
            Employee.OnModelCreating(builder);

            Container.OnModelCreating(builder);

            PricingMaster.OnModelCreating(builder);

            PricingForCustomer.OnModelCreating(builder);
        }

        public override int SaveChanges()
        {
            PreProcessModels();
            return base.SaveChanges();
        }

        public override async Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {

            PreProcessModels();

            var auditLogList = CreateAuditLogs();

            var result = await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);

            await CompleteAuditLog(auditLogList);

            return result;

        }

        private void PreProcessModels()
        {
            AddTimestamps();
        }

        private void AddTimestamps()
        {
            var entities = ChangeTracker.Entries()
                .Where(x => x.Entity is CoreEntity && (x.State == EntityState.Added || x.State == EntityState.Modified));

            foreach (var entity in entities)
            {
                var now = DateTime.UtcNow;

                if (entity.State == EntityState.Added)
                {
                    ((CoreEntity)entity.Entity).Created = now;
                }
                ((CoreEntity)entity.Entity).Modified = now;

            }
        }

        private List<AuditLog> CreateAuditLogs()
        {
            var CurrentUserId = GetCurrentUserId();
            var TransactionId = Guid.NewGuid();

            ChangeTracker.DetectChanges();
            var auditEntries = new List<AuditLog>();
            foreach (var entry in ChangeTracker.Entries())
            {
                if (entry.Entity is AuditLog || entry.State == EntityState.Detached || entry.State == EntityState.Unchanged)
                    continue;

                var auditEntry = new AuditLog();
                auditEntry.TableName = entry.Metadata.GetTableName(); 
                auditEntry.TransactionId = TransactionId;
                auditEntry.UserId = CurrentUserId;

                
                var shouldSaveEntry = false;
                foreach (var property in entry.Properties)
                {
                    // The following condition is ok with EF Core 2.2 onwards.
                    // If you are using EF Core 2.1, you may need to change the following condition to support navigation properties: https://github.com/dotnet/efcore/issues/17700
                    // if (property.IsTemporary || (entry.State == EntityState.Added && property.Metadata.IsForeignKey()))
                    if (property.IsTemporary)
                    {
                        // value will be generated by the database, get the value after saving
                        auditEntry.TemporaryProperties.Add(property);
                        continue;
                    }

                    string propertyName = property.Metadata.Name;
                    if (property.Metadata.IsPrimaryKey())
                    {
                        auditEntry.KeyValuesDict[propertyName] = property.CurrentValue;
                        continue;
                    }

                    

                    switch (entry.State)
                    {
                        case EntityState.Added:
                            auditEntry.NewValuesDict[propertyName] = property.CurrentValue;
                            shouldSaveEntry = true;
                            break;

                        case EntityState.Deleted:
                            auditEntry.OldValuesDict[propertyName] = property.OriginalValue;
                            shouldSaveEntry = true;
                            break;

                        case EntityState.Modified:
                            //Ignore changes to created and modified fields
                            if (propertyName.Equals("Created") || propertyName.Equals("Modified"))
                            {
                                break;
                            }

                            if (property.IsModified && property.OriginalValue != property.CurrentValue)
                            {
                                auditEntry.OldValuesDict[propertyName] = property.OriginalValue;
                                auditEntry.NewValuesDict[propertyName] = property.CurrentValue;
                                shouldSaveEntry = true;
                            }
                            break;
                    }
                }

                //If there are changes, add it. Otherwise, skip it.
                if (shouldSaveEntry)
                {
                    auditEntry.Created = DateTime.UtcNow;
                    auditEntry.Modified = DateTime.UtcNow;
                    
                    auditEntries.Add(auditEntry);
                }
            }


            // Save audit entities that have all the modifications
            foreach (var auditEntry in auditEntries.Where(_ => !_.HasTemporaryProperties))
            {
                auditEntry.ProcessDictionaries();
                AuditLogSet.Add(auditEntry);
            }

            // keep a list of entries where the value of some properties are unknown at this step
            return auditEntries.Where(_ => _.HasTemporaryProperties).ToList();
        }

        private Guid? GetCurrentUserId()
        {
            var userId = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.PrimarySid);
            if (userId != null)
            {
                return Guid.Parse(userId.Value);
            }
            else
            {
                return null;
            }
        }

        private Task CompleteAuditLog(List<AuditLog> auditEntries)
        {
            if (auditEntries == null || auditEntries.Count == 0)
                return Task.CompletedTask;

            foreach (var auditEntry in auditEntries)
            {
                // Get the final value of the temporary properties
                foreach (var prop in auditEntry.TemporaryProperties)
                {
                    if (prop.Metadata.IsPrimaryKey())
                    {
                        auditEntry.KeyValuesDict[prop.Metadata.Name] = prop.CurrentValue;
                    }
                    else
                    {
                        auditEntry.NewValuesDict[prop.Metadata.Name] = prop.CurrentValue;
                    }
                }

                // Save the Audit entry
                auditEntry.ProcessDictionaries();
                AuditLogSet.Add(auditEntry);
            }

            return SaveChangesAsync();
        }
    }
}
