namespace API_Application.Core.Models;

public partial class Director
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Description { get; set; }

    public string? Avatar { get; set; }

    public DateOnly? Birthday { get; set; }

    public DateOnly? CreatedAt { get; set; }

    public DateOnly? UpdatedAt { get; set; }
}
