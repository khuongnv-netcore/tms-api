﻿// <auto-generated />
using System;
using CORE_API.CORE.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CORE_API.Migrations
{
    [DbContext(typeof(CoreContext))]
    [Migration("20240115010536_addColumnstoBookingContainerDetail")]
    partial class addColumnstoBookingContainerDetail
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseCollation("SQL_Latin1_General_CP1_CS_AS")
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Proxies:ChangeTracking", false)
                .HasAnnotation("Proxies:CheckEquality", false)
                .HasAnnotation("Proxies:LazyLoading", true)
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("CORE_API.CORE.Models.Entities.AuditLog", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Action")
                        .HasColumnType("int");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("KeyValues")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Modified")
                        .HasColumnType("datetime2");

                    b.Property<string>("NewValues")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("OldValues")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TableName")
                        .HasColumnType("nvarchar(450)");

                    b.Property<Guid>("TransactionId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("Created");

                    b.HasIndex("Modified");

                    b.HasIndex("TableName");

                    b.HasIndex("UserId");

                    b.ToTable("AuditLog", (string)null);
                });

            modelBuilder.Entity("CORE_API.CORE.Models.Entities.Authentication", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("ExpiredDate")
                        .HasColumnType("datetime2");

                    b.Property<bool?>("IsExpired")
                        .HasColumnType("bit");

                    b.Property<DateTime>("Modified")
                        .HasColumnType("datetime2");

                    b.Property<string>("Token")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("Created");

                    b.HasIndex("Modified");

                    b.HasIndex("UserId");

                    b.ToTable("Authentication", (string)null);
                });

            modelBuilder.Entity("CORE_API.CORE.Models.Entities.DistributedLock", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("Expiration")
                        .HasColumnType("datetime2");

                    b.Property<string>("LockName")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("Modified")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("LockName")
                        .IsUnique()
                        .HasFilter("[LockName] IS NOT NULL");

                    b.ToTable("DistributedLock", (string)null);
                });

            modelBuilder.Entity("CORE_API.CORE.Models.Entities.Organization", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("Modified")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Organization", (string)null);
                });

            modelBuilder.Entity("CORE_API.CORE.Models.Entities.Role", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("DisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Modified")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("Created");

                    b.HasIndex("Modified");

                    b.ToTable("Role", (string)null);
                });

            modelBuilder.Entity("CORE_API.CORE.Models.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("AuthId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("EmailAddress")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("EmailVerified")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Modified")
                        .HasColumnType("datetime2");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StripeCustomerId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime?>("SubscriptionEndDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("SubscriptionProduct")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("AuthId")
                        .IsUnique()
                        .HasFilter("[AuthId] IS NOT NULL");

                    b.HasIndex("Created");

                    b.HasIndex("EmailAddress")
                        .IsUnique()
                        .HasFilter("[EmailAddress] IS NOT NULL");

                    b.HasIndex("Modified");

                    b.HasIndex("StripeCustomerId");

                    b.ToTable("User", (string)null);
                });

            modelBuilder.Entity("CORE_API.CORE.Models.Entities.UserOrganization", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("Modified")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("OrganizationId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("OrganizationUserRole")
                        .HasColumnType("int");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("Created");

                    b.HasIndex("Modified");

                    b.HasIndex("OrganizationId");

                    b.HasIndex("UserId");

                    b.ToTable("UserOrganization", (string)null);
                });

            modelBuilder.Entity("CORE_API.CORE.Models.Entities.UserRole", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("Modified")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("Created");

                    b.HasIndex("Modified");

                    b.HasIndex("RoleId");

                    b.HasIndex("UserId");

                    b.ToTable("UserRole", (string)null);
                });

            modelBuilder.Entity("CORE_API.Tms.Models.Entities.Booking", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ApprovedBy")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("ApprovedUserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("BKGContactEmail")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("BKGContactName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("BKGContactTel")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("BLNo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("BRD")
                        .HasColumnType("bit");

                    b.Property<string>("BookingNo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("BookingType")
                        .HasColumnType("int");

                    b.Property<string>("CMTD1")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CMTD2")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("ClosingTime")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("ConsigneeId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("CreatedBy")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("CreatedUserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("DEL1")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DEL2")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("DeliveryAddress")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("ETBDT")
                        .HasColumnType("datetime2");

                    b.Property<string>("ExtRemark")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Fh")
                        .HasColumnType("bit");

                    b.Property<Guid>("ForwarderId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("FullReturnCy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("IntRemark")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LOFC1")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LOFC2")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Modified")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("ModifiedBy")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("ModifiedUserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("POD1")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("POD2")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("POL1")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("POL2")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("POR1")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("POR2")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PickUpCy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("PickUpDT")
                        .HasColumnType("datetime2");

                    b.Property<string>("PickupAddress")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RDTerm1")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RDTerm2")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("RequestOrderId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("RequestOrderNo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("SI")
                        .HasColumnType("bit");

                    b.Property<DateTime>("SaillingDueDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("ScheduleStatus")
                        .HasColumnType("int");

                    b.Property<Guid>("ShipperId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<string>("TVVD")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UnitType")
                        .HasColumnType("int");

                    b.Property<Guid>("VirtualBookingId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("VirtualBookingNo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Weight")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("ApprovedUserId");

                    b.HasIndex("Created");

                    b.HasIndex("CreatedUserId");

                    b.HasIndex("Modified");

                    b.HasIndex("ModifiedUserId");

                    b.ToTable("Booking", (string)null);
                });

            modelBuilder.Entity("CORE_API.Tms.Models.Entities.BookingContainer", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("BookingId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ContainerCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("ContainerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("CreatedBy")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("CreatedUserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("datetime2");

                    b.Property<double>("EQSub")
                        .HasColumnType("float");

                    b.Property<DateTime>("Modified")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("ModifiedBy")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("ModifiedUserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<double>("SOC")
                        .HasColumnType("float");

                    b.Property<int>("Vol")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("BookingId");

                    b.HasIndex("Created");

                    b.HasIndex("CreatedUserId");

                    b.HasIndex("Modified");

                    b.HasIndex("ModifiedUserId");

                    b.ToTable("BookingContainer", (string)null);
                });

            modelBuilder.Entity("CORE_API.Tms.Models.Entities.BookingContainerDetail", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("BookingContainerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("BookingId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("BookingNo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("ContainerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ContainerNo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("CreatedBy")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("CreatedUserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("datetime2");

                    b.Property<double>("Measure")
                        .HasColumnType("float");

                    b.Property<DateTime>("Modified")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("ModifiedBy")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("ModifiedUserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Package")
                        .HasColumnType("int");

                    b.Property<bool>("RF")
                        .HasColumnType("bit");

                    b.Property<string>("ST")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("ScheduleId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("ScheduleId1")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Scheduled")
                        .HasColumnType("bit");

                    b.Property<string>("SealNo1")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SealNo2")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("VGM")
                        .HasColumnType("float");

                    b.Property<double>("Weight")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("BookingContainerId");

                    b.HasIndex("Created");

                    b.HasIndex("CreatedUserId");

                    b.HasIndex("Modified");

                    b.HasIndex("ModifiedUserId");

                    b.HasIndex("ScheduleId1");

                    b.ToTable("BookingContainerDetail", (string)null);
                });

            modelBuilder.Entity("CORE_API.Tms.Models.Entities.Schedule", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("ActTa")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("ActTd")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("AssignedDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("BookingContainerDetailId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("BookingContainerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("BookingId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("BookingNo")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<DateTime?>("CancelDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("CompletedDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("ContainerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ContainerNo")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("ContainerTruckCode")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<Guid>("ContainerTruckId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("CreatedBy")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("CreatedUserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("DeliveryAddress")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<DateTime>("DeliveryPlan")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("DriverId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("DriverName")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("EndLocation")
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<int>("HourNumberAlarm")
                        .HasColumnType("int");

                    b.Property<DateTime?>("InprocessDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("Modified")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("ModifiedBy")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("ModifiedUserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("PickupAddress")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<DateTime>("PickupPlan")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("RefuseDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("ScheduleStatus")
                        .HasColumnType("int");

                    b.Property<string>("StartLocation")
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<decimal>("TransportCost")
                        .HasPrecision(16, 2)
                        .HasColumnType("decimal(16,2)");

                    b.Property<decimal>("Vgm")
                        .HasPrecision(16, 2)
                        .HasColumnType("decimal(16,2)");

                    b.HasKey("Id");

                    b.HasIndex("Created");

                    b.HasIndex("CreatedUserId");

                    b.HasIndex("Modified");

                    b.HasIndex("ModifiedUserId");

                    b.ToTable("Schedule", (string)null);
                });

            modelBuilder.Entity("CORE_API.CORE.Models.Entities.AuditLog", b =>
                {
                    b.HasOne("CORE_API.CORE.Models.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId");

                    b.Navigation("User");
                });

            modelBuilder.Entity("CORE_API.CORE.Models.Entities.Authentication", b =>
                {
                    b.HasOne("CORE_API.CORE.Models.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId");

                    b.Navigation("User");
                });

            modelBuilder.Entity("CORE_API.CORE.Models.Entities.UserOrganization", b =>
                {
                    b.HasOne("CORE_API.CORE.Models.Entities.Organization", "Organization")
                        .WithMany("UserOrganizations")
                        .HasForeignKey("OrganizationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CORE_API.CORE.Models.Entities.User", "User")
                        .WithMany("UserOrganizations")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Organization");

                    b.Navigation("User");
                });

            modelBuilder.Entity("CORE_API.CORE.Models.Entities.UserRole", b =>
                {
                    b.HasOne("CORE_API.CORE.Models.Entities.Role", "Role")
                        .WithMany("UserRoles")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CORE_API.CORE.Models.Entities.User", "User")
                        .WithMany("UserRoles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");

                    b.Navigation("User");
                });

            modelBuilder.Entity("CORE_API.Tms.Models.Entities.Booking", b =>
                {
                    b.HasOne("CORE_API.CORE.Models.Entities.User", "ApprovedUser")
                        .WithMany()
                        .HasForeignKey("ApprovedUserId");

                    b.HasOne("CORE_API.CORE.Models.Entities.User", "CreatedUser")
                        .WithMany()
                        .HasForeignKey("CreatedUserId");

                    b.HasOne("CORE_API.CORE.Models.Entities.User", "ModifiedUser")
                        .WithMany()
                        .HasForeignKey("ModifiedUserId");

                    b.Navigation("ApprovedUser");

                    b.Navigation("CreatedUser");

                    b.Navigation("ModifiedUser");
                });

            modelBuilder.Entity("CORE_API.Tms.Models.Entities.BookingContainer", b =>
                {
                    b.HasOne("CORE_API.Tms.Models.Entities.Booking", null)
                        .WithMany("BookingContainers")
                        .HasForeignKey("BookingId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CORE_API.CORE.Models.Entities.User", "CreatedUser")
                        .WithMany()
                        .HasForeignKey("CreatedUserId");

                    b.HasOne("CORE_API.CORE.Models.Entities.User", "ModifiedUser")
                        .WithMany()
                        .HasForeignKey("ModifiedUserId");

                    b.Navigation("CreatedUser");

                    b.Navigation("ModifiedUser");
                });

            modelBuilder.Entity("CORE_API.Tms.Models.Entities.BookingContainerDetail", b =>
                {
                    b.HasOne("CORE_API.Tms.Models.Entities.BookingContainer", null)
                        .WithMany("BookingContainerDetails")
                        .HasForeignKey("BookingContainerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CORE_API.CORE.Models.Entities.User", "CreatedUser")
                        .WithMany()
                        .HasForeignKey("CreatedUserId");

                    b.HasOne("CORE_API.CORE.Models.Entities.User", "ModifiedUser")
                        .WithMany()
                        .HasForeignKey("ModifiedUserId");

                    b.HasOne("CORE_API.Tms.Models.Entities.Schedule", "Schedule")
                        .WithMany()
                        .HasForeignKey("ScheduleId1");

                    b.Navigation("CreatedUser");

                    b.Navigation("ModifiedUser");

                    b.Navigation("Schedule");
                });

            modelBuilder.Entity("CORE_API.Tms.Models.Entities.Schedule", b =>
                {
                    b.HasOne("CORE_API.CORE.Models.Entities.User", "CreatedUser")
                        .WithMany()
                        .HasForeignKey("CreatedUserId");

                    b.HasOne("CORE_API.CORE.Models.Entities.User", "ModifiedUser")
                        .WithMany()
                        .HasForeignKey("ModifiedUserId");

                    b.Navigation("CreatedUser");

                    b.Navigation("ModifiedUser");
                });

            modelBuilder.Entity("CORE_API.CORE.Models.Entities.Organization", b =>
                {
                    b.Navigation("UserOrganizations");
                });

            modelBuilder.Entity("CORE_API.CORE.Models.Entities.Role", b =>
                {
                    b.Navigation("UserRoles");
                });

            modelBuilder.Entity("CORE_API.CORE.Models.Entities.User", b =>
                {
                    b.Navigation("UserOrganizations");

                    b.Navigation("UserRoles");
                });

            modelBuilder.Entity("CORE_API.Tms.Models.Entities.Booking", b =>
                {
                    b.Navigation("BookingContainers");
                });

            modelBuilder.Entity("CORE_API.Tms.Models.Entities.BookingContainer", b =>
                {
                    b.Navigation("BookingContainerDetails");
                });
#pragma warning restore 612, 618
        }
    }
}
