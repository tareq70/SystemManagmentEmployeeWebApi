namespace SystemManagmentEmployeeWebApi.Controllers.Fake_Api
{
    public class BankTransferResponse
    {
        public bool Success { get; set; }
        public string TransactionId { get; set; } = default!;
        public string Message { get; set; } = default!;
    }
}
