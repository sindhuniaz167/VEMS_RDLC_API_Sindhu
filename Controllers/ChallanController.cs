using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VEMS_RDLC_API.Models;
using VEMS_RDLC_API.Services;

namespace VEMS_RDLC_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChallanController : ControllerBase
    {
        private readonly IChallanService _service;
        private readonly IChallanReportService _reportService;

        public ChallanController(IChallanService service, IChallanReportService reportService)
        {
            _service = service;
            _reportService = reportService;
        }

        /// <summary>
        /// Get challan report data for RDLC
        /// </summary>
       
        /// <summary>
        /// Download challan as PDF (RDLC Report)
        /// </summary>
        [HttpGet("{challanNo}")]
        public async Task<IActionResult> DownloadChallanPdf(string challanNo)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(challanNo))
                {
                    return BadRequest(new { error = "Challan number is required" });
                }

                var reportData = await _service.GetChallanReportDataAsync(challanNo);
                var pdfBytes = _reportService.GeneratePdf(reportData);

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