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

        public ChallanController(IChallanService service)
        {
            _service = service;
        }

        /// <summary>
        /// Get challan report data for RDLC
        /// </summary>
        [HttpGet("report/{challanNo}")]
        public async Task<ActionResult<ApiResponse<ChallanData>>> GetChallanReport(string challanNo)
        {
            try
            {
                // Validate input
                if (string.IsNullOrWhiteSpace(challanNo))
                {
                    return BadRequest(new ApiResponse<ChallanData>
                    {
                        Success = false,
                        Message = "Challan number is required",
                        Errors = new List<string> { "Challan number cannot be empty" }
                    });
                }

                // Get report data
                var reportData = await _service.GetChallanReportDataAsync(challanNo);

                if (reportData == null)
                {
                    return NotFound(new ApiResponse<ChallanData>
                    {
                        Success = false,
                        Message = "Challan not found",
                        Errors = new List<string> { $"No challan found with number: {challanNo}" }
                    });
                }

                return Ok(new ApiResponse<ChallanData>
                {
                    Success = true,
                    Message = "Report data retrieved successfully",
                    Data = reportData,
                    Errors = new List<string>()
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<ChallanData>
                {
                    Success = false,
                    Message = "Error retrieving challan report",
                    Data = null,
                    Errors = new List<string> { ex.Message }
                });
            }
        }

        /// <summary>
        /// Print challan (RDLC Report)
        /// </summary>
        [HttpGet("print/{challanNo}")]
        public async Task<IActionResult> PrintChallan(string challanNo)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(challanNo))
                {
                    return BadRequest(new { error = "Challan number is required" });
                }

                var reportData = await _service.GetChallanReportDataAsync(challanNo);

                if (reportData == null)
                {
                    return NotFound(new { error = "Challan not found" });
                }

                // Return data for RDLC report
                return Ok(new
                {
                    success = true,
                    message = "RDLC Report data retrieved successfully",
                    data = reportData,
                    reportPath = "Reports/Challan.rdlc",
                    datasetName = "VEMSDataSet"
                });
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