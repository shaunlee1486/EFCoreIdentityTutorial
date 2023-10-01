namespace Client
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			builder.Services.AddAuthentication(config =>
			{
				// we check the cookie to confirm that we are authenticated
				config.DefaultAuthenticateScheme = "ClientCookie";

				// when we sign in we will deal out a cookie
				config.DefaultSignInScheme = "ClientCookie";

				// use this to check if we are allowed to do 
				config.DefaultChallengeScheme = "OurServer";
			})
				.AddCookie("ClientCookie")
				.AddOAuth("OurServer", config =>
				{
					config.ClientId = "client_id";
					config.ClientSecret = "client_secret";
					config.CallbackPath = "/oauth/callback";
					config.AuthorizationEndpoint = "https://localhost:7066/oauth/authorize";
					config.TokenEndpoint = "https://localhost:7066/oauth/token";
				});

			// Add services to the container.
			builder.Services.AddControllersWithViews();

			var app = builder.Build();

			app.UseRouting();

			app.UseAuthentication();

			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapDefaultControllerRoute();
			});

			app.Run();
		}
	}
}