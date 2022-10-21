using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Catalog.Entities;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Catalog.Repositories
{
    public class MongoDBItemRepos : IItemsRepos
    {
        private readonly IMongoCollection<Item> itemCollection;

        private readonly FilterDefinitionBuilder<Item> filterBuilder = Builders<Item>.Filter;
        private const string databaseName = "catalog";
        private const string collectionName = "items";

        public MongoDBItemRepos(IMongoClient mongoClient)
        {
            IMongoDatabase database = mongoClient.GetDatabase(databaseName);
            itemCollection = database.GetCollection<Item>(collectionName);
        }
        public async Task CreateItemAsync(Item item)
        {
            await itemCollection.InsertOneAsync(item);
        }

        public async Task DeleteItemAsync(Guid id)
        {
            var filter = filterBuilder.Eq(item => item.Id, id);
            await itemCollection.DeleteOneAsync(filter);
        }

        public async Task<Item> GetItemAsync(Guid id)
        {
            var filter = filterBuilder.Eq(item => item.Id, id);
            return await itemCollection.Find(filter).SingleOrDefaultAsync();

        }

        public async Task<IEnumerable<Item>> GetItemAsync()
        {
            return await itemCollection.Find(new BsonDocument()).ToListAsync();
        }

        public async Task UpdateItemAsync(Item item)
        {
            var filter = filterBuilder.Eq(existingItem => existingItem.Id, item.Id);
            await itemCollection.ReplaceOneAsync(filter, item);
        }


    }
}