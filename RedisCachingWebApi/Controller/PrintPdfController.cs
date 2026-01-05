using MediatR;
using Microsoft.AspNetCore.Mvc;
using RedisCachingWebApi.Application.Handlers;
using RedisCachingWebApi.Application.Handlers.Manager;
using RedisCachingWebApi.Services;

namespace RedisCachingWebApi.Controller
{
    public class PrintPdfController : ControllerBase
    {
        private readonly IPdfService _pdfService;
        private readonly IMediator _mediator;

        public PrintPdfController(IPdfService pdfService, IMediator mediator)
        {
            _pdfService=pdfService;
            _mediator=mediator;
        }

        [HttpGet("printPdf")]
        public async Task<IActionResult> PrintPdf()
        {
            var result = await _mediator.Send(new PrintPdfHandler.Query());

            return File(
                result.Content,
                result.ContentType,
                result.FileName
            );
        }
    }
}