using ProposalRequestLogging.Models.Abstract;

namespace ProposalRequestLogging.Models.Concrete
{
    public class RequestLogs : IEntity
    {
        public int LogId { get; set; }
        public decimal ProposalNo { get; set; }
        public int EndorsNo { get; set; }
        public int RenewalNo { get; set; }
        public string ProductNo { get; set; }
        public string Response { get; set; }
    }
}
