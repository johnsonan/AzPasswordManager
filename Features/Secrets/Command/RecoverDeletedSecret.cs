using MediatR;
using System.Text.Json;

namespace AzPasswordManager.Features.Secrets
{
    public class RecoverDeletedSecret
    {
        public class Command : IRequest<SecretBundle>
        {
            public string SecretName { get; set; }
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
                var response = await _HttpClient.PostAsync($"deletedsecrets/{request.SecretName}/recover", null);
                return JsonSerializer.Deserialize<SecretBundle>(await response?.EnsureSuccessStatusCode()?.Content?.ReadAsStringAsync());

            }
        }
    }
}
