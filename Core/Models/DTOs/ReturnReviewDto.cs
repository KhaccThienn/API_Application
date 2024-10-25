namespace API_Application.Core.Models.DTOs
{
    public class ReturnReviewDto
    {
        public int?      ReviewId   { get; set; }
        public string?   UserName   { get; set; }
        public string?   UserAvatar { get; set; }
        public DateOnly? CreatedAt  { get; set; }
        public string?   Comment    { get; set; }
        public int?      Rating     { get; set; }
    }

}
