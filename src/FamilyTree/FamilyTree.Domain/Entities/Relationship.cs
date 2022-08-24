using FamilyTree.Domain.Enums;

namespace FamilyTree.Domain.Entities
{
    public class Relationship
    {
        public int Parent { get; set; }

        public int Children { get; set; }

        public RelationshipTypeEnum RelationshipType { get; set; }
    }
}
