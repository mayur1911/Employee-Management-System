using MediatR;
using RedisCachingWebApi.Domain;
using RedisCachingWebApi.Services;

namespace RedisCachingWebApi.Application.Handlers
{
    public class PrintPdfHandler
    {
        public class Query : IRequest<Response>
        {
        }

        public class Response
        {
            public byte[] Content { get; set; } = Array.Empty<byte>();
            public string FileName { get; set; } = string.Empty;
            public string ContentType { get; set; } = "application/pdf";
        }

        public class Handler : IRequestHandler<Query, Response>
        {
            private readonly IPdfService _pdfService;

            public Handler(IPdfService pdfService)
            {
                _pdfService=pdfService;
            }
            public async Task<Response> Handle(Query request, CancellationToken cancellationToken)
            {
                var employees = new List<EmployeeData>
        {
            new() { EmployeeID = 3, EmpName = "secondname", ManagerId = "1", EmpDesignation = "string", ProjectName = "string", Skill = "string" },
            new() { EmployeeID = 1002, EmpName = "employeeName", ManagerId = "1007", EmpDesignation = "tester", ProjectName = "domo", Skill = "java" },
            new() { EmployeeID = 1003, EmpName = "employeeName2", ManagerId = "1005", EmpDesignation = "tester", ProjectName = "fomo", Skill = "javascripot" }
        };

                var pdfBytes = _pdfService.GenerateEmployeePdf(employees);

                return new Response
                {
                    Content = pdfBytes,
                    FileName = "EmployeeReport.pdf",
                    ContentType = "application/pdf"
                };
            }
        }
    }
}