using VEMS_RDLC_API.Models;

namespace VEMS_RDLC_API.Services
{
    public interface IChallanReportService
    {
        byte[] GeneratePdf(ChallanData data);
    }
}
