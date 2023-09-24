﻿using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Basics.Controllers
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
			var grandmaClaims = new List<Claim>()
			{
				new Claim (ClaimTypes.Name, "Bob"),
				new Claim (ClaimTypes.Email, "Bob@gmail.com"),
				new Claim ("Grandma.Says", "Very nice boy."),
			};

			var licenseClaims = new List<Claim>()
			{
				new Claim (ClaimTypes.Name, "Bob K Foo"),
				new Claim ("DrivingLicense", "A+"),
			};

			var grandmaIdentity = new ClaimsIdentity(grandmaClaims, "Grandma Identity");
			var licenseIdentity = new ClaimsIdentity(licenseClaims, "Government");

			var userPrincipal = new ClaimsPrincipal(new[] { grandmaIdentity, licenseIdentity });

			HttpContext.SignInAsync(userPrincipal);

			return RedirectToAction("index");
		}
	}
}