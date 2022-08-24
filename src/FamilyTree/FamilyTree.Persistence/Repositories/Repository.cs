using FamilyTree.Domain.Attributes;
using FamilyTree.Domain.Interfaces;
using FamilyTree.Persistence.Interfaces;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FamilyTree.Persistence.Repositories
{
    public class Repository<TDocument> : IRepository<TDocument> 
        where TDocument : IDocument
    {
        private readonly IMongoCollection<TDocument> _collection;

        public Repository(IFamilyTreeDatabaseContext context)
        {
            var db = new MongoClient(context.ConnectionString).GetDatabase(context.DatabaseName);
            _collection = db.GetCollection<TDocument>(GetCollectionName(typeof(TDocument)));
        }

        public IEnumerable<TDocument> GetBy(Expression<Func<TDocument, bool>> filter)
        {
            return _collection.Find(filter).ToEnumerable();            
        }

        public virtual Task<TDocument> GetByIdAsync(string id)
        {
            return Task.Run(() =>
            {
                var objectId = new ObjectId(id);
                var filter = Builders<TDocument>.Filter.Eq(x => x.Id, objectId);

                return _collection.Find(filter).SingleOrDefaultAsync();
            });
        }

        public virtual Task InsertAsync(TDocument document)
        {
            return Task.Run(() => _collection.InsertOneAsync(document));
        }

        public virtual async Task InsertManyAsync(ICollection<TDocument> documents)
        {
            await _collection.InsertManyAsync(documents);
        }

        public virtual async Task UpdateAsync(TDocument document)
        {
            var filter = Builders<TDocument>.Filter.Eq(x => x.Id, document.Id);

            await _collection.FindOneAndReplaceAsync(filter, document);
        }
        public Task DeleteAsync(string id)
        {
            return Task.Run(() =>
            {
                var objectId = new ObjectId(id);
                var filter = Builders<TDocument>.Filter.Eq(x => x.Id, objectId);

                _collection.DeleteOneAsync(filter);
            });

        }

        private protected string? GetCollectionName(Type documentType)
        {
            return ((BsonCollectionAttribute)documentType
                .GetCustomAttributes(typeof(BsonCollectionAttribute), true)
                .FirstOrDefault())?
                .CollectionName;
        }
    }
}
