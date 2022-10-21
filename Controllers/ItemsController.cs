using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Catalog.DTOs;
using Catalog.Entities;
using Catalog.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.Controllers
{


    [ApiController]
    [Route("[controller]")]
    public class ItemsController : ControllerBase
    {
        private readonly IItemsRepos _repos;

        public ItemsController(IItemsRepos repos)
        {
            _repos = repos;
        }

        [HttpGet]
        public async Task<IEnumerable<ItemDto>> GetItemAsync()
        {
            var items = (await _repos.GetItemAsync())
                        .Select(Item => Item.asDto());
            return items;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ItemDto>> GetItemAsync(Guid id)
        {
            var lItem = await _repos.GetItemAsync(id);
            if (lItem is null)
            {
                return NotFound();
            }

            return lItem.asDto();

        }

        [HttpPost]
        public async Task<ActionResult<ItemDto>> CreateItemAsync(CreateItemDto itemDto)
        {
            Item item = new()
            {
                Id = Guid.NewGuid(),
                Name = itemDto.Name,
                Price = itemDto.Price,
                CreatedDate = DateTimeOffset.UtcNow
            };
            await _repos.CreateItemAsync(item);

            return CreatedAtAction(nameof(GetItemAsync), new { id = item.Id }, item.asDto());
        }

        //PUT   /items/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateItemAsync(Guid id, UpdateItemDto itemDto)
        {
            var existingItem = await _repos.GetItemAsync(id);
            if (existingItem is null)
            {
                return NotFound();
            }

            Item UpdatedItem = existingItem with
            {
                Name = itemDto.Name,
                Price = itemDto.Price

            };
            await _repos.UpdateItemAsync(UpdatedItem);

            return NoContent();
        }

        //DELETE   /items/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteItemAsync(Guid id)
        {
            var existingItem = await _repos.GetItemAsync(id);
            if (existingItem is null)
            {
                return NotFound();
            }

            await _repos.DeleteItemAsync(id);

            return NoContent();
        }


    }
}