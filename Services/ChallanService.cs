using System;
using System.Threading.Tasks;
using VEMS_RDLC_API.Models;
using VEMS_RDLC_API.Repositories;

namespace VEMS_RDLC_API.Services
{
    public class ChallanService : IChallanService
    {
        private readonly IChallanRepository _repository;

        public ChallanService(IChallanRepository repository)
        {
            _repository = repository;
        }

        public async Task<ChallanData> GetChallanReportDataAsync(string challanNo)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(challanNo))
                    throw new Exception("Challan number is required");

                var reportData = await _repository.GetChallanReportDataAsync(challanNo);

                if (reportData == null)
                    throw new Exception($"Challan not found with number: {challanNo}");

                return reportData;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving challan report data: {ex.Message}");
            }
        }
    }
}