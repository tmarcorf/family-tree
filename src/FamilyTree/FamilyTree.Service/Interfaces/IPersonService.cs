using FamilyTree.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FamilyTree.Service.Interfaces
{
    public interface IPersonService
    {
        Task<Person> FindById(string id);

        Task<Person> Create(Person person);

        Task<Person> Update(Person person);

        Task<Person> Delete(string id);
    }
}
