using FamilyTree.Domain.Entities;
using FamilyTree.Persistence.Interfaces;
using FamilyTree.Service.Interfaces;

namespace FamilyTree.Service.ImplementedContracts
{
    public class PersonService : IPersonService
    {
        private readonly IPersonRepository _personRepository;

        public PersonService(IPersonRepository personRepository)
        {
            _personRepository = personRepository;
        }

        public async Task<Person> FindById(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                return await _personRepository.GetByIdAsync(id);
            }

            return null;
        }

        public async Task<Person> Create(Person person)
        {
            if (!string.IsNullOrEmpty(person.Name))
            {
                await _personRepository.InsertAsync(person);
            }

            return null;
        }

        public async Task<Person> Update(Person person)
        {
            if (!string.IsNullOrEmpty(person.Name))
            {
                await _personRepository.UpdateAsync(person);
            }

            return null;
        }

        public async Task<Person> Delete(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                await _personRepository.DeleteAsync(id);
            }

            return null;
        }
    }
}
