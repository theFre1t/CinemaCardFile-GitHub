using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace CinemaCardFile
{
    public partial class CinemaLibContext : DbContext
    {
        public CinemaLibContext()
        {
        }

        public CinemaLibContext(DbContextOptions<CinemaLibContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Film> Film { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Film>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Descrpion)
                    .HasColumnName("descrpion")
                    .HasMaxLength(1000);

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasMaxLength(100);

                entity.Property(e => e.Producer)
                    .HasColumnName("producer")
                    .HasMaxLength(250);

                entity.Property(e => e.Username)
                    .HasColumnName("username")
                    .HasMaxLength(50);

                entity.Property(e => e.Year)
                    .HasColumnName("year")
                    .HasMaxLength(4);

                entity.Property(e => e.Poster).HasColumnName("poster");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
