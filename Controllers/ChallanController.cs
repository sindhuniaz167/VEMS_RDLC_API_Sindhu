using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using VEMS_RDLC_API.Services;

namespace VEMS_RDLC_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChallanController : ControllerBase
    {
        private readonly IChallanService _service;

        public ChallanController(IChallanService service)
        {
            _service = service;
        }

        /// <summary>
        /// Download challan as PDF (RDLC Report)
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> DownloadChallanPdf(string challanNo)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(challanNo))
                {
                    return BadRequest(new { error = "Challan number is required" });
                }

                var pdfBytes = await _service.GeneratePdfAsync(challanNo);

                return File(pdfBytes, "application/pdf", $"Challan-{challanNo}.pdf");
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    success = false,
                    error = ex.Message
                });
            }
        }
       
    }
}