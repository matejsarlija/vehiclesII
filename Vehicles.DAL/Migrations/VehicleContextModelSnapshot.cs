﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Vehicles.DAL.Data;

#nullable disable

namespace Vehicles.DAL.Migrations
{
    [DbContext(typeof(VehicleContext))]
    partial class VehicleContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.3");

            modelBuilder.Entity("Vehicles.DAL.Models.VehicleMake", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Abrv")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("VehicleMake");
                });

            modelBuilder.Entity("Vehicles.DAL.Models.VehicleModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Abrv")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("VehicleMakeId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("VehicleMakeId");

                    b.ToTable("VehicleModel");
                });

            modelBuilder.Entity("Vehicles.DAL.Models.VehicleModel", b =>
                {
                    b.HasOne("Vehicles.DAL.Models.VehicleMake", "VehicleMake")
                        .WithMany("VehicleModels")
                        .HasForeignKey("VehicleMakeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("VehicleMake");
                });

            modelBuilder.Entity("Vehicles.DAL.Models.VehicleMake", b =>
                {
                    b.Navigation("VehicleModels");
                });
#pragma warning restore 612, 618
        }
    }
}
