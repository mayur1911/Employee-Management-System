using iTextSharp.text;
using iTextSharp.text.pdf;
using RedisCachingWebApi.Domain;

namespace RedisCachingWebApi.Services
{
    public class PdfService : IPdfService
    {
        public byte[] GenerateEmployeePdf(List<EmployeeData> employees)
        {
            using var stream = new MemoryStream();

            var document = new Document(PageSize.A4, 20, 20, 20, 20);
            PdfWriter.GetInstance(document, stream);
            document.Open();

            // Title
            var titleFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 16);
            var title = new Paragraph("Employee Report", titleFont)
            {
                Alignment = Element.ALIGN_CENTER,
                SpacingAfter = 20
            };
            document.Add(title);

            // Table
            PdfPTable table = new PdfPTable(6)
            {
                WidthPercentage = 100
            };

            table.SetWidths(new float[] { 10, 20, 15, 20, 20, 15 });

            AddHeader(table, "Employee ID");
            AddHeader(table, "Name");
            AddHeader(table, "Manager ID");
            AddHeader(table, "Designation");
            AddHeader(table, "Project");
            AddHeader(table, "Skill");

            foreach (var emp in employees)
            {
                table.AddCell(emp.EmployeeID.ToString());
                table.AddCell(emp.EmpName);
                table.AddCell(emp.ManagerId);
                table.AddCell(emp.EmpDesignation);
                table.AddCell(emp.ProjectName);
                table.AddCell(emp.Skill);
            }

            document.Add(table);
            document.Close();

            return stream.ToArray();
        }

        private void AddHeader(PdfPTable table, string text)
        {
            var font = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 10);
            var cell = new PdfPCell(new Phrase(text, font))
            {
                BackgroundColor = BaseColor.LIGHT_GRAY,
                HorizontalAlignment = Element.ALIGN_CENTER
            };
            table.AddCell(cell);
        }
    }
}