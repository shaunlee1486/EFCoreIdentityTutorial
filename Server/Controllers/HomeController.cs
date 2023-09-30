using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualBasic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Server.Controllers
{
	public class HomeController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}

		[Authorize]
		public IActionResult Secret()
		{
			return View();
		}

		public IActionResult Authenticate()
		{
			var claims = new[]
			{
				new Claim(JwtRegisteredClaimNames.Sub, "some_id"),
				new Claim("granny", "cookie")
			};

			var scretBytes = Encoding.UTF8.GetBytes(Constants.Secret);
			var key = new SymmetricSecurityKey(scretBytes);
			var algorithm = SecurityAlgorithms.HmacSha256;

			var signiingCredentials = new SigningCredentials(key, algorithm);

			var token = new JwtSecurityToken(
				Constants.Issuer,
				Constants.Audiance,
				claims,
				notBefore: DateTime.Now,
				expires: DateTime.Now.AddHours(1),
				signiingCredentials);

			var tokenJson = new JwtSecurityTokenHandler().WriteToken(token);

			return Ok(new { access_token = tokenJson });
		}

		public IActionResult Decode(string token)
		{
			var bytes = Convert.FromBase64String(token);

			return Ok(Encoding.UTF8.GetString(bytes));
		}
	}
}