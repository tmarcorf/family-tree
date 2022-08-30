using FamilyTree.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FamilyTree.Domain.Interfaces
{
    public interface IPersonTree
    {
        public string Name { get; set; }

        public List<Relationship> Ascendants { get; set; }

        public List<Relationship> Descendants { get; set; }

    }
}
