using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using TicketReSail.Core.Interface;
using TicketReSail.DAL;
using TicketReSail.DAL.Model;

namespace TicketReSail
{
    public class DataSeeder
    {
        private readonly TicketsContext _context;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ILocalizationService _localization;
        private readonly List<Category> _categories;
        private readonly List<City> _cities;
        private readonly List<Venue> _venues;
        private readonly List<Event> _events;
        private readonly List<User> _users;
        private readonly List<Ticket> _tickets;

        public DataSeeder(UserManager<User> userManager,
            RoleManager<IdentityRole> roleManager, ILocalizationService localization, TicketsContext context)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _localization = localization;
            _context = context;

            _categories = new List<Category>
            {
                new Category {Name = "Film"},
                new Category {Name = "Concert"},
                new Category {Name = "Party"},
                new Category {Name = "Exhibition"}
            };

            _cities = new List<City>
            {
                new City {Name = "Brest"},
                new City {Name = "Minsk"},
                new City {Name = "Grodno"},
                new City {Name = "Gomel"},
                new City {Name = "Mogilev"},
                new City {Name = "Vitebsk"},
            };

            _venues = new List<Venue>
            {
                new Venue {Name = "Cinema \"Mir\"", Address = "st. Pushkinskaya 4", City = _cities[0]},
                new Venue {Name = "Cinema \"Bolshoj\"", Address = "st. Sovetskaya 25", City = _cities[0]},

                new Venue {Name = "Cinema \"Silver\"", Address = "st. Bobruyskaya 6", City = _cities[1]},
                new Venue {Name = "Cinema \"V Zamke\"", Address = "ave. Pobediteley 10", City = _cities[1]},

                new Venue {Name = "Cinema \"Oktyabr\"", Address = "st. Popovicha 3", City = _cities[2]},
                new Venue {Name = "Cinema \"Kosmos\"", Address = "st. Maksima Gorkogo 49", City = _cities[2]},

                new Venue {Name = "Cinema \"Dom Kino\"", Address = "st. Lenina 40", City = _cities[3]},
                new Venue {Name = "Cinema \"Drive\"", Address = "st. Tereshkovoy 3a-30", City = _cities[3]},

                new Venue {Name = "Cinema \"Rodina\"", Address = "st. Mira 23", City = _cities[4]},
                new Venue {Name = "Cinema \"Zvezda\"", Address = "st. Pervomayskaya 14", City = _cities[4]},

                new Venue {Name = "Cinema \"Oktyabr 3D\"", Address = "st. Barikina 127", City = _cities[5]},
                new Venue {Name = "Cinema \"Oktyabr\"", Address = "st. Barikina 127", City = _cities[5]},
            };

