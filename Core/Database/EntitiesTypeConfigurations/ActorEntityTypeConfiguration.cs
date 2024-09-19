using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace API_Application.Core.Database.EntitiesTypeConfigurations
{
    public class ActorEntityTypeConfiguration : IEntityTypeConfiguration<Actor>
    {
        public void Configure(EntityTypeBuilder<Actor> builder)
        {
            builder.HasKey(e => e.Id).HasName("PK__actor__3213E83F4FDCEA32");

            builder.ToTable("actor");

            builder.Property(e => e.Id).HasColumnName("id");
            builder.Property(e => e.Avatar)
                .HasMaxLength(255)
                .HasColumnName("avatar");
            builder.Property(e => e.Birthday).HasColumnName("birthday");
            builder.Property(e => e.CreatedAt).HasColumnName("created_at");
            builder.Property(e => e.Description)
                .HasColumnType("text")
                .HasColumnName("description");
            builder.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
            builder.Property(e => e.UpdatedAt).HasColumnName("updated_at");
        }
    }
}
