
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System;

namespace CORE_API.Tms.Models.Entities
{
    public class BookingContainerEx
    {
        public string Id { get; set; }
        public string BookingId { get; set; }
        public string ContainerId { get; set; }
        public string ContainerCode { get; set; }
        public int Vol { get; set; } = 0;
        public double EQSub { get; set; } = 0;
        public double SOC { get; set; } = 0;
        public virtual BookingEx booking { get; set; }
        public virtual IList<BookingContainerDetailEx> BookingContainerDetails { get; set; }

        internal static void OnModelCreating(ModelBuilder builder)
        {
            //Config
            string tableName = "bl_BookingContainers";

            //Generic
            builder.Entity<BookingContainerEx>().ToTable(tableName);
            builder.Entity<BookingContainerEx>().HasKey(m => m.Id);
            builder.Entity<BookingContainerEx>().Property(m => m.Id).IsRequired().ValueGeneratedOnAdd();
        }
    }
}
