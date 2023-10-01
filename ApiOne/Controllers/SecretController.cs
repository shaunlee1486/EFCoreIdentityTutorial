using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApiOne.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class SecretController : ControllerBase
	{
		[HttpGet("/secret")]
		[Authorize]
		public string Index()
		{
			return "secret message from ApiOne";
		}
	}
}