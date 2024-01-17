using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Lazer_Kaynak_WindowsForm.Models
{
    public partial class LazerKaynakDBContext : DbContext
    {
        public LazerKaynakDBContext()
        {
        }

        public LazerKaynakDBContext(DbContextOptions<LazerKaynakDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<CompanyLoginTbl> CompanyLoginTbls { get; set; } = null!;
        public virtual DbSet<InterruptTbl> InterruptTbls { get; set; } = null!;
        public virtual DbSet<MachineListTbl> MachineListTbls { get; set; } = null!;
        public virtual DbSet<PieceOfCountTbl> PieceOfCountTbls { get; set; } = null!;
        public virtual DbSet<ProgramTbl> ProgramTbls { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseNpgsql("Host=localhost;Database=LazerKaynakDB;Username=postgres;Password=Esk5877*");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CompanyLoginTbl>(entity =>
            {
                entity.ToTable("Company_Login_Tbl");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.IsActive).HasColumnType("bit(1)");

                entity.Property(e => e.Type).HasColumnType("bit(1)");
            });

            modelBuilder.Entity<InterruptTbl>(entity =>
            {
                entity.ToTable("Interrupt_Tbl");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.InterruptEndDate).HasColumnType("timestamp without time zone");

                entity.Property(e => e.InterruptStartDate).HasColumnType("timestamp without time zone");
            });

            modelBuilder.Entity<MachineListTbl>(entity =>
            {
                entity.ToTable("MachineList_Tbl");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Ip).HasColumnName("IP");
            });

            modelBuilder.Entity<PieceOfCountTbl>(entity =>
            {
                entity.ToTable("Piece_Of_Count_Tbl");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Date).HasColumnType("timestamp without time zone");
            });

            modelBuilder.Entity<ProgramTbl>(entity =>
            {
                entity.ToTable("Program_Tbl");

                entity.Property(e => e.Id).HasColumnName("ID");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
