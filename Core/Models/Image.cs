namespace API_Application.Core.Models;

public partial class Image
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Url { get; set; }

    public int? DisplayOrder { get; set; }

    public DateOnly? CreatedAt { get; set; }

    public DateOnly? UpdatedAt { get; set; }

    public int? EpisodeId { get; set; }

    public virtual Episode? Episode { get; set; }
}
