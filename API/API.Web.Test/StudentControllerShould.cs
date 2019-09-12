using API.Business.Interfaces;
using API.Domain.Common;
using API.Domain.Dtos.Parameter;
using API.Domain.Dtos.Result;
using API.Web.Controllers;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace API.Web.Test
{
    public class StudentControllerShould : ControllerShould<IStudentService, StudentController>
    {
        private readonly StudentController _studentController;

        private StudentInputDto _defaultStudentInput;

        public StudentControllerShould()
        {
            _defaultStudentInput = new StudentInputDto();

            _studentController = new StudentController(ServiceMock.Object, LoggerMock.Object);
        }

        #region Mocks
        private void SetupServiceMock(Response<StudentResultDto> response)
        {
            ServiceMock
                .Setup(serv => serv.Add(It.IsAny<StudentInputDto>()))
                .Returns(Task.FromResult(response));
        }
        #endregion

        [Fact]
        public async Task CallStudentServiceWhenCallingAddNewService()
        {
            // act
            await _studentController.AddNewStudent(_defaultStudentInput);

            // assert
            ServiceMock.Verify(
                service => 
                    service.Add(It.IsAny<StudentInputDto>()),
                    Times.Once);
        }

        [Fact]
        public async Task ReturnOkResultWhenServiceReturnsValidObjectOnCallAddNewService()
        {
            // arrange
            var studentResultDto = new StudentResultDto();
            var serviceResult = Response<StudentResultDto>.Valid(studentResultDto);

            SetupServiceMock(serviceResult);

            // act
            var result = await _studentController.AddNewStudent(_defaultStudentInput);

            // assert
            AssertController.Ok(result, studentResultDto);
        }

        [Fact]
        public async Task ReturnBadRequestWhenServiceReturnsInvalidOjbectOnCallAddNewService()
        {
            // arrange
            var serviceResult = Response<StudentResultDto>.Invalid(new List<string> { "validationError" });

            SetupServiceMock(serviceResult);

            // act
            var result = await _studentController.AddNewStudent(_defaultStudentInput);

            // assert
            AssertController.BadRequest(result);
        }

        [Fact]
        public async Task ReturnInternalServerErrorWhenServiceThrowsExceptionOnCallAddNewService()
        {
            // arrange
            ServiceMock
                .Setup(serv => serv.Add(It.IsAny<StudentInputDto>()))
                .Throws<Exception>();

            // act
            var result = await _studentController.AddNewStudent(_defaultStudentInput);

            // assert
            AssertController.InternalServerError(result);
        }
    }
}
