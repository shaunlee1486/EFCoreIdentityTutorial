using IdentityModel;
using IdentityServer4.Models;

namespace IdentityServer4Demo
{
	public static class Configuration
	{
		public static IEnumerable<ApiScope> GetApiScopes() =>
			new List<ApiScope>
			{
				new ApiScope("ApiOne"),
			};

		public static IEnumerable<ApiResource> GetApiResource() =>
			new List<ApiResource>
			{
				new ApiResource("ApiOne"),
			};


		public static IEnumerable<Client> GetClients() =>
			new List<Client>
			{
				new Client {
					ClientId = "client_id",
					ClientSecrets = { new Secret("client_secret".ToSha256())},
					AllowedGrantTypes = GrantTypes.ClientCredentials,
					AllowedScopes = { "ApiOne" }
				}
			};
	}
}
