using Newtonsoft.Json;
using SumUpDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace SumUpDemo.Services
{
    public class AuthService : IAuthService
    {
        private readonly string _clientId;
        private readonly string _clientSecret;

        public AuthService()
        {
            var clientId = Environment.GetEnvironmentVariable("ClientId");
            if (!string.IsNullOrEmpty(clientId))
            {
                _clientId = clientId;
            }

            var clientSecret = Environment.GetEnvironmentVariable("ClientSecret");
            if (!string.IsNullOrEmpty(clientSecret))
            {
                _clientSecret = clientSecret;
            }
        }

        public async Task<SumUpToken> GetSumUpToken()
        {
            try
            {
                if (string.IsNullOrEmpty(_clientId) || string.IsNullOrEmpty(_clientSecret))
                {
                    return new SumUpToken
                    {
                        success = false,
                        error_message = "Empty client_id or client_secret",
                        error_code = "UNKNOWN"
                    };
                }

                var url = $"https://api.sumup.com/token";
                using var httpClient = new HttpClient();
                using var httpRequestMessage = new HttpRequestMessage(new HttpMethod("POST"), url);

                var FormValues = new Dictionary<string, string>()
                {
                    {"client_id", _clientId},
                    {"client_secret", _clientSecret},
                    {"grant_type", "client_credentials" }
                };

                var Content = new FormUrlEncodedContent(FormValues);
                httpRequestMessage.Content = Content;
                httpRequestMessage.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/x-www-form-urlencoded");

                var httpResponseMessage = await httpClient.SendAsync(httpRequestMessage);
                httpResponseMessage.EnsureSuccessStatusCode();

                var res = await httpResponseMessage.Content.ReadAsStringAsync();

                if (!string.IsNullOrEmpty(res))
                {
                    var tokenResult = JsonConvert.DeserializeObject<SumUpToken>(res);
                    if (tokenResult != null)
                    {
                        tokenResult.success = true;
                        return tokenResult;
                    }
                }

                return new SumUpToken
                {
                    success = false,
                    error_message = "FAILED"
                };
            }
            catch (Exception ex)
            {
                return new SumUpToken
                {
                    success = false,
                    error_message = ex.Message
                };
            }
        }
    }
}
