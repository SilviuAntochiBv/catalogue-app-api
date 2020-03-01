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
        private IStudentService Service { get; }

        private StudentInputDto DefaultInputDto { get; }

        private int DefaultStudentId { get; }

        public StudentServiceShould()
        {
            DefaultInputDto = new StudentInputDto();
            DefaultStudentId = 1;

            Service = new StudentService(UnitOfWorkMock.Object, ValidatorMock.Object, MapperInstance, LoggerMock.Object);
        }

        #region Add
        [Fact]
        public async Task CallRepositoryWhenCallingAdd()
        {
            // act
            await Service.Add(DefaultInputDto);

            // assert
            RepositoryMock
                .Verify(repo => repo.Add(It.IsAny<Student>()), Times.Once);
        }

        [Fact]
        public async Task ReturnValidResponseWhenValidationPasses()
        {
            // act 
            var result = await Service.Add(DefaultInputDto);

            // assert
            Assert.True(result.IsValid);
        }

        [Fact]
        public async Task ReturnInvalidResponseWhenValidationFails()
        {
            // arrange
            SetupInvalidValidationResultMock();

            // act 
            var result = await Service.Add(DefaultInputDto);

            // assert
            Assert.False(result.IsValid);
        }

        #endregion

        #region GetAll

        [Fact]
        public async Task CallStudentRepositoryWhenCallingGetAll()
        {
            // act
            await Service.GetAll();

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
            var result = await Service.GetAll();

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
            await Service.GetById(DefaultStudentId);

            // assert
            RepositoryMock
                .Verify(repo => repo.GetById(It.Is<int>(matcher => matcher == DefaultStudentId)), Times.Once);
        }

        [Fact]
        public async Task ReturnEntityWhenRepositoryReturnsValidDataWhenCallingGetById()
        {
            // arrange
            RepositoryMock
                .Setup(repo => repo.GetById(DefaultStudentId))
                .Returns(Task.FromResult(new Student()));

            // act
            var result = await Service.GetById(DefaultStudentId);

            // assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task ReturnNullWhenRepositoryReturnsNullWhenCallingGetById()
        {
            // arrange
            RepositoryMock
                .Setup(repo => repo.GetById(DefaultStudentId))
                .Returns(Task.FromResult<Student>(null));

            // act
            var result = await Service.GetById(DefaultStudentId);

            // assert
            Assert.Null(result);
        }

        #endregion
    }
}
