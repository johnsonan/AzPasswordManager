using MediatR;

namespace AzPasswordManager.Features.Secrets
{
    public class DeleteSecret
    {
        public class Command : IRequest<DeletedSecretBundle>
        {
            public string SecretName { get; set; }
        }

        public class CommandHandler : IRequestHandler<Command, DeletedSecretBundle>
        {
            private readonly HttpClient _HttpClient;

            public CommandHandler(IHttpClientFactory httpClientFactory)
            {
                _HttpClient = httpClientFactory.CreateClient("keyVaultClient");

            }
            public async Task<DeletedSecretBundle> Handle(Command request, CancellationToken cancellationToken)
            {
                var response = await _HttpClient.DeleteFromJsonAsync<DeletedSecretBundle>($"secrets/{request.SecretName}");
                return response;
            }
        }
    }
}
