using System.Threading.Tasks;
using TicketReSail.Core.Infrastructure;


namespace TicketReSail.Core.Interface
{
    public interface IAction<T> where T : class
    {
        Task<OperationDetails> Create(T modelDto);
        Task Delete(int id);
    }
}
