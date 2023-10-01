using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Json.Serialization;

namespace Server.Controllers
{
	public class OAuthController : Controller
	{
		//https://localhost:7066/oauth/authorize?client_id=client_id&scope=&response_type=code&redirect_uri=https%3A%2F%2Flocalhost%3A7201%2Foauth%2Fcallback&state=CfDJ8BTT-9n12UZCh3ovU0KPEyamQ9p5ayjCOuKo4MqSx4LqD4fcKuTy99exjs6qyEVjvGeZSb-Ml3bLyCGXN5cWmoVGfv7AzLCaKnFDVY3NJhPrXwLsVxiSTihg4S0QGQVJgy4-qiovMUOc3MYTY9hJqnPDSBnSuEhTeYXF-4cQNDrb81a8Wuf4kXU3dN5IFv9iJo9zkRGYMWVNQWDqn5Tzj1RN1H9VFhx6EMcB2bYT8Sfw
		[HttpGet]
		public IActionResult Authorize(
			string client_id,// client id
			string scope, // what info I want = email, grandma, tel
			string response_type, // authorization flow type
			string redirect_uri, //
			string state // random string generated to confirm that we are going to back to the same client
			)
		{
			// ?a=foo&b=bar
			var query = new QueryBuilder();
			query.Add("redirectUri", redirect_uri);
			query.Add("state", state);
			return View(model: query.ToString());
		} 

		[HttpPost]
		public IActionResult Authorize(
			string username,
			string redirectUri,
			string state)
		{
			var code = Guid.NewGuid().ToString();
			var query = new QueryBuilder();
			query.Add("code", code);
			query.Add("state", state);
			return Redirect($"{redirectUri}{query.ToString()}");
		}

		public async Task<IActionResult> Token(
			string grant_type, // flow of access_token request
			string code, //confirmation of the authentication progress
			string redirect_uri,
			string client_id)
		{
			//some machanism for validating the code
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

			var access_token = new JwtSecurityTokenHandler().WriteToken(token);

			var responseObject = new
			{
				access_token,
				token_type = "Bearer",
				raw_claim = "oauthTutorial"
			};

			var responseJson = JsonConvert.SerializeObject(responseObject);
			var responseBytes = Encoding.UTF8.GetBytes(responseJson);

			await Response.Body.WriteAsync(responseBytes, 0, responseBytes.Length);
			return Redirect(redirect_uri);
		}
	}
}
