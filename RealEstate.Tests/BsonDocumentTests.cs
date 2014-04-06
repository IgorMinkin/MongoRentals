using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.IO;
using NUnit.Framework;

namespace RealEstate.Tests
{

    public class BsonDocumentTests
    {
        public BsonDocumentTests()
        {
            JsonWriterSettings.Defaults.Indent = true;
        }

        [Test]
        public void EmptyDocument()
        {
            var document = new BsonDocument();

            Console.WriteLine(document.ToJson());
        }

        [Test]
        public void AddElements()
        {
            var person = new BsonDocument
            {
                {"firstName", new BsonString("bob")},
                {"age", 52},
                {"IsAlive", true}
            };

            Console.WriteLine(person);
        }

        [Test]
        public void AddingArrays()
        {
            var person = new BsonDocument();
            person.Add("address", new BsonArray(new[] {"101 some road", "blah", "blah"}));

            Console.WriteLine(person);
        }
    }
}
