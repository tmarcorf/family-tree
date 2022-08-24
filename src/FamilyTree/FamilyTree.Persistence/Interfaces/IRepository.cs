using FamilyTree.Domain.Interfaces;
using System.Linq.Expressions;

namespace FamilyTree.Persistence.Interfaces
{
    public interface IRepository<TDocument> where TDocument : IDocument
    {
        Task<TDocument> GetByIdAsync(string id);

        IEnumerable<TDocument> GetBy(Expression<Func<TDocument, bool>> filter);

        Task InsertAsync(TDocument document);

        Task InsertManyAsync(ICollection<TDocument> documents);

        Task UpdateAsync(TDocument document);

        Task DeleteAsync(string id);
    }
}
