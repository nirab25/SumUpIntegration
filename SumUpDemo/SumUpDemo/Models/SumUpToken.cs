namespace SumUpDemo.Models
{
    public class SumUpToken
    {
        public string access_token { get; set; }
        public string token_type { get; set; }
        public int expires_in { get; set; }
        public bool success { get; set; }
        public string error_message { get; set; }
        public string error_code { get; set; }
    }
}
