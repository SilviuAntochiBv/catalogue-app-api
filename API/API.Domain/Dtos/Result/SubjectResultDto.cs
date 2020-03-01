using API.Domain.Dtos.Result.Base;

namespace API.Domain.Dtos.Result
{
    public class SubjectResultDto : NamedResultDto<int>
    {
        public string Description { get; set; }
    }
}
