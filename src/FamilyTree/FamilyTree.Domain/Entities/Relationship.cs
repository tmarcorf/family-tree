using FamilyTree.Domain.Enums;

namespace FamilyTree.Domain.Entities
{
    public class Relationship
    {
        public Relationship()
        {
            Relationships = new List<Relationship>();
        }

        public string Name { get; set; }

        public string RelationshipType { get; set; }

        public List<Relationship> Relationships { get; set; }
    }
}
