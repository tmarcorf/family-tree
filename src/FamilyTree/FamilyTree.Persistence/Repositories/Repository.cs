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

        public virtual async Task<TDocument> GetOneBy(Expression<Func<TDocument, bool>> filter)
        {
            var result = await _collection.FindAsync(filter);

            return result.First();
        }

        public virtual Task<List<TDocument>> GetManyBy(Expression<Func<TDocument, bool>> filter)
        {
            return Task.Run(() =>
            {
                var documents = _collection.Find(filter).ToCursor();

                return documents.ToList();
            });
        }

        public virtual async Task<TDocument> GetByIdAsync(string id)
        {
            var result = await _collection.FindAsync(x => x.Id == id);

            return result.ToListAsync().Result.First();
        }

        public virtual async Task InsertAsync(TDocument document)
        {
            await _collection.InsertOneAsync(document);
        }

        public virtual async Task<TDocument> UpdateAsync(TDocument document)
        {
            var documentExist = _collection.AsQueryable().Any(x => x.Id == document.Id);

            if (documentExist)
            {
                return await _collection.FindOneAndReplaceAsync(x => x.Id == document.Id, document);
            }

            return default;
        }

        public virtual async Task<bool> DeleteAsync(string id)
        {
            if (_collection.Find(x => x.Id == id).First() != null)
            {
                var result = await _collection.DeleteOneAsync(x => x.Id == id);

                return (result.IsAcknowledged && result.DeletedCount > 0);
            }

            return false;
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
