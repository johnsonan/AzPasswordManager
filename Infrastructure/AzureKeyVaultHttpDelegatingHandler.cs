using Microsoft.Identity.Web;
using System.Net.Http.Headers;
using System.Diagnostics;

namespace AzPasswordManager.Infrastructure
{
    public class AzureKeyVaultHttpDelegatingHandler : DelegatingHandler
    {
        private readonly ITokenAcquisition _TokenAcquisition;
        private readonly IConfiguration _Configuration;
        private readonly MicrosoftIdentityConsentAndConditionalAccessHandler _ConsentHandler;

        public AzureKeyVaultHttpDelegatingHandler(ITokenAcquisition tokenAcquisition, IConfiguration configuration, MicrosoftIdentityConsentAndConditionalAccessHandler consentHandler)
        {
            _TokenAcquisition = tokenAcquisition;
            _Configuration = configuration;
            _ConsentHandler = consentHandler;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            try
            {
                var ApiVersion = _Configuration["KeyVaultConfig:ApiVersion"];
                var Scope = _Configuration["KeyVaultConfig:BaseUri"] + _Configuration["KeyVaultConfig:Scope"];

                // Get access token
                // Microsoft Identity Web specific exception class for use in Blazor or Razor pages to process the user challenge.
                // Handles the MsalUiRequiredException.
                var token = string.Empty;
                try
                {
                    token = await _TokenAcquisition.GetAccessTokenForAppAsync(Scope);
                }
                catch (MicrosoftIdentityWebChallengeUserException ex)
                {
                    _ConsentHandler.HandleException(ex);
                }
                catch (Exception)
                {
                    throw new Exception("Error getting access token.");
                }

                Debug.WriteLine("Token: " + token);

                request.RequestUri = new Uri(request.RequestUri + $"{ApiVersion}");
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

                Debug.WriteLine("RequestUri: " + request.RequestUri);
                var response = await base.SendAsync(request, cancellationToken);

                Debug.WriteLine(response.ToString());

                return response;
            }
            catch (Exception e)
            {
                throw;
            }
        }
    }
}
