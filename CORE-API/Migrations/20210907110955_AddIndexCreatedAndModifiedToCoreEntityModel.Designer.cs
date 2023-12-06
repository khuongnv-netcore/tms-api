﻿// <auto-generated />
using System;
using CORE_API.CORE.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CORE_API.Migrations
{
    [DbContext(typeof(CoreContext))]
    [Migration("20210907110955_AddIndexCreatedAndModifiedToCoreEntityModel")]
    partial class AddIndexCreatedAndModifiedToCoreEntityModel
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 64)
                .HasAnnotation("ProductVersion", "5.0.3");

            modelBuilder.Entity("CORE_API.CORE.Models.Entities.AuditLog", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<int>("Action")
                        .HasColumnType("int");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("KeyValues")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<DateTime>("Modified")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("NewValues")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("OldValues")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("TableName")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<Guid>("TransactionId")
                        .HasColumnType("char(36)");

                    b.Property<Guid?>("UserId")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.HasIndex("Created");

                    b.HasIndex("Modified");

                    b.HasIndex("TableName");

                    b.HasIndex("UserId");

                    b.ToTable("AuditLog");
                });

            modelBuilder.Entity("CORE_API.CORE.Models.Entities.Authentication", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime?>("ExpiredDate")
                        .HasColumnType("datetime(6)");

                    b.Property<bool?>("IsExpired")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTime>("Modified")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Token")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<Guid?>("UserId")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.HasIndex("Created");

                    b.HasIndex("Modified");

                    b.HasIndex("UserId");

                    b.ToTable("Authentication");
                });

            modelBuilder.Entity("CORE_API.CORE.Models.Entities.Role", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("DisplayName")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<DateTime>("Modified")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("RoleType")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("Created");

                    b.HasIndex("Modified");

                    b.ToTable("Role");
                });

            modelBuilder.Entity("CORE_API.CORE.Models.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("AuthId")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("EmailAddress")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<string>("EmailVerified")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("FirstName")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("LastName")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<DateTime>("Modified")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.HasIndex("AuthId")
                        .IsUnique();

                    b.HasIndex("Created");

                    b.HasIndex("EmailAddress")
                        .IsUnique();

                    b.HasIndex("Modified");

                    b.ToTable("User");
                });

            modelBuilder.Entity("CORE_API.CORE.Models.Entities.UserRole", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("Modified")
                        .HasColumnType("datetime(6)");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("char(36)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.HasIndex("Created");

                    b.HasIndex("Modified");

                    b.HasIndex("RoleId");

                    b.HasIndex("UserId");

                    b.ToTable("UserRole");
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

            modelBuilder.Entity("CORE_API.CORE.Models.Entities.Role", b =>
                {
                    b.Navigation("UserRoles");
                });

            modelBuilder.Entity("CORE_API.CORE.Models.Entities.User", b =>
                {
                    b.Navigation("UserRoles");
                });
#pragma warning restore 612, 618
        }
    }
}
