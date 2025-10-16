using DigitalRepository.Data;
using DigitalRepository.DTOs;
using DigitalRepository.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DigitalRepository.Controllers
{
    // Controllers/ItemsController.cs
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
        public async Task<ActionResult<IEnumerable<ItemDto>>> GetItems(
            [FromQuery] string category = null,
            [FromQuery] string type = null,
            [FromQuery] string member = null,
            [FromQuery] string search = null)
        {
            var query = _context.Items.AsQueryable();

            // Filtering functionality
            if (!string.IsNullOrEmpty(category))
                query = query.Where(i => i.Category.ToString() == category);

            if (!string.IsNullOrEmpty(type))
                query = query.Where(i => i.Type.Contains(type));

            if (!string.IsNullOrEmpty(member))
                query = query.Where(i => i.MemberName.Contains(member));

            if (!string.IsNullOrEmpty(search))
                query = query.Where(i =>
                    i.Title.Contains(search) ||
                    i.Description.Contains(search) ||
                    i.Subject.Contains(search));

            var items = await query
                .OrderBy(i => i.Category)
                .ThenBy(i => i.MemberName)
                .Select(i => new ItemDto
                {
                    Id = i.Id,
                    Title = i.Title,
                    Creator = i.Creator,
                    Subject = i.Subject,
                    Description = i.Description,
                    Publisher = i.Publisher,
                    DateCreated = i.DateCreated,
                    DateUploaded = i.DateUploaded,
                    Type = i.Type,
                    Format = i.Format,
                    Identifier = i.Identifier,
                    Source = i.Source,
                    Language = i.Language,
                    Rights = i.Rights,
                    Category = i.Category,
                    MemberName = i.MemberName,
                    Explanation = i.Explanation
                })
                .ToListAsync();

            return Ok(items);
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
                DateCreated = item.DateCreated,
                DateUploaded = item.DateUploaded,
                Type = item.Type,
                Format = item.Format,
                Identifier = item.Identifier,
                Source = item.Source,
                Language = item.Language,
                Rights = item.Rights,
                Category = item.Category,
                MemberName = item.MemberName,
                Explanation = item.Explanation
            };

            return itemDto;
        }

        // POST: api/items
        [HttpPost]
        public async Task<ActionResult<ItemDto>> CreateItem(CreateItemDto createItemDto)
        {
            var item = new Item
            {
                Title = createItemDto.Title,
                Creator = createItemDto.Creator,
                Subject = createItemDto.Subject,
                Description = createItemDto.Description,
                Publisher = createItemDto.Publisher,
                DateCreated = createItemDto.DateCreated,
                DateUploaded = DateTime.UtcNow,
                Type = createItemDto.Type,
                Format = createItemDto.Format,
                Identifier = createItemDto.Identifier,
                Source = createItemDto.Source,
                Language = createItemDto.Language,
                Rights = createItemDto.Rights,
                Category = createItemDto.Category,
                MemberName = createItemDto.MemberName,
                Explanation = createItemDto.Explanation
            };

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
                DateCreated = item.DateCreated,
                DateUploaded = item.DateUploaded,
                Type = item.Type,
                Format = item.Format,
                Identifier = item.Identifier,
                Source = item.Source,
                Language = item.Language,
                Rights = item.Rights,
                Category = item.Category,
                MemberName = item.MemberName,
                Explanation = item.Explanation
            };

            return CreatedAtAction(nameof(GetItem), new { id = item.Id }, itemDto);
        }

        // Additional endpoints for update and delete as needed
    }
}
