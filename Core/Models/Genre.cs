namespace API_Application.Core.Models;

public partial class Genre
{
    public int? Id { get; set; }

    public string? Name { get; set; }

    public string? Slug { get; set; }

    public DateOnly? CreatedAt { get; set; }

    public DateOnly? UpdatedAt { get; set; }
}
