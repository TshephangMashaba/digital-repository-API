using DigitalRepository.Models;
using System.ComponentModel.DataAnnotations;

namespace DigitalRepository.DTOs
{
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
            public string Contributor { get; set; }
            public DateTime? Date { get; set; }
            public DateTime DateUploaded { get; set; }
            public string Type { get; set; }
            public string Format { get; set; }
            public string Identifier { get; set; }
            public string Source { get; set; }
            public string Language { get; set; }
            public string Relation { get; set; }
            public string Coverage { get; set; }
            public string Rights { get; set; }
            public ItemCategory Category { get; set; }
            public string MemberName { get; set; }
            public string Explanation { get; set; }

            // File information for frontend
            public string FileName { get; set; }
            public string FileContentType { get; set; }
            public long? FileSize { get; set; }
            public bool HasFile => !string.IsNullOrEmpty(FileName);
        }
    }
    public class CreateItemDto
    {
        [Required]
        public string Title { get; set; } = string.Empty;

        public string Creator { get; set; } = string.Empty; // Optional
        public string Subject { get; set; } = string.Empty; // Optional
        public string Description { get; set; } = string.Empty; // Optional
        public string Publisher { get; set; } = string.Empty; // Optional
        public string Contributor { get; set; } = string.Empty; // Optional - NO [Required]
        public DateTime? Date { get; set; } // Optional

        [Required]
        public string Type { get; set; } = string.Empty;

        public string Format { get; set; } = string.Empty; // Optional
        public string Identifier { get; set; } = string.Empty; // Optional - NO [Required]
        public string Source { get; set; } = string.Empty; // Optional - NO [Required]
        public string Language { get; set; } = "en"; // Optional
        public string Relation { get; set; } = string.Empty; // Optional - NO [Required]
        public string Coverage { get; set; } = string.Empty; // Optional - NO [Required]

        [Required]
        public string Rights { get; set; } = string.Empty;

        [Required]
        public ItemCategory Category { get; set; }

        [Required]
        public string MemberName { get; set; } = string.Empty;

        public string Explanation { get; set; } = string.Empty; // Optional

        // File data (will be converted from base64 string)
        public string FileName { get; set; } = string.Empty; // Optional
        public string FileContentType { get; set; } = string.Empty; // Optional
        public long? FileSize { get; set; } // Optional
        public string FileData { get; set; } = string.Empty; // Optional - Base64 encoded file data
    }
}