using System.ComponentModel.DataAnnotations.Schema;

namespace SystemManagmentEmployeeWebApi.Models.Entities
{
    public class Employee
    {
        public int Id { get; set; }
        public string FullName { get; set; } = default!;
        public int Age { get; set; }
        public string Email { get; set; } = default!;
        public string Phone { get; set; } = default!;
        public string Address { get; set; } = default!;
        public decimal Salary { get; set; }
        public DateTime HireDate { get; set; }

        [ForeignKey("Department")]
        public int DepartmentId { get; set; }
        public Department? Department { get; set; }

        public ICollection<Attendance> Attendances { get; set; } = new List<Attendance>();



    }
}
