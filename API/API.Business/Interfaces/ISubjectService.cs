using System.Threading.Tasks;
using API.Domain.Common;
using API.Domain.Dtos.Parameter;
using API.Domain.Dtos.Result;
using API.Domain.Interfaces;
using API.Domain.Logging;

namespace API.Business.Interfaces
{
    public interface ISubjectService : 
        IAddable<SubjectInputDto, Response<SubjectResultDto>>,
        IInterrogable<SubjectResultDto>, 
        ILoggable<ISubjectService>
    {
        Task<Response<SubjectResultDto>> Update(int subjectId, SubjectInputDto subjectInputDto);
    }
}
