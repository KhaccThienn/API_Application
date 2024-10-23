namespace API_Application.Core.Models.DTOs
{
    public class ComicHistoryDTO
    {
        public int?      ComicId { get; set; }
        public string?   ComicName { get; set; }
        public int?      EpisodeId { get; set; }
        public string?   EpisodeName { get; set; }
        public string?   ImageUrl { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }

}
