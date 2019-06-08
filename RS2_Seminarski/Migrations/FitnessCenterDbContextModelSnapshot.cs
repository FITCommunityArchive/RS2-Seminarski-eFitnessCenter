﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RS2_Seminarski.Database;

namespace RS2_Seminarski.Migrations
{
    [DbContext(typeof(FitnessCenterDbContext))]
    partial class FitnessCenterDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.8-servicing-32085")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("RS2_Seminarski.Database.AppUser", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address");

                    b.Property<DateTime>("CreatedAt");

                    b.Property<string>("Email");

                    b.Property<string>("FirstName");

                    b.Property<string>("HashedPassword");

                    b.Property<string>("LastName");

                    b.Property<string>("PhoneNumber");

                    b.Property<int>("Status");

                    b.HasKey("Id");

                    b.ToTable("AppUsers");
                });

            modelBuilder.Entity("RS2_Seminarski.Database.Client", b =>
                {
                    b.Property<int?>("Id");

                    b.HasKey("Id");

                    b.ToTable("Clients");
                });

            modelBuilder.Entity("RS2_Seminarski.Database.Employee", b =>
                {
                    b.Property<int?>("Id");

                    b.Property<decimal>("Salary");

                    b.HasKey("Id");

                    b.ToTable("Employees");
                });

            modelBuilder.Entity("RS2_Seminarski.Database.MembershipPayment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("ClientId");

                    b.Property<DateTime>("CreatedAt");

                    b.Property<int?>("EmployeeId");

                    b.Property<int?>("MembershipTypeId");

                    b.HasKey("Id");

                    b.HasIndex("ClientId");

                    b.HasIndex("EmployeeId");

                    b.HasIndex("MembershipTypeId");

                    b.ToTable("MembershipPayment");
                });

            modelBuilder.Entity("RS2_Seminarski.Database.MembershipType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("MonthsValid");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<double>("Price");

                    b.HasKey("Id");

                    b.ToTable("MembershipTypes");

                    b.HasData(
                        new { Id = 1, MonthsValid = 1, Name = "Jedan Mjesec", Price = 40.0 },
                        new { Id = 2, MonthsValid = 2, Name = "Dva Mjeseca", Price = 70.0 },
                        new { Id = 3, MonthsValid = 6, Name = "Sest Mjeseci", Price = 110.0 },
                        new { Id = 4, MonthsValid = 12, Name = "Godisnja Clanarina", Price = 180.0 }
                    );
                });

            modelBuilder.Entity("RS2_Seminarski.Database.Client", b =>
                {
                    b.HasOne("RS2_Seminarski.Database.AppUser", "AppUser")
                        .WithOne("Client")
                        .HasForeignKey("RS2_Seminarski.Database.Client", "Id")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("RS2_Seminarski.Database.Employee", b =>
                {
                    b.HasOne("RS2_Seminarski.Database.AppUser", "AppUser")
                        .WithOne("Employee")
                        .HasForeignKey("RS2_Seminarski.Database.Employee", "Id")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("RS2_Seminarski.Database.MembershipPayment", b =>
                {
                    b.HasOne("RS2_Seminarski.Database.Client", "Client")
                        .WithMany("MembershipPayments")
                        .HasForeignKey("ClientId");

                    b.HasOne("RS2_Seminarski.Database.Employee", "Employee")
                        .WithMany("MembershipPayments")
                        .HasForeignKey("EmployeeId");

                    b.HasOne("RS2_Seminarski.Database.MembershipType", "MembershipType")
                        .WithMany()
                        .HasForeignKey("MembershipTypeId");
                });
#pragma warning restore 612, 618
        }
    }
}
