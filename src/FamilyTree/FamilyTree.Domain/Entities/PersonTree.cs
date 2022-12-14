using FamilyTree.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FamilyTree.Domain.Entities
{
    public class PersonTree : IPersonTree
    {
        public string Name { get; set; }

        public List<Relationship> Ascendants { get; set; }

        public List<Relationship> Descendants { get; set; }
    }
}
