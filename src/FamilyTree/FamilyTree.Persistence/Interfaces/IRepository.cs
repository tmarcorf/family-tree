using FamilyTree.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FamilyTree.Persistence.Interfaces
{
    public interface IRepository<TDocument> where TDocument : IDocument
    {
        Task<TDocument> GetByIdAsync(string id);

        Task<IEnumerable<TDocument>> GetAllAsync();

        Task InsertAsync(TDocument document);

        Task InsertManyAsync(ICollection<TDocument> documents);

        Task UpdateAsync(TDocument document);

        Task DeleteAsync(string id);
    }
}
