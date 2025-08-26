using System.ComponentModel.DataAnnotations;
using SystemManagmentEmployeeWebApi.Validation;

namespace SystemManagmentEmployeeWebApi.DTOs
{
    public class LeaveDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Start date is required")]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "End date is required")]
        [DateGreaterThan("StartDate", ErrorMessage = "End date must be after start date")]
        public DateTime EndDate { get; set; }

        [Required(ErrorMessage = "Reason is required")]
        [StringLength(250, ErrorMessage = "Reason cannot exceed 250 characters")]
        public string Reason { get; set; } = string.Empty;

        [RegularExpression("^(Pending|Approved|Rejected)$", ErrorMessage = "Status must be Pending, Approved, or Rejected")]
        public string Status { get; set; } = "Pending";

        [Required(ErrorMessage = "EmployeeId is required")]
        public int EmployeeId { get; set; }

        public string EmpName { get; set; } = string.Empty;


    }
}
