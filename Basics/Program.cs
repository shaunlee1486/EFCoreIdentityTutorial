using Basics.AuthorizationRequirements;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

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

			builder.Services.AddAuthorization(config =>
			{
				//var defaultAuthBuilder = new AuthorizationPolicyBuilder();
				//config.DefaultPolicy = defaultAuthBuilder
				//							.RequireAuthenticatedUser()
				//							.RequireClaim(ClaimTypes.DateOfBirth)
				//							.Build();

				//config.AddPolicy("Claim.Bob", policyBuilder =>
				//{
				//	policyBuilder.RequireClaim(ClaimTypes.DateOfBirth);
				//});
				config.AddPolicy("Admin", policyBuilder => policyBuilder.RequireClaim(ClaimTypes.Role, "Admin"));
				
				config.AddPolicy("Claim.Bob", policyBuilder =>
				{
					policyBuilder.AddRequirements(new CustomRequireClaim(ClaimTypes.DateOfBirth));
				});
			});

			builder.Services.AddScoped<IAuthorizationHandler, CustomRequireClaimHandler>();

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