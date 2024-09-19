namespace API_Application.Core.Models;

public partial class Episode
{
    public int Id { get; set; }

    public string? Title { get; set; }

    public int? DisplayOrder { get; set; }

    public byte? Status { get; set; }

    public DateOnly? CreatedAt { get; set; }

    public DateOnly? UpdatedAt { get; set; }

    public DateOnly? PublishedAt { get; set; }

    public int? ComicId { get; set; }

    public virtual Comic? Comic { get; set; }

    public virtual ICollection<Image> Images { get; set; } = new List<Image>();
}
