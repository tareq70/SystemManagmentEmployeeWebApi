namespace SystemManagmentEmployeeWebApi.Models.Entities
{
    public class Attendance
    {
        public int Id { get; set; }
        public DateTime CheckIn { get; set; }
        public DateTime? CheckOut { get; set; }

        public double? WorkingHours
        {
            get
            {
                if (CheckOut.HasValue)
                    return (CheckOut.Value - CheckIn).TotalHours;
                return null;
            }
        }

        // Relations
        public int EmployeeId { get; set; }
        public Employee? Employee { get; set; }
    }
}
