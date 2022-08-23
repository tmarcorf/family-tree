using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FamilyTree.Persistence.Interfaces
{
    public interface IFamilyTreeDatabaseContext
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}
