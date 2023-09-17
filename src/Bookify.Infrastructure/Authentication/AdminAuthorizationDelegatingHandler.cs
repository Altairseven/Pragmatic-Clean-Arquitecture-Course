using System.Net.Http.Headers;
using System.Net.Http.Json;
using Bookify.Infrastructure.Authentication.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Bookify.Infrastructure.Authentication;

public sealed class AdminAuthorizationDelegatingHandler : DelegatingHandler
{
    private readonly KeycloakOptions _keycloakOptions;

    private readonly ILogger<AdminAuthorizationDelegatingHandler> _logger;

    public AdminAuthorizationDelegatingHandler(IOptions<KeycloakOptions> keycloakOptions, 
        ILogger<AdminAuthorizationDelegatingHandler> logger) {
        _keycloakOptions = keycloakOptions.Value;
        _logger = logger;
    }

    protected override async Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request,
        CancellationToken cancellationToken)
    {
        var authorizationToken = await GetAuthorizationToken(cancellationToken);

        request.Headers.Authorization = new AuthenticationHeaderValue(
            JwtBearerDefaults.AuthenticationScheme,
            authorizationToken.AccessToken);

        var httpResponseMessage = await base.SendAsync(request, cancellationToken);

        httpResponseMessage.EnsureSuccessStatusCode();

        return httpResponseMessage;
    }

    private async Task<AuthorizationToken> GetAuthorizationToken(CancellationToken cancellationToken)
    {
        var authorizationRequestParameters = new KeyValuePair<string, string>[]
        {
            new("client_id", _keycloakOptions.AdminClientId),
            new("client_secret", _keycloakOptions.AdminClientSecret),
            new("scope", "openid email"),
            new("grant_type", "client_credentials")
        };

        var authorizationRequestContent = new FormUrlEncodedContent(authorizationRequestParameters);

        var uri = new Uri(_keycloakOptions.TokenUrl);

        _logger.LogDebug("KeyCloak HttpClient Request at: " + uri.ToString());

        var authorizationRequest = new HttpRequestMessage(
            HttpMethod.Post, uri
            )
        {
            Content = authorizationRequestContent, 
        };



        _logger.LogDebug("KeyCloak HttpClient Request at: " + authorizationRequestParameters.ToString());

        var authorizationResponse = await base.SendAsync(authorizationRequest, cancellationToken);

        _logger.LogDebug("Response: " + Environment.NewLine + authorizationResponse.Content.ReadAsStringAsync());


        authorizationResponse.EnsureSuccessStatusCode();

        return await authorizationResponse.Content.ReadFromJsonAsync<AuthorizationToken>() ??
               throw new ApplicationException();
    }
}