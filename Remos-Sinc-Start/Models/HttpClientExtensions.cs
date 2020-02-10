using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Remos_Sinc_Start.Models
{
    public static class HttpClientExtensions
    {
        public static HttpClient AddTokenToHeader(this HttpClient client, object token, string urlAPI)
        {
            string contentType = "application/json";
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(contentType));
            client.DefaultRequestHeaders.Add("Authorization", String.Format("Bearer {0}", token));
            client.BaseAddress = new Uri(urlAPI);
            var userAgent = "d-fens HttpClient";
            client.DefaultRequestHeaders.Add("User-Agent", userAgent);
            return client;
        }
    }

    public class UserLogin
    {
        public string Login { get; set; }
        public string Password { get; set; }
    }
}
