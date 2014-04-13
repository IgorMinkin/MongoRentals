﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Ajax.Utilities;
using Microsoft.AspNet.SignalR;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using MongoDB.Driver.GridFS;
using MongoDB.Driver.Linq;
using RealEstate.Database;
using RealEstate.Hubs;
using RealEstate.Rentals;

namespace RealEstate.Controllers
{
    public class RentalController : Controller
    {
        private readonly RealEstateContext _context;
        private readonly static Lazy<IHubContext> RentalHub = new Lazy<IHubContext>(() => GlobalHost.ConnectionManager.GetHubContext<RentalHub>());

        public RentalController(RealEstateContext context)
        {
            _context = context;
        }
        
        //
        // GET: /Rentals/
        public ActionResult Index(RentalsFilter filters)
        {
            var rentals = FilterRentals(filters);

            var model = new RentalsList
            {
                Rentals = rentals,
                Filters = filters
            };

            return View(model);
        }

        public JsonResult List()
        {
            var mostExpensive =
                _context.Rentals.AsQueryable()
                    .OrderByDescending(r => r.Price)
                    .Select(r => new {r.Price, r.Description, r.NumberOfRooms});

            return Json(mostExpensive, JsonRequestBehavior.AllowGet);
        }

        private IEnumerable<Rental> FilterRentals(RentalsFilter filters)
        {
            IQueryable<Rental> rentals = _context.Rentals.AsQueryable()
                .OrderBy(r => r.Price);

            if (filters.MinimumRooms.HasValue)
            {
                rentals = rentals
                    .Where(r => r.NumberOfRooms >= filters.MinimumRooms);
            }

            if (filters.PriceLimit.HasValue)
            {
                var query = Query<Rental>.LTE(r => r.Price, filters.PriceLimit);

                rentals = rentals
                    .Where(r => query.Inject());
            }

            return rentals;
        }

        public ActionResult Post()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Post(PostRental postRental)
        {
            var rental = new Rental(postRental);

            _context.Rentals.Insert(rental);

            RentalHub.Value.Clients.All.rentalAdded();

            return RedirectToAction("Index");
        }

        public ActionResult AdjustPrice(string id)
        {
            var rental = GetRental(id);
            return View(rental);
        }

        public JsonResult Json(string id)
        {
            var rental = GetRental(id);
            return Json(rental, JsonRequestBehavior.AllowGet);
        }

        private Rental GetRental(string id)
        {
            var rental = _context.Rentals.FindOneById(new ObjectId(id));
            return rental;
        }

        [HttpPost]
        public ActionResult AdjustPrice(string id, AdjustPrice adjustPrice)
        {
            var rental = GetRental(id);

            rental.AdjustPrice(adjustPrice);

            _context.Rentals.Save(rental);

            return RedirectToAction("Index");
        }

        public ActionResult Delete(string id)
        {
            _context.Rentals.Remove(Query.EQ("_id", new ObjectId(id)));

            RentalHub.Value.Clients.All.rentalAdded();

            return RedirectToAction("Index");
        }

        public string PriceDistribution()
        {
            return new QuerPriceDistribution()
                .Run(_context.Rentals)
                .ToJson();
        }

        public ActionResult AttachImage(string id)
        {
            var rental = GetRental(id);
            return View(rental);
        }

        [HttpPost]
        public ActionResult AttachImage(string id, HttpPostedFileBase file)
        {
            var rental = GetRental(id);

            if (rental.HasImage())
            {
                DeleteImage(rental);
            }

            UploadImage(file, rental);

            return RedirectToAction("Index");
        }

        private void DeleteImage(Rental rental)
        {
            _context.Db.GridFS.DeleteById(new ObjectId(rental.ImageId));

            rental.ImageId = null;

            _context.Rentals.Save(rental);
        }

        private void UploadImage(HttpPostedFileBase file, Rental rental)
        {
            var imageId = ObjectId.GenerateNewId();

            rental.ImageId = imageId.ToString();

            _context.Rentals.Save(rental);

            var options = new MongoGridFSCreateOptions
            {
                Id = imageId,
                ContentType = file.ContentType
            };

            _context.Db.GridFS.Upload(file.InputStream, file.FileName, options);
        }

        public ActionResult GetImage(string id)
        {
            var image = _context.Db.GridFS
                .FindOneById(new ObjectId(id));

            if (image == null)
            {
                return HttpNotFound();
            }

            return File(image.OpenRead(), image.ContentType);
        }
    }
}