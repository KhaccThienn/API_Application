
using API_Application.Core.Database.EntitiesTypeConfigurations;

namespace API_Application.Core.Database;

public partial class DbComicAppContext : DbContext
{
    public DbComicAppContext()
    {
    }

    public DbComicAppContext(DbContextOptions<DbComicAppContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Actor> Actors { get; set; }

    public virtual DbSet<Comic> Comics { get; set; }

    public virtual DbSet<ComicActor> ComicActors { get; set; }

    public virtual DbSet<ComicDirector> ComicDirectors { get; set; }

    public virtual DbSet<ComicGenre> ComicGenres { get; set; }

    public virtual DbSet<Director> Directors { get; set; }

    public virtual DbSet<Episode> Episodes { get; set; }

    public virtual DbSet<Genre> Genres { get; set; }

    public virtual DbSet<Image> Images { get; set; }

    public virtual DbSet<Review> Reviews { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=KHACCTHIENN\\SQLEXPRESS; Database=Db_Comic_App; User Id=sa; Password=1234$; Trusted_Connection=True; TrustServerCertificate=True; MultipleActiveResultSets=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration<Actor>(new ActorEntityTypeConfiguration());

        modelBuilder.ApplyConfiguration<Comic>(new ComicTypeConfiguration());

        modelBuilder.ApplyConfiguration<ComicActor>(new ComicActorTypeConfiguration());

        modelBuilder.ApplyConfiguration<Director>(new DirectorTypeConfiguration());

        modelBuilder.ApplyConfiguration<ComicDirector>(new ComicDirectorTypeConfiguration());

        modelBuilder.ApplyConfiguration<User>(new UserTypeConfiguration());

        modelBuilder.Entity<ComicGenre>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("comic_genre");

            entity.Property(e => e.ComicId).HasColumnName("comic_id");
            entity.Property(e => e.GenreId).HasColumnName("genre_id");

            entity.HasOne(d => d.Comic).WithMany()
                .HasForeignKey(d => d.ComicId)
                .HasConstraintName("FK__comic_gen__comic__52593CB8");

            entity.HasOne(d => d.Genre).WithMany()
                .HasForeignKey(d => d.GenreId)
                .HasConstraintName("FK__comic_gen__genre__534D60F1");
        });

        

        modelBuilder.Entity<Episode>(entity =>
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
        });

        modelBuilder.Entity<Genre>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__genre__3213E83F0636D778");

            entity.ToTable("genre");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedAt).HasColumnName("created_at");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
            entity.Property(e => e.Slug)
                .HasMaxLength(255)
                .HasColumnName("slug");
            entity.Property(e => e.UpdatedAt).HasColumnName("updated_at");
        });

        modelBuilder.Entity<Image>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__images__3213E83F7E5CB749");

            entity.ToTable("images");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedAt).HasColumnName("created_at");
            entity.Property(e => e.DisplayOrder).HasColumnName("display_order");
            entity.Property(e => e.EpisodeId).HasColumnName("episode_id");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
            entity.Property(e => e.UpdatedAt).HasColumnName("updated_at");
            entity.Property(e => e.Url).HasColumnName("url");

            entity.HasOne(d => d.Episode).WithMany(p => p.Images)
                .HasForeignKey(d => d.EpisodeId)
                .HasConstraintName("FK__images__episode___4D94879B");
        });

        modelBuilder.Entity<Review>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__review__3213E83FD8773093");

            entity.ToTable("review");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ComicId).HasColumnName("comic_id");
            entity.Property(e => e.Comment)
                .HasColumnType("text")
                .HasColumnName("comment");
            entity.Property(e => e.CreatedAt).HasColumnName("created_at");
            entity.Property(e => e.Rating).HasColumnName("rating");
            entity.Property(e => e.UpdatedAt).HasColumnName("updated_at");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Comic).WithMany(p => p.Reviews)
                .HasForeignKey(d => d.ComicId)
                .HasConstraintName("FK__review__comic_id__4CA06362");

            entity.HasOne(d => d.User).WithMany(p => p.Reviews)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__review__user_id__4BAC3F29");
        });

        

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
