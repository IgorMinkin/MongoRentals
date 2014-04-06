using MongoDB.Driver;
using RealEstate.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

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
    }
}