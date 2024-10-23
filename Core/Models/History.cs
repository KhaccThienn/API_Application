namespace API_Application.Core.Models
{
    public partial class History
    {
        public int? Id { get; set; }

        public int? ComicId { get; set; }

        public int? UserId { get; set; }

        public int? EpisodeId { get; set; }

        public DateTime? Created_At { get; set; }

        public DateTime? Updated_At { get; set; }

        public virtual Comic? Comic { get; set; }

        public virtual User? User { get; set; }

        public virtual Episode? Episode { get; set; }
    }
}
