using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace VEMS_RDLC_API.Models
{
    // Main Challan Data Model - Matches Stored Procedure Output
    public class ChallanData
    {
        // Students Table Fields
        public int StudentID { get; set; }
        public string? RegistrationNo { get; set; }
        public string StudentName { get; set; }
        public string? MobileNo { get; set; }
        public string? Email { get; set; }
        public bool Student_IsActive { get; set; }
        public DateTime Student_CreatedOn { get; set; }

        // Challans Table Fields
        public int Challan_Uid { get; set; }
        public string ChallanNo { get; set; }
        public int? Challan_StudentID { get; set; }
        public int? StructureID { get; set; }
        public string Semester { get; set; }
        public short AcademicYear { get; set; }
        public DateTime IssueDate { get; set; }
        public DateTime DueDate { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal Challan_DiscountAmount { get; set; }
        public decimal LateFineAmount { get; set; }
        public decimal NetPayable { get; set; }
        public decimal AmountPaid { get; set; }
        public string Status { get; set; }
        public string? Remarks { get; set; }
        public bool Challan_IsActive { get; set; }
        public int Challan_CreatedBy { get; set; }
        public DateTime Challan_CreatedAt { get; set; }
        public int? Challan_UpdatedBy { get; set; }
        public DateTime? Challan_UpdatedAt { get; set; }
        public int? ApplicationUid { get; set; }
        public string? ChallanMonth { get; set; }
        public string? ChallanYear { get; set; }

        // Aggregated Totals
        public int TotalDetailCount { get; set; }
        public decimal TotalDetailAmount { get; set; }
        public decimal TotalDetailDiscount { get; set; }
        public decimal TotalDetailLateFine { get; set; }
        public decimal TotalDetailNetAmount { get; set; }

        // Detail 1 (FeeHeadID = 1)
        public int? Detail1_Uid { get; set; }
        public short? Detail1_FeeHeadID { get; set; }
        public decimal Detail1_Amount { get; set; }
        public decimal Detail1_DiscountAmount { get; set; }
        public decimal Detail1_LateFine { get; set; }
        public decimal Detail1_NetAmount { get; set; }
        public int? Detail1_CreatedBy { get; set; }
        public DateTime? Detail1_CreatedAt { get; set; }

        // Detail 2 (FeeHeadID = 2)
        public int? Detail2_Uid { get; set; }
        public short? Detail2_FeeHeadID { get; set; }
        public decimal Detail2_Amount { get; set; }
        public decimal Detail2_DiscountAmount { get; set; }
        public decimal Detail2_LateFine { get; set; }
        public decimal Detail2_NetAmount { get; set; }
        public int? Detail2_CreatedBy { get; set; }
        public DateTime? Detail2_CreatedAt { get; set; }

        // Detail 3 (FeeHeadID = 3)
        public int? Detail3_Uid { get; set; }
        public short? Detail3_FeeHeadID { get; set; }
        public decimal Detail3_Amount { get; set; }
        public decimal Detail3_DiscountAmount { get; set; }
        public decimal Detail3_LateFine { get; set; }
        public decimal Detail3_NetAmount { get; set; }
        public int? Detail3_CreatedBy { get; set; }
        public DateTime? Detail3_CreatedAt { get; set; }

        // Detail 4 (FeeHeadID = 4)
        public int? Detail4_Uid { get; set; }
        public short? Detail4_FeeHeadID { get; set; }
        public decimal Detail4_Amount { get; set; }
        public decimal Detail4_DiscountAmount { get; set; }
        public decimal Detail4_LateFine { get; set; }
        public decimal Detail4_NetAmount { get; set; }
        public int? Detail4_CreatedBy { get; set; }
        public DateTime? Detail4_CreatedAt { get; set; }

        // Detail 5 (FeeHeadID = 5)
        public int? Detail5_Uid { get; set; }
        public short? Detail5_FeeHeadID { get; set; }
        public decimal Detail5_Amount { get; set; }
        public decimal Detail5_DiscountAmount { get; set; }
        public decimal Detail5_LateFine { get; set; }
        public decimal Detail5_NetAmount { get; set; }
        public int? Detail5_CreatedBy { get; set; }
        public DateTime? Detail5_CreatedAt { get; set; }

        // Detail 6 (FeeHeadID = 6)
        public int? Detail6_Uid { get; set; }
        public short? Detail6_FeeHeadID { get; set; }
        public decimal Detail6_Amount { get; set; }
        public decimal Detail6_DiscountAmount { get; set; }
        public decimal Detail6_LateFine { get; set; }
        public decimal Detail6_NetAmount { get; set; }
        public int? Detail6_CreatedBy { get; set; }
        public DateTime? Detail6_CreatedAt { get; set; }

        // Detail 7 (FeeHeadID = 7)
        public int? Detail7_Uid { get; set; }
        public short? Detail7_FeeHeadID { get; set; }
        public decimal Detail7_Amount { get; set; }
        public decimal Detail7_DiscountAmount { get; set; }
        public decimal Detail7_LateFine { get; set; }
        public decimal Detail7_NetAmount { get; set; }
        public int? Detail7_CreatedBy { get; set; }
        public DateTime? Detail7_CreatedAt { get; set; }

        // Computed Properties
        [NotMapped]
        public decimal BalanceDue => NetPayable - AmountPaid;

        // Helper method to get all details as a list
        public List<ChallanDetailData> GetChallanDetails()
        {
            var details = new List<ChallanDetailData>();

            // Fee Head Names (you can fetch from database or hardcode)
            var feeHeadNames = new Dictionary<short, string>
            {
                { 1, "Tuition Fee" },
                { 2, "Exam Fee" },
                { 3, "Library Fee" },
                { 4, "Lab Fee" },
                { 5, "Sports Fee" },
                { 6, "Other Charges" },
                { 7, "Miscellaneous" }
            };

            // Detail 1
            if (Detail1_FeeHeadID.HasValue)
            {
                details.Add(new ChallanDetailData
                {
                    FeeHeadID = Detail1_FeeHeadID.Value,
                    FeeHeadName = feeHeadNames.ContainsKey(Detail1_FeeHeadID.Value) ? feeHeadNames[Detail1_FeeHeadID.Value] : $"Head {Detail1_FeeHeadID.Value}",
                    Amount = Detail1_Amount,
                    DiscountAmount = Detail1_DiscountAmount,
                    LateFine = Detail1_LateFine,
                    NetAmount = Detail1_NetAmount
                });
            }

            // Detail 2
            if (Detail2_FeeHeadID.HasValue)
            {
                details.Add(new ChallanDetailData
                {
                    FeeHeadID = Detail2_FeeHeadID.Value,
                    FeeHeadName = feeHeadNames.ContainsKey(Detail2_FeeHeadID.Value) ? feeHeadNames[Detail2_FeeHeadID.Value] : $"Head {Detail2_FeeHeadID.Value}",
                    Amount = Detail2_Amount,
                    DiscountAmount = Detail2_DiscountAmount,
                    LateFine = Detail2_LateFine,
                    NetAmount = Detail2_NetAmount
                });
            }

            // Detail 3
            if (Detail3_FeeHeadID.HasValue)
            {
                details.Add(new ChallanDetailData
                {
                    FeeHeadID = Detail3_FeeHeadID.Value,
                    FeeHeadName = feeHeadNames.ContainsKey(Detail3_FeeHeadID.Value) ? feeHeadNames[Detail3_FeeHeadID.Value] : $"Head {Detail3_FeeHeadID.Value}",
                    Amount = Detail3_Amount,
                    DiscountAmount = Detail3_DiscountAmount,
                    LateFine = Detail3_LateFine,
                    NetAmount = Detail3_NetAmount
                });
            }

            // Detail 4
            if (Detail4_FeeHeadID.HasValue)
            {
                details.Add(new ChallanDetailData
                {
                    FeeHeadID = Detail4_FeeHeadID.Value,
                    FeeHeadName = feeHeadNames.ContainsKey(Detail4_FeeHeadID.Value) ? feeHeadNames[Detail4_FeeHeadID.Value] : $"Head {Detail4_FeeHeadID.Value}",
                    Amount = Detail4_Amount,
                    DiscountAmount = Detail4_DiscountAmount,
                    LateFine = Detail4_LateFine,
                    NetAmount = Detail4_NetAmount
                });
            }

            // Detail 5
            if (Detail5_FeeHeadID.HasValue)
            {
                details.Add(new ChallanDetailData
                {
                    FeeHeadID = Detail5_FeeHeadID.Value,
                    FeeHeadName = feeHeadNames.ContainsKey(Detail5_FeeHeadID.Value) ? feeHeadNames[Detail5_FeeHeadID.Value] : $"Head {Detail5_FeeHeadID.Value}",
                    Amount = Detail5_Amount,
                    DiscountAmount = Detail5_DiscountAmount,
                    LateFine = Detail5_LateFine,
                    NetAmount = Detail5_NetAmount
                });
            }

            // Detail 6
            if (Detail6_FeeHeadID.HasValue)
            {
                details.Add(new ChallanDetailData
                {
                    FeeHeadID = Detail6_FeeHeadID.Value,
                    FeeHeadName = feeHeadNames.ContainsKey(Detail6_FeeHeadID.Value) ? feeHeadNames[Detail6_FeeHeadID.Value] : $"Head {Detail6_FeeHeadID.Value}",
                    Amount = Detail6_Amount,
                    DiscountAmount = Detail6_DiscountAmount,
                    LateFine = Detail6_LateFine,
                    NetAmount = Detail6_NetAmount
                });
            }

            // Detail 7
            if (Detail7_FeeHeadID.HasValue)
            {
                details.Add(new ChallanDetailData
                {
                    FeeHeadID = Detail7_FeeHeadID.Value,
                    FeeHeadName = feeHeadNames.ContainsKey(Detail7_FeeHeadID.Value) ? feeHeadNames[Detail7_FeeHeadID.Value] : $"Head {Detail7_FeeHeadID.Value}",
                    Amount = Detail7_Amount,
                    DiscountAmount = Detail7_DiscountAmount,
                    LateFine = Detail7_LateFine,
                    NetAmount = Detail7_NetAmount
                });
            }

            return details;
        }
    }

    // Challan Detail Data for Report
    public class ChallanDetailData
    {
        public short FeeHeadID { get; set; }
        public string FeeHeadName { get; set; }
        public decimal Amount { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal LateFine { get; set; }
        public decimal NetAmount { get; set; }
    }

    // API Response
    public class ApiResponse<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
        public List<string> Errors { get; set; }

        public ApiResponse()
        {
            Errors = new List<string>();
            Success = true;
        }
    }
}