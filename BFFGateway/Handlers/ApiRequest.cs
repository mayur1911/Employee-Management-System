using MediatR;

namespace BFFGateway.Handlers
{
    public class ApiRequest : IRequest<HttpResponseMessage>
    {
        public string ServiceName { get; }
        public string Path { get; }  // ✅ Changed 'Endpoint' to 'Path' to match BFFController

        public ApiRequest(string serviceName, string path)
        {
            ServiceName = serviceName;
            Path = path;
        }
    }

}