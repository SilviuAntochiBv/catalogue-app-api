using API.Business.Test.Utilities;
using API.Domain.Dtos.Parameter;
using API.Domain.Dtos.Result;
using API.Domain.Entities;
using AutoMapper;
using Xunit;

namespace API.Business.Test.Mappers
{
    public class StudentMapperProfileShould
    {
        private IMapper _mapper;

        public StudentMapperProfileShould()
        {
            _mapper = AutoMapperUtilities.Init();
        }

        [Theory]
        [InlineData("input_FirstName", "input_LastName", 20)]
        [InlineData("input_AnotherFirstName", "input_AnotherLastName", 35)]
        public void MapStudentInputDtoToStudentEntity(
            string firstName,
            string lastName,
            short age)
        {
            // arrange
            var inputDto = new StudentInputDto
            {
                FirstName = firstName,
                LastName = lastName,
                Age = age
            };

            // act 
            var result = _mapper.Map<StudentInputDto, Student>(inputDto);

            // assert
            Assert.Equal(firstName, result.FirstName);
            Assert.Equal(lastName, result.LastName);
            Assert.Equal(age, result.Age);
            Assert.Equal(0, result.Id);
            Assert.Null(result.AssociatedClass);
        }

        [Theory]
        [InlineData(1, "result_FirstName", "result_LastName", 20)]
        [InlineData(2, "result_AnotherFirstName", "result_AnotherLastName", 35)]
        public void MapStudentEntityToStudentResultDto(
            int id,
            string firstName,
            string lastName,
            short age)
        {
            // arrange
            var entity = new Student
            {
                Id = id,
                FirstName = firstName,
                LastName = lastName,
                Age = age
            };

            // act 
            var result = _mapper.Map<Student, StudentResultDto>(entity);

            // assert
            Assert.Equal(firstName, result.FirstName);
            Assert.Equal(lastName, result.LastName);
            Assert.Equal(age, result.Age);
            Assert.Equal(id, result.Id);
        }
    }
}
