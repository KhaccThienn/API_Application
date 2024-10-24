namespace API_Application.Core.Models.DTOs
{
    public class EpisodeDto
    {
        public int? Id { get; set; }
        public string? Title { get; set; }
        public int? DisplayOrder { get; set; }
        public int? Status { get; set; }
        public DateOnly? CreatedAt { get; set; }
        public DateOnly? UpdatedAt { get; set; }
        public DateOnly? PublishedAt { get; set; }
    }

    public class ReviewDto
    {
        public int? Id { get; set; }
        public string? Comment { get; set; }
        public int? Rating { get; set; }
        public int? UserId { get; set; }
        public DateOnly? CreatedAt { get; set; }
        public DateOnly? UpdatedAt { get; set; }
    }

    public class ComicDirectorDto
    {
        public int? Id { get; set; }
        public string? Name { get; set; }  // Adjust this according to the properties of your Director model
    }

    public class ComicActorDto
    {
        public int? Id { get; set; }
        public string? Name { get; set; }  // Adjust this according to the properties of your Actor model
    }

    public class ComicGenreDto
    {
        public int? Id { get; set; }
        public string? Name { get; set; }  // Adjust this according to the properties of your Genre model
    }

    public class ComicDetailsDto
    {
        public int? Id { get; set; }
        public string? Title { get; set; }
        public string? Slug { get; set; }
        public string? Description { get; set; }
        public string? Poster { get; set; }
        public int? ReleaseYear { get; set; }
        public int? View { get; set; }
        public double? Rating { get; set; }
        public string? Type { get; set; }
        public int? Status { get; set; }
        public DateOnly? CreatedAt { get; set; }
        public DateOnly? UpdatedAt { get; set; }
        public DateOnly? PublishedAt { get; set; }
        public List<EpisodeDto> Episodes { get; set; }
        public List<ReviewDto> Reviews { get; set; }

        public List<ComicDirectorDto> Directors { get; set; }  // Added Directors
        public List<ComicActorDto> Actors { get; set; }        // Added Actors
        public List<ComicGenreDto> Genres { get; set; }        // Added Genres
    }
}
