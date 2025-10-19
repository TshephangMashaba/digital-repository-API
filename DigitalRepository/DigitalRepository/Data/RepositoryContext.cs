namespace DigitalRepository.Data
{
    using DigitalRepository.Models;
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Reflection.Emit;

    // In DigitalRepository.Data namespace
    public class RepositoryContext : DbContext
    {
        public RepositoryContext(DbContextOptions<RepositoryContext> options) : base(options) { }

        public DbSet<Item> Items { get; set; }
        public DbSet<Category> Categories { get; set; } // NEW

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure Category
            modelBuilder.Entity<Category>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(50);
                entity.Property(e => e.Description).HasMaxLength(200);
            });

            modelBuilder.Entity<Item>(entity =>
            {
                entity.HasKey(e => e.Id);

                // Required fields
                entity.Property(e => e.Title).IsRequired().HasMaxLength(255);
                entity.Property(e => e.Type).IsRequired().HasMaxLength(50);
                entity.Property(e => e.Rights).IsRequired().HasMaxLength(500);
                entity.Property(e => e.CategoryId).IsRequired(); // Changed
                entity.Property(e => e.MemberName).IsRequired().HasMaxLength(100);

                // Optional fields
                entity.Property(e => e.Creator).HasMaxLength(100).IsRequired(false);
                entity.Property(e => e.Subject).HasMaxLength(500).IsRequired(false);
                entity.Property(e => e.Description).HasMaxLength(2000).IsRequired(false);
                entity.Property(e => e.Publisher).HasMaxLength(200).IsRequired(false);
                entity.Property(e => e.Contributor).HasMaxLength(200).IsRequired(false);
                entity.Property(e => e.Format).HasMaxLength(50).IsRequired(false);
                entity.Property(e => e.Identifier).HasMaxLength(1000).IsRequired(false);
                entity.Property(e => e.Source).HasMaxLength(500).IsRequired(false);
                entity.Property(e => e.Language).HasMaxLength(10).IsRequired(false);
                entity.Property(e => e.Relation).HasMaxLength(500).IsRequired(false);
                entity.Property(e => e.Coverage).HasMaxLength(500).IsRequired(false);
                entity.Property(e => e.Explanation).HasMaxLength(2000).IsRequired(false);
                entity.Property(e => e.FileName).HasMaxLength(255).IsRequired(false);
                entity.Property(e => e.FileContentType).HasMaxLength(100).IsRequired(false);
                entity.Property(e => e.FileUrl).HasMaxLength(500).IsRequired(false);

                // Foreign key relationship
                entity.HasOne(i => i.Category)
                      .WithMany(c => c.Items)
                      .HasForeignKey(i => i.CategoryId);

                // Index for searching/filtering
                entity.HasIndex(e => e.CategoryId);
                entity.HasIndex(e => e.Type);
                entity.HasIndex(e => e.MemberName);
            });

            // Seed categories
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Song", Description = "Audio recordings and music" },
                new Category { Id = 2, Name = "DigitalArtifact", Description = "Digital copies of physical artifacts" },
                new Category { Id = 3, Name = "BornDigital", Description = "Originally created digital content" }
            );
        }
    }
}
