using System;
using MongoDB.Driver;
using RealEstate.Properties;

namespace RealEstate.Database
{
    public class MongoDbFactory
    {
        private static readonly Lazy<MongoDatabase> Db = new Lazy<MongoDatabase>(CreateInstance);

        private static MongoDatabase CreateInstance()
        {
            var client = new MongoClient(Settings.Default.MongoConnectionString);
            var server = client.GetServer();
            return server.GetDatabase(Settings.Default.RealEstateDbName);
        }

        public static MongoDatabase Instance
        {
            get { return Db.Value; }
        }
    }
}