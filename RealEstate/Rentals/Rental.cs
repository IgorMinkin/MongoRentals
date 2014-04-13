using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Ajax.Utilities;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace RealEstate.Rentals
{
    public class Rental
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Description { get; set; }
        public int NumberOfRooms { get; set; }
        public List<string> Address = new List<string>();
        public PosterIdentity Poster { get; set; }
        public DateTime DatePosted { get; set; }

        public Rental()
        {
          
        }

        public static Rental CreatePosting(PostRental postRental, PosterIdentity identity)
        {
            return new Rental
            {
                Description = postRental.Description,
                NumberOfRooms = postRental.NumberOfRooms,
                Price = postRental.Price,
                Address = (postRental.Address ?? string.Empty).Split('\n').ToList(),
                DatePosted = DateTime.Now,
                Poster = identity
            };

        }

        [BsonRepresentation(BsonType.Double)]
        public decimal Price { get; set; }

        public List<PriceAdjustment> Adjustments = new List<PriceAdjustment>();

        public void AdjustPrice(AdjustPrice adjustPrice)
        {
            var adjustment = new PriceAdjustment(adjustPrice, Price);

            Adjustments.Add(adjustment);

            Price = adjustPrice.NewPrice;
        }

        public string ImageId { get; set; }



        public bool HasImage()
        {
            return !ImageId.IsNullOrWhiteSpace();
        }
    }
}