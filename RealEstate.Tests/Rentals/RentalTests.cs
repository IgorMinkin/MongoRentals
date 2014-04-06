using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using NUnit.Framework;
using RealEstate.Rentals;

namespace RealEstate.Tests.Rentals
{
    [TestFixture]
    public class RentalTests : AssertionHelper
    {
        [Test]
        public void ToDoument_RentalWithPrice_PriceRepresentedAsDouble()
        {
            var rental = new Rental();
            rental.Price = 1;

            var document = rental.ToBsonDocument();

            Assert.AreEqual(document["Price"].BsonType, BsonType.Double);
            
        }

        [Test]
        public void ToDocument_RentalWithAnId_String()
        {
            var rental = new Rental()
            {
                Id = ObjectId.GenerateNewId().ToString()
            };

            var document = rental.ToBsonDocument();

            //Assert.AreEqual(document["_id"].BsonType, typeof(BsonObjectId));
            Expect(document["_id"].BsonType, Is.EqualTo(BsonType.ObjectId));
        }


    }
}
