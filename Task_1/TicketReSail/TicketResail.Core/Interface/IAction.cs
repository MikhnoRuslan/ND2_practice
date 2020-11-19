using System.Threading.Tasks;
using TicketReSail.Core.Infrastructure;
using TicketReSail.DAL.Model;


namespace TicketReSail.Core.Interface
{
    public interface IAction<in T1, T2> 
        where T1 : class
        where T2 : class
    {
        Task<OperationDetails> Create(T1 modelDto);
        Task<T2> Delete(int id);
    }
}
