using System;
using System.Net.Http;

namespace MondayAPIV2_BasicExample
{
    public class MondayClient : HttpClient
    {
        private string _apiToken;
        private string _apiRoot;

        public MondayClient(string token, string rootUri)     
        {
            _apiToken = token;
            _apiRoot = rootUri;

            this.BaseAddress = new Uri(_apiRoot);

            // set the HTTP header 'Authorization' to the API token
            if (!string.IsNullOrWhiteSpace(_apiToken))
                this.DefaultRequestHeaders.Authorization = System.Net.Http.Headers.AuthenticationHeaderValue.Parse(_apiToken);

            // clear any old accept headers and add JSON type
            this.DefaultRequestHeaders.Accept.Clear();
            this.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        }
    }
}
