using Microsoft.Reporting.NETCore;
using VEMS_RDLC_API.Models;
using VEMS_RDLC_API.Reports;
using VEMS_RDLC_API.Repositories;

namespace VEMS_RDLC_API.Services
{
    public class ChallanService : IChallanService
    {
        private const string ReportFileName = "Challan.rdlc";
        private const string DataSetName = "DataSet1";

        private readonly IChallanRepository _repository;

        public ChallanService(IChallanRepository repository)
        {
            _repository = repository;
        }

        public async Task<ChallanData> GetChallanReportDataAsync(string challanNo)
        {
            if (string.IsNullOrWhiteSpace(challanNo))
                throw new Exception("Challan number is required");

            var reportData = await _repository.GetChallanReportDataAsync(challanNo);

            if (reportData == null)
                throw new Exception($"Challan not found with number: {challanNo}");

            return reportData;
        }

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

        public async Task<byte[]> GeneratePdfAsync(string challanNo)
        {
            var reportData = await GetChallanReportDataAsync(challanNo);
            return GeneratePdf(reportData);
        }
    }
}
