using API.Domain.Common;
using API.Domain.Dtos.Parameter;
using API.Domain.Dtos.Result;
using API.Domain.Interfaces;
using API.Domain.Logging;

namespace API.Business.Interfaces
{
    public interface IStudentService : IAddable<StudentInputDto, Response<StudentResultDto>>, ILoggable<IStudentService>
    {
    }
}