            _events = new List<Event>
            {
                new Event
                {
                    Category = _categories[0], Name = "Home Alone",
                    Date = new DateTime(2020, 10, 9, 19, 0, 0), Venue = _venues[0],
                    Banner = ConvertImageToByte("../TicketReSail/wwwroot/img/Home Alone.jpg"),
                    Description = "lala"
                },
                new Event
                {
                    Category = _categories[0], Name = "The Silence of the Lambs",
                    Date = new DateTime(2020, 10, 3, 19, 30, 0), Venue = _venues[1],
                    Banner = ConvertImageToByte("../TicketReSail/wwwroot/img/The Silence of the Lambs.jpg"), Description = "lala"
                },
                new Event
                {
                    Category = _categories[0], Name = "Venom",
                    Date = new DateTime(2020, 11, 2, 20, 00, 00), Venue = _venues[2],
                    Banner = ConvertImageToByte("../TicketReSail/wwwroot/img/Venom.jpg"),
                    Description = "lala"
                },

                new Event
                {
                    Category = _categories[1], Name = "Stand Up",
                    Date = new DateTime(2020, 10, 24, 20, 0, 0), Venue = _venues[3],
                    Banner = ConvertImageToByte("../TicketReSail/wwwroot/img/StendUp.jpg"),
                    Description = "lala"
                },
                new Event
                {
                    Category = _categories[1], Name = "Zivert",
                    Date = new DateTime(2020, 11, 5, 19, 30, 0), Venue = _venues[4],
                    Banner = ConvertImageToByte("../TicketReSail/wwwroot/img/Zivert.jpg"),
                    Description = "lala"
                },
                new Event
                {
                    Category = _categories[1], Name = "Live Lounge",
                    Date = new DateTime(2020, 10, 25, 21, 00, 00), Venue = _venues[5],
                    Banner = ConvertImageToByte("../TicketReSail/wwwroot/img/LiveLounge.jpg"),
                    Description = "lala"
                },

                new Event
                {
                    Category = _categories[2], Name = "Brooklyn Live!",
                    Date = new DateTime(2020, 9, 19, 21, 0, 0), Venue = _venues[6],
                    Banner = ConvertImageToByte("../TicketReSail/wwwroot/img/BrooklynLive.jpg"),
                    Description = "lala"
                },
                new Event
                {
                    Category = _categories[2], Name = "Relax party",
                    Date = new DateTime(2020, 11, 1, 22, 00, 00), Venue = _venues[7],
                    Banner = ConvertImageToByte("../TicketReSail/wwwroot/img/RelaxParty.jpg"),
                    Description = "lala"
                },
                new Event
                {
                    Category = _categories[2], Name = "Electronic Music",
                    Date = new DateTime(2020, 9, 19, 23, 00, 00), Venue = _venues[8],
                    Banner = ConvertImageToByte("../TicketReSail/wwwroot/img/ElectronicMusic.jpg"),
                    Description = "lala"
                },

                new Event
                {
                    Category = _categories[3], Name = "ZooWorld",
                    Date = new DateTime(2020, 10, 23), Venue = _venues[9], 
                    Banner = ConvertImageToByte("../TicketReSail/wwwroot/img/ZooWorld.jpg"), Description = "lala"
                },
                new Event
                {
                    Category = _categories[3], Name = "Cat and Autumn",
                    Date = new DateTime(2020, 10, 23), Venue = _venues[10],
                    Banner = ConvertImageToByte("../TicketReSail/wwwroot/img/CatAndAutumn.jpg"),
                    Description = "lala"
                },
                new Event
                {
                    Category = _categories[3], Name = "Motherhood, childhood",
                    Date = new DateTime(2020, 11, 11), Venue = _venues[11],
                    Banner = ConvertImageToByte("../TicketReSail/wwwroot/img/MotherhoodAndChildhood.jpg"),
                    Description = "lala"
                }
            };

            _users = new List<User>
            {
                new User
                {
                    Login = "Bob45", Email = "tralivali@gmail.com", UserName = "tralivali@gmail.com", FirstName = "Bob",
                    LastName = "Brown", LocalizationId = 1
                },
                new User
                {
                    Login = "Make11", Email = "tilitili@gmail.com", UserName = "tilitili@gmail.com", FirstName = "Mike",
                    LastName = "White", LocalizationId = 1
                },
                new User
                {
                    Login = "Tod124", Email = "lalala@gmail.com", UserName = "lalala@gmail.com", FirstName = "Tod",
                    LastName = "Black", LocalizationId = 1
                },
                new User
                {
                    Login = Constants.Administrator, Email = Constants.Email, UserName = Constants.Email, EmailConfirmed = true, LocalizationId = 1,
                    FirstName = Constants.Administrator, LastName = Constants.Administrator
                }
            };

