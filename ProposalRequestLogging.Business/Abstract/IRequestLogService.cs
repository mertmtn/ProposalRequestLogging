using ProposalRequestLogging.Models.Concrete;
using System.Threading.Tasks;

namespace ProposalRequestLogging.Business.Abstract
{
    public interface IRequestLogService
    {
        Task<ProposalResponse> AddRequestLog(ProposalRequest request);
    }
}
