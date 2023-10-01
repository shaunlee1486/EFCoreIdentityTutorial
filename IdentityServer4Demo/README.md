https://stackoverflow.com/questions/62543026/identity-server-invalid-scope-in-example-from-the-documentation-with-version-4-0
https://localhost:7094/.well-known/openid-configuration

result
```
{
    "issuer": "https://localhost:7094",
    "jwks_uri": "https://localhost:7094/.well-known/openid-configuration/jwks",
    "authorization_endpoint": "https://localhost:7094/connect/authorize",
    "token_endpoint": "https://localhost:7094/connect/token",
    "userinfo_endpoint": "https://localhost:7094/connect/userinfo",
    "end_session_endpoint": "https://localhost:7094/connect/endsession",
    "check_session_iframe": "https://localhost:7094/connect/checksession",
    "revocation_endpoint": "https://localhost:7094/connect/revocation",
    "introspection_endpoint": "https://localhost:7094/connect/introspect",
    "device_authorization_endpoint": "https://localhost:7094/connect/deviceauthorization",
    "frontchannel_logout_supported": true,
    "frontchannel_logout_session_supported": true,
    "backchannel_logout_supported": true,
    "backchannel_logout_session_supported": true,
    "scopes_supported": [
        "offline_access"
    ],
    "claims_supported": [],
    "grant_types_supported": [
        "authorization_code",
        "client_credentials",
        "refresh_token",
        "implicit",
        "urn:ietf:params:oauth:grant-type:device_code"
    ],
    "response_types_supported": [
        "code",
        "token",
        "id_token",
        "id_token token",
        "code id_token",
        "code token",
        "code id_token token"
    ],
    "response_modes_supported": [
        "form_post",
        "query",
        "fragment"
    ],
    "token_endpoint_auth_methods_supported": [
        "client_secret_basic",
        "client_secret_post"
    ],
    "id_token_signing_alg_values_supported": [
        "RS256"
    ],
    "subject_types_supported": [
        "public"
    ],
    "code_challenge_methods_supported": [
        "plain",
        "S256"
    ],
    "request_parameter_supported": true
}
```
