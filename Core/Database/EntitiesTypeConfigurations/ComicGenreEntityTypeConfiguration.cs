using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace API_Application.Core.Database.EntitiesTypeConfigurations
{
    public class ComicGenreEntityTypeConfiguration : IEntityTypeConfiguration<ComicGenre>
    {
        public void Configure(EntityTypeBuilder<ComicGenre> entity)
        {
            entity.ToTable("comic_genre");

            entity.HasKey(cg => new { cg.ComicId, cg.GenreId });

            entity.Property(e => e.ComicId).HasColumnName("comic_id");
            entity.Property(e => e.GenreId).HasColumnName("genre_id");

            entity.HasOne(d => d.Comic).WithMany()
                .HasForeignKey(d => d.ComicId)
                .HasConstraintName("FK__comic_gen__comic__52593CB8");

            entity.HasOne(d => d.Genre).WithMany()
                .HasForeignKey(d => d.GenreId)
                .HasConstraintName("FK__comic_gen__genre__534D60F1");
        }
    }
}
