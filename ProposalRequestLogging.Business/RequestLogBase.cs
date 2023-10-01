using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ProposalRequestLogging.Business
{
    public class RequestLogBase
    {
        public async Task<string> HttpClientPostAsync<TRequest>(string url, string clientName, TRequest request, IHttpClientFactory httpClientFactory)
        {
            var body = JsonSerializer.Serialize(request);
            var httpClient = httpClientFactory.CreateClient(clientName);
            var httpResponse = await httpClient.PostAsync(url, new StringContent(body, Encoding.UTF8, "application/json"));
            httpResponse.EnsureSuccessStatusCode();
            var responseData = await httpResponse.Content.ReadAsStringAsync();    
            return responseData;
        } 
    }
}
