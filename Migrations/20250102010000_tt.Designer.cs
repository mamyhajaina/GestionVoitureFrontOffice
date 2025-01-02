﻿// <auto-generated />
using System;
using GestionVoitureFrontOffice.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace GestionVoitureFrontOffice.Migrations
{
    [DbContext(typeof(ApplicationDBContext))]
    [Migration("20250102010000_tt")]
    partial class tt
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("GestionVoitureFrontOffice.Models.Mission", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime");

                    b.Property<int>("NumberTrips")
                        .HasColumnType("int");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime");

                    b.Property<int>("idVehicle")
                        .HasColumnType("int");

                    b.Property<int>("offerId")
                        .HasColumnType("int");

                    b.Property<bool>("status")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("idVehicle");

                    b.HasIndex("offerId");

                    b.ToTable("Mission");
                });

            modelBuilder.Entity("GestionVoitureFrontOffice.Models.NosTrager", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Destination")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("NosTrager");
                });

            modelBuilder.Entity("GestionVoitureFrontOffice.Models.Offer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<decimal?>("Capacity")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int?>("ClientId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateMission")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(254)
                        .HasColumnType("nvarchar(254)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("IdClient")
                        .HasColumnType("int");

                    b.Property<int?>("IdTragerArriving")
                        .HasColumnType("int");

                    b.Property<int?>("IdTragerDeparture")
                        .HasColumnType("int");

                    b.Property<int?>("IdVehicle")
                        .HasColumnType("int");

                    b.Property<string>("NameSociete")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("OtherTragerDescription")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal?>("TotalAmount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int?>("TragerArrivingId")
                        .HasColumnType("int");

                    b.Property<int?>("TragerDepartureId")
                        .HasColumnType("int");

                    b.Property<int?>("VehicleId")
                        .HasColumnType("int");

                    b.Property<int?>("status")
                        .HasColumnType("int");

                    b.Property<string>("statusName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ClientId");

                    b.HasIndex("TragerArrivingId");

                    b.HasIndex("TragerDepartureId");

                    b.HasIndex("VehicleId");

                    b.ToTable("Offer");
                });

            modelBuilder.Entity("GestionVoitureFrontOffice.Models.RoleUser", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("RoleUser");
                });

            modelBuilder.Entity("GestionVoitureFrontOffice.Models.TragerVehicule", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("TragerArriverId")
                        .HasColumnType("int");

                    b.Property<int>("TragerDepartId")
                        .HasColumnType("int");

                    b.Property<int>("VehicleId")
                        .HasColumnType("int");

                    b.Property<int>("idTragerArriver")
                        .HasColumnType("int");

                    b.Property<int>("idTragerDepart")
                        .HasColumnType("int");

                    b.Property<int>("idVehicle")
                        .HasColumnType("int");

                    b.Property<decimal>("prixLocation")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.HasIndex("TragerArriverId");

                    b.HasIndex("TragerDepartId");

                    b.HasIndex("VehicleId");

                    b.ToTable("TragerVehicule");
                });

            modelBuilder.Entity("GestionVoitureFrontOffice.Models.TypeVehicle", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("TypeVehicle");
                });

            modelBuilder.Entity("GestionVoitureFrontOffice.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool?>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.Property<int?>("RoleUserId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("RoleUserId");

                    b.ToTable("User");
                });

            modelBuilder.Entity("GestionVoitureFrontOffice.Models.Vehicle", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Brand")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Capacity")
                        .HasColumnType("int");

                    b.Property<string>("Descriptions")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Model")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Number")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhotoUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Pseudo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TableKilometer")
                        .HasColumnType("int");

                    b.Property<int>("TypeVehicleId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("TypeVehicleId");

                    b.ToTable("Vehicle");
                });

            modelBuilder.Entity("GestionVoitureFrontOffice.Models.Mission", b =>
                {
                    b.HasOne("GestionVoitureFrontOffice.Models.Vehicle", "Vehicle")
                        .WithMany()
                        .HasForeignKey("idVehicle")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("GestionVoitureFrontOffice.Models.Offer", "Offer")
                        .WithMany()
                        .HasForeignKey("offerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Offer");

                    b.Navigation("Vehicle");
                });

            modelBuilder.Entity("GestionVoitureFrontOffice.Models.Offer", b =>
                {
                    b.HasOne("GestionVoitureFrontOffice.Models.User", "Client")
                        .WithMany()
                        .HasForeignKey("ClientId");

                    b.HasOne("GestionVoitureFrontOffice.Models.NosTrager", "TragerArriving")
                        .WithMany()
                        .HasForeignKey("TragerArrivingId");

                    b.HasOne("GestionVoitureFrontOffice.Models.NosTrager", "TragerDeparture")
                        .WithMany()
                        .HasForeignKey("TragerDepartureId");

                    b.HasOne("GestionVoitureFrontOffice.Models.Vehicle", "Vehicle")
                        .WithMany()
                        .HasForeignKey("VehicleId");

                    b.Navigation("Client");

                    b.Navigation("TragerArriving");

                    b.Navigation("TragerDeparture");

                    b.Navigation("Vehicle");
                });

            modelBuilder.Entity("GestionVoitureFrontOffice.Models.TragerVehicule", b =>
                {
                    b.HasOne("GestionVoitureFrontOffice.Models.NosTrager", "TragerArriver")
                        .WithMany()
                        .HasForeignKey("TragerArriverId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("GestionVoitureFrontOffice.Models.NosTrager", "TragerDepart")
                        .WithMany()
                        .HasForeignKey("TragerDepartId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("GestionVoitureFrontOffice.Models.Vehicle", "Vehicle")
                        .WithMany()
                        .HasForeignKey("VehicleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("TragerArriver");

                    b.Navigation("TragerDepart");

                    b.Navigation("Vehicle");
                });

            modelBuilder.Entity("GestionVoitureFrontOffice.Models.User", b =>
                {
                    b.HasOne("GestionVoitureFrontOffice.Models.RoleUser", "RoleUser")
                        .WithMany()
                        .HasForeignKey("RoleUserId");

                    b.Navigation("RoleUser");
                });

            modelBuilder.Entity("GestionVoitureFrontOffice.Models.Vehicle", b =>
                {
                    b.HasOne("GestionVoitureFrontOffice.Models.TypeVehicle", "TypeVehicle")
                        .WithMany()
                        .HasForeignKey("TypeVehicleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("TypeVehicle");
                });
#pragma warning restore 612, 618
        }
    }
}
