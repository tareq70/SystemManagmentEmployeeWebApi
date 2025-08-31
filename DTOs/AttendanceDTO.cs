using System.ComponentModel.DataAnnotations;
using SystemManagmentEmployeeWebApi.Models.Entities;

namespace SystemManagmentEmployeeWebApi.DTOs
{
    public class AttendanceDTO
    {
        public int Id { get; set; } 
        public DateTime CheckIn { get; set; }

        public DateTime? CheckOut { get; set; }

        [Required(ErrorMessage = "Employee ID is required")]
        public int EmployeeId { get; set; }

        [StringLength(100, ErrorMessage = "Employee name cannot exceed 100 characters")]
        public string EmpName { get; set; } = default!;

        public double? WorkingHours { get; set; }

    }
}
