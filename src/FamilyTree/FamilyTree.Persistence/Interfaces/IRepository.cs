using FamilyTree.Domain.Interfaces;
using System.Linq.Expressions;

namespace FamilyTree.Persistence.Interfaces
{
    public interface IRepository<TDocument> where TDocument : IDocument
    {
        Task<TDocument> GetByIdAsync(string id);

        Task<TDocument> GetOneBy(Expression<Func<TDocument, bool>> filter);

        Task<List<TDocument>> GetManyBy(Expression<Func<TDocument, bool>> filter);

        Task InsertAsync(TDocument document);

        Task<TDocument> UpdateAsync(TDocument document);

        Task<bool> DeleteAsync(string id);
    }
}
