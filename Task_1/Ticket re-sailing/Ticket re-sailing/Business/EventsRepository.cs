using System;
using System.Collections.Generic;
using System.Linq;
using Ticket_re_sailing.Business.Model;

namespace Ticket_re_sailing.Business
{
    public class EventsRepository
    {
        private readonly List<Category> _categories;
        private readonly List<City> _cities;
        private readonly List<Venue> _venues;
        private readonly List<Event> _events;

        public EventsRepository()
        {
            _categories = new List<Category>
            {
                new Category{Id = 1, Name = "Film"},
                new Category{Id = 2, Name = "Concert"},
                new Category{Id = 3, Name = "Party"},
                new Category{Id = 4, Name = "Exhibition"}
            };

            _cities = new List<City>
            {
                new City{Id = 1, Name = "Brest"},
                new City{Id = 2, Name = "Minsk"},
                new City{Id = 3, Name = "Grodno"},
                new City{Id = 4, Name = "Vitebsk"},
                new City{Id = 5, Name = "Mogilev"},
                new City{Id = 6, Name = "Gomel"},
            };

            _venues = new List<Venue>
            {
                new Venue{ Id = 1, Name = "Cinema \"Mir\"", Address = "st. Pushkinskaya 4", City = _cities[0]},
                new Venue{ Id = 2, Name = "Cinema \"Bolshoj\"", Address = "st. Sovetskaya 25", City = _cities[0]},

                new Venue{ Id = 3, Name = "Cinema \"Silver\"", Address = "st. Bobruyskaya 6", City = _cities[1]},
                new Venue{ Id = 4, Name = "Cinema \"V Zamke\"", Address = "ave. Pobediteley 10", City = _cities[1]},

                new Venue{ Id = 5, Name = "Cinema \"Oktyabr\"", Address = "st. Popovicha 3", City = _cities[2]},
                new Venue{ Id = 6, Name = "Cinema \"Kosmos\"", Address = "st. Maksima Gorkogo 49", City = _cities[2]},

                new Venue{ Id = 7, Name = "Cinema \"Dom Kino\"", Address = "st. Lenina 40", City = _cities[3]},
                new Venue{ Id = 8, Name = "Cinema \"Drive\"", Address = "st. Tereshkovoy 3a-30", City = _cities[3]},

                new Venue{ Id = 9, Name = "Cinema \"Rodina\"", Address = "st. Mira 23", City = _cities[4]},
                new Venue{ Id = 10, Name = "Cinema \"Zvezda\"", Address = "st. Pervomayskaya 14", City = _cities[4]},

                new Venue{ Id = 11, Name = "Cinema \"Oktyabr 3D\"", Address = "st. Barikina 127", City = _cities[5]},
                new Venue{ Id = 12, Name = "Cinema \"Oktyabr\"", Address = "st. Barikina 127", City = _cities[5]},
            };

            _events = new List<Event>
            {
                new Event{Id = 1, Category = _categories[0], Name = "Home Alone",
                    Date = new DateTime(2020, 10, 9, 19, 0,0), Venue = _venues[0], Banner = "Home Alone.jpg", Description = "lala"},
                new Event{Id = 2, Category = _categories[0], Name = "The Silence of the Lambs",
                    Date = new DateTime(2020,10,3, 19, 30,0), Venue = _venues[1], Banner = "The Silence of the Lambs.jpg", Description = "lala"},
                new Event{Id = 3, Category = _categories[0], Name = "Venom",
                    Date = new DateTime(2020, 11,2, 20,00,00), Venue = _venues[2], Banner = "Venom.jpg", Description = "lala"},

                new Event{Id = 4, Category = _categories[1], Name = "Stand Up",
                    Date = new DateTime(2020,10,24,20,0,0), Venue = _venues[3], Banner = "StendUp.jpg", Description = "lala"},
                new Event{Id = 5, Category = _categories[1], Name = "Zivert",
                    Date = new DateTime(2020,11,5,19,30,0), Venue = _venues[4], Banner = "Zivert.jpg", Description = "lala"},
                new Event{Id = 6, Category = _categories[1], Name = "Live Lounge",
                    Date = new DateTime(2020, 10,25,21,00,00), Venue = _venues[5], Banner = "LiveLounge.jpg", Description = "lala"},

                new Event{Id = 7, Category = _categories[2], Name = "Brooklyn Live!",
                    Date = new DateTime(2020,9,19,21,0,0), Venue = _venues[6], Banner = "BrooklynLive.jpg", Description = "lala"},
                new Event{Id = 8, Category = _categories[2], Name = "Relax party",
                    Date = new DateTime(2020,11,1,22,00,00), Venue = _venues[7], Banner = "RelaxParty.jpg", Description = "lala"},
                new Event{Id = 9, Category = _categories[2], Name = "Electronic Music",
                    Date = new DateTime(2020,9,19,23,00,00), Venue = _venues[8], Banner = "ElectronicMusic.jpg", Description = "lala"},

                new Event{Id = 10, Category = _categories[3], Name = "ZooWorld",
                    Date = new DateTime(2020,10,23), Venue = _venues[9], Banner = "ZooWorld.jpg", Description = "lala"},
                new Event{Id = 11, Category = _categories[3], Name = "Cat and Autumn",
                    Date = new DateTime(2020,10,23), Venue = _venues[10], Banner = "CatAndAutumn.jpg", Description = "lala"},
                new Event{Id = 12, Category = _categories[3], Name = "Motherhood, childhood",
                    Date = new DateTime(2020,11,11), Venue = _venues[11], Banner = "MotherhoodAndChildhood.jpg", Description = "lala"},
            };
        }

        public Category[] GetCategories()
        {
            return _categories.ToArray();
        }

        public City[] GetCities()
        {
            return _cities.ToArray();
        }

        public Venue[] GetVenues()
        {
            return _venues.ToArray();
        }

        public Event[] GetEvents()
        {
            return _events.ToArray();
        }

        public Event GetEventsById(int id)
        {
            return _events.FirstOrDefault(e => e.Id.Equals(id));
        }
    }
}
