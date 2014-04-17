using MongoDB.Driver;
using RealEstate.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RealEstate.Rentals;

namespace RealEstate.Database
{
    public class RealEstateContext
    {
        public readonly MongoDatabase Db;

        public RealEstateContext(MongoDatabase db)
        {
            Db = db;
        }

        public MongoCollection<Rental> Rentals
        {
            get { return Db.GetCollection<Rental>("rentals"); }
        }
    }
}