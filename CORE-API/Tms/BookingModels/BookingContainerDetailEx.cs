using System;
using CORE_API.CORE.Models.Entities.Abstract;
using CORE_API.CORE.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Globalization;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;

namespace CORE_API.Tms.Models.Entities
{
    public class BookingContainerDetailEx
    {
        public string Id { get; set; }
        public string BookingId { get; set; }
        public string BookingNo { get; set; }
        public string ContainerId { get; set; }
        public string ContainerNo { get; set; }
        public string BookingContainerId { get; set; }
        public string SealNo1 { get; set; }
        public string SealNo2 { get; set; }
        public double Package { get; set; } = 0;
        public double Weight { get; set; } = 0;
        public double VGM { get; set; } = 0;
        public double Measure { get; set; } = 0;
        public string ST { get; set; }
        public bool RF { get; set; } = false;
        public bool Scheduled { get; set; } = false;
        [NotMapped]
        public Schedule Schedule { get; set; }
        public virtual BookingContainerEx BookingContainer { get; set; }

        internal static void OnModelCreating(ModelBuilder builder)
        {
            //Config
            string tableName = "bl_BookingContainerDetails";

            //Generic
            builder.Entity<BookingContainerDetailEx>().ToTable(tableName);
            builder.Entity<BookingContainerDetailEx>().HasKey(m => m.Id);
            builder.Entity<BookingContainerDetailEx>().Property(m => m.Id).IsRequired().ValueGeneratedOnAdd();

        }
    }
}
