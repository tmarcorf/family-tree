using FamilyTree.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FamilyTree.Service.Interfaces
{
    public interface ITreeProcessor
    {
        PersonTree GetPersonTree(string idPerson, List<Person> persons);
    }
}
