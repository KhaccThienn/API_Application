using API_Application.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace API_Application.Core.Database.EntitiesTypeConfigurations
{
    public class ComicTypeConfiguration : IEntityTypeConfiguration<Comic>
    {
        public void Configure(EntityTypeBuilder<Comic> entity)
        {
            entity.HasKey(e => e.Id).HasName("PK__comic__3213E83F429B0A55");

            entity.ToTable("comic");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedAt).HasColumnName("created_at");
            entity.Property(e => e.Description)
                .HasColumnType("text")
                .HasColumnName("description");
            entity.Property(e => e.Poster)
                .HasMaxLength(255)
                .HasColumnName("poster");
            entity.Property(e => e.PublishedAt).HasColumnName("published_at");
            entity.Property(e => e.Rating).HasColumnName("rating");
            entity.Property(e => e.ReleaseYear).HasColumnName("release_year");
            entity.Property(e => e.Slug)
                .HasMaxLength(255)
                .HasColumnName("slug");
            entity.Property(e => e.Status)
                .HasDefaultValue((byte)1)
                .HasColumnName("status");
            entity.Property(e => e.Title)
                .HasMaxLength(255)
                .HasColumnName("title");
            entity.Property(e => e.Type)
                .HasMaxLength(255)
                .HasColumnName("type");
            entity.Property(e => e.UpdatedAt).HasColumnName("updated_at");
            entity.Property(e => e.View).HasColumnName("view");
        }
    }
}
