
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace ApiOne
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			builder.Services.AddAuthentication("Bearer")
				.AddJwtBearer("Bearer", config =>
				{
					config.Authority = "https://localhost:7094";

					config.Audience = "ApiOne";
				});

			builder.Services.AddControllers();

			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen();

			var app = builder.Build();

			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
			}

			app.UseRouting();

			app.UseAuthorization();

			app.UseAuthorization();

			app.MapControllers();

			app.Run();
		}
	}
}