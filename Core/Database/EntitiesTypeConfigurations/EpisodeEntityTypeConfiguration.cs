using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace API_Application.Core.Database.EntitiesTypeConfigurations
{
    public class EpisodeEntityTypeConfiguration : IEntityTypeConfiguration<Episode>
    {
        public void Configure(EntityTypeBuilder<Episode> entity)
        {
            entity.HasKey(e => e.Id).HasName("PK__episode__3213E83F660786D9");

            entity.ToTable("episode");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ComicId).HasColumnName("comic_id");
            entity.Property(e => e.CreatedAt).HasColumnName("created_at");
            entity.Property(e => e.DisplayOrder).HasColumnName("display_order");
            entity.Property(e => e.PublishedAt).HasColumnName("published_at");
            entity.Property(e => e.Status)
                .HasDefaultValue((byte)1)
                .HasColumnName("status");
            entity.Property(e => e.Title)
                .HasMaxLength(255)
                .HasColumnName("title");
            entity.Property(e => e.UpdatedAt).HasColumnName("updated_at");

            entity.HasOne(d => d.Comic).WithMany(p => p.Episodes)
                .HasForeignKey(d => d.ComicId)
                .HasConstraintName("FK__episode__comic_i__4AB81AF0");
        }
    }
}
