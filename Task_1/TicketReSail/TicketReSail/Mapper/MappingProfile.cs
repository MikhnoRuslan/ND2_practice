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

            CreateMap<Category, CategoryDTO>()
                .ForMember(cDTO => cDTO.Id, opt => opt.MapFrom(c => c.Id))
                .ForMember(cDTO => cDTO.Name, opt => opt.MapFrom(c => c.Name))
                .ForAllOtherMembers(opt => opt.Ignore());
        }
    }
}