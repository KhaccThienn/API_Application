using API_Application.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace API_Application.Core.Database.EntitiesTypeConfigurations
{
    public class ComicDirectorEntityTypeConfiguration : IEntityTypeConfiguration<ComicDirector>
    {
        public void Configure(EntityTypeBuilder<ComicDirector> entity)
        {
            entity
                .HasNoKey()
                .ToTable("comic_director");

            entity.Property(e => e.ComicId).HasColumnName("comic_id");
            entity.Property(e => e.DirectorId).HasColumnName("director_id");

            entity.HasOne(d => d.Comic).WithMany()
                .HasForeignKey(d => d.ComicId)
                .HasConstraintName("FK__comic_dir__comic__5070F446");

            entity.HasOne(d => d.Director).WithMany()
                .HasForeignKey(d => d.DirectorId)
                .HasConstraintName("FK__comic_dir__direc__5165187F");
        }
    }
}
