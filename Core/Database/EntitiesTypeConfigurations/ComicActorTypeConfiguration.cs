using API_Application.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace API_Application.Core.Database.EntitiesTypeConfigurations
{
    public class ComicActorTypeConfiguration : IEntityTypeConfiguration<ComicActor>
    {
        public void Configure(EntityTypeBuilder<ComicActor> entity)
        {
            entity
                .HasNoKey()
                .ToTable("comic_actor");

            entity.Property(e => e.ActorId).HasColumnName("actor_id");
            entity.Property(e => e.ComicId).HasColumnName("comic_id");

            entity.HasOne(d => d.Actor).WithMany()
                .HasForeignKey(d => d.ActorId)
                .HasConstraintName("FK__comic_act__actor__4F7CD00D");

            entity.HasOne(d => d.Comic).WithMany()
                .HasForeignKey(d => d.ComicId)
                .HasConstraintName("FK__comic_act__comic__4E88ABD4");
        }
    }
}
