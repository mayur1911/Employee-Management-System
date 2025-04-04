using BFFGateway.Handlers;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Threading.Tasks;

namespace BFFGateway.Controllers
{
    [ApiController]
    [Route("bff/{serviceName}/proxy")]
    public class BFFController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BFFController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{*path}")]  // ✅ Renamed from 'endpoint' to 'path' to prevent conflicts
        public async Task<IActionResult> ForwardRequest(string serviceName, string path)
        {
            var response = await _mediator.Send(new ApiRequest(serviceName, path));
            var content = await response.Content.ReadAsStringAsync();

            return Content(content, "application/json");
        }

        // 🔹 POST Request
        [HttpPost]
        public async Task<IActionResult> ForwardPostRequest(string serviceName, string path, [FromBody] object body)
        {
            var response = await _mediator.Send(new ApiRequest(serviceName, path));
            return Ok(await response.Content.ReadAsStringAsync());
        }

        // 🔹 DELETE Request
        [HttpDelete]
        public async Task<IActionResult> ForwardDeleteRequest(string serviceName, string path)
        {
            var response = await _mediator.Send(new ApiRequest(serviceName, path));
            return Ok(await response.Content.ReadAsStringAsync());
        }


    }
}