using SumUpDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SumUpDemo.Services
{
    public interface IAuthService
    {
        Task<SumUpToken> GetSumUpToken();
    }
}
