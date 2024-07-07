using MediatR;
using Microsoft.JSInterop;

namespace AzPasswordManager.Features.Clipboard
{
    public class GetClipboard
    {
        public class Query : IRequest<ValueTask<string>>
        {

        }

        public class QueryHandler : IRequestHandler<Query, ValueTask<string>>
        {
            private readonly IJSRuntime _jsRuntime;

            public QueryHandler(IJSRuntime jsRuntime)
            {
                _jsRuntime = jsRuntime;
            }
            public async Task<ValueTask<string>> Handle(Query request, CancellationToken cancellationToken)
            {
                return _jsRuntime.InvokeAsync<string>("navigator.clipboard.readText");
            }
        }
    }
}
