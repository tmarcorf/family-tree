using FamilyTree.Domain.Entities;

namespace FamilyTree.Service.Interfaces
{
    public interface IPersonService
    {
        Task<PersonTree> FindById(string id);

        Task<Person> FindByName(string name);

        Task<List<Person>> FindAll();

        Task<Person> Create(Person person);

        Task<Person> Update(Person person);

        Task<bool> Delete(string id);
    }
}
