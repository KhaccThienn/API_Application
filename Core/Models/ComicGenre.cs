namespace API_Application.Core.Models;

public partial class ComicGenre
{
    public int? ComicId { get; set; }

    public int? GenreId { get; set; }

    public virtual Comic? Comic { get; set; }

    public virtual Genre? Genre { get; set; }
}
