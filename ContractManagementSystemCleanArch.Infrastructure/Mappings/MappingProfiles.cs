using AutoMapper;
using CMS.Application.DTOs.Request;
using CMS.Domain.Entities;

namespace CMS.Infrastructure.Mappings
{
    // AutoMapper profile class to define mappings between domain entities and DTOs
    public class MappingProfile : Profile
    {
        // Constructor to define mappings
        public MappingProfile()
        {
            // Map Negotiation entity to NegotiationDto and vice versa
            CreateMap<Negotiation, NegotiationsDto>().ReverseMap();
        }
    }
}
