using MediatR;
using System.Diagnostics;
using System.Text;
using System.Text.Json;

namespace AzPasswordManager.Features.Secrets
{
    public class SetSecret
    {
        public class Command : IRequest<SecretBundle>
        {
            public string SecretName { get; set; }
            public object RequestBody { get; set; }
        }

        public class CommandHandler : IRequestHandler<Command, SecretBundle>
        {
            private readonly HttpClient _HttpClient;

            public CommandHandler(IHttpClientFactory httpClientFactory)
            {
                _HttpClient = httpClientFactory.CreateClient("keyVaultClient");

            }
            public async Task<SecretBundle> Handle(Command request, CancellationToken cancellationToken)
            {
                var content = JsonSerializer.Serialize(request.RequestBody);
                var httpSecretContent = new StringContent(content, Encoding.UTF8, "application/json");

                var postResponse = await _HttpClient.PutAsync($"secrets/{request.SecretName}", httpSecretContent);
                postResponse.EnsureSuccessStatusCode(); // Throws exception when not success status code

                var jsonResponseContent = await postResponse.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<SecretBundle>(jsonResponseContent);
            }
        }
    }
}
