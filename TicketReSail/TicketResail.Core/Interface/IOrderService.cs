using System.Collections.Generic;
using System.Threading.Tasks;
using TicketReSail.DAL.Model;

namespace TicketReSail.Core.Interface
{
    public interface IOrderService
    {
        Task<IEnumerable<Order>> GetOrdersForBuy(string status, string userName);
        Task<IEnumerable<Order>> GetOrdersForSell(string status, string userName);
        Task ChangeStatusToSoldForSeller(int ticketId, string buyerId);
        string GetStatusByTicketId(int id);
    }
}
