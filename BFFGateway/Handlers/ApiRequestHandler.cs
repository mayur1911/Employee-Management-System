using MediatR;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace BFFGateway.Handlers
{
    public class ApiRequestHandler : IRequestHandler<ApiRequest, HttpResponseMessage>
    {
        private readonly HttpClient _httpClient;

        public ApiRequestHandler(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<HttpResponseMessage> Handle(ApiRequest request, CancellationToken cancellationToken)
        {
            var url = $"https://localhost:8443/{request.Path}";

            if (request.Path.StartsWith("api/manager", System.StringComparison.OrdinalIgnoreCase))
            {
                // Handle POST request if it's coming from BFFController's POST action
                if (request.Path.Contains("post", System.StringComparison.OrdinalIgnoreCase))
                {
                    return await _httpClient.PostAsync(url, null, cancellationToken);
                }
                // Handle DELETE request
                else if (request.Path.Contains("delete", System.StringComparison.OrdinalIgnoreCase))
                {
                    return await _httpClient.DeleteAsync(url, cancellationToken);
                }
            }

            // Default: Assume GET
            return await _httpClient.GetAsync(url, cancellationToken);
        }
    }
}
