using System.Threading.Tasks;
using VEMS_RDLC_API.Models;

namespace VEMS_RDLC_API.Repositories
{
    public interface IChallanRepository
    {
        // Get Challan Report Data using Stored Procedure
        Task<ChallanData?> GetChallanReportDataAsync(string challanNo);
    }
}