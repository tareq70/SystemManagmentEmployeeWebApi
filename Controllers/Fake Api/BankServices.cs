using System.Net.Http.Json;

namespace SystemManagmentEmployeeWebApi.Controllers.Fake_Api
{
    public class BankServices : IBankService
    {
        private readonly HttpClient _httpClient;

        public BankServices(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("http://localhost:5031/api/bank/");
        }

        public async Task<BankTransferResponse?> TransferSalaryAsync(string accountNumber, decimal amount)
        {
            var request = new BankTransferRequest
            {
                AccountNumber = accountNumber,
                Amount = amount
            };

            var response = await _httpClient.PostAsJsonAsync("transfer", request);

            if (!response.IsSuccessStatusCode)
                return null;

            return await response.Content.ReadFromJsonAsync<BankTransferResponse>();
        }

        public async Task<bool> ValidateAccountAsync(string accountNumber)
        {
            var response = await _httpClient.GetAsync($"validate-account/{accountNumber}");
            if (!response.IsSuccessStatusCode)
                return false;

            var result = await response.Content.ReadFromJsonAsync<ValidateAccountResponse>();
            return result?.IsValid ?? false;
        }

        public async Task<decimal> CheckBalanceAsync(string accountNumber)
        {
            var response = await _httpClient.GetAsync($"check-balance/{accountNumber}");
            if (!response.IsSuccessStatusCode)
                return 0;

            var result = await response.Content.ReadFromJsonAsync<CheckBalanceResponse>();
            return result?.Balance ?? 0;
        }
    }

    // DTO Models
    public class ValidateAccountResponse
    {
        public bool IsValid { get; set; }
    }

    public class CheckBalanceResponse
    {
        public decimal Balance { get; set; }
    }

}
