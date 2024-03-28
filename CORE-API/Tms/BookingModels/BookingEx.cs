using System;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace CORE_API.Tms.Models.Entities
{
    public class BookingEx
    {
        public string Id { get; set; }
        public string BookingNo { get; set; }
        public DateTime CreatedAt { get; set; }
        public virtual IList<BookingContainerEx> BookingContainers { get; set; }

        internal static void OnModelCreating(ModelBuilder builder)
        {
            //Config
            string tableName = "bl_Bookings";

            //Generic
            builder.Entity<BookingEx>().ToTable(tableName);
            builder.Entity<BookingEx>().HasKey(m => m.Id);
            builder.Entity<BookingEx>().Property(m => m.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<BookingEx>().HasIndex(m => m.CreatedAt);
            //Relationship
            builder.Entity<BookingEx>().HasMany(m => m.BookingContainers).WithOne(m => m.booking).HasForeignKey(m => m.BookingId);
        }
    }
}
