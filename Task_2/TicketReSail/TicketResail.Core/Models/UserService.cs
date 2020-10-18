using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TicketReSail.Core.Interface;
using TicketReSail.DAL;
using TicketReSail.DAL.Model;
using Microsoft.Extensions.Localization;

namespace TicketReSail.Core.Models
{
    public class UserService : IUserService
    {
        private readonly TicketsContext _context;
        private readonly IStringLocalizer<UserService> _localizer;

        public UserService(TicketsContext context, IStringLocalizer<UserService> localizer)
        {
            _context = context;
            _localizer = localizer;
        }

        public async Task<IEnumerable<User>> GetUserById(int id)
        {
            var users = _context.Users.Where(u => u.Id == id);
            return await users.ToListAsync();
        }

        public string GetRole(string login)
        {
            return _context.Users
                .SingleOrDefault(u => u.Login.ToLower().Equals(login.ToLower()))
                ?.Role;
        }

        public bool ValidatePassword(string login, string password)
        {
            var user = _context.Users.SingleOrDefault(u => u.Login.ToLower().Equals(login.ToLower()));

            if (user != null)
            {
                return user.Password.Equals(password);
            }

            throw new ArgumentException(_localizer["User not found"].Value);
        }
    }
}
