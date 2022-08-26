using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FamilyTree.Domain.Entities
{
    public class PersonTree
    {
        public string Name { get; set; }

        public List<Relationship> Relationships { get; set; }
    }
}
