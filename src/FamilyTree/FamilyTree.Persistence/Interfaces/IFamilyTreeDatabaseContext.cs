namespace FamilyTree.Persistence.Interfaces
{
    public interface IFamilyTreeDatabaseContext
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}
