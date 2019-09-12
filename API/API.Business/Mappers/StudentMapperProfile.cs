using API.Domain.Dtos.Parameter;
using API.Domain.Dtos.Result;
using API.Domain.Entities;
using AutoMapper;

namespace API.Business.Mappers
{
    public class StudentMapperProfile : Profile
    {
        public StudentMapperProfile()
        {
            CreateMap<StudentInputDto, Student>()
                .ForMember(entity => entity.FirstName, mappingOptions => mappingOptions.MapFrom(dto => dto.FirstName))
                .ForMember(entity => entity.LastName, mappingOptions => mappingOptions.MapFrom(dto => dto.LastName))
                .ForMember(entity => entity.Age, mappingOptions => mappingOptions.MapFrom(dto => dto.Age))
                .ForMember(entity => entity.Id, mappingOptions => mappingOptions.Ignore())
                .ForMember(entity => entity.AssociatedClass, mappingOptions => mappingOptions.Ignore());

            CreateMap<Student, StudentResultDto>()
                .ForMember(dto => dto.FirstName, mappingOptions => mappingOptions.MapFrom(entity => entity.FirstName))
                .ForMember(dto => dto.LastName, mappingOptions => mappingOptions.MapFrom(entity => entity.LastName))
                .ForMember(dto => dto.Age, mappingOptions => mappingOptions.MapFrom(entity => entity.Age))
                .ForMember(dto => dto.Id, mappingOptions => mappingOptions.MapFrom(entity => entity.Id));
        }
    }
}
