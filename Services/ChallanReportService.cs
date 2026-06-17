using Microsoft.Reporting.NETCore;
using VEMS_RDLC_API.Models;
using VEMS_RDLC_API.Reports;

namespace VEMS_RDLC_API.Services
{
    public class ChallanReportService : IChallanReportService
    {
        private const string ReportFileName = "Challan.rdlc";
        private const string DataSetName = "DataSet1";

        public byte[] GeneratePdf(ChallanData data)
        {
            if (data == null)
                throw new ArgumentNullException(nameof(data));

            var reportPath = Path.Combine(AppContext.BaseDirectory, "Reports", ReportFileName);
            if (!File.Exists(reportPath))
                throw new FileNotFoundException("Challan report file not found.", reportPath);

            using var reportStream = File.OpenRead(reportPath);
            using var report = new LocalReport();
            report.LoadReportDefinition(reportStream);

            var dataTable = ChallanReportMapper.ToDataTable(data);
            report.DataSources.Add(new ReportDataSource(DataSetName, dataTable));

            if (!string.IsNullOrWhiteSpace(data.ChallanNo))
            {
                report.SetParameters(new ReportParameter("ChallanNo", data.ChallanNo));
            }

            return report.Render("PDF");
        }
    }
}
