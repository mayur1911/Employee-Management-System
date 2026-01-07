using MediatR;
using RedisCachingWebApi.Domain;
using RedisCachingWebApi.Services;
using System.Text;

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
                _pdfService = pdfService;
            }

            public async Task<Response> Handle(Query request, CancellationToken cancellationToken)
            {
                var employees = new List<EmployeeData>
        {
            new() { EmployeeID = 3, EmpName = "secondname", ManagerId = "1", EmpDesignation = "string", ProjectName = "string", Skill = "string" },
            new() { EmployeeID = 1002, EmpName = "employeeName", ManagerId = "1007", EmpDesignation = "tester", ProjectName = "domo", Skill = "java" },
            new() { EmployeeID = 1003, EmpName = "employeeName2", ManagerId = "1005", EmpDesignation = "tester", ProjectName = "fomo", Skill = "javascripot" }
        };

                var html = BuildEmployeeHtml(employees);

                var pdfBytes = _pdfService.GenerateFromHtml(html);

                return new Response
                {
                    Content = pdfBytes,
                    FileName = "EmployeeReport.pdf",
                    ContentType = "application/pdf"
                };
            }

            private string BuildEmployeeHtml(List<EmployeeData> employees)
            {
                var templatePath = Path.Combine(Directory.GetCurrentDirectory(), "Templates", "EmployeeReport.html");

                var htmlTemplate = File.ReadAllText(templatePath);

                var rowsBuilder = new StringBuilder();

                foreach (var e in employees)
                {
                    rowsBuilder.Append($"""
            <tr>
                <td>{e.EmployeeID}</td>
                <td>{e.EmpName}</td>
                <td>{e.ManagerId}</td>
                <td>{e.EmpDesignation}</td>
                <td>{e.ProjectName}</td>
                <td>{e.Skill}</td>
            </tr>
        """);
                }

                htmlTemplate = htmlTemplate
                    .Replace("{{ROWS}}", rowsBuilder.ToString())
                    .Replace("{{GENERATED_ON}}", DateTime.Now.ToString("dd-MMM-yyyy HH:mm"));

                return htmlTemplate;
            }

        }
    }
}