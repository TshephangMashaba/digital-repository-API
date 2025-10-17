namespace DigitalRepository.Data
{
    using DigitalRepository.Models;
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Reflection.Emit;

    public class RepositoryContext : DbContext
    {
        public RepositoryContext(DbContextOptions<RepositoryContext> options) : base(options) { }

        public DbSet<Item> Items { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Item>(entity =>
            {
                entity.HasKey(e => e.Id);

                // Required fields
                entity.Property(e => e.Title).IsRequired().HasMaxLength(255);
                entity.Property(e => e.Type).IsRequired().HasMaxLength(50);
                entity.Property(e => e.Rights).IsRequired().HasMaxLength(500);
                entity.Property(e => e.Category).IsRequired();
                entity.Property(e => e.MemberName).IsRequired().HasMaxLength(100);

                // Optional fields - make them nullable in database
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
                entity.Property(e => e.FileUrl).HasMaxLength(500).IsRequired(false); // NOW OPTIONAL

                // Index for searching/filtering
                entity.HasIndex(e => e.Category);
                entity.HasIndex(e => e.Type);
                entity.HasIndex(e => e.MemberName);
            });
        }

    }
}
