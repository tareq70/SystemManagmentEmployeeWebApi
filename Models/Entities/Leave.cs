using System.ComponentModel.DataAnnotations;

namespace SystemManagmentEmployeeWebApi.Models.Entities
{
    public class Leave
    {
        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Reason { get; set; } = string.Empty;
        public string Status { get; set; } = "Pending";
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; } = default!;

    }
}
