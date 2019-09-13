using System.Collections.Generic;
using System.Threading.Tasks;
using API.Business.Implementation.Specific;
using API.Business.Interfaces;
using API.Data.Interfaces.Specific;
using API.Domain.Dtos.Parameter;
using API.Domain.Entities;
using Xunit;
using Moq;

namespace API.Business.Test.Implementation
{
    public class StudentServiceShould : ServiceShould<IStudentService, Student, IStudentRepository>
    {
        private readonly IStudentService _service;

        private readonly StudentInputDto _defaultInputDto;

        private readonly int _defaultStudentId;

        public StudentServiceShould()
        {
            _defaultInputDto = new StudentInputDto();
            _defaultStudentId = 1;

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

        #region Add

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

        #endregion

        #region GetAll

        [Fact]
        public async Task CallStudentRepositoryWhenCallingGetAll()
        {
            // act
            await _service.GetAll();

            // assert
            RepositoryMock
                .Verify(repo => repo.GetAll(), Times.Once);
        }

        [Fact]
        public async Task ReturnDataWhenRepositorySendsValidData()
        {
            // arrange
            RepositoryMock
                .Setup(repo => repo.GetAll())
                .Returns(Task.FromResult(StudentEntities));

            // act
            var result = await _service.GetAll();

            // assert
            Assert.NotEmpty(result);
        }

        private static IEnumerable<Student> StudentEntities =>
            new List<Student>
            {
                new Student()
            };

        #endregion

        #region GetById

        [Fact]
        public async Task CallStudentRepositoryWhenCallingGetById()
        {
            // act
            await _service.GetById(_defaultStudentId);

            // assert
            RepositoryMock
                .Verify(repo => repo.GetById(It.Is<int>(matcher => matcher == _defaultStudentId)), Times.Once);
        }

        [Fact]
        public async Task ReturnEntityWhenRepositoryReturnsValidDataWhenCallingGetById()
        {
            // arrange
            RepositoryMock
                .Setup(repo => repo.GetById(_defaultStudentId))
                .Returns(Task.FromResult(new Student()));

            // act
            var result = await _service.GetById(_defaultStudentId);

            // assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task ReturnNullWhenRepositoryReturnsNullWhenCallingGetById()
        {
            // arrange
            RepositoryMock
                .Setup(repo => repo.GetById(_defaultStudentId))
                .Returns(Task.FromResult<Student>(null));

            // act
            var result = await _service.GetById(_defaultStudentId);

            // assert
            Assert.Null(result);
        }

        #endregion
    }
}
