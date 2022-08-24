using FamilyTree.Domain.Interfaces;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FamilyTree.Domain.Entities
{
    public abstract class Document : IDocument
    {
        public ObjectId Id { get; set; }
    }
}
