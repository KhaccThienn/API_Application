namespace API_Application.Core.Models;

public partial class ComicActor
{
    public int? ComicId { get; set; }

    public int? ActorId { get; set; }

    public virtual Actor? Actor { get; set; }

    public virtual Comic? Comic { get; set; }
}
