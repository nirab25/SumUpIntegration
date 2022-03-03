using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SumUpDemo.Models;
using SumUpDemo.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SumUpDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("token")]
        public async Task<SumUpToken> GetSumUpToken()
        {
            return await _authService.GetSumUpToken();
        }
    }
}
