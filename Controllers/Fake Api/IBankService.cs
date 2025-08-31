namespace SystemManagmentEmployeeWebApi.Controllers.Fake_Api
{
    public interface IBankService
    {
        Task<BankTransferResponse?> TransferSalaryAsync(string accountNumber, decimal amount);
        Task<bool> ValidateAccountAsync(string accountNumber);
        Task<decimal> CheckBalanceAsync(string accountNumber);
    }
}
