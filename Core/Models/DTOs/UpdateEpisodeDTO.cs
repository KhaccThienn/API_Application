namespace API_Application.Core.Models.DTOs
{
    public class UpdateEpisodeDTO
    {
        public int? Id { get; set; }

        public string? Title { get; set; }

        public int? DisplayOrder { get; set; }

        public byte? Status { get; set; }

        public int? ComicId { get; set; }
    }
}
