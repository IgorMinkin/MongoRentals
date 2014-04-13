using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace RealEstate.Security
{
    public class IdentityUser : IUser<string>
    {
        public IdentityUser()
        {
            Id = ObjectId.GenerateNewId().ToString();
            Logins = new List<UserLoginInfo>();
        }

        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; private set; }

        public string UserName { get; set; }

        public string PasswordHash { get; set; }

        public List<UserLoginInfo> Logins { get; set; }
    }
}