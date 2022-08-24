using FamilyTree.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FamilyTree.Persistence.Interfaces
{
    public interface IPersonRepository : IRepository<Person>
    {
    }
}
