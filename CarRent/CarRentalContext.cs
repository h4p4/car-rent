using System;
using System.Collections.Generic;
using CarRent.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace CarRent
{
    public partial class CarRentalContext : DbContext
    {
        public CarRentalContext()
        {
        }

        public CarRentalContext(DbContextOptions<CarRentalContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Car> Cars { get; set; } = null!;
        public virtual DbSet<CarBrand> CarBrands { get; set; } = null!;
        public virtual DbSet<CarStatus> CarStatuses { get; set; } = null!;
        public virtual DbSet<Rent> Rents { get; set; } = null!;
        public virtual DbSet<Renter> Renters { get; set; } = null!;
        public virtual DbSet<SteeringWheelSide> SteeringWheelSides { get; set; } = null!;
        public virtual DbSet<TransmissionType> TransmissionTypes { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=CarRental");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Car>(entity =>
            {
                entity.HasOne(d => d.CarBrand)
                    .WithMany(p => p.Cars)
                    .HasForeignKey(d => d.CarBrandId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Cars_CarBrands");

                entity.HasOne(d => d.CarStatus)
                    .WithMany(p => p.Cars)
                    .HasForeignKey(d => d.CarStatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Cars_CarStatuses");

                entity.HasOne(d => d.SteeringWheelSide)
                    .WithMany(p => p.Cars)
                    .HasForeignKey(d => d.SteeringWheelSideId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Cars_SteeringWheelSides");

                entity.HasOne(d => d.TransmissionType)
                    .WithMany(p => p.Cars)
                    .HasForeignKey(d => d.TransmissionTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Cars_TransmissionTypes");
            });

            modelBuilder.Entity<CarBrand>(entity =>
            {
                entity.Property(e => e.Title).HasMaxLength(100);
            });

            modelBuilder.Entity<CarStatus>(entity =>
            {
                entity.Property(e => e.Title).HasMaxLength(50);
            });

            modelBuilder.Entity<Rent>(entity =>
            {
                entity.HasOne(d => d.Car)
                    .WithMany(p => p.Rents)
                    .HasForeignKey(d => d.CarId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Rents_Cars");

                entity.HasOne(d => d.Renter)
                    .WithMany(p => p.Rents)
                    .HasForeignKey(d => d.RenterId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Rents_Renters");
            });

            modelBuilder.Entity<Renter>(entity =>
            {
                entity.Property(e => e.FirstName).HasMaxLength(50);

                entity.Property(e => e.PhoneNumber).HasMaxLength(12);
            });

            modelBuilder.Entity<SteeringWheelSide>(entity =>
            {
                entity.Property(e => e.Title).HasMaxLength(50);
            });

            modelBuilder.Entity<TransmissionType>(entity =>
            {
                entity.Property(e => e.Title).HasMaxLength(50);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
