
- new Claim(JwtRegisteredClaimNames.Sub, "some_id"),
https://datatracker.ietf.org/doc/html/rfc7519#section-4.1.2

```
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
```
https://localhost:7066/home/secret?access_token=eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJzb21lX2lkIiwiZ3Jhbm55IjoiY29va2llIiwibmJmIjoxNjk2MDc1MTY2LCJleHAiOjE2OTYwNzg3NjYsImlzcyI6IlNoYXVuIiwiYXVkIjoiU2hhdW4ifQ.frDjFwIDEzroGPtU8ZdL6NhJMAjxafbvUUaN_Azp40I
