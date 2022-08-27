using FamilyTree.Domain.Entities;
using FamilyTree.Domain.Interfaces;
using FamilyTree.Persistence.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FamilyTree.Persistence.Helpers
{
    internal class ConsistencyPersonHelper
    {
        private readonly IRepository<Person> _personRepository;

        public ConsistencyPersonHelper(IRepository<Person> personRepository)
        {
            _personRepository = personRepository;
        }

        public void ExecuteConsistence(string idPerson, List<string> idsParentChildren, bool consistParents)
        {
            if (idsParentChildren.Count() > 0)
            {
                foreach (var id in idsParentChildren)
                {
                    if (!string.IsNullOrEmpty(id))
                    {
                        var person = _personRepository.GetByIdAsync(id).Result;

                        if (person != null)
                        {
                            ConsistRelationships(idPerson, person, consistParents);
                        }
                    }
                }
            }
        }

        private async void ConsistRelationships(string idPerson, Person personToConsist, bool consistParents)
        {
            if (consistParents)
            {
                if (personToConsist.Children.All(x => x != idPerson))
                {
                    personToConsist.Children.Add(idPerson);
                }
            }
            else
            {
                if (personToConsist.Parent.All(x => x != idPerson))
                {
                    personToConsist.Parent.Add(idPerson);
                }
            }

            await _personRepository.UpdateAsync(personToConsist);
        }
    }
}
