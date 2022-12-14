using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Catalog.Entities;

namespace Catalog.Repositories
{

    public interface IItemsRepos
    {
        Task<Item> GetItemAsync(Guid id);
        Task<IEnumerable<Item>> GetItemAsync();
        Task CreateItemAsync(Item item);
        Task UpdateItemAsync(Item item);
        Task DeleteItemAsync(Guid id);
    }
}