using FamilyTree.Domain.Attributes;
using FamilyTree.Domain.Interfaces;
using FamilyTree.Persistence.Interfaces;
using MongoDB.Driver;
using System.Linq.Expressions;

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
                return _collection.Find(x => x.Id == id).SingleOrDefaultAsync();
            });
        }

        public virtual Task InsertAsync(TDocument document)
        {
            return Task.Run(() => _collection.InsertOneAsync(document));
        }

        public virtual async Task InsertManyAsync(ICollection<TDocument> documents)
        {
            if (documents != null && documents.Count > 0)
            {
                await _collection.InsertManyAsync(documents);
            }
        }

        public virtual async Task UpdateAsync(TDocument document)
        {
            if (_collection.Find(x => x.Id == document.Id).First() != null)
            {
                await _collection.FindOneAndReplaceAsync(x => x.Id == document.Id, document);
            }
        }

        public Task DeleteAsync(string id)
        {
            return Task.Run(() =>
            {
                if (_collection.Find(x => x.Id == id).First() != null)
                {
                    _collection.DeleteOneAsync(x => x.Id == id);
                }
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
