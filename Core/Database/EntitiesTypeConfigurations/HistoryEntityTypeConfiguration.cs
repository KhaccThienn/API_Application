using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace API_Application.Core.Database.EntitiesTypeConfigurations
{
    public class HistoryEntityTypeConfiguration : IEntityTypeConfiguration<History>
    {
        public void Configure(EntityTypeBuilder<History> entity)
        {
            entity.HasKey(e => e.Id).HasName("PK__history__3213E83F0636D778");

            entity.ToTable("history");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.ComicId).HasColumnName("comic_id");
            entity.Property(e => e.EpisodeId).HasColumnName("episode_id");
            entity.Property(e => e.Created_At).HasColumnName("Created_At");
            entity.Property(e => e.Updated_At).HasColumnName("Updated_At");

            entity.HasOne(d => d.User).WithMany()
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__comic_act__user__4F7CD10D");

            entity.HasOne(d => d.Comic).WithMany()
                .HasForeignKey(d => d.ComicId)
                .HasConstraintName("FK__comic_act__comic__4E88ABD1");

            entity.HasOne(d => d.Episode).WithMany()
                .HasForeignKey(d => d.EpisodeId)
                .HasConstraintName("FK__comic_act__episode__4E88ABD4");
        }
    }
}
