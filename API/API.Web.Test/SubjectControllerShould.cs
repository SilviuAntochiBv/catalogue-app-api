using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using API.Business.Interfaces;
using API.Domain.Common;
using API.Domain.Dtos.Parameter;
using API.Domain.Dtos.Result;
using API.Web.Controllers;
using Moq;
using Xunit;

namespace API.Web.Test
{
    public class SubjectControllerShould : ControllerShould<ISubjectService, SubjectController>
    {
        private SubjectController SubjectController { get; }

        private int DefaultSubjectId { get; }

        private SubjectInputDto DefaultSubjectInputDto { get; }

        private SubjectResultDto DefaultResponseContent { get; }

        private Response<SubjectResultDto> DefaultServiceResponse { get; }

        public SubjectControllerShould()
        {
            DefaultSubjectId = 1;
            DefaultSubjectInputDto = new SubjectInputDto
            {
                Name = Guid.Empty.ToString()
            };

            DefaultResponseContent = new SubjectResultDto();
            DefaultServiceResponse = Response<SubjectResultDto>.Valid(DefaultResponseContent);

            SetupMockResponseServiceAdd(DefaultServiceResponse);
            SetupMockResponseServiceUpdate(DefaultServiceResponse);

            SubjectController = new SubjectController(ServiceMock.Object, LoggerMock.Object);
        }

        #region Mocks
        private void SetupMockResponseServiceAdd(Response<SubjectResultDto> response)
        {
            ServiceMock
                .Setup(service => service.Add(It.IsAny<SubjectInputDto>()))
                .Returns(Task.FromResult(response));
        }

        private void SetupMockResponseServiceUpdate(Response<SubjectResultDto> response)
        {
            ServiceMock
                .Setup(service => service.Update(It.IsAny<int>(), It.IsAny<SubjectInputDto>()))
                .Returns(Task.FromResult(response));
        }
        #endregion

        #region Add
        [Fact]
        public async Task CallAddFromServiceWhenAddNewStudentIsCalled()
        {
            // act
            await SubjectController.AddNewSubject(DefaultSubjectInputDto);

            // assert
            ServiceMock.Verify(service => service.Add(DefaultSubjectInputDto));
        }

        [Fact]
        public async Task ReturnBadRequestWhenResponseFromServiceIsInvalidWhenAddNewStudentIsCalled()
        {
            // arrange
            var invalidServiceResponse = Response<SubjectResultDto>.Invalid(new List<string>());
            SetupMockResponseServiceAdd(invalidServiceResponse);

            // act
            var result = await SubjectController.AddNewSubject(DefaultSubjectInputDto);

            // assert
            AssertController.BadRequest(result);
        }

        [Fact]
        public async Task ReturnOkWhenResponseFromServiceIsValidWhenAddNewStudentIsCalled()
        {
            // act
            var result = await SubjectController.AddNewSubject(DefaultSubjectInputDto);

            // assert
            AssertController.Ok(result, DefaultResponseContent);
        }

        [Fact]
        public async Task ReturnInternalServerErrorWhenServiceThrowsException()
        {
            // arrange
            ServiceMock
                .Setup(service => service.Add(It.IsAny<SubjectInputDto>()))
                .Throws<Exception>();

            // act
            var result = await SubjectController.AddNewSubject(DefaultSubjectInputDto);

            // assert
            AssertController.InternalServerError(result);
        }
        #endregion 

        #region GetAllSubjects
        [Fact]
        public async Task CallGetAllMethodFromServiceWhenGetAllSubjectsIsCalled()
        {
            // act
            await SubjectController.GetAllSubjects();

            // assert
            ServiceMock.Verify(service => service.GetAll());
        }

        [Fact]
        public async Task ReturnOkStatusWhenServiceReturnsValidDataOnGetAllSubjectsCall()
        {
            // arrange
            ServiceMock
                .Setup(service => service.GetAll())
                .Returns(Task.FromResult(Subjects));

            // act 
            var result = await SubjectController.GetAllSubjects();

            // assert
            AssertController.Ok(result);
        }

        private IEnumerable<SubjectResultDto> Subjects =>
            new List<SubjectResultDto>
            {
                new SubjectResultDto()
            };

        [Fact]
        public async Task ReturnInternalServerWhenServiceThrowsExceptionOnGetAllSubjectsCall()
        {
            // arrange
            ServiceMock
                .Setup(service => service.GetAll())
                .Throws(new Exception());

            // act 
            var result = await SubjectController.GetAllSubjects();

            // assert
            AssertController.InternalServerError(result);
        }
        #endregion

