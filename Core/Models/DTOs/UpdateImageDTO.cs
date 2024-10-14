namespace API_Application.Core.Models.DTOs
{
    public class UpdateImageDTO
    {
        public int Id { get; set; }
        public string? Name { get; set; }

        public string? Url { get; set; }

        public int? DisplayOrder { get; set; }

        public int? EpisodeId { get; set; }

        public IFormFile? ImageFile { get; set; }
    }
}
