using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace UniversityNews_aNewTry.Models
{
    public partial class NewsContext : DbContext
    {
        public virtual DbSet<News> News { get; set; }

        public NewsContext(DbContextOptions<NewsContext> options) : base(options)
        {  }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            

            modelBuilder.Entity<News>(entity =>
            {
                entity.HasKey(e => e.Title)
                    .HasName("PK__News__2CB664DD09E4C9C6");

                entity.Property(e => e.Title).HasMaxLength(100);

                entity.Property(e => e.NewsUrl).HasMaxLength(512);

                entity.Property(e => e.OriginalDate).HasColumnType("datetime");

                entity.Property(e => e.PictureUrl).HasMaxLength(512);

                entity.Property(e => e.PublishDate).HasColumnType("datetime");

                entity.Property(e => e.UniversityName).HasMaxLength(10);
            });

        }
    }
}