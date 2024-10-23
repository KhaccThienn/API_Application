namespace API_Application.Core.Models
{
    public partial class Favourite
    {
        public int? ComicId { get; set; }

        public int? UserId { get; set; }

        public virtual Comic? Comic { get; set; }

        public virtual User? User { get; set; }

        public DateTime? Created_At { get; set; }
    }
}
