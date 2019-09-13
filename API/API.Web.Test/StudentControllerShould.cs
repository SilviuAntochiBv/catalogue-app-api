using API.Business.Interfaces;
using API.Domain.Common;
using API.Domain.Dtos.Parameter;
using API.Domain.Dtos.Result;
using API.Web.Controllers;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using API.Business.Implementation;
using Xunit;

namespace API.Web.Test
{
    public class StudentControllerShould : ControllerShould<IStudentService, StudentController>
    {
        private readonly StudentController _studentController;

        private readonly StudentInputDto _defaultStudentInput;

        private readonly int _defaultStudentId;

        public StudentControllerShould()
        {
            _defaultStudentId = 1;
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

        #region AddNewStudent
        
        [Fact]
        public async Task ReturnBadRequestForInvalidModelStateWhenAddNewStudentIsCalled()
        {
            // arrange
            _studentController.ModelState.AddModelError("error", Guid.NewGuid().ToString());

            // act
            var result = await _studentController.AddNewStudent(_defaultStudentInput);

            // assert
            AssertController.BadRequest(result);
        }

        [Fact]
        public async Task CallStudentServiceWhenCallingAddNewStudent()
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
        public async Task ReturnOkResultWhenServiceReturnsValidObjectOnCallAddNewStudent()
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
        public async Task ReturnBadRequestWhenServiceReturnsInvalidOjbectOnCallAddNewStudent()
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
        public async Task ReturnInternalServerErrorWhenServiceThrowsExceptionOnCallAddNewStudent()
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

        #endregion

        #region GetAllStudents

        [Fact]
        public async Task CallStudentServiceWhenGetAllStudentsIsCalled()
        {
            // act
            await _studentController.GetAllStudents();

            // assert
            ServiceMock.Verify(service => service.GetAll(), Times.Once);
        }

        [Fact]
        public async Task ReturnOkWhenServiceGetAllReturnsValidData()
        {
            // arrange
            ServiceMock
                .Setup(service => service.GetAll())
                .Returns(Task.FromResult(Students));

            // act
            var result = await _studentController.GetAllStudents();

            // assert
            AssertController.Ok(result);
        }

        private static IEnumerable<StudentResultDto> Students => 
            new List<StudentResultDto>
            {
                new StudentResultDto()
            };

        [Fact]
        public async Task ReturnInternalServerErrorWhenServiceGetAllThrowsException()
        {
            // arrange
            ServiceMock
                .Setup(service => service.GetAll())
                .Throws<Exception>();

            // act
            var result = await _studentController.GetAllStudents();

            // assert
            AssertController.InternalServerError(result);
        }

        #endregion

        #region GetStudent 

        [Fact]
        public async Task CallStudentServiceWhenGetStudentIsCalled()
        {
            // act
            await _studentController.GetStudent(_defaultStudentId);

            // assert
            ServiceMock
                .Verify(service => service.GetById(It.Is<int>(matcher => matcher == _defaultStudentId)));
        }

        [Fact]
        public async Task ReturnOkWhenServiceReturnsValidResultWhenGetStudentIsCalled()
        {
            // arrange
            ServiceMock
                .Setup(service => service.GetById(_defaultStudentId))
                .Returns(Task.FromResult(new StudentResultDto()));

            // act
            var result = await _studentController.GetStudent(_defaultStudentId);

            // assert
            AssertController.Ok(result);
        }

        [Fact]
        public async Task ReturnNotFoundWhenServiceReturnsNullWhenGetStudentIsCalled()
        {
            // arrange
            ServiceMock
                .Setup(service => service.GetById(_defaultStudentId))
                .Returns(Task.FromResult<StudentResultDto>(null));

            // act
            var result = await _studentController.GetStudent(_defaultStudentId);

            // assert
            AssertController.NotFound(result);
        }

        [Fact]
        public async Task ReturnInternalServerErrorWhenServiceThrowsExceptionWhenGetStudentIsCalled()
        {
            // arrange
            ServiceMock
                .Setup(service => service.GetById(_defaultStudentId))
                .Throws<Exception>();

            // act
            var result = await _studentController.GetStudent(_defaultStudentId);

            // assert
            AssertController.InternalServerError(result);
        }

        #endregion
    }
}
