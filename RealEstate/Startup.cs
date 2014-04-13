using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Antlr.Runtime.Misc;
using Microsoft.Owin;
using Owin;
[assembly: OwinStartup(typeof(RealEstate.Startup))]
namespace RealEstate
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // Any connection or hub wire up and configuration should go here
            app.MapSignalR();

            //app.Use<TestOwinComponent>();
            ConfigureAuth(app);
        }
    }
}