using AutoMapper;
using TicketReSail.Core.ModelDTO;
using TicketReSail.DAL.Model;

namespace TicketReSail.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Venue, VenueDTO>();
        }
    }
}