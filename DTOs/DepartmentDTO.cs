using System.ComponentModel.DataAnnotations;

namespace SystemManagmentEmployeeWebApi.DTOs
{
    public class DepartmentDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [StringLength(100, ErrorMessage = "Name can't be longer than 100 characters")]
        public string Name { get; set; } = default!;

        [StringLength(250, ErrorMessage = "Description can't be longer than 250 characters")]
        public string Description { get; set; } = default!;
    }
}
