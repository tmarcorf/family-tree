﻿using FamilyTree.Persistence.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FamilyTree.Persistence.Context
{
    public class FamilyTreeDatabaseContext : IFamilyTreeDatabaseContext
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}
