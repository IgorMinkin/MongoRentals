using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.Facebook;
using Owin;
using RealEstate.Properties;

namespace RealEstate
{
	public partial class Startup
	{
	    public void ConfigureAuth(IAppBuilder app)
	    {
	        app.UseCookieAuthentication(new CookieAuthenticationOptions
	        {
	            AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
	            LoginPath = new PathString("/Account/Login")
	        });

	        app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);

	        app.UseGoogleAuthentication();

	        app.UseFacebookAuthentication(new FacebookAuthenticationOptions
	        {
	            AppId = Settings.Default.FacebookAppID,
	            AppSecret = Settings.Default.FacebookAppSecret
	        });
	    }
	}
}