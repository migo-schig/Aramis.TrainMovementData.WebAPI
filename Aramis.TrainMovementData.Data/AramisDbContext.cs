﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Data.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Aramis.TrainMovementData.Data
{
    public partial class AramisDbContext : DbContext
    {
        private readonly string connectionString;

        public AramisDbContext(DbConnectionStringBuilder connectionStringBuilder)
        {
            connectionString = connectionStringBuilder.ConnectionString;
        }

        public virtual DbSet<BasicData> BasicData { get; set; }
        public virtual DbSet<Delay> Delay { get; set; }
        public virtual DbSet<GeoData> GeoData { get; set; }
        public virtual DbSet<Log> Log { get; set; }
        public virtual DbSet<Modification> Modification { get; set; }
        public virtual DbSet<Notification> Notification { get; set; }
        public virtual DbSet<Remark> Remark { get; set; }
        public virtual DbSet<SectionOfLine> SectionOfLine { get; set; }
        public virtual DbSet<Vehicle> Vehicle { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BasicData>(entity =>
            {
                entity.HasKey(e => new { e.Date, e.TrainNumber });

                entity.Property(e => e.Date).HasColumnType("date");

                entity.Property(e => e.TrainNumber).HasMaxLength(10);

                entity.Property(e => e.Line).HasMaxLength(20);

                entity.Property(e => e.Operator).HasMaxLength(50);

                entity.Property(e => e.Orderer).HasMaxLength(50);

                entity.Property(e => e.ReferenceStationShort).HasMaxLength(6);

                entity.Property(e => e.RefereneTime).HasColumnType("time(3)");

                entity.Property(e => e.TractionProvider).HasMaxLength(50);

                entity.Property(e => e.TrainCategory).HasMaxLength(20);

                entity.Property(e => e.TrainIdentifier).HasMaxLength(20);

                entity.Property(e => e.TrainQuality).HasMaxLength(5);

                entity.Property(e => e.TrainType).HasMaxLength(10);
            });

            modelBuilder.Entity<Delay>(entity =>
            {
                entity.HasKey(e => new { e.Id, e.Date, e.TrainNumber });

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.Date).HasColumnType("date");

                entity.Property(e => e.TrainNumber).HasMaxLength(10);

                entity.Property(e => e.ActualTime).HasColumnType("time(3)");

                entity.Property(e => e.DelayTrainNumber).HasMaxLength(10);

                entity.Property(e => e.DelayType)
                    .IsRequired()
                    .HasMaxLength(5);

                entity.Property(e => e.Description).HasMaxLength(255);

                entity.Property(e => e.DisruptionDate).HasColumnType("date");

                entity.Property(e => e.Number).HasMaxLength(10);

                entity.Property(e => e.Region).HasMaxLength(5);

                entity.Property(e => e.Station).HasMaxLength(255);

                entity.Property(e => e.StationShort).HasMaxLength(6);

                entity.HasOne(d => d.BasicData)
                    .WithMany(p => p.Delay)
                    .HasForeignKey(d => new { d.Date, d.TrainNumber })
                    .HasConstraintName("FK_Delay_BasicData");
            });

            modelBuilder.Entity<GeoData>(entity =>
            {
                entity.Property(e => e.Latitude).HasColumnType("decimal(12, 9)");

                entity.Property(e => e.Longitude).HasColumnType("decimal(12, 9)");

                entity.Property(e => e.Station)
                    .HasMaxLength(64)
                    .IsUnicode(false);

                entity.Property(e => e.StationShort)
                    .HasMaxLength(16)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Log>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .IsClustered(false);

                entity.HasIndex(e => new { e.Id, e.TrainNumber, e.Date })
                    .HasName("CI_Log")
                    .IsUnique()
                    .IsClustered();

                entity.Property(e => e.Date).HasColumnType("date");

                entity.Property(e => e.FileDate).HasColumnType("date");

                entity.Property(e => e.FileName).HasMaxLength(255);

                entity.Property(e => e.FileTrainNumber).HasMaxLength(50);

                entity.Property(e => e.TrainNumber).HasMaxLength(10);
            });

            modelBuilder.Entity<Modification>(entity =>
            {
                entity.HasKey(e => new { e.Id, e.Date, e.TrainNumber });

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.Date).HasColumnType("date");

                entity.Property(e => e.TrainNumber).HasMaxLength(10);

                entity.Property(e => e.Description).HasMaxLength(255);

                entity.Property(e => e.DisruptionDate).HasColumnType("date");

                entity.Property(e => e.ModificationType)
                    .IsRequired()
                    .HasMaxLength(5);

                entity.Property(e => e.Number).HasMaxLength(10);

                entity.Property(e => e.ReferenceTime).HasColumnType("time(3)");

                entity.Property(e => e.Region).HasMaxLength(5);

                entity.Property(e => e.StationFrom).HasMaxLength(255);

                entity.Property(e => e.StationShortFrom).HasMaxLength(6);

                entity.Property(e => e.StationShortTo).HasMaxLength(6);

                entity.Property(e => e.StationTo).HasMaxLength(255);

                entity.Property(e => e.TemplateTrainnumber).HasMaxLength(10);

                entity.Property(e => e.Track).HasMaxLength(6);

                entity.HasOne(d => d.BasicData)
                    .WithMany(p => p.Modification)
                    .HasForeignKey(d => new { d.Date, d.TrainNumber })
                    .HasConstraintName("FK_Modification_BasicData");
            });

            modelBuilder.Entity<Notification>(entity =>
            {
                entity.HasKey(e => new { e.Id, e.Date, e.TrainNumber });

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.Date).HasColumnType("date");

                entity.Property(e => e.TrainNumber).HasMaxLength(10);

                entity.Property(e => e.ActualRoute).HasMaxLength(10);

                entity.Property(e => e.ActualTime).HasColumnType("datetime2(3)");

                entity.Property(e => e.ActualTrack).HasMaxLength(6);

                entity.Property(e => e.NotificationType)
                    .IsRequired()
                    .HasMaxLength(5);

                entity.Property(e => e.ProjectedActualTime).HasColumnType("time(3)");

                entity.Property(e => e.ScheduledRoute).HasMaxLength(10);

                entity.Property(e => e.ScheduledTime).HasColumnType("datetime2(3)");

                entity.Property(e => e.ScheduledTrack).HasMaxLength(6);

                entity.Property(e => e.Signal).HasMaxLength(6);

                entity.Property(e => e.Source)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Station).HasMaxLength(255);

                entity.Property(e => e.StationShort).HasMaxLength(6);

                entity.Property(e => e.TrainState)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.HasOne(d => d.BasicData)
                    .WithMany(p => p.Notification)
                    .HasForeignKey(d => new { d.Date, d.TrainNumber })
                    .HasConstraintName("FK_Notification_BasicData");
            });

            modelBuilder.Entity<Remark>(entity =>
            {
                entity.HasKey(e => new { e.Id, e.Date, e.TrainNumber });

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.Date).HasColumnType("date");

                entity.Property(e => e.TrainNumber).HasMaxLength(10);

                entity.Property(e => e.InputTime).HasColumnType("time(3)");

                entity.Property(e => e.Notes).HasMaxLength(255);

                entity.Property(e => e.RemarkType)
                    .IsRequired()
                    .HasMaxLength(5);

                entity.Property(e => e.ScheduledTime).HasColumnType("time(3)");

                entity.Property(e => e.StationFrom).HasMaxLength(255);

                entity.Property(e => e.StationShortFrom).HasMaxLength(6);

                entity.Property(e => e.StationShortTo).HasMaxLength(6);

                entity.Property(e => e.StationTo).HasMaxLength(255);

                entity.HasOne(d => d.BasicData)
                    .WithMany(p => p.Remark)
                    .HasForeignKey(d => new { d.Date, d.TrainNumber })
                    .HasConstraintName("FK_Remark_BasicData");
            });

            modelBuilder.Entity<SectionOfLine>(entity =>
            {
                entity.HasKey(e => new { e.Id, e.Date, e.TrainNumber });

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.Date).HasColumnType("date");

                entity.Property(e => e.TrainNumber).HasMaxLength(10);

                entity.Property(e => e.ExceptionalSending).HasMaxLength(10);

                entity.Property(e => e.Line).HasMaxLength(20);

                entity.Property(e => e.SectionOfLineType)
                    .IsRequired()
                    .HasMaxLength(5);

                entity.Property(e => e.Station).HasMaxLength(255);

                entity.Property(e => e.StationShort).HasMaxLength(6);

                entity.Property(e => e.TimeActual).HasColumnType("time(3)");

                entity.Property(e => e.TimeScheduled).HasColumnType("time(3)");

                entity.Property(e => e.TractionVehicle2).HasMaxLength(15);

                entity.Property(e => e.TractionVehicle3).HasMaxLength(15);

                entity.Property(e => e.TractionVeihicle1).HasMaxLength(15);

                entity.HasOne(d => d.BasicData)
                    .WithMany(p => p.SectionOfLine)
                    .HasForeignKey(d => new { d.Date, d.TrainNumber })
                    .HasConstraintName("FK_SectionOfLine_BasicData");
            });

            modelBuilder.Entity<Vehicle>(entity =>
            {
                entity.HasKey(e => new { e.Id, e.Date, e.TrainNumber });

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.Date).HasColumnType("date");

                entity.Property(e => e.TrainNumber).HasMaxLength(10);

                entity.Property(e => e.CarriageVehicleNumber).HasMaxLength(20);

                entity.Property(e => e.CarriageVehicleType).HasMaxLength(20);

                entity.Property(e => e.Info).HasMaxLength(255);

                entity.Property(e => e.RestaurantCar).HasMaxLength(5);

                entity.Property(e => e.StationShortFrom).HasMaxLength(6);

                entity.Property(e => e.StationShortTo).HasMaxLength(6);

                entity.Property(e => e.VehicleType)
                    .IsRequired()
                    .HasMaxLength(5);

                entity.HasOne(d => d.BasicData)
                    .WithMany(p => p.Vehicle)
                    .HasForeignKey(d => new { d.Date, d.TrainNumber })
                    .HasConstraintName("FK_Vehicle_BasicData");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}