﻿// <auto-generated />
using EcoTrails.Api.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace EcoTrails.Api.Persistence.Data.Migrations
{
    [DbContext(typeof(EcoTrailsDbContext))]
    [Migration("20250329204922_AddWaypoints")]
    partial class AddWaypoints
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "9.0.3");

            modelBuilder.Entity("EcoTrails.Api.Persistence.Entities.RouteInstruction", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("Stage")
                        .HasColumnType("INTEGER");

                    b.Property<int>("TrailId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("TrailId");

                    b.ToTable("RouteInstructions");
                });

            modelBuilder.Entity("EcoTrails.Api.Persistence.Entities.Trail", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Image")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("Length")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("TimeInMinutes")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("Trails");
                });

            modelBuilder.Entity("EcoTrails.Api.Persistence.Entities.Waypoint", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<decimal>("Latitude")
                        .HasColumnType("TEXT");

                    b.Property<decimal>("Longitude")
                        .HasColumnType("TEXT");

                    b.Property<int>("TrailId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("TrailId");

                    b.ToTable("Waypoints");
                });

            modelBuilder.Entity("EcoTrails.Api.Persistence.Entities.RouteInstruction", b =>
                {
                    b.HasOne("EcoTrails.Api.Persistence.Entities.Trail", "Trail")
                        .WithMany("Route")
                        .HasForeignKey("TrailId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Trail");
                });

            modelBuilder.Entity("EcoTrails.Api.Persistence.Entities.Waypoint", b =>
                {
                    b.HasOne("EcoTrails.Api.Persistence.Entities.Trail", "Trail")
                        .WithMany("Waypoints")
                        .HasForeignKey("TrailId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Trail");
                });

            modelBuilder.Entity("EcoTrails.Api.Persistence.Entities.Trail", b =>
                {
                    b.Navigation("Route");

                    b.Navigation("Waypoints");
                });
#pragma warning restore 612, 618
        }
    }
}
