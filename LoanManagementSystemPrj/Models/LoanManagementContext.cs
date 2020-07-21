using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace LoanManagementSystemPrj.Models
{
    public partial class LoanManagementContext : DbContext
    {
        public LoanManagementContext()
        {
        }

        public LoanManagementContext(DbContextOptions<LoanManagementContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Admincredentials> Admincredentials { get; set; }
        public virtual DbSet<Loan> Loan { get; set; }
        public virtual DbSet<Usercredentials> Usercredentials { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Data Source=Shivangi-25;Initial Catalog=LoanManagement;Persist Security Info=True;User ID=sa;Password=1289;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Admincredentials>(entity =>
            {
                entity.HasKey(e => e.Username);

                entity.ToTable("admincredentials");

                entity.Property(e => e.Username)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Loan>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.AccBal).HasColumnType("decimal(10, 2)");

                entity.Property(e => e.AccType)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.Address)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Gender)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.LoanAmt)
                    .HasColumnName("loanAmt")
                    .HasColumnType("decimal(10, 2)");

                entity.Property(e => e.LoanPremium)
                    .HasColumnName("loanPremium")
                    .HasColumnType("decimal(10, 2)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.PhnNo)
                    .HasMaxLength(10)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Usercredentials>(entity =>
            {
                entity.ToTable("usercredentials");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
