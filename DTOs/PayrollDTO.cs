using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SystemManagmentEmployeeWebApi.Models.Entities;

namespace SystemManagmentEmployeeWebApi.DTOs
{
    public class PayrollDTO
    {
        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "Net salary must be non-negative.")]
        public decimal NetSalary { get; set; }

        [Required]
        public DateTime Month { get; set; }
        public bool IsPaid { get; set; } = false;

        [MaxLength(100)]
        public string? TransactionId { get; set; }

        [Required]
        [ForeignKey("Employee")]
        public int EmployeeId { get; set; }
        public string EmpName { get; set; } = default!;
    }
}
