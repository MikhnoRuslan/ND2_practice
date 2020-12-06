using System.Threading.Tasks;
using TicketReSail.DAL.Model;

namespace TicketReSail.Core.Interface
{
    public interface IUserService
    {
        Task<User> GetUserById(string id);
        string GetUserIdByName(string name);
        string GetUserNameById(string id);
        string GetLoginByUserName(string userName);
        string GetFirstNameById(string id);
        string GetLastNameById(string id);
        string GetAddressById(string id);
        string GetPhoneNumberById(string id);
    }
}
