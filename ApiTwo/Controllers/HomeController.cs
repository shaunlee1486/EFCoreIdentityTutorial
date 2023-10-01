using IdentityModel.Client;
using Microsoft.AspNetCore.Mvc;

namespace ApiTwo.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class HomeController : ControllerBase
	{
		private readonly IHttpClientFactory _httpClientFactory;

		public HomeController(IHttpClientFactory httpClientFactory)
		{
			_httpClientFactory = httpClientFactory;
		}

		[HttpGet]
		public async Task<IActionResult> Index()
		{
			//retrieve access token
			var serverClient = _httpClientFactory.CreateClient();

			var discoveryDocument = await serverClient.GetDiscoveryDocumentAsync("https://localhost:7094/");

			var tokenResponse = await serverClient.RequestClientCredentialsTokenAsync(
				new ClientCredentialsTokenRequest
				{
					Address = discoveryDocument.TokenEndpoint,
					ClientId = "client_id",
					ClientSecret = "client_secret",
					Scope = "ApiOne"
				});

			//retrieve secret data
			var apiClient = _httpClientFactory.CreateClient();

			apiClient.SetBearerToken(tokenResponse.AccessToken);

			var response = await apiClient.GetAsync("https://localhost:7077/secret"); // api one

			var content = await response.Content.ReadAsStringAsync();

			return Ok(new
			{
				access_token = tokenResponse.AccessToken,
				message = content
			});
		}
	}
}