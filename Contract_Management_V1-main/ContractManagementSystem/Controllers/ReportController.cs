using ClosedXML.Excel; // For Excel export
using ContractManagementSystem.Models.Reports;
using ContractManagementSystem.Services;
using iText.Kernel.Pdf; // For PDF export
using iText.Layout; // For PDF export
using iText.Layout.Element; // For PDF export
using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.IO;

namespace ContractManagementSystem.Controllers
{
    public class ReportController : Controller
    {
        private readonly IReportService _reportService;

        public ReportController(IReportService reportService)
        {
            _reportService = reportService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View(new ContractReportFilter());
        }

        [HttpPost]
        public IActionResult Generate(ContractReportFilter filter)
        {
            var report = _reportService.GenerateContractReport(filter);
            return View("ReportResult", report);
        }

        [HttpPost]
        public IActionResult ExportToExcel(ContractReportFilter filter)
        {
            var report = _reportService.GenerateContractReport(filter);

            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Report");
                worksheet.Cell(1, 1).Value = "Contract ID";
                worksheet.Cell(1, 2).Value = "Party Name";
                worksheet.Cell(1, 3).Value = "Contract Type";
                worksheet.Cell(1, 4).Value = "Start Date";
                worksheet.Cell(1, 5).Value = "End Date";
                worksheet.Cell(1, 6).Value = "Contract Value";
                worksheet.Cell(1, 7).Value = "Status";

                int row = 2;
                foreach (var item in report)
                {
                    worksheet.Cell(row, 1).Value = item.ContractId;
                    worksheet.Cell(row, 2).Value = item.PartyName;
                    worksheet.Cell(row, 3).Value = item.ContractType;
                    worksheet.Cell(row, 4).Value = item.StartDate.ToShortDateString();
                    worksheet.Cell(row, 5).Value = item.EndDate.ToShortDateString();
                    worksheet.Cell(row, 6).Value = item.ContractValue;
                    worksheet.Cell(row, 7).Value = item.Status;
                    row++;
                }

                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();
                    return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Report.xlsx");
                }
            }
        }

        [HttpPost]
        public IActionResult ExportToPdf(ContractReportFilter filter)
        {
            var report = _reportService.GenerateContractReport(filter);

            using (var stream = new MemoryStream())
            {
                var writer = new PdfWriter(stream);
                var pdf = new PdfDocument(writer);
                var document = new Document(pdf);

                var table = new Table(7); // Number of columns
                table.AddHeaderCell("Contract ID");
                table.AddHeaderCell("Party Name");
                table.AddHeaderCell("Contract Type");
                table.AddHeaderCell("Start Date");
                table.AddHeaderCell("End Date");
                table.AddHeaderCell("Contract Value");
                table.AddHeaderCell("Status");

                foreach (var item in report)
                {
                    table.AddCell(item.ContractId.ToString());
                    table.AddCell(item.PartyName);
                    table.AddCell(item.ContractType);
                    table.AddCell(item.StartDate.ToString("d"));
                    table.AddCell(item.EndDate.ToString("d"));
                    table.AddCell(item.ContractValue.ToString("F2"));
                    table.AddCell(item.Status);
                }

                document.Add(table);
                document.Close();

                return File(stream.ToArray(), "application/pdf", "Report.pdf");
            }
        }

        [HttpPost]
        public IActionResult ExportToCsv(ContractReportFilter filter)
        {
            var report = _reportService.GenerateContractReport(filter);
            var csv = new StringBuilder();
            csv.AppendLine("Contract ID,Party Name,Contract Type,Start Date,End Date,Contract Value,Status");

            foreach (var item in report)
            {
                csv.AppendLine($"{item.ContractId},{item.PartyName},{item.ContractType},{item.StartDate.ToShortDateString()},{item.EndDate.ToShortDateString()},{item.ContractValue},{item.Status}");
            }

            return File(Encoding.UTF8.GetBytes(csv.ToString()), "text/csv", "Report.csv");
        }
    }
}
