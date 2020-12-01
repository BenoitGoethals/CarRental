﻿// <auto-generated />
using System;
using GrpcServiceCar.data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace GrpcServiceCar.Migrations
{
    [DbContext(typeof(CarDbContext))]
    partial class CarDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.0");

            modelBuilder.Entity("Shared.model.Car", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<DateTime>("InCirculationDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("InCirculationDate");

                    b.Property<int>("Km")
                        .HasColumnType("int")
                        .HasColumnName("Email");

                    b.Property<DateTime>("LastMaintenace")
                        .HasColumnType("datetime2")
                        .HasColumnName("LastMaintenace");

                    b.Property<DateTime>("NextMaintenace")
                        .HasColumnType("datetime2")
                        .HasColumnName("NextMaintenace");

                    b.Property<bool>("Operational")
                        .HasColumnType("bit")
                        .HasColumnName("Operational");

                    b.Property<string>("PlateNbr")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("PlateNbr");

                    b.Property<string>("Remarks")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Remarks");

                    b.Property<int>("Type")
                        .HasColumnType("int")
                        .HasColumnName("Type");

                    b.HasKey("Id");

                    b.ToTable("Cars");
                });
#pragma warning restore 612, 618
        }
    }
}
