using FamilyTree.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FamilyTree.Domain.Entities
{
    public class Relationship
    {
        public int Parent { get; set; }

        public int Children { get; set; }

        public RelationshipTypeEnum RelationshipType { get; set; }
    }
}
