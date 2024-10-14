namespace API_Application.Core.Models.DTOs
{
    public class AddEpisodeDTO
    {
        public string? Title { get; set; }

        public int? DisplayOrder { get; set; }

        public byte? Status { get; set; }

        public int? ComicId { get; set; }
    }
}
