using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SystemManagmentEmployeeWebApi.Models.Entities;

namespace SystemManagmentEmployeeWebApi.DTOs
{
    public class EmployeeDTO
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Name is required")]
        [StringLength(50, ErrorMessage = "Name can't be longer than 50 characters")]
        public string FullName { get; set; } = default!;

        [Range(18, 60, ErrorMessage = "Age must be between 18 and 60")]
        public int Age { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        public string Email { get; set; } = default!;

        [Phone(ErrorMessage = "Invalid phone number")]
        public string Phone { get; set; } = default!;

        public string Address { get; set; } = default!;
        [Range(0, double.MaxValue, ErrorMessage = "Salary must be positive")]

        public decimal Salary { get; set; }
        public DateTime HireDate { get; set; }
        public string DepartmentName { get; set; } = default!;

        [Required(ErrorMessage = "DepartmentId is required")]
        public int DepartmentId { get; set; }


    }
}
