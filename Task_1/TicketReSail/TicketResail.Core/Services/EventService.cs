using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TicketReSail.Core.Infrastructure;
using TicketReSail.Core.Interface;
using TicketReSail.Core.ModelDTO;
using TicketReSail.DAL;
using TicketReSail.DAL.Model;

namespace TicketReSail.Core.Services
{
    public class EventService : IEventService, IAction<EventDTO>
    {
        private readonly TicketsContext _context;
        private readonly IVenueService _venueService;
        private readonly ITickerService _tickerService;

        public EventService(TicketsContext context, IVenueService venueService, ITickerService tickerService)
        {
            _context = context;
            _venueService = venueService;
            _tickerService = tickerService;
        }

        public async Task<Event> GetEventById(int id)
        {
            return await _context.Events.FindAsync(id);
        }

        public async Task<IEnumerable<Event>> GetEvents()
        {
            return await _context.Events.ToListAsync();
        }

        public string GetEventNameByTicketId(int ticketId)
        {
            var eventId = _tickerService.GetEventIdByTicketId(ticketId);
            var @event = _context.Events.Find(eventId);

            return @event.Name;
        }

        public async Task<IEnumerable<Ticket>> GetTicketsByIdEvents(int id)
        {
            var tickets = _context.Tickets.Where(t => t.Event.Id.Equals(id));
            return await tickets.ToListAsync();
        }

        public async Task<OperationDetails> Create(EventDTO modelDto)
        {
            var @event = new Event
            {
                Name = modelDto.Name,
                CategoryId = modelDto.CategoryId,
                Date = modelDto.DateTime,
                Description = modelDto.Description,
                VenueId = modelDto.VenueId,
                Banner = ConvertImageToByte(modelDto)
            };

            var eventName = _context.Events.FirstOrDefault(e => e.Name.Equals(@event.Name));
            var eventDataTime = _context.Events.FirstOrDefault(e => e.Date == @event.Date);
            var venueName = _venueService.GetNameVenueById(@event.VenueId);
            var cityName = _venueService.GetCityNameByVenueId(@event.VenueId);

            if (eventName == null || eventDataTime == null || venueName == null || cityName == null)
            {
                await _context.Events.AddAsync(@event);
                await _context.SaveChangesAsync();

                return new OperationDetails(true, "The event is successfully created", string.Empty);
            }
            else 
            {
                return new OperationDetails(false, $"An event with the name {@event.Name} already exist", "Name");
            }
        }

        public async Task Delete(int id)
        {
            var @event = await _context.Events.FindAsync(id);
            if (@event != null)
                _context.Events.Remove(@event);

            await _context.SaveChangesAsync();
        }

        private byte[] ConvertImageToByte(EventDTO modeDto)
        {
            var @event = new Event();
            if (modeDto.Banner != null)
            {
                byte[] imageInByte;
                using (var binaryReader = new BinaryReader(modeDto.Banner.OpenReadStream()))
                {
                    imageInByte = binaryReader.ReadBytes((int) modeDto.Banner.Length);
                }

                @event.Banner = imageInByte;

                return @event.Banner;
            }

            return default;
        }
    }
}
