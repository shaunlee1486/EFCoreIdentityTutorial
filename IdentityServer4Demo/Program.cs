namespace IdentityServer4Demo
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			builder.Services.AddIdentityServer()
				.AddInMemoryApiScopes(Configuration.GetApiScopes())
				.AddInMemoryApiResources(Configuration.GetApiResource())
				.AddInMemoryClients(Configuration.GetClients())
				.AddDeveloperSigningCredential();

			builder.Services.AddControllersWithViews();

			var app = builder.Build();

			app.UseRouting();

			app.UseIdentityServer();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapDefaultControllerRoute();
			});

			app.Run();
		}
	}
}