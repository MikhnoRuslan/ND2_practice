using System.Collections.Generic;
using System.Threading.Tasks;
using TicketReSail.Core.Infrastructure;
using TicketReSail.DAL.Model;

namespace TicketReSail.Core.Interface
{
    public interface IOrderService
    {
        Task<IEnumerable<Order>> GetOrdersForBuy(string status, string userName);
        Task<IEnumerable<Order>> GetOrdersForSell(string status, string userName);
        Task ChangeStatusToSoldForSeller(int ticketId, string sellerId, string buyerId);
        Order GetOrderByTicketId(int ticketId);
        Task SetStatusForOrderSeller(int ticketId, string sellerId);
    }
}
