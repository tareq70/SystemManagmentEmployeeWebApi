namespace SystemManagmentEmployeeWebApi.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string message) : base(message)
        {

        }
        public override string ToString()
        {
            return base.ToString();
        }
    }
}
