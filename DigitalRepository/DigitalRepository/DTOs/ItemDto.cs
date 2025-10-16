using DigitalRepository.Models;
using System.ComponentModel.DataAnnotations;

namespace DigitalRepository.DTOs
{
    public class ItemDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Creator { get; set; }
        public string Subject { get; set; }
        public string Description { get; set; }
        public string Publisher { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime DateUploaded { get; set; }
        public string Type { get; set; }
        public string Format { get; set; }
        public string Identifier { get; set; }
        public string Source { get; set; }
        public string Language { get; set; }
        public string Rights { get; set; }
        public ItemCategory Category { get; set; }
        public string MemberName { get; set; }
        public string Explanation { get; set; }
    }

    public class CreateItemDto
    {
        [Required]
        public string Title { get; set; }

        public string Creator { get; set; }
        public string Subject { get; set; }
        public string Description { get; set; }
        public string Publisher { get; set; }
        public DateTime? DateCreated { get; set; }

        [Required]
        public string Type { get; set; }

        public string Format { get; set; }

        [Required]
        public string Identifier { get; set; } // File path or URL

        public string Source { get; set; }
        public string Language { get; set; }

        [Required]
        public string Rights { get; set; }

        [Required]
        public ItemCategory Category { get; set; }

        [Required]
        public string MemberName { get; set; }

        public string Explanation { get; set; }
    }

}
