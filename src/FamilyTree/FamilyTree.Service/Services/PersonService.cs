using FamilyTree.Domain.Entities;
using FamilyTree.Persistence.Interfaces;
using FamilyTree.Service.Interfaces;
using System.Linq.Expressions;

namespace FamilyTree.Service.Services
{
    public class PersonService : IPersonService
    {
        private readonly IPersonRepository _personRepository;
        private readonly ITreeProcessor _treeProcessor;

        public PersonService(IPersonRepository personRepository, ITreeProcessor treeProcessor)
        {
            _personRepository = personRepository;
            _treeProcessor = treeProcessor;
        }

        public async Task<PersonTree> FindById(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                var person = await _personRepository.GetByIdAsync(id);
                return _treeProcessor.GetPersonTree(person.Id, _personRepository.GetBy(x => true).ToList());
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

        public async Task Delete(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                await _personRepository.DeleteAsync(id);
            }
        }
    }
}
