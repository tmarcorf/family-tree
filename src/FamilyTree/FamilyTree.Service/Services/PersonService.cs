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
                var persons = await _personRepository.GetManyBy(x => true);

                if (person != null)
                {
                    return _treeProcessor.GetPersonTree(person.Id, persons);
                }
            }

            return null;
        }

        public async Task<Person> FindByName(string name)
        {
            if (!string.IsNullOrEmpty(name))
            {
                var person = await _personRepository.GetOneBy(x => x.Name == name);

                return person;
            }

            return null;
        }

        public async Task<List<Person>> FindAll()
        {
            return await _personRepository.GetManyBy(x => true);
        }

        public async Task<Person> Create(Person person)
        {
            if (person != null && !string.IsNullOrEmpty(person.Name))
            {
                await _personRepository.InsertAsync(person);

                return await _personRepository.GetByIdAsync(person.Id);
            }

            return null;
        }

        public async Task<Person> Update(Person person)
        {
            if (!string.IsNullOrEmpty(person.Name))
            {
                await _personRepository.UpdateAsync(person);

                return await _personRepository.GetByIdAsync(person.Id);
            }

            return null;
        }

        public async Task<bool> Delete(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                return await _personRepository.DeleteAsync(id);
            }

            return false;
        }
    }
}
