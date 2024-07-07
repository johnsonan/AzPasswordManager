using MediatR;

namespace AzPasswordManager.Features.Secrets
{
    public class GetDeletedSecrets
    {

        public class Query : IRequest<SecretListResult>
        {

        }

        public class QueryHandler : IRequestHandler<Query, SecretListResult>
        {
            private readonly HttpClient _HttpClient;

            public QueryHandler(IHttpClientFactory httpClientFactory)
            {
                _HttpClient = httpClientFactory.CreateClient("keyVaultClient");

            }
            public async Task<SecretListResult> Handle(Query request, CancellationToken cancellationToken)
            {
                var response = await _HttpClient.GetFromJsonAsync<SecretListResult>("deletedsecrets");
                return response;
            }
        }
    }
}
