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

        public RealEstateContext()
        {
            MongoClient client = new MongoClient(Settings.Default.MongoConnectionString);
            var server = client.GetServer();
            Db = server.GetDatabase(Settings.Default.RealEstateDbName);
        }

        public MongoCollection<Rental> Rentals
        {
            get { return Db.GetCollection<Rental>("rentals"); }
        }
    }
}