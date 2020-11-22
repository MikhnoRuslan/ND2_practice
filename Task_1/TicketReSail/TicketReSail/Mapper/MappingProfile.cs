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
            CreateMap<Venue, VenueDTO>();

            CreateMap<Category, CategoryDTO>()
                .ForMember(cDTO => cDTO.Id, opt => opt.MapFrom(c => c.Id))
                .ForMember(cDTO => cDTO.Name, opt => opt.MapFrom(c => c.Name))
                .ForAllOtherMembers(opt => opt.Ignore());

            CreateMap<Event, EventResource>()
                .ForMember(eDTO => eDTO.Id, opt => opt.MapFrom(e => e.Id))
                .ForMember(eDTO => eDTO.Name, opt => opt.MapFrom(e => e.Name))
                .ForMember(eDTO => eDTO.DateTime, opt => opt.MapFrom(e => e.Date))
                .ForMember(eDTO => eDTO.Description, opt => opt.MapFrom(e => e.Description))
                .ForMember(eDTO => eDTO.CategoryId, opt => opt.MapFrom(e => e.CategoryId))
                .ForMember(eDTO => eDTO.VenueId, opt => opt.MapFrom(e => e.VenueId))
                .ForMember(eDTO => eDTO.Category, opt => opt.MapFrom(e => e.Category))
                .ForMember(eDTO => eDTO.Venue, opt => opt.MapFrom(e => e.Venue))
                .ForAllOtherMembers(opt => opt.Ignore());
        }
    }
}