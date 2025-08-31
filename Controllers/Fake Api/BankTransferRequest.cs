namespace SystemManagmentEmployeeWebApi.Controllers.Fake_Api
{
    public class BankTransferRequest
    {
        public string AccountNumber { get; set; } = default!;
        public decimal Amount { get; set; }
    }
}
