using System.Threading.Tasks;
using API.Business.Implementation.Specific;
using API.Business.Interfaces;
using API.Data.Interfaces.Specific;
using API.Domain.Dtos.Parameter;
using API.Domain.Entities;
using Xunit;
using Moq;
using FluentValidation.Results;

namespace API.Business.Test.Implementation
{
    public class StudentServiceShould : ServiceShould<IStudentService, Student, IStudentRepository>
    {
        private readonly IStudentService _service;

        private readonly StudentInputDto _defaultInputDto;

        public StudentServiceShould()
        {
            _defaultInputDto = new StudentInputDto();

            SetupDefaultMocks();

            _service = new StudentService(UnitOfWorkMock.Object, ValidatorMock.Object, MapperInstance, LoggerMock.Object);
        }

        #region Mocks

        private void SetupDefaultMocks()
        {
            SetupUnitOfWorkMock();

            SetupValidatorMock();
        }

        private void SetupUnitOfWorkMock()
        {
            UnitOfWorkMock
                .Setup(uow => uow.GetRepository<IStudentRepository>())
                .Returns(RepositoryMock.Object);
        }

        private void SetupValidatorMock()
        {
            ValidatorMock
                .Setup(validator => validator.Validate(It.IsAny<Student>()))
                .Returns(ValidationResultMock.Object);

            SetupValidationResultMock();
        }

        private void SetupValidationResultMock(bool isValid = true)
        {
            ValidationResultMock
                .SetupGet(result => result.IsValid)
                .Returns(isValid);
        }

        #endregion

        [Fact]
        public async Task CallRepositoryWhenCallingAdd()
        {
            // act
            await _service.Add(_defaultInputDto);

            // assert
            RepositoryMock
                .Verify(repo => repo.Add(It.IsAny<Student>()), Times.Once);
        }

        [Fact]
        public async Task ReturnValidResponseWhenValidationPasses()
        {
            // act 
            var result = await _service.Add(_defaultInputDto);

            // assert
            Assert.True(result.IsValid);
        }

        [Fact]
        public async Task ReturnInvalidResponseWhenValidationFails()
        {
            // arrange
            SetupValidationResultMock(false);

            // act 
            var result = await _service.Add(_defaultInputDto);

            // assert
            Assert.False(result.IsValid);
        }
    }
}
