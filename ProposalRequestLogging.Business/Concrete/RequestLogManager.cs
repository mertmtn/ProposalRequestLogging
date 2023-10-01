using Microsoft.Extensions.Configuration;
using ProposalRequestLogging.Business.Abstract;
using ProposalRequestLogging.Data.Abstract;
using ProposalRequestLogging.Models.Concrete;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace ProposalRequestLogging.Business.Concrete
{
    public class RequestLogManager : RequestLogBase, IRequestLogService
    {
        private IConfiguration _configuration;
        private readonly IRequestLogsDal _proposalDal;
        private readonly IHttpClientFactory _httpClientFactory;

        public RequestLogManager(IRequestLogsDal proposalDal, IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _proposalDal = proposalDal;
            _configuration = configuration;
        }

        public async Task<ProposalResponse> AddRequestLog(ProposalRequest request)
        {
            var requestLog = new RequestLogs();
            var proposalResponse = new ProposalResponse();

            try
            {
                requestLog.ProductNo = request.Object.ProductNo ?? "0";
                requestLog.RenewalNo = request.Object.RenewalNo;
                requestLog.EndorsNo = request.Object.EndorsNo;
                requestLog.ProposalNo = request.Object.ProposalNo;
                requestLog.LogId = _proposalDal.Add(requestLog);

                var apiUrl = _configuration.GetSection("ApiInformations").GetSection("SampleEngineUrl").Value;
                var clientName = _configuration.GetSection("ApiInformations").GetSection("HttpClientName").Value;
                var responseString = await HttpClientPostAsync(apiUrl, clientName, request, _httpClientFactory);

                if (!string.IsNullOrWhiteSpace(responseString))
                {
                    requestLog.Response = responseString;
                    _proposalDal.UpdateResponseRequestLog(requestLog);
                    proposalResponse = JsonSerializer.Deserialize<ProposalResponse>(requestLog.Response);
                }
            }
            catch
            {
                //TODO: Exception loglama sistemi yapılacak.
            }

            return proposalResponse;
        }

    }
}
