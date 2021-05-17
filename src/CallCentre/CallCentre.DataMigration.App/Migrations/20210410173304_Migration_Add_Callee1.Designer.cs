﻿// <auto-generated />
using System;
using CallCentre.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CallCentre.DataMigration.App.Migrations
{
    [DbContext(typeof(CallCentreDBContext))]
    [Migration("20210410173304_Migration_Add_Callee1")]
    partial class Migration_Add_Callee1
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.5")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("CallCentre.Model.ApplicationUser", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("EmailAddress")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("GivenName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Surname")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id")
                        .IsClustered();

                    b.ToTable("ApplicationUsers");
                });

            modelBuilder.Entity("CallCentre.Model.Callee", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Lastname")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id")
                        .IsClustered();

                    b.ToTable("Callees");
                });

            modelBuilder.Entity("CallCentre.Model.CalleeCallDuration", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CallDateTime")
                        .HasColumnType("datetime2");

                    b.Property<long>("CallDurationInSecond")
                        .HasColumnType("bigint");

                    b.Property<Guid>("Callee_Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("Charge")
                        .HasPrecision(18, 4)
                        .HasColumnType("decimal(18,4)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id")
                        .IsClustered();

                    b.HasIndex("Callee_Id");

                    b.ToTable("CalleeCallDurations");
                });

            modelBuilder.Entity("CallCentre.Model.CostingTimeSlot", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<float>("CostPerMinute")
                        .HasColumnType("real");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("EndTime")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("StartTime")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id")
                        .IsClustered();

                    b.ToTable("CostingTimeSlots");
                });

            modelBuilder.Entity("CallCentre.Model.UserLoginHistory", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("LoginTime")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("User_Id")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id")
                        .IsClustered();

                    b.HasIndex("User_Id");

                    b.ToTable("UserLoginHistories");
                });

            modelBuilder.Entity("CallCentre.Model.CalleeCallDuration", b =>
                {
                    b.HasOne("CallCentre.Model.Callee", "Callee")
                        .WithMany("CalleeCallDurations")
                        .HasForeignKey("Callee_Id")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Callee");
                });

            modelBuilder.Entity("CallCentre.Model.UserLoginHistory", b =>
                {
                    b.HasOne("CallCentre.Model.ApplicationUser", "User")
                        .WithMany("UserLoginHistories")
                        .HasForeignKey("User_Id")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("CallCentre.Model.ApplicationUser", b =>
                {
                    b.Navigation("UserLoginHistories");
                });

            modelBuilder.Entity("CallCentre.Model.Callee", b =>
                {
                    b.Navigation("CalleeCallDurations");
                });
#pragma warning restore 612, 618
        }
    }
}
