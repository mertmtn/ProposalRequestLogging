using ProposalRequestLogging.Data.Operations;
using ProposalRequestLogging.Models.Concrete;

namespace ProposalRequestLogging.Data.Abstract
{
    public interface IRequestLogsDal : IAdd<RequestLogs>
    {
        int UpdateResponseRequestLog(RequestLogs entity);
    }
}
