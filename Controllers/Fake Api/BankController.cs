using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SystemManagmentEmployeeWebApi.Controllers.Fake_Api;

namespace SystemManagmentEmployeeWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BankController : ControllerBase
    {

        [HttpPost("transfer")]
        public IActionResult Transfer([FromBody] BankTransferRequest request)
        {
            if (request.Amount <= 0)
                return BadRequest("Invalid amount.");

            var transactionId = Guid.NewGuid().ToString();

            return Ok(new BankTransferResponse
            {
                Success = true,
                TransactionId = transactionId,
                Message = $"Transferred {request.Amount} to {request.AccountNumber}"
            });
        }



    }
}
