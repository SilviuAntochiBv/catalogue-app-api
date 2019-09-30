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

        private Response<SubjectResultDto> DefaultUpdateResponse { get; }

        public SubjectControllerShould()
        {
            DefaultSubjectId = 1;
            DefaultSubjectInputDto = new SubjectInputDto
            {
                Name = Guid.Empty.ToString()
            };
            DefaultUpdateResponse = Response<SubjectResultDto>.Valid(new SubjectResultDto());

            SetupServiceMockResponse(DefaultUpdateResponse);

            SubjectController = new SubjectController(ServiceMock.Object, LoggerMock.Object);
        }

        #region Mocks
        private void SetupServiceMockResponse(Response<SubjectResultDto> response)
        {
            ServiceMock
                .Setup(service => service.Update(It.IsAny<int>(), It.IsAny<SubjectInputDto>()))
                .Returns(Task.FromResult(response));
        }
        #endregion

        #region GetAllSubjects
        [Fact]
        public async Task CallGetAllMethodFromServiceWhenGetAllSubjectsIsCalled()
        {
            // act
            await SubjectController.GetAllSubjects();

            // assert
            ServiceMock.Verify(service => service.GetAll(), Times.Once);
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
            ServiceMock.Verify(service => service.GetById(DefaultSubjectId), Times.Once);
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
            ServiceMock.Verify(service => service.Update(DefaultSubjectId, It.IsAny<SubjectInputDto>()), Times.Once);
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
            SetupServiceMockResponse(invalidResponse);

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
