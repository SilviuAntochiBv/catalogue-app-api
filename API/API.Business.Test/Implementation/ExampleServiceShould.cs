using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Moq;
using Microsoft.Extensions.Logging;
using API.Business.Implementation.Specific;
using API.Business.Interfaces;
using API.Data.Interfaces.Specific;
using API.Domain.Entities;
using Xunit;

namespace API.Business.Test.Implementation
{
    public class ExampleServiceShould : ServiceShould<BaseEntity<long>, IExampleRepository>
    {
        private readonly IExampleService _service;

        private readonly Mock<ILogger<ExampleService>> _loggerMock;

        public ExampleServiceShould()
        {
            _loggerMock = new Mock<ILogger<ExampleService>>();

            _service = new ExampleService(UnitOfWork.Object, Validator.Object, Mapper.Instance, _loggerMock.Object);
        }

        [Fact]
        public async void ReturnNotEmptyCollectionWhenCallingGetAll()
        {
            // arrange
            Repository
                .Setup(repo => repo.GetAll())
                .Returns(Task.FromResult<IEnumerable<BaseEntity<long>>>(new List<BaseEntity<long>> { new BaseEntity<long>() }));

            // act
            var result = await _service.GetAll();

            // assert
            Assert.NotEmpty(result);
        }

        [Fact]
        public async void ReturnDtoWhenCallingGetById()
        {
            // arrange
            Repository
                .Setup(repo => repo.GetById(It.IsAny<object>()))
                .Returns(Task.FromResult(new BaseEntity<long>()));

            // act
            var result = await _service.GetById(new object());

            // assert
            Assert.NotNull(result);
        }
    }
}
