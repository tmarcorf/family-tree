using FamilyTree.Persistence.Interfaces;

namespace FamilyTree.Persistence.Context
{
    public class FamilyTreeDatabaseContext : IFamilyTreeDatabaseContext
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}
