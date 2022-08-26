using FamilyTree.Domain.Entities;

namespace FamilyTree.Service.Interfaces
{
    public interface IPersonService
    {
        Task<PersonTree> FindById(string id);

        IEnumerable<Person> FindAll();

        Task<Person> Create(Person person);

        Task<Person> Update(Person person);

        Task Delete(string id);
    }
}
