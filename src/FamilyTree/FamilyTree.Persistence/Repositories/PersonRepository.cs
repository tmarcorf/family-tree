using FamilyTree.Domain.Entities;
using FamilyTree.Persistence.Interfaces;
using MongoDB.Bson;
using System;

namespace FamilyTree.Persistence.Repositories
{
    public class PersonRepository : Repository<Person>, IPersonRepository
    {
        public PersonRepository(IFamilyTreeDatabaseContext context)
            : base(context)
        {
        }

        public override Task InsertAsync(Person person)
        {
            person.Id = ObjectId.GenerateNewId().ToString();

            ExecuteConsistence(person.Id, person.Parent, true);
            ExecuteConsistence(person.Id, person.Children, false);

            return base.InsertAsync(person);
        }

        public override Task UpdateAsync(Person person)
        {
            ExecuteConsistence(person.Id, person.Parent, true);
            ExecuteConsistence(person.Id, person.Children, false);

            return base.UpdateAsync(person);
        }

        private void ExecuteConsistence(string idPerson, List<string> idsParentChildren, bool consistParents)
        {
            if (idsParentChildren.Count() > 0)
            {
                foreach (var id in idsParentChildren)
                {
                    if (!string.IsNullOrEmpty(id))
                    {
                        var person = GetByIdAsync(id).Result;

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

            await base.UpdateAsync(personToConsist);
        }
    }
}
