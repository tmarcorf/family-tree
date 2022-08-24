using FamilyTree.Domain.Entities;
using FamilyTree.Persistence.Interfaces;

namespace FamilyTree.Persistence.Repositories
{
    public class PersonRepository : Repository<Person>, IPersonRepository
    {
        public PersonRepository(IFamilyTreeDatabaseContext context) 
            : base(context)
        {
        }
    }
}
