using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace BlogProject.Models
{
    public  class BlogDbContext : DbContext
    {
     
        public BlogDbContext(DbContextOptions<BlogDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Article> Articles { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Comment> Comments { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Turkish_CI_AS");

            modelBuilder.Entity<Article>(entity =>
            {
                entity.ToTable("Article");

                entity.Property(e => e.ContentMain)
                    .HasMaxLength(500)
                    .HasColumnName("Content_Main");

                entity.Property(e => e.ContentSummary)
                    .HasMaxLength(500)
                    .HasColumnName("Content_Summary");

                entity.Property(e => e.Picture).HasMaxLength(500);

                entity.Property(e => e.PublishDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Publish_Date");

                entity.Property(e => e.Title).HasMaxLength(50);

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Articles)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Article_Category");
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("Category");

                entity.Property(e => e.Name).HasMaxLength(50);
            });

            modelBuilder.Entity<Comment>(entity =>
            {
                entity.ToTable("Comment");

                entity.Property(e => e.ArticleId).HasColumnName("Article_Id");

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.PublishDate).HasColumnType("datetime");

                entity.HasOne(d => d.Article)
                    .WithMany(p => p.Comments)
                    .HasForeignKey(d => d.ArticleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Comment_Article");
            });

        }

    }
}
