using Bookify.Application.Abstractions.Authentication;
using Bookify.Domain.Users;
using Bookify.Infrastructure.Authentication.Models;
using System.Net.Http.Json;

namespace Bookify.Infrastructure.Authentication;

internal sealed class AuthenticationService : IAuthenticationService
{
    private const string PasswordCredentialType = "password";

    private readonly HttpClient _httpClient;

    public AuthenticationService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    

    public async Task<string> RegisterAsync(
        User user,
        string password,
        CancellationToken cancellationToken = default)
    {

        //var authorizationRequestParameters = new KeyValuePair<string, string>[]
        //{
        //    new("client_id", "bookify-admin-client"),
        //    new("client_secret", "UZDmbNxWmV4TlpaCRcju6pMRsyuV3er1"),
        //    new("scope", "openid email"),
        //    new("grant_type", "client_credentials")
        //};

        //var httpClient = new HttpClient();

        //var authorizationRequestContent = new FormUrlEncodedContent(authorizationRequestParameters);

        //var uri = new Uri("http://bookify-idp:8080/auth/realms/bookify/protocol/openid-connect/token");

        //var authorizationRequest = new HttpRequestMessage(
        //    HttpMethod.Post, uri
        //    ) {
        //    Content = authorizationRequestContent,
        //};

        /*  try {
              var response = await httpClient.PostAsync(uri, authorizationRequestContent, cancellationToken);
              var res = response.Content.ReadAsStringAsync();
          }
          catch (Exception ex) {

              throw;
          }*/

        //return "asdasdasd";

        var userRepresentationModel = UserRepresentationModel.FromUser(user);

        userRepresentationModel.Credentials = new CredentialRepresentationModel[]
        {
            new()
            {
                Value = password,
                Temporary = false,
                Type = PasswordCredentialType
            }
        };

        var response = await _httpClient.PostAsJsonAsync(
            "users",
            userRepresentationModel,
            cancellationToken);



        return ExtractIdentityIdFromLocationHeader(response);
    }

    private static string ExtractIdentityIdFromLocationHeader(
        HttpResponseMessage httpResponseMessage)
    {
        const string usersSegmentName = "users/";

        var locationHeader = httpResponseMessage.Headers.Location?.PathAndQuery;

        if (locationHeader is null)
        {
            throw new InvalidOperationException("Location header can't be null");
        }

        var userSegmentValueIndex = locationHeader.IndexOf(
            usersSegmentName,
            StringComparison.InvariantCultureIgnoreCase);

        var userIdentityId = locationHeader.Substring(
            userSegmentValueIndex + usersSegmentName.Length);

        return userIdentityId;
    }
}