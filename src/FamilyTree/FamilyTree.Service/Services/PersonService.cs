using FamilyTree.Domain.Entities;
using FamilyTree.Persistence.Interfaces;
using FamilyTree.Service.Interfaces;
using System.Linq.Expressions;

namespace FamilyTree.Service.Services
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

        public IEnumerable<Person> FindAll()
        {
            return _personRepository.GetBy(x => true);
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
