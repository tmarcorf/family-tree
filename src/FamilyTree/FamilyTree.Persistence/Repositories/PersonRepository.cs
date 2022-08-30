using FamilyTree.Domain.Entities;
using FamilyTree.Persistence.Interfaces;
using MongoDB.Bson;

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

            ExecuteConsistency(person.Id, person.Parent, true);
            ExecuteConsistency(person.Id, person.Children, false);

            return base.InsertAsync(person);
        }

        public override Task<Person> UpdateAsync(Person person)
        {
            ExecuteConsistency(person.Id, person.Parent, true);
            ExecuteConsistency(person.Id, person.Children, false);

            return base.UpdateAsync(person);
        }

        #region PRIVATE METHODS

        private void ExecuteConsistency(string idPerson, List<string> idsParentChildren, bool consistParents)
        {
            if (idsParentChildren.Count() > 0)
            {
                foreach (var id in idsParentChildren)
                {
                    if (!string.IsNullOrEmpty(id))
                    {
                        var person = GetByIdAsync(id);

                        if (person != null)
                        {
                            ConsistRelationships(idPerson, person.Result, consistParents);
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

        #endregion
    }
}
