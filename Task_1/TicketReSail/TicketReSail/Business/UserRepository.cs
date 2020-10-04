using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Localization;
using TicketReSail.Business.Model;

namespace TicketReSail.Business
{
    public class UserRepository
    {
        private readonly List<User> _users;
        private readonly IStringLocalizer<UserRepository> _localizer;

        public UserRepository(IStringLocalizer<UserRepository> localizer)
        {
            _localizer = localizer;

            _users = new List<User>
            {
                new User{Id = 1, Login = "Alex90", Password = "alex", Role = Constants.User, FirstName = "Alex", LastName = "Ivanov", Address = "Brest, st. Gogolya 45", Localization = "ru", PhoneNumber = 375112561065},
                new User{Id = 2, Login = "Bob123", Password = "bob", Role = Constants.User, FirstName = "Bob", LastName = "Petrov", Address = "Minst, st. Popova 34", Localization = "by", PhoneNumber = 4419845621},
                new User{Id = 3, Login = "Admin", Password = "Admin", Role = Constants.Administrator, FirstName = "Mike", LastName = "Ostrov", Address = "Pinsk, st. Sovetskaja 11", Localization = "pl", PhoneNumber = 274563215488},
            };
        }

        public bool ValidatePassword(string login, string password)
        {
            var user = _users.SingleOrDefault(u => u.Login.ToLower().Equals(login.ToLower()));

            if (user != null)
            {
                return user.Password.Equals(password);
            }

            throw new ArgumentException(_localizer["User not found"].Value);
        }

        public string GetRole(string login) => _users
            .SingleOrDefault(u => u.Login.ToLower().Equals(login.ToLower()))
            ?.Role;

        public User[] GetUsers()
        {
            return _users.ToArray();
        }

        public User[] GetUserById(int userId)
        {
            return _users.ToArray().Where(u => u.Id == userId).ToArray();
        }
    }
}