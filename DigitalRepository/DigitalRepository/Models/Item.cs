namespace DigitalRepository.Models
{
    public class Item
    {
        public int Id { get; set; }

        // Dublin Core Metadata
        public string Title { get; set; }
        public string Creator { get; set; }
        public string Subject { get; set; }
        public string Description { get; set; }
        public string Publisher { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime DateUploaded { get; set; }
        public string Type { get; set; } // Audio, Image, Text, etc.
        public string Format { get; set; } // mp3, jpg, pdf, etc.
        public string Identifier { get; set; } // URL or unique identifier
        public string Source { get; set; }
        public string Language { get; set; }
        public string Rights { get; set; } // Copyright information

        // Additional project-specific fields
        public ItemCategory Category { get; set; } // Song, DigitalArtifact, BornDigital
        public string MemberName { get; set; }
        public string Explanation { get; set; } // Why this item was chosen
    }


    public enum ItemCategory
    {
        Song,
        DigitalArtifact,
        BornDigital
    }
}