            _tickets = new List<Ticket>
            {
                new Ticket {Event = _events[0], Price = 5m, User = _users[0], Status = Constants.Selling},
                new Ticket {Event = _events[1], Price = 7m, User = _users[0], Status = Constants.Selling},
                new Ticket {Event = _events[2], Price = 10m, User = _users[0], Status = Constants.Selling},
                new Ticket {Event = _events[3], Price = 4.5m, User = _users[1], Status = Constants.Selling},
                new Ticket {Event = _events[4], Price = 6m, User = _users[1], Status = Constants.Selling},
                new Ticket {Event = _events[5], Price = 10m, User = _users[1], Status = Constants.Selling},
                new Ticket {Event = _events[6], Price = 10m, User = _users[2], Status = Constants.Selling},
                new Ticket {Event = _events[7], Price = 10m, User = _users[2], Status = Constants.Selling},
                new Ticket {Event = _events[8], Price = 10m, User = _users[2], Status = Constants.Selling},
                new Ticket {Event = _events[9], Price = 6m, User = _users[0], Status = Constants.Selling},
                new Ticket {Event = _events[10], Price = 9m, User = _users[0], Status = Constants.Selling},
                new Ticket {Event = _events[11], Price = 11m, User = _users[0], Status = Constants.Selling},

                new Ticket {Event = _events[0], Price = 12m, User = _users[1], Status = Constants.Selling},
                new Ticket {Event = _events[1], Price = 10m, User = _users[1], Status = Constants.Selling},
                new Ticket {Event = _events[2], Price = 8m, User = _users[1], Status = Constants.Selling},
                new Ticket {Event = _events[3], Price = 8m, User = _users[2], Status = Constants.Selling},
                new Ticket {Event = _events[4], Price = 8m, User = _users[2], Status = Constants.Selling},
                new Ticket {Event = _events[5], Price = 8m, User = _users[2], Status = Constants.Selling},
                new Ticket {Event = _events[6], Price = 15m, User = _users[0], Status = Constants.Selling},
                new Ticket {Event = _events[7], Price = 16m, User = _users[0], Status = Constants.Selling},
                new Ticket {Event = _events[8], Price = 17m, User = _users[0], Status = Constants.Selling},
                new Ticket {Event = _events[9], Price = 6m, User = _users[1], Status = Constants.Selling},
                new Ticket {Event = _events[10], Price = 8m, User = _users[1], Status = Constants.Selling},
                new Ticket {Event = _events[11], Price = 10m, User = _users[1], Status = Constants.Selling},

                new Ticket {Event = _events[0], Price = 11m, User = _users[2], Status = Constants.Selling},
                new Ticket {Event = _events[1], Price = 11m, User = _users[2], Status = Constants.Selling},
                new Ticket {Event = _events[2], Price = 11m, User = _users[2], Status = Constants.Selling},
                new Ticket {Event = _events[3], Price = 6m, User = _users[0], Status = Constants.Selling},
                new Ticket {Event = _events[4], Price = 7m, User = _users[0], Status = Constants.Selling},
                new Ticket {Event = _events[5], Price = 8m, User = _users[0], Status = Constants.Selling},
                new Ticket {Event = _events[6], Price = 9m, User = _users[1], Status = Constants.Selling},
                new Ticket {Event = _events[7], Price = 10m, User = _users[1], Status = Constants.Selling},
                new Ticket {Event = _events[8], Price = 11m, User = _users[1], Status = Constants.Selling},
                new Ticket {Event = _events[9], Price = 11m, User = _users[2], Status = Constants.Selling},
                new Ticket {Event = _events[10], Price = 11m, User = _users[2], Status = Constants.Selling},
                new Ticket {Event = _events[11], Price = 11m, User = _users[2], Status = Constants.Selling},
            };
        }

        public async Task SeedDataAsync()
        {
            await _localization.AddLocalization();

            if (await _roleManager.FindByNameAsync(Constants.Administrator) == null)
                await _roleManager.CreateAsync(new IdentityRole(Constants.Administrator));

            if (await _roleManager.FindByNameAsync(Constants.Moderator) == null)
                await _roleManager.CreateAsync(new IdentityRole(Constants.Moderator));

            if (await _roleManager.FindByNameAsync(Constants.Employee) == null)
                await _roleManager.CreateAsync(new IdentityRole(Constants.Employee));

            if (await _userManager.FindByNameAsync(Constants.Email) == null)
            {
                var result = await _userManager.CreateAsync(_users[3], Constants.Password);
                if (result.Succeeded)
                    await _userManager.AddToRoleAsync(_users[3], Constants.Administrator);
            }

            if (await _userManager.FindByNameAsync("tralivali@gmail.com") == null)
            {
                var result = await _userManager.CreateAsync(_users[0], Constants.Password);
                if (result.Succeeded)
                    await _userManager.AddToRoleAsync(_users[0], Constants.Employee);
            }

            if (await _userManager.FindByNameAsync("tilitili@gmail.com") == null)
            {
                var result = await _userManager.CreateAsync(_users[1], Constants.Password);
                if (result.Succeeded)
                    await _userManager.AddToRoleAsync(_users[1], Constants.Employee);
            }

            if (await _userManager.FindByNameAsync("lalala@gmail.com") == null)
            {
                var result = await _userManager.CreateAsync(_users[2], Constants.Password);
                if (result.Succeeded)
                    await _userManager.AddToRoleAsync(_users[2], Constants.Employee);
            }

            if (!_context.Categories.Any())
            {
                await _context.Categories.AddRangeAsync(_categories);
            }

            if (!_context.Tickets.Any())
            {
                await _context.Tickets.AddRangeAsync(_tickets);
            }

            if (!_context.Events.Any())
            {
                await _context.Events.AddRangeAsync(_events);
            }

            if (!_context.Cities.Any())
            {
                await _context.Cities.AddRangeAsync(_cities);
            }

            if (!_context.Venues.Any())
            {
                await _context.Venues.AddRangeAsync(_venues);
            }

            await _context.SaveChangesAsync();
        }
        
        private byte[] ConvertImageToByte(string path)
        {
            var imgData = File.ReadAllBytes(path);
            return imgData;
        }
    }
}
