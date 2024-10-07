namespace API_Application.Core.Models.DTOs
{
    public class UpdateComicDTO
    {
        public int? Id { get; set; }
        public string? Title { get; set; }

        public string? Slug { get; set; }

        public string? Description { get; set; }

        public string? Poster { get; set; }

        public int? ReleaseYear { get; set; }

        public int? View { get; set; }

        public double? Rating { get; set; }

        public string Type { get; set; }

        public byte? Status { get; set; }

        public IFormFile? ImageFile { get; set; }

        public List<int>? ListActors { get; set; }
        public List<int>? ListDirector { get; set; }
        public List<int>? ListGenres { get; set; }
    }
}
