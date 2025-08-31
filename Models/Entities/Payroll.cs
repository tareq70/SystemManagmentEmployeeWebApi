using System.ComponentModel.DataAnnotations;

namespace SystemManagmentEmployeeWebApi.Models.Entities
{
    public class Payroll
    {
        public int Id { get; set; }
        public Employee? Employee { get; set; }
        public decimal NetSalary { get; set; }
        public DateTime Month { get; set; }  
        public bool IsPaid { get; set; } = false;
        public string? TransactionId { get; set; }
        public int EmployeeId { get; set; }

    }
}
