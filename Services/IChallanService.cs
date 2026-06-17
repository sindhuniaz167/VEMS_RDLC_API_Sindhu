using System.Threading.Tasks;
using VEMS_RDLC_API.Models;

namespace VEMS_RDLC_API.Services
{
    public interface IChallanService
    {
        // Get Challan Report Data
        Task<ChallanData> GetChallanReportDataAsync(string challanNo);
    }
}