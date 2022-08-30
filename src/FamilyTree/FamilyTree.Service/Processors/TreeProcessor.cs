using FamilyTree.Domain.Entities;
using FamilyTree.Domain.Enums;
using FamilyTree.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace FamilyTree.Service.Processors
{
    public class TreeProcessor : ITreeProcessor
    {
        private readonly Dictionary<RelationshipTypeEnum, string> _relationshipType;

        public TreeProcessor()
        {
            _relationshipType = this.GetRelationshipTypeDictionary();
        }

        public PersonTree GetPersonTree(string idPerson, List<Person> persons)
        {
            var personTree = new PersonTree();
            var person = persons.Find(x => x.Id == idPerson);

            if (person != null)
            {
                personTree.Name = person.Name;
                personTree.Ascendants = PopulateRelationships(person, persons, true);
                personTree.Descendants = PopulateRelationships(person, persons, false);
            }

            return personTree;
        }

        private List<Relationship> PopulateRelationships(Person personCore, List<Person> persons, bool ascendants)
        { 
            var relationships = new List<Relationship>();

            persons.ForEach(person =>
            {
                if (ascendants)
                {
                    if (!string.IsNullOrEmpty(personCore.Id) && person.Children.Contains(personCore.Id))
                    {
                        var relationship = GetRelationship(person.Name, _relationshipType[RelationshipTypeEnum.PARENT]);
                        relationship.Relationships = PopulateRelationships(person, persons, true);

                        relationships.Add(relationship);
                    }
                }
                else
                {
                    if (!string.IsNullOrEmpty(personCore.Id) && person.Parent.Contains(personCore.Id))
                    {
                        var relationship = GetRelationship(person.Name, _relationshipType[RelationshipTypeEnum.CHILD]);
                        relationship.Relationships = PopulateRelationships(person, persons, false);

                        relationships.Add(relationship);
                    }
                }
                
            });

            return relationships;
        }

        private Relationship GetRelationship(string name, string relationshipType)
        {
            return new Relationship
            {
                Name = name,
                RelationshipType = relationshipType
            };
        }

        private Dictionary<RelationshipTypeEnum, string> GetRelationshipTypeDictionary()
        {
            return new Dictionary<RelationshipTypeEnum, string>
            {
                { RelationshipTypeEnum.PARENT, "parent" },
                { RelationshipTypeEnum.CHILD, "child" },
            };
        }
    }
}
