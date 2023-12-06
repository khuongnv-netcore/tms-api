using Auth0.ManagementApi;
using CORE_API.CORE.Models.Configuration;
using CORE_API.CORE.Models.Entities;
using CORE_API.CORE.Services.Abstract;
using Microsoft.Extensions.Options;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;

namespace CORE_API.CORE.Services
{
    public class Auth0ManagementService : IAuth0ManagementService
    {
        private static ManagementApiClient _auth0Client;
        private static string Hostname;
        private static string Audience;
        private static string ClientSecret;
        private static string ClientId;
        private static Auth0Token Token;

        private readonly ILogService _logService;


        private readonly CoreConfigurationOptions _coreConfigurationOptions;

        public Auth0ManagementService(IOptions<CoreConfigurationOptions> configurationOptions, ILogService logService)
        {
            _coreConfigurationOptions = configurationOptions.Value;
            _logService = logService;
            configure();
        }

        public void configure()
        {
            Hostname = _coreConfigurationOptions.AuthenticationOptions.ManagementHostname;
            Audience = _coreConfigurationOptions.AuthenticationOptions.ManagementAudience;
            ClientSecret = _coreConfigurationOptions.AuthenticationOptions.ManagementClientSecret;
            ClientId = _coreConfigurationOptions.AuthenticationOptions.ApiClientId;

            if (_auth0Client != null)
            {
                _auth0Client.Dispose();
            }

            getNewTokenIfNecessary();

            _auth0Client = new ManagementApiClient(Token.access_token, Hostname);
        }

        public async Task<bool> ChangeUserEmailAddress(User user, string EmailAddress)
        {
            getNewTokenIfNecessary();

            try
            {
                var result = await _auth0Client.Users.UpdateAsync(user.AuthId, new Auth0.ManagementApi.Models.UserUpdateRequest
                {
                    Email = EmailAddress,
                    VerifyEmail = true
                });
            }catch (Exception ex)
            {
                //TODO log stuff
                _logService.LogException(ex);
                return false;
            }

            return true;
        }

        public async Task<bool> SetUserName(User user)
        {
            getNewTokenIfNecessary();

            try
            {
                var result = await _auth0Client.Users.UpdateAsync(user.AuthId, new Auth0.ManagementApi.Models.UserUpdateRequest
                {
                    FirstName = user.FirstName,
                    LastName = user.LastName
                });
            }
            catch (Exception ex)
            {
                //TODO log stuff
                _logService.LogException(ex);
                return false;
            }

            return true;
        }

        private void getNewTokenIfNecessary()
        {
            if (Token != null)
            {
                //Get Expiration DateTime and subtract 5 minutes so we get a new token before expiration.
                var expiration = Token.Created.AddSeconds(Token.expires_in).AddMinutes(-5);

                if (expiration > DateTime.UtcNow)
                {
                    //Token is stil valid.
                    return;
                }
            }

            var auth0TokenUrl = _coreConfigurationOptions.AuthenticationOptions.TokenUrl;
            var client = new RestClient(auth0TokenUrl);
            var request = new RestRequest(Method.POST);
            request.AddHeader("content-type", "application/x-www-form-urlencoded");
            request.AddHeader("Accept", "application/json");
            request.AddParameter("application/x-www-form-urlencoded", "grant_type=client_credentials&client_id=" + ClientId + "&client_secret=" + ClientSecret + "&audience=" + Audience, ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);

            var readOnlySpan = new ReadOnlySpan<byte>(response.RawBytes);

            var token = JsonSerializer.Deserialize<Auth0Token>(readOnlySpan);
            token.Created = DateTime.UtcNow;

            Token = token;

        }
    }

    public class Auth0Token
    {
        public string access_token { get; set; }

        public int expires_in { get; set; }
        
        public string token_type { get; set; }

        public DateTime Created { get; set; }

    }
}
