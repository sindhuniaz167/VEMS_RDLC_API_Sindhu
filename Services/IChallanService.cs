using System.Threading.Tasks;
using VEMS_RDLC_API.Models;

namespace VEMS_RDLC_API.Services
{
    public interface IChallanService
    {
        Task<ChallanData> GetChallanReportDataAsync(string challanNo);
        byte[] GeneratePdf(ChallanData data);
        Task<byte[]> GeneratePdfAsync(string challanNo);
    }
}
