namespace DigitalRepository.Models
{
    // In DigitalRepository.Models namespace
    public class Item
    {
        public int Id { get; set; }

        // Dublin Core Metadata
        public string Title { get; set; }
        public string Creator { get; set; }
        public string Subject { get; set; }
        public string Description { get; set; }
        public string Publisher { get; set; }
        public string Contributor { get; set; }
        public DateTime? Date { get; set; }
        public string Type { get; set; }
        public string Format { get; set; }
        public string Identifier { get; set; }
        public string Source { get; set; }
        public string Language { get; set; }
        public string Relation { get; set; }
        public string Coverage { get; set; }
        public string Rights { get; set; }

        // Additional fields - CHANGE TO INT
        public int CategoryId { get; set; } // Changed from ItemCategory enum
        public string MemberName { get; set; }
        public string Explanation { get; set; }
        public DateTime DateUploaded { get; set; }

        // File storage fields
        public string FileName { get; set; }
        public string FileUrl { get; set; }
        public long? FileSize { get; set; }
        public string FileContentType { get; set; }
        public byte[] FileData { get; set; }

        // Navigation property
        public Category Category { get; set; }
    }

    // NEW: Add Category model
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        // Navigation property
        public ICollection<Item> Items { get; set; }
    }
}