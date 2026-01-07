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
                var sb = new StringBuilder();
                var generatedOn = DateTime.Now.ToString("dd-MMM-yyyy HH:mm");

                sb.Append("""
        <!DOCTYPE html>
        <html>
        <head>
            <meta charset='utf-8' />
            <style>
                body {
                    font-family: Arial, Helvetica, sans-serif;
                    font-size: 12px;
                    color: green;
                }

                h1 {
                    text-align: center;
                    color: #2c3e50;
                    margin-bottom: 20px;
                }

                table {
                    width: 100%;
                    border-collapse: collapse;
                }

                th {
                    background-color: #34495e;
                    color: white;
                    padding: 8px;
                    text-align: left;
                }

                td {
                    padding: 8px;
                    border-bottom: 1px solid #ddd;
                }

                tr:nth-child(even) {
                    background-color: #f2f2f2;
                }

                .footer {
                    margin-top: 20px;
                    font-size: 10px;
                    text-align: right;
                    color: red;
                }
            </style>
        </head>
        <body>
            <h1>Employee Report</h1>

            <table>
                <thead>
                    <tr>
                        <th>Employee ID</th>
                        <th>Name</th>
                        <th>Manager ID</th>
                        <th>Designation</th>
                        <th>Project</th>
                        <th>Skill</th>
                    </tr>
                </thead>
                <tbody>
        """);

                foreach (var e in employees)
                {
                    sb.Append($"""
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

                sb.Append("""
                </tbody>
            </table>

            <div class="footer">
                Generated on: : {generatedOn}
            </div>

        </body>
        </html>
        """);

                return sb.ToString();
            }
        }
    }
}