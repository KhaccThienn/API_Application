namespace API_Application.Core.Models;

public partial class ComicDirector
{
    public int? ComicId { get; set; }

    public int? DirectorId { get; set; }

    public virtual Comic? Comic { get; set; }

    public virtual Director? Director { get; set; }
}
