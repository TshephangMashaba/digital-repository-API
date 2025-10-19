using DigitalRepository.Models;
using System.ComponentModel.DataAnnotations;

namespace DigitalRepository.DTOs
{
    namespace DigitalRepository.DTOs
    {
        // In DigitalRepository.DTOs namespace
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

            // CHANGE TO INT and add CategoryName
            public int CategoryId { get; set; }
            public string CategoryName { get; set; } // For frontend display

            public string MemberName { get; set; }
            public string Explanation { get; set; }

            // File information for frontend
            public string FileName { get; set; }
            public string FileContentType { get; set; }
            public long? FileSize { get; set; }
            public bool HasFile => !string.IsNullOrEmpty(FileName);
        }

        public class CreateItemDto
        {
            [Required]
            public string Title { get; set; } = string.Empty;

            public string Creator { get; set; } = string.Empty;
            public string Subject { get; set; } = string.Empty;
            public string Description { get; set; } = string.Empty;
            public string Publisher { get; set; } = string.Empty;
            public string Contributor { get; set; } = string.Empty;
            public DateTime? Date { get; set; }

            [Required]
            public string Type { get; set; } = string.Empty;

            public string Format { get; set; } = string.Empty;
            public string Identifier { get; set; } = string.Empty;
            public string Source { get; set; } = string.Empty;
            public string Language { get; set; } = "en";
            public string Relation { get; set; } = string.Empty;
            public string Coverage { get; set; } = string.Empty;

            [Required]
            public string Rights { get; set; } = string.Empty;

            // CHANGE TO INT
            [Required]
            public int CategoryId { get; set; }

            [Required]
            public string MemberName { get; set; } = string.Empty;

            public string Explanation { get; set; } = string.Empty;

            // File data
            public string FileName { get; set; } = string.Empty;
            public string FileContentType { get; set; } = string.Empty;
            public long? FileSize { get; set; }
            public string FileData { get; set; } = string.Empty;
        }

        // NEW: Add Category DTO
        public class CategoryDto
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
        }
    }


}