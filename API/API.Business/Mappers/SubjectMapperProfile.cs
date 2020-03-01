using API.Domain.Dtos.Parameter;
using API.Domain.Dtos.Result;
using API.Domain.Entities;
using AutoMapper;

namespace API.Business.Mappers
{
    public sealed class SubjectMapperProfile : Profile
    {
        public SubjectMapperProfile()
        {
            CreateMap<SubjectInputDto, Subject>()
                .ForMember(entity => entity.Id, opt => opt.Ignore())
                .ForMember(entity => entity.Name, opt => opt.MapFrom(dto => dto.Name))
                .ForMember(entity => entity.Description, opt => opt.MapFrom(dto => dto.Description));

            CreateMap<Subject, SubjectResultDto>()
                .ForMember(dto => dto.Id, opt => opt.MapFrom(entity => entity.Id))
                .ForMember(dto => dto.Name, opt => opt.MapFrom(entity => entity.Name))
                .ForMember(dto => dto.Description, opt => opt.MapFrom(entity => entity.Description));
                
        }
    }
}
