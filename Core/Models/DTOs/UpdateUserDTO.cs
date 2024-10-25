namespace API_Application.Core.Models.DTOs
{
    public class UpdateUserDTO
    {
        public int? Id { get; set; }
        public string? Name { get; set; }

        public string? Email { get; set; }
        public string? Password { get; set; }

        public string? Avatar { get; set; }

        public string? OldImage { get; set; }

        public string? Role { get; set; }

        public DateOnly? CreatedAt { get; set; }

        public DateOnly? UpdatedAt { get; set; }

        public IFormFile? ImageFile { get; set; }
    }
}
