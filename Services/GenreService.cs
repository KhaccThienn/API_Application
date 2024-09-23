using API_Application.Core.IRepositories;
using API_Application.Core.IServices;

namespace API_Application.Services
{
    public class GenreService : IGenreService
    {
        private readonly IGenreRepository _repo;
        public GenreService(IGenreRepository repo)
        {
            _repo = repo;
        }
        public async Task<Genre> Delete(int id)
        {
            return await _repo.Delete(id);
        }

        public async Task<Genre> GetById(int id)
        { 
            return await _repo.GetById(id);
        }

        public Task<IEnumerable<Genre>> GetGenres()
        {
            return _repo.GetGenres();
        }

        public async Task<Genre> Insert(Genre g)
        {
            return await _repo.Insert(g);
        }

        public async Task<Genre> Update(int id, Genre g)
        {
            return await _repo.Update(id, g);
        }
    }
}
