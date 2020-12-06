using System.Linq;
using System.Threading.Tasks;
using TicketReSail.Core.Interface;
using TicketReSail.DAL;
using TicketReSail.DAL.Model;

namespace TicketReSail.Core.Services
{
    public class UserService : IUserService
    {
        private readonly TicketsContext _context;

        public UserService(TicketsContext context)
        {
            _context = context;
        }

        public async Task<User> GetUserById(string id)
        {
            return await _context.Users.FindAsync(id);
        }

        public string GetUserIdByName(string name)
        {
            var user = _context.Users.FirstOrDefault(u => u.UserName.Equals(name));
            return user.Id;
        }

        public string GetUserNameById(string id)
        {
            var user = _context.Users.Find(id);

            return user.Login;
        }

        public string GetLoginByUserName(string userName)
        {
            var user = _context.Users.FirstOrDefault(u => u.UserName.Equals(userName));

            return user.Login;
        }

        public string GetFirstNameById(string id)
        {
            var user = _context.Users.Find(id);

            return user.FirstName;
        }

        public string GetLastNameById(string id)
        {
            var user = _context.Users.Find(id);

            return user.LastName;
        }

        public string GetAddressById(string id)
        {
            var user = _context.Users.Find(id);

            return user.Address;
        }

        public string GetPhoneNumberById(string id)
        {
            var user = _context.Users.Find(id);

            return user.PhoneNumber;
        }
    }
}
