﻿namespace API_Application.Core.Models.DTOs
{
    public class CreateActorDTO
    {
        public string? Name { get; set; }

        public string? Description { get; set; }

        public string? Avatar { get; set; }
        public IFormFile? ImageFile { get; set; }

        public DateOnly? Birthday { get; set; }

        public DateOnly? CreatedAt { get; set; }

        public DateOnly? UpdatedAt { get; set; }
    }
}