using AutoMapper;
using TicketReSail.Controllers.Api.Models;
using TicketReSail.Core.ModelDTO;
using TicketReSail.DAL.Model;

namespace TicketReSail.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Event, EventResource>()
                .ForMember(eDTO => eDTO.Id, opt => opt.MapFrom(e => e.Id))
                .ForMember(eDTO => eDTO.EventName, opt => opt.MapFrom(e => e.Name))
                .ForMember(eDTO => eDTO.DateTime, opt => opt.MapFrom(e => e.Date))
                .ForMember(eDTO => eDTO.Description, opt => opt.MapFrom(e => e.Description))
                .ForMember(eDTO => eDTO.CategoryId, opt => opt.MapFrom(e => e.CategoryId))
                .ForMember(eDTO => eDTO.VenueId, opt => opt.MapFrom(e => e.VenueId))
                .ForMember(eDTO => eDTO.Category, opt => opt.MapFrom(e => e.Category))
                .ForMember(eDTO => eDTO.Venue, opt => opt.MapFrom(e => e.Venue))
                .ForPath(eDTO => eDTO.Venue.City.Name, opt => opt.MapFrom(e => e.Venue.City.Name))
                .ForAllOtherMembers(opt => opt.Ignore());

            CreateMap<Venue, VenueResource>()
                .ForMember(v => v.Id, opt => opt.MapFrom(v => v.Id))
                .ForMember(v => v.Name, opt => opt.MapFrom(v => v.Name))
                .ForMember(v => v.Address, opt => opt.MapFrom(v => v.Address))
                .ForMember(v => v.CityId, opt => opt.MapFrom(v => v.CityId))
                .ForMember(v => v.City, opt => opt.MapFrom(v => v.City))
                .ForPath(v => v.City.Name, opt => opt.MapFrom(v => v.City.Name))
                .ForAllOtherMembers(opt => opt.Ignore());
        }
    }
}