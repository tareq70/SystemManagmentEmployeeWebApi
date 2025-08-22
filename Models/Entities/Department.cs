namespace SystemManagmentEmployeeWebApi.Models.Entities
{
    public class Department
    {
        public int Id { get; set; }

        public string Name { get; set; } = default!;

        public string Description { get; set; } = default!;


        public ICollection<Employee> Employees { get; set; } = new List<Employee>();
    }
}
