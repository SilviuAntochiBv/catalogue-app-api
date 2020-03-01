using System.Collections.Generic;
using System.Threading.Tasks;
using API.Business.Implementation.Specific;
using API.Business.Interfaces;
using API.Data.Interfaces.Specific;
using API.Domain.Dtos.Parameter;
using API.Domain.Entities;
using Moq;
using Xunit;

namespace API.Business.Test.Implementation
{
    public class SubjectServiceShould : ServiceShould<ISubjectService, Subject, ISubjectRepository>
    {
        private ISubjectService Service { get; }

        private SubjectInputDto DefaultSubjectInputDto { get; }

        private int DefaultSubjectId { get; }

        public SubjectServiceShould()
        {
            DefaultSubjectInputDto = new SubjectInputDto();

            DefaultSubjectId = 1;

            Service = new SubjectService(UnitOfWorkMock.Object, ValidatorMock.Object, MapperInstance, LoggerMock.Object);
        }

        #region Add
        [Fact]
        public async Task CallRepositoryWhenCallingAdd()
        {
            // act
            await Service.Add(DefaultSubjectInputDto);

            // assert
            RepositoryMock.Verify(repo => repo.Add(It.IsAny<Subject>()));
        }

        [Fact]
        public async Task ReturnValidResponseWhenValidationPasses()
        {
            // act
            var result = await Service.Add(DefaultSubjectInputDto);

            // assert
            Assert.True(result.IsValid);
        }

        [Fact]
        public async Task ReturnInvalidResponseWhenValidationFails()
        {
            // arrange
            SetupInvalidValidationResultMock();

            // act
            var result = await Service.Add(DefaultSubjectInputDto);

            // assert
            Assert.False(result.IsValid);
        }
        #endregion

        #region Get
        [Fact]
        public async Task CallSubjectRepositoryWhenGettingAllSubjects()
        {
            // act
            await Service.GetAll();

            // assert
            RepositoryMock.Verify(repo => repo.GetAll());
        }

        [Fact]
        public async Task ReturnDataWhenRepositoryRespondsWithValidData()
        {
            // arrange
            RepositoryMock
                .Setup(repo => repo.GetAll())
                .Returns(Task.FromResult(SubjectEntities));

            // act
            var result = await Service.GetAll();

            // assert
            Assert.NotEmpty(result);
        }

        private static IEnumerable<Subject> SubjectEntities =>
            new List<Subject>
            {
                new Subject()
            };

        [Fact]
        public async Task CallSubjectRepositoryWhenGettingById()
        {
            // act
            await Service.GetById(DefaultSubjectId);

            // assert
            RepositoryMock.Verify(repo => repo.GetById(DefaultSubjectId));
        }

        [Fact]
        public async Task ReturnDataWhenRepositoryReturnsEntity()
        {
            // arrange
            RepositoryMock
                .Setup(repo => repo.GetById(It.IsAny<object>()))
                .Returns(Task.FromResult(new Subject()));

            // act
            var result = await Service.GetById(DefaultSubjectId);

            // assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task ReturnNullWhenRepositoryReturnsNull()
        {
            // arrange
            RepositoryMock
                .Setup(repo => repo.GetById(It.IsAny<object>()))
                .Returns(Task.FromResult<Subject>(null));

            // act
            var result = await Service.GetById(DefaultSubjectId);

            // assert
            Assert.Null(result);
        }
        #endregion

        #region Update
        [Fact]
        public async Task RetrieveSubjectUsingSubjectIdParameterWhenCallingUpdate()
        {
            // act
            await Service.Update(DefaultSubjectId, DefaultSubjectInputDto);

            // assert
            RepositoryMock
                .Verify(repo => repo.GetById(DefaultSubjectId), Times.Once);
        }

        [Fact]
        public async Task ReturnNullWhenRepositoryReturnsNullWhenCallingUpdate()
        {
            // arrange
            RepositoryMock
                .Setup(repo => repo.GetById(It.IsAny<object>()))
                .ReturnsAsync((Subject)null);

            // act
            var result = await Service.Update(DefaultSubjectId, DefaultSubjectInputDto);

            // assert
            Assert.Null(result);
        }
        #endregion
    }
}