        #region GetSubjectById
        [Fact]
        public async Task CallGetByIdMethodFromServiceOnGetSubjectCall()
        {
            // act
            await SubjectController.GetSubject(DefaultSubjectId);

            // assert
            ServiceMock.Verify(service => service.GetById(DefaultSubjectId));
        }

        [Fact]
        public async Task ReturnDtoWhenServiceReturnsValidDataOnGetSubjectCall()
        {
            // arrange
            var existingSubject = new SubjectResultDto();

            ServiceMock
                .Setup(service => service.GetById(It.IsAny<int>()))
                .Returns(Task.FromResult(existingSubject));

            // act
            var result = await SubjectController.GetSubject(DefaultSubjectId);

            // assert
            AssertController.Ok(result, existingSubject);
        }

        [Fact]
        public async Task ReturnNotFoundWhenServiceReturnsNullOnGetSubjectCall()
        {
            // arrange
            ServiceMock
                .Setup(service => service.GetById(It.IsAny<int>()))
                .Returns(Task.FromResult<SubjectResultDto>(null));

            // act
            var result = await SubjectController.GetSubject(DefaultSubjectId);

            // assert
            AssertController.NotFound(result);
        }

        [Fact]
        public async Task ReturnInternalServerErrorWhenServiceThrowsExceptionOnGetSubjectCall()
        {
            // arrange
            ServiceMock
                .Setup(service => service.GetById(It.IsAny<int>()))
                .Throws(new Exception());

            // act
            var result = await SubjectController.GetSubject(DefaultSubjectId);

            // assert
            AssertController.InternalServerError(result);
        }
        #endregion

        #region UpdateSubject
        [Fact]
        public async Task CallUpdateSubjectMethodFromServiceOnEditSubjectCall()
        {
            // act
            await SubjectController.EditSubject(DefaultSubjectId, DefaultSubjectInputDto);

            // assert
            ServiceMock.Verify(service => service.Update(DefaultSubjectId, DefaultSubjectInputDto));
        }

        [Fact]
        public async Task ReturnNoContentWhenUpdateWasSuccessfulOnEditSubjectCall()
        {
            // act
            var result = await SubjectController.EditSubject(DefaultSubjectId, DefaultSubjectInputDto);

            // assert
            AssertController.NoContent(result);
        }

        [Fact]
        public async Task ReturnBadRequestWhenModelStateIsInvalidOnEditSubjectCall()
        {
            // arrange
            SubjectController.ModelState.AddModelError("invalidState", Guid.NewGuid().ToString());

            // act
            var result = await SubjectController.EditSubject(DefaultSubjectId, DefaultSubjectInputDto);

            // assert
            AssertController.BadRequest(result);
        }

        [Fact]
        public async Task ReturnBadRequestWhenBodyIsNullOnEditSubjectCall()
        {
            // act
            var result = await SubjectController.EditSubject(DefaultSubjectId, null);

            // assert
            AssertController.BadRequest(result);
        }

        [Fact]
        public async Task ReturnBadRequestWhenAllFieldsOfBodyAreNullOnEditSubjectCall()
        {
            // arrange
            DefaultSubjectInputDto.Name = null;

            // act
            var result = await SubjectController.EditSubject(DefaultSubjectId, DefaultSubjectInputDto);

            // assert
            AssertController.BadRequest(result);
        }

        [Fact]
        public async Task ReturnNotFoundWhenServiceReturnsNullOnEditSubjectCall()
        {
            // arrange
            ServiceMock
                .Setup(service => service.Update(It.IsAny<int>(), It.IsAny<SubjectInputDto>()))
                .Returns(Task.FromResult<Response<SubjectResultDto>>(null));

            // act
            var result = await SubjectController.EditSubject(DefaultSubjectId, DefaultSubjectInputDto);

            // assert
            AssertController.NotFound(result);
        }

        [Fact]
        public async Task ReturnBadRequestWhenServiceReturnsInvalidResponseOnEditSubjectCall()
        {
            // arrange
            var invalidResponse = Response<SubjectResultDto>.Invalid(new List<string>());
            SetupMockResponseServiceUpdate(invalidResponse);

            // act
            var result = await SubjectController.EditSubject(DefaultSubjectId, DefaultSubjectInputDto);

            // assert
            AssertController.BadRequest(result);
        }

        [Fact]
        public async Task ReturnInternalServerErrorWhenServiceThrowsExceptionOnEditSubjectCall()
        {
            // arrange
            ServiceMock
                .Setup(service => service.Update(It.IsAny<int>(), It.IsAny<SubjectInputDto>()))
                .Throws(new Exception());

            // act
            var result = await SubjectController.EditSubject(DefaultSubjectId, DefaultSubjectInputDto);

            // assert
            AssertController.InternalServerError(result);
        }
        #endregion
    }
}
