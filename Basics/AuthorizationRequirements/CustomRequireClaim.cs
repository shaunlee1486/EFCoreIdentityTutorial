﻿using Microsoft.AspNetCore.Authorization;

namespace Basics.AuthorizationRequirements
{
	public class CustomRequireClaim : IAuthorizationRequirement
	{
        public string ClaimType { get; }

        public CustomRequireClaim(string claimType)
        {
            ClaimType = claimType;
        }
    }

	public class CustomRequireClaimHandler : AuthorizationHandler<CustomRequireClaim>
	{
		protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, CustomRequireClaim requirement)
		{
			var hasClaim = context.User.Claims.Any(c => c.Type == requirement.ClaimType);
			
			if (hasClaim) context.Succeed(requirement);

			return Task.CompletedTask;
		}
	}

	public static class AuthorizationPolicyBuilderExtensions
	{
		public static AuthorizationPolicyBuilder RequireCustomClaim(
			this AuthorizationPolicyBuilder builder,
			string claimType)
		{
			builder.AddRequirements(new CustomRequireClaim(claimType));

			return builder;
		}
	}
}
