using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace RealEstate.Hubs
{
    public class RentalHub : Hub
    {
        public void RentalAdded()
        {
            Clients.All.rentalAdded();
        }
    }
}