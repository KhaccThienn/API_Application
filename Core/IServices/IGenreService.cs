namespace API_Application.Core.IServices
{
    public interface IGenreService
    {
        Task<IEnumerable<Genre>> GetGenres();
        Task<Genre> Insert(Genre g);
        Task<Genre> GetById(int id);
        Task<Genre> Update(int id, Genre g);
        Task<Genre> Delete(int id);
    }
}
