using FamilyTree.Domain.Entities;
using FamilyTree.Persistence.Helpers;
using FamilyTree.Persistence.Interfaces;
using MongoDB.Bson;
using System;

namespace FamilyTree.Persistence.Repositories
{
    public class PersonRepository : Repository<Person>, IPersonRepository
    {
        private readonly ConsistencyPersonHelper _personHelper;

        public PersonRepository(IFamilyTreeDatabaseContext context)
            : base(context)
        {
            _personHelper = new ConsistencyPersonHelper(this);
        }

        public override Task InsertAsync(Person person)
        {
            person.Id = ObjectId.GenerateNewId().ToString();

            _personHelper.ExecuteConsistence(person.Id, person.Parent, true);
            _personHelper.ExecuteConsistence(person.Id, person.Children, false);

            return base.InsertAsync(person);
        }

        public override Task UpdateAsync(Person person)
        {
            _personHelper.ExecuteConsistence(person.Id, person.Parent, true);
            _personHelper.ExecuteConsistence(person.Id, person.Children, false);

            return base.UpdateAsync(person);
        }

        
    }
}
