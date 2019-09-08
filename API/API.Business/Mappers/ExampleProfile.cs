using AutoMapper;
using API.Domain.Dtos.Parameter;
using API.Domain.Entities;

namespace API.Business.Mappers
{
    public class ExampleProfile : Profile
    {
        public ExampleProfile()
        {
            CreateMap<BaseEntity<long>, BaseEntityDto>()
                .ForMember(
                    dto => dto.InternalId,
                    opt => opt.MapFrom(entity => entity.Id));
        }
    }
}
