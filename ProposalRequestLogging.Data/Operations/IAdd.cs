using ProposalRequestLogging.Models.Abstract;

namespace ProposalRequestLogging.Data.Operations
{
    public interface IAdd<T> where T : class, IEntity, new()
    {
        int Add(T entity);
    }
}
