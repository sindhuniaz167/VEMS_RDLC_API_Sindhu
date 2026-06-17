using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using VEMS_RDLC_API.Data;
using VEMS_RDLC_API.Models;

namespace VEMS_RDLC_API.Repositories
{
    public class ChallanRepository : IChallanRepository
    {
        private readonly ApplicationDbContext _context;

        public ChallanRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ChallanData> GetChallanReportDataAsync(string challanNo)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(challanNo))
                    return null;

                // Execute stored procedure
                var param = new SqlParameter("@ChallanNo", challanNo);

                // Stored procedures are non-composable; materialize before FirstOrDefault
                var results = await _context.ChallanData
                    .FromSqlRaw("EXEC GetChallanData @ChallanNo", param)
                    .ToListAsync();

                return results.FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to execute GetChallanData for challan '{challanNo}': {ex.Message}", ex);
            }
        }
    }
}