namespace ProposalRequestLogging.Models.Concrete
{
    public class ProposalRequest
    {
        public ProposalRequest()
        {
            Authentication = new Authentication();
            Object= new Proposal();
        }

        public Authentication Authentication { get; set; }
        public Proposal Object { get; set; }       
    }
}
