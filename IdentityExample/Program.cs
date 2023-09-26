using IdentityExample.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NETCore.MailKit.Extensions;
using NETCore.MailKit.Infrastructure.Internal;

namespace IdentityExample
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			builder.Services.AddDbContext<AppDbContext>(config =>
			{
				config.UseInMemoryDatabase("Memory");
			});

			// AddIdentity  register the services
			builder.Services.AddIdentity<IdentityUser, IdentityRole>(config =>
			{
				config.Password.RequiredLength = 4;
				config.Password.RequireDigit = false;
				config.Password.RequireNonAlphanumeric = false;
				config.Password.RequireUppercase = false;
				config.SignIn.RequireConfirmedEmail = true;
			})
				.AddEntityFrameworkStores<AppDbContext>()
				.AddDefaultTokenProviders();

			builder.Services.ConfigureApplicationCookie(config =>
			{
				config.Cookie.Name = "Identity.Cookie";
				config.LoginPath = "/Home/Login";
			});

			var mailKitOptions = builder.Configuration.GetSection("EmailSetting").Get<MailKitOptions>();

			builder.Services.AddMailKit(config => config.UseMailKit(mailKitOptions));

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