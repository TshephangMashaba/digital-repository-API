using DigitalRepository.Data;
using DigitalRepository.DTOs;
using DigitalRepository.DTOs.DigitalRepository.DTOs;
using DigitalRepository.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace DigitalRepository.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ItemsController : ControllerBase
    {
        private readonly RepositoryContext _context;

        public ItemsController(RepositoryContext context)
        {
            _context = context;
        }

        // GET: api/items
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ItemDto>>> GetItems()
        {
            var items = await _context.Items
                .Include(i => i.Category) // Include category
                .OrderBy(i => i.CategoryId)
                .ThenBy(i => i.MemberName)
                .Select(i => new ItemDto
                {
                    Id = i.Id,
                    Title = i.Title,
                    Creator = i.Creator,
                    Subject = i.Subject,
                    Description = i.Description,
                    Publisher = i.Publisher,
                    Contributor = i.Contributor,
                    Date = i.Date,
                    Type = i.Type,
                    Format = i.Format,
                    Identifier = i.Identifier,
                    Source = i.Source,
                    Language = i.Language,
                    Relation = i.Relation,
                    Coverage = i.Coverage,
                    Rights = i.Rights,
                    CategoryId = i.CategoryId,
                    CategoryName = i.Category.Name, // Add category name
                    MemberName = i.MemberName,
                    Explanation = i.Explanation,
                    DateUploaded = i.DateUploaded,
                    FileName = i.FileName,
                    FileContentType = i.FileContentType,
                    FileSize = i.FileSize
                })
                .ToListAsync();

            return Ok(items);
        }

        // Add this method to get categories
        [HttpGet("categories")]
        public async Task<ActionResult<IEnumerable<CategoryDto>>> GetCategories()
        {
            var categories = await _context.Categories
                .Select(c => new CategoryDto
                {
                    Id = c.Id,
                    Name = c.Name,
                    Description = c.Description
                })
                .ToListAsync();

            return Ok(categories);
        }

        // GET: api/items/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ItemDto>> GetItem(int id)
        {
            var item = await _context.Items.FindAsync(id);

            if (item == null)
                return NotFound();

            var itemDto = new ItemDto
            {
                Id = item.Id,
                Title = item.Title,
                Creator = item.Creator,
                Subject = item.Subject,
                Description = item.Description,
                Publisher = item.Publisher,
                Contributor = item.Contributor,
                Date = item.Date,
                Type = item.Type,
                Format = item.Format,
                Identifier = item.Identifier,
                Source = item.Source,
                Language = item.Language,
                Relation = item.Relation,
                Coverage = item.Coverage,
                Rights = item.Rights,
             
                MemberName = item.MemberName,
                Explanation = item.Explanation,
                DateUploaded = item.DateUploaded,
                FileName = item.FileName,
                FileContentType = item.FileContentType,
                FileSize = item.FileSize
            };

            return itemDto;
        }

        // GET: api/items/5/file - Download the actual file
        [HttpGet("{id}/file")]
        public async Task<IActionResult> GetItemFile(int id)
        {
            var item = await _context.Items.FindAsync(id);

            if (item == null || item.FileData == null || item.FileData.Length == 0)
                return NotFound();

            // Set appropriate content type and headers
            var contentType = item.FileContentType ?? "application/octet-stream";
            return File(item.FileData, contentType, item.FileName);
        }

        // Add a method to check if file exists
        [HttpGet("{id}/hasfile")]
        public async Task<ActionResult<bool>> HasFile(int id)
        {
            var item = await _context.Items.FindAsync(id);
            if (item == null) return NotFound();

            return Ok(item.FileData != null && item.FileData.Length > 0);
        }


        [HttpPost]
        public async Task<ActionResult<ItemDto>> CreateItem([FromBody] CreateItemDto createItemDto)
        {
            // Verify category exists
            var category = await _context.Categories.FindAsync(createItemDto.CategoryId);
            if (category == null)
            {
                return BadRequest("Invalid category ID");
            }

            var item = new Item
            {
                Title = createItemDto.Title,
                Creator = createItemDto.Creator,
                Subject = createItemDto.Subject,
                Description = createItemDto.Description,
                Publisher = createItemDto.Publisher,
                Contributor = createItemDto.Contributor,
                Date = createItemDto.Date,
                Type = createItemDto.Type,
                Format = createItemDto.Format,
                Identifier = createItemDto.Identifier,
                Source = createItemDto.Source,
                Language = createItemDto.Language,
                Relation = createItemDto.Relation,
                Coverage = createItemDto.Coverage,
                Rights = createItemDto.Rights,
                CategoryId = createItemDto.CategoryId, // Use CategoryId
                MemberName = createItemDto.MemberName,
                Explanation = createItemDto.Explanation,
                DateUploaded = DateTime.UtcNow,
                FileName = createItemDto.FileName,
                FileContentType = createItemDto.FileContentType,
                FileSize = createItemDto.FileSize
            };

            // Convert base64 file data to byte array if provided
            if (!string.IsNullOrEmpty(createItemDto.FileData))
            {
                try
                {
                    item.FileData = Convert.FromBase64String(createItemDto.FileData);
                }
                catch (FormatException)
                {
                    return BadRequest("Invalid file data format");
                }
            }

            _context.Items.Add(item);
            await _context.SaveChangesAsync();

            var itemDto = new ItemDto
            {
                Id = item.Id,
                Title = item.Title,
                Creator = item.Creator,
                Subject = item.Subject,
                Description = item.Description,
                Publisher = item.Publisher,
                Contributor = item.Contributor,
                Date = item.Date,
                Type = item.Type,
                Format = item.Format,
                Identifier = item.Identifier,
                Source = item.Source,
                Language = item.Language,
                Relation = item.Relation,
                Coverage = item.Coverage,
                Rights = item.Rights,
                CategoryId = item.CategoryId,
                CategoryName = category.Name, // Include category name
                MemberName = item.MemberName,
                Explanation = item.Explanation,
                DateUploaded = item.DateUploaded,
                FileName = item.FileName,
                FileContentType = item.FileContentType,
                FileSize = item.FileSize
            };

            return CreatedAtAction(nameof(GetItem), new { id = item.Id }, itemDto);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateItem(int id, [FromBody] CreateItemDto updateItemDto)
        {
            var item = await _context.Items.FindAsync(id);
            if (item == null)
            {
                return NotFound();
            }

            // Verify category exists
            var category = await _context.Categories.FindAsync(updateItemDto.CategoryId);
            if (category == null)
            {
                return BadRequest("Invalid category ID");
            }

            item.Title = updateItemDto.Title;
            item.Creator = updateItemDto.Creator;
            item.Subject = updateItemDto.Subject;
            item.Description = updateItemDto.Description;
            item.Publisher = updateItemDto.Publisher;
            item.Contributor = updateItemDto.Contributor;
            item.Date = updateItemDto.Date;
            item.Type = updateItemDto.Type;
            item.Format = updateItemDto.Format;
            item.Identifier = updateItemDto.Identifier;
            item.Source = updateItemDto.Source;
            item.Language = updateItemDto.Language;
            item.Relation = updateItemDto.Relation;
            item.Coverage = updateItemDto.Coverage;
            item.Rights = updateItemDto.Rights;
            item.CategoryId = updateItemDto.CategoryId; // Use CategoryId
            item.MemberName = updateItemDto.MemberName;
            item.Explanation = updateItemDto.Explanation;
            item.FileName = updateItemDto.FileName;
            item.FileContentType = updateItemDto.FileContentType;
            item.FileSize = updateItemDto.FileSize;

            // Update file data if provided
            if (!string.IsNullOrEmpty(updateItemDto.FileData))
            {
                try
                {
                    item.FileData = Convert.FromBase64String(updateItemDto.FileData);
                }
                catch (FormatException)
                {
                    return BadRequest("Invalid file data format");
                }
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ItemExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/items/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteItem(int id)
        {
            var item = await _context.Items.FindAsync(id);
            if (item == null)
            {
                return NotFound();
            }

            _context.Items.Remove(item);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ItemExists(int id)
        {
            return _context.Items.Any(e => e.Id == id);
        }

        // Upload file endpoint
        [HttpPost("upload")]
        public async Task<ActionResult> UploadFile(IFormFile file)
        {
            try
            {
                if (file == null || file.Length == 0)
                    return BadRequest("No file uploaded");

                // Create uploads directory if it doesn't exist
                var uploadsPath = Path.Combine(Directory.GetCurrentDirectory(), "Uploads");
                if (!Directory.Exists(uploadsPath))
                    Directory.CreateDirectory(uploadsPath);

                // Generate unique filename
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                var filePath = Path.Combine(uploadsPath, fileName);

                // Save file
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                // Return file information
                var result = new
                {
                    fileUrl = $"/api/Items/download/{fileName}",
                    fileName = file.FileName,
                    fileSize = file.Length,
                    contentType = file.ContentType
                };

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("download/{fileName}")]
        public IActionResult DownloadFile(string fileName)
        {
            try
            {
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "Uploads", fileName);
                if (!System.IO.File.Exists(filePath))
                    return NotFound();

                var fileBytes = System.IO.File.ReadAllBytes(filePath);
                var contentType = GetContentType(filePath);
                return File(fileBytes, contentType, fileName);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }



        private string GetContentType(string path)
        {
            var types = new Dictionary<string, string>
            {
                { ".mp3", "audio/mpeg" },
                { ".wav", "audio/wav" },
                { ".jpg", "image/jpeg" },
                { ".jpeg", "image/jpeg" },
                { ".png", "image/png" },
                { ".gif", "image/gif" },
                { ".pdf", "application/pdf" },
                { ".txt", "text/plain" },
                { ".doc", "application/msword" },
                { ".docx", "application/vnd.openxmlformats-officedocument.wordprocessingml.document" }
            };

            var ext = Path.GetExtension(path).ToLowerInvariant();
            return types.ContainsKey(ext) ? types[ext] : "application/octet-stream";
        }
    }
}