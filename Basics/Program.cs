namespace Basics
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			builder.Services.AddAuthentication("CookieAuth")
				.AddCookie("CookieAuth", config =>
				{
					config.Cookie.Name = "Grandmas.Cookie";
					config.LoginPath = "/Home/Authenticate";
				});


			// Add services to the container.
			builder.Services.AddControllersWithViews();

			var app = builder.Build();

			app.UseRouting();

			//who are you?
			app.UseAuthentication();

			// are you allowed?
			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapDefaultControllerRoute();
			});

			app.Run();
		}
	}
}