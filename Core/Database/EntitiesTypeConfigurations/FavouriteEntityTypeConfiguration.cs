using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace API_Application.Core.Database.EntitiesTypeConfigurations
{
    public class FavouriteEntityTypeConfiguration : IEntityTypeConfiguration<Favourite>
    {
        public void Configure(EntityTypeBuilder<Favourite> entity)
        {
            entity.HasKey(ca => new { ca.ComicId, ca.UserId });

            entity.ToTable("favourite");

            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.ComicId).HasColumnName("comic_id");
            entity.Property(e => e.Created_At).HasColumnName("created_at");

            entity.HasOne(d => d.User).WithMany()
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__comic_act__user__4F7CD00D");

            entity.HasOne(d => d.Comic).WithMany()
                .HasForeignKey(d => d.ComicId)
                .HasConstraintName("FK__comic_act__comic__4E88ABD9");
        }
    }
}
