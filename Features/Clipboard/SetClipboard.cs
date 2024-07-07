using MediatR;
using Microsoft.JSInterop;

namespace AzPasswordManager.Features.Clipboard
{
    public class SetClipboard
    {
        public class Command : IRequest<ValueTask>
        {
            public string? Text { get; set; }
        }

        public class CommandHandler : IRequestHandler<Command, ValueTask>
        {
            private readonly IJSRuntime _jsRuntime;

            public CommandHandler(IJSRuntime jsRuntime)
            {
                _jsRuntime = jsRuntime;
            }
            public async Task<ValueTask> Handle(Command request, CancellationToken cancellationToken)
            {
                return _jsRuntime.InvokeVoidAsync("navigator.clipboard.writeText", request.Text);
            }
        }
    }
}
