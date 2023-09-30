using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Server
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);
			builder.Services.AddAuthentication("OAuth")
				.AddJwtBearer("OAuth", config =>
				{
					var scretBytes = Encoding.UTF8.GetBytes(Constants.Secret);
					var key = new SymmetricSecurityKey(scretBytes);

					config.Events = new JwtBearerEvents()
					{
						OnMessageReceived = context =>
						{
							if (context.Request.Query.ContainsKey("access_token"))
							{
								context.Token = context.Request.Query["access_token"];
							}
							return Task.CompletedTask;
						}
					};

					config.TokenValidationParameters = new TokenValidationParameters()
					{
						ValidIssuer = Constants.Issuer,
						ValidAudience = Constants.Audiance,
						IssuerSigningKey = key
					};
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