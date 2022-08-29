using FamilyTree.Domain.Entities;
using FamilyTree.Domain.Enums;

namespace FamilyTree.Tests.Helpers
{
    public static class ObjectBuilder
    {
        public static Person GetPerson(string id, string name, GenderTypeEnum genderType, List<string>? children, List<string>? parent)
        {
            return new Person
            {
                Id = id,
                Name = name,
                GenderType = genderType,
                Children = children,
                Parent = parent
            };
        }
    }
}
