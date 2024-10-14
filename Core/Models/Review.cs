﻿namespace API_Application.Core.Models;

public partial class Review
{
    public int? Id { get; set; }

    public string? Comment { get; set; }

    public int? Rating { get; set; }

    public int? UserId { get; set; }

    public int? ComicId { get; set; }

    public DateOnly? CreatedAt { get; set; }

    public DateOnly? UpdatedAt { get; set; }

    public virtual Comic? Comic { get; set; }

    public virtual User? User { get; set; }
}
