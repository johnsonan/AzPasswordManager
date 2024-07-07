using MediatR;

namespace AzPasswordManager.Features.Secrets
{
    public class GetSecret
    {

        public class Query : IRequest<SecretBundle>
        {
            public string SecretName { get; set; }
        }

        public class QueryHandler : IRequestHandler<Query, SecretBundle>
        {
            private readonly HttpClient _HttpClient;

            public QueryHandler(IHttpClientFactory httpClientFactory)
            {
                _HttpClient = httpClientFactory.CreateClient("keyVaultClient");

            }
            public async Task<SecretBundle> Handle(Query request, CancellationToken cancellationToken)
            {
                var response = await _HttpClient.GetFromJsonAsync<SecretBundle>($"secrets/{request.SecretName}");
                return response;
            }
        }
    }
}
