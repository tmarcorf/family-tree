using FamilyTree.Domain.Attributes;
using FamilyTree.Domain.Enums;

namespace FamilyTree.Domain.Entities
{
    [BsonCollection("persons")]
    public class Person : Document
    {
        public string Name { get; set; }

        public List<string> Parent { get; set; }

        public List<string> Children { get; set; }

        public GenderTypeEnum GenderType { get; set; }
    }
}
