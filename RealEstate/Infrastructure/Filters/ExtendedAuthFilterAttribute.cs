using System.Diagnostics;
using System.Web.Mvc;
using RealEstate.Messaging;

namespace RealEstate.Infrastructure.Filters
{
    public class ExtendedAuthFilterAttribute : AuthorizeAttribute
    {
        public MessageBus MessageBus { get; set; }

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            base.OnAuthorization(filterContext);

            MessageBus.Publish(new TestMessage(this, "hello from authorization filter"));

            if(!filterContext.HttpContext.User.Identity.IsAuthenticated) return;
        }
    }
}