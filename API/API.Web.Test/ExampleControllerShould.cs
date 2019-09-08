using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using API.Business.Interfaces;
using API.Domain.Dtos.Parameter;
using API.Web.Controllers;
using Xunit;

namespace API.Web.Test
{
    public class ExampleControllerShould
    {
        private readonly ExampleController _exampleController;
        private readonly Mock<IExampleService> _exampleServiceMock;
        private readonly Mock<ILogger<ExampleController>> _loggerMock;

        public ExampleControllerShould()
        {
            _exampleServiceMock = new Mock<IExampleService>();
            _loggerMock = new Mock<ILogger<ExampleController>>();

            _exampleController = new ExampleController(_exampleServiceMock.Object, _loggerMock.Object);
        }

        [Fact]
        public async Task CallServiceMethodWhenCallingGetAll()
        {
            // act
            await _exampleController.GetAll();

            // assert 
            _exampleServiceMock.Verify(mock => mock.GetAll(), Times.Once);
        }

        [Fact]
        public async Task ReturnNotFoundResultWhenCallingGetAll()
        {
            // arrange
            _exampleServiceMock
                .Setup(service => service.GetAll())
                .Returns(CreateGetAllMockedResult(null));

            // act
            var actionResult = await _exampleController.GetAll();

            // assert 
            Assert.IsType<NotFoundResult>(actionResult);
        }

        [Fact]
        public async Task ReturnOkResultWhenCallingGetAll()
        {
            // arrange
            _exampleServiceMock
                .Setup(service => service.GetAll())
                .Returns(CreateGetAllMockedResult(new BaseEntityDto { InternalId = 1L }));

            // act
            var actionResult = await _exampleController.GetAll();

            // assert 
            AssertController.Ok(actionResult);
        }

        [Fact]
        public async Task ReturnValidListAsResultFromServiceWhenCallingGetAll()
        {
            // arrange
            _exampleServiceMock
                .Setup(service => service.GetAll())
                .Returns(CreateGetAllMockedResult(new BaseEntityDto { InternalId = 1L }));

            // act
            var response = await _exampleController.GetAll();

            // assert 
            var okResult = (OkObjectResult)response;
            Assert.IsAssignableFrom<IEnumerable<BaseEntityDto>>(okResult.Value);
        }

        [Fact]
        public async Task ReturnEmptyListOfResultsFromServiceWhenCallingGetAll()
        {
            // arrange
            _exampleServiceMock
                .Setup(service => service.GetAll())
                .Returns(CreateGetAllMockedResult());

            // act
            var response = await _exampleController.GetAll();

            // assert 
            var okResult = (OkObjectResult)response;
            var values = (IEnumerable<BaseEntityDto>)okResult.Value;
            Assert.Empty(values);
        }

        private static Task<IEnumerable<BaseEntityDto>> CreateGetAllMockedResult(params BaseEntityDto[] dtos)
        {
            return Task.FromResult(dtos?.AsEnumerable());
        }
    }
}