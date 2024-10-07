namespace API_Application.Core.Models;

public partial class Comic
{
    public int Id { get; set; }

    public string? Title { get; set; }

    public string? Slug { get; set; }

    public string? Description { get; set; }

    public string? Poster { get; set; }

    public int? ReleaseYear { get; set; }

    public int? View { get; set; }

    public double? Rating { get; set; }

    public string Type { get; set; }

    public byte? Status { get; set; }

    public DateOnly? CreatedAt { get; set; }

    public DateOnly? UpdatedAt { get; set; }

    public DateOnly? PublishedAt { get; set; }

    public virtual ICollection<Episode> Episodes { get; set; } = new List<Episode>();

    public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();
}
