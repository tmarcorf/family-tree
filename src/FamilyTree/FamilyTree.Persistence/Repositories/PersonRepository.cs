using FamilyTree.Domain.Entities;
using FamilyTree.Persistence.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
