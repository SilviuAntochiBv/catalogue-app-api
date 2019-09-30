using API.Domain.Dtos.Result.Base;

namespace API.Domain.Dtos.Result
{
    public class StudentResultDto : PersonResultDto<int>
    {
        public short Age { get; set; }
    }
}
