using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace WpfApp1
{
    public partial class ISPr2337_SkvorcovAOzakaziContext : DbContext
    {
        private static ISPr2337_SkvorcovAOzakaziContext iner;
        public ISPr2337_SkvorcovAOzakaziContext()
        {
        }

        public static ISPr2337_SkvorcovAOzakaziContext init() {
            if (iner == null) {
                iner = new ISPr2337_SkvorcovAOzakaziContext();
            }

            return iner;
        }

        public ISPr2337_SkvorcovAOzakaziContext(DbContextOptions<ISPr2337_SkvorcovAOzakaziContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Company> Companies { get; set; } = null!;
        public virtual DbSet<Role> Roles { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;
        public virtual DbSet<UsersZakazi> UsersZakazis { get; set; } = null!;
        public virtual DbSet<Zakazi> Zakazis { get; set; } = null!;
        public virtual DbSet<ZakaziCompany> ZakaziCompanies { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseMySql("server=cfif31.ru;password=ISPr23-37_SkvorcovAO;userid=ISPr23-37_SkvorcovAO;database=ISPr23-37_SkvorcovAO.zakazi;port=3306;characterset=utf8", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.36-mysql"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("utf8mb4_0900_ai_ci")
                .HasCharSet("utf8mb4");

            modelBuilder.Entity<Company>(entity =>
            {
                entity.ToTable("companies");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Title)
                    .HasMaxLength(45)
                    .HasColumnName("title");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("roles");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Title)
                    .HasMaxLength(45)
                    .HasColumnName("title");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("users");

                entity.HasIndex(e => e.RoleId, "role_identity_idx");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Image).HasColumnName("image");

                entity.Property(e => e.RoleId).HasColumnName("role_id");

                entity.Property(e => e.Surname)
                    .HasMaxLength(45)
                    .HasColumnName("surname");

                entity.Property(e => e.Telephone)
                    .HasMaxLength(16)
                    .HasColumnName("telephone");

                entity.Property(e => e.Username)
                    .HasMaxLength(45)
                    .HasColumnName("username");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientCascade)
                    .HasConstraintName("role_identity");
            });

            modelBuilder.Entity<UsersZakazi>(entity =>
            {
                entity.ToTable("users_zakazi");

                entity.HasIndex(e => e.UsersId, "users_identity_idx");

                entity.HasIndex(e => e.ZakaziId, "zakazi_identity_idx");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.UsersId).HasColumnName("users_id");

                entity.Property(e => e.ZakaziId).HasColumnName("zakazi_id");

                entity.HasOne(d => d.Users)
                    .WithMany(p => p.UsersZakazis)
                    .HasForeignKey(d => d.UsersId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("users_identity");

                entity.HasOne(d => d.Zakazi)
                    .WithMany(p => p.UsersZakazis)
                    .HasForeignKey(d => d.ZakaziId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("zakazi_identity");
            });

            modelBuilder.Entity<Zakazi>(entity =>
            {
                entity.ToTable("zakazi");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Createddate).HasColumnName("createddate");

                entity.Property(e => e.Createdtime)
                    .HasColumnType("time")
                    .HasColumnName("createdtime");

                entity.Property(e => e.Title)
                    .HasMaxLength(45)
                    .HasColumnName("title");
            });

            modelBuilder.Entity<ZakaziCompany>(entity =>
            {
                entity.ToTable("zakazi_companies");

                entity.HasIndex(e => e.CompanyId, "companies_identity_idx");

                entity.HasIndex(e => e.ZakazId, "zakazi_identity_idx");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.CompanyId).HasColumnName("company_id");

                entity.Property(e => e.ZakazId).HasColumnName("zakaz_id");

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.ZakaziCompanies)
                    .HasForeignKey(d => d.CompanyId)
                    .OnDelete(DeleteBehavior.ClientCascade)
                    .HasConstraintName("companies_fk");

                entity.HasOne(d => d.Zakaz)
                    .WithMany(p => p.ZakaziCompanies)
                    .HasForeignKey(d => d.ZakazId)
                    .OnDelete(DeleteBehavior.ClientCascade)
                    .HasConstraintName("zakazi_fk");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
