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
        modelBuilder.ApplyConfiguration<User>(new UserEntityTypeConfiguration());

        modelBuilder.ApplyConfiguration<Actor>(new ActorEntityTypeConfiguration());

        modelBuilder.ApplyConfiguration<Comic>(new ComicEntityTypeConfiguration());

        modelBuilder.ApplyConfiguration<ComicActor>(new ComicActorEntityTypeConfiguration());

        modelBuilder.ApplyConfiguration<Director>(new DirectorEntityTypeConfiguration());

        modelBuilder.ApplyConfiguration<ComicDirector>(new ComicDirectorEntityTypeConfiguration());

        modelBuilder.ApplyConfiguration<Genre>(new GenreEntityTypeConfiguration());

        modelBuilder.ApplyConfiguration<ComicGenre>(new ComicGenreEntityTypeConfiguration());

        modelBuilder.ApplyConfiguration<Episode>(new EpisodeEntityTypeConfiguration());

        modelBuilder.ApplyConfiguration<Image>(new ImageEntityTypeConfiguration());

        modelBuilder.ApplyConfiguration<Review>(new ReviewEntityTypeConfiguration());

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
