using API.Business.Test.Utilities;
using API.Domain.Dtos.Parameter;
using API.Domain.Dtos.Result;
using API.Domain.Entities;
using AutoMapper;
using Xunit;

namespace API.Business.Test.Mappers
{
    public class SubjectMapperProfileShould
    {
        private IMapper _mapper;

        public SubjectMapperProfileShould()
        {
            _mapper = AutoMapperUtilities.MapperInstance;
        }

        [Theory]
        [InlineData("input_Name", "input_Description")]
        [InlineData("input_AnotherName", "input_AnotherDescription")]
        public void MapSubjectInputDtoToSubjectEntity(
            string name,
            string description)
        {
            // arrange
            var inputDto = new SubjectInputDto
            {
                Name = name,
                Description = description,
            };

            // act 
            var result = _mapper.Map<SubjectInputDto, Subject>(inputDto);

            // assert
            Assert.Equal(name, result.Name);
            Assert.Equal(description, result.Description);
            Assert.Equal(0, result.Id);
            Assert.Null(result.AssociatedCourses);
            Assert.Null(result.Teachers);
        }

        [Theory]
        [InlineData(1, "result_Name", "result_Description")]
        [InlineData(2, "result_AnotherName", "result_AnotherDescription")]
        public void MapStudentEntityToStudentResultDto(
            int id,
            string name,
            string description)
        {
            // arrange
            var entity = new Subject
            {
                Id = id,
                Name = name,
                Description = description
            };

            // act 
            var result = _mapper.Map<Subject, SubjectResultDto>(entity);

            // assert
            Assert.Equal(name, result.Name);
            Assert.Equal(description, result.Description);
            Assert.Equal(id, result.Id);
        }
    }
}
