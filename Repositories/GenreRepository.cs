using API_Application.Core.Database.InMemory;
using API_Application.Core.Database;
using API_Application.Core.IRepositories;
using API_Application.Core.Models;

namespace API_Application.Repositories
{
    public class GenreRepository : IGenreRepository
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<GenreRepository> _logger;
        private readonly DbComicAppContext _db;
        private readonly GenreMemory _inMem;

        public GenreRepository(IConfiguration configuration, ILogger<GenreRepository> logger, DbComicAppContext context, GenreMemory inMem)
        {
            _configuration = configuration;
            _logger = logger;
            _db = context;
            _inMem = inMem;
        }
        public async Task<Genre> Delete(int id)
        {
            var genre = new Genre();
            var g = _inMem.GenreMem.FirstOrDefault(x => x.Key.Equals(id.ToString()));
            if (g.Value != null)
            {
                try
                {
                    genre = g.Value;

                    _inMem.GenreMem.Remove(g.Key);

                    _db.Genres.Remove(genre);
                    _db.SaveChanges();
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message);
                }
            }

            return genre;
        }

        public async Task<Genre> GetById(int id)
        {
            var genre = new Genre();
            var g = _inMem.GenreMem.FirstOrDefault(x => x.Key.Equals(id.ToString()));
            if (g.Value != null)
            {
                genre = g.Value;
            }
            return genre;
        }

        public async Task<IEnumerable<Genre>> GetGenres()
        {
            List<Genre> genres = new List<Genre>();

            try
            {
                genres = _inMem.GenreMem.Values.ToList();
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                throw;
            }

            return genres;
        }

        public async Task<Genre> Insert(Genre g)
        {
            try
            {
                _db.Entry(g).State = EntityState.Added; // added row

                // insert into db
                var genre = await _db.Genres.AddAsync(g);
                _db.SaveChanges();

                // Save into memory
                _inMem.GenreMem.Add(g.Id.ToString(), g);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
            return g;
        }

        public async Task<Genre> Update(int id, Genre g)
        {
            var genre = new Genre();

            var oldGenre = _inMem.GenreMem.FirstOrDefault(x => x.Key.Equals(id.ToString()));
            genre.Id = id;
            genre.Name = g.Name;
            genre.Slug = g.Slug;
            genre.UpdatedAt = DateOnly.FromDateTime(DateTime.Now);

            try
            {
                _db.Genres.Update(genre);
                _db.SaveChanges();

                _inMem.GenreMem.Remove(genre.Id.ToString());
                _inMem.GenreMem.Add(genre.Id.ToString(), g);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }

            return genre;
        }
    }
}
