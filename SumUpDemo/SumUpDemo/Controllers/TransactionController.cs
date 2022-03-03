using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SumUpDemo.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace SumUpDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly IAuthService _authService;

        public TransactionController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpGet]
        public async Task<IActionResult> GetTransactionHistory()
        {
            try
            {
                var authToken = await _authService.GetSumUpToken();
                if (authToken != null && authToken.success)
                {
                    var url = "https://api.sumup.com/v0.1/me/transactions/history";
                    using var httpClient = new HttpClient();
                    httpClient.DefaultRequestHeaders.Authorization =
                        new AuthenticationHeaderValue("Bearer", authToken.access_token);

                    using var httpRequestMessage = new HttpRequestMessage(new HttpMethod("GET"), url);

                    var httpResponseMessage = await httpClient.SendAsync(httpRequestMessage);
                    httpResponseMessage.EnsureSuccessStatusCode();

                    return Ok(await httpResponseMessage.Content.ReadAsStringAsync());
                }
            }
            catch (Exception ex)
            {
                return Ok(ex.Message);
            }
            
            return Ok("Failed.");
        }
    }
}
