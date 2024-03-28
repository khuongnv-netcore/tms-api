using System;
using CORE_API.CORE.Models.Entities.Abstract;
using CORE_API.CORE.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Globalization;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace CORE_API.Tms.Models.Entities
{
    public class BookingContainer : CoreEntity
    {      
        public Guid BookingId { get; set; }
        public Guid ContainerId { get; set; }
        public string ContainerCode { get; set; }
        public int Vol { get; set; } = 0;
        public double EQSub { get; set; } = 0;
        public double SOC { get; set; } = 0;
        public Guid CreatedBy { get; set; }
        public virtual User CreatedUser { get; set; }
        public Guid ModifiedBy { get; set; }
        public virtual User ModifiedUser { get; set; }

        public virtual IList<BookingContainerDetail> BookingContainerDetails { get; set; }

        internal static void OnModelCreating(ModelBuilder builder)
        {
            //Config
            string tableName = "BookingContainer";

            //Generic
            builder.Entity<BookingContainer>().ToTable(tableName);
            builder.Entity<BookingContainer>().HasKey(m => m.Id);
            builder.Entity<BookingContainer>().Property(m => m.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<BookingContainer>().HasIndex(m => m.Created);
            builder.Entity<BookingContainer>().HasIndex(m => m.Modified);

            // QueryFilter for SoftDelete
            builder.Entity<BookingContainer>().HasQueryFilter(m => m.DeletedAt == null);

            //Relationship
        }

        public override async Task OnSoftDeleteAsync(SoftDeletes.Core.DbContext context,
            CancellationToken cancellationToken = default)
        {
            var taskList = new List<Task>
            {
                context.RemoveRangeAsync(BookingContainerDetails, cancellationToken)
            };

            await Task.WhenAll(taskList);
        }

        public override void OnSoftDelete(SoftDeletes.Core.DbContext context)
        {
            context.RemoveRange(BookingContainerDetails);
        }

        public override async Task LoadRelationsAsync(SoftDeletes.Core.DbContext context,
            CancellationToken cancellationToken = default)
        {
            var taskList = new List<Task>
            {
                context.Entry(this)
                    .Collection(m => m.BookingContainerDetails)
                    .LoadAsync(cancellationToken)
            };

            await Task.WhenAll(taskList);
        }
        public override void LoadRelations(SoftDeletes.Core.DbContext context)
        {
            context.Entry(this)
                .Collection(m => m.BookingContainerDetails)
                .Load();
        }
    }
}
