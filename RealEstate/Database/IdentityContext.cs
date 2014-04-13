using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using RealEstate.Security;

namespace RealEstate.Database
{
    public class IdentityContext
    {
        private MongoDatabase _db;
        private MongoCollection _users;

        public IdentityContext(MongoDatabase database)
        {
            _db = database;
            _users = _db.GetCollection<IdentityUser>("users");
            EnsureUniqueIndexOnUserName(_users);
        }

        public MongoCollection Users
        {
            get { return _users; }
        }

        private void EnsureUniqueIndexOnUserName(MongoCollection users)
        {
            var userName = new IndexKeysBuilder().Ascending("UserName");
            var unique = new IndexOptionsBuilder().SetUnique(true);
            users.CreateIndex(userName, unique);
        }
    }
}