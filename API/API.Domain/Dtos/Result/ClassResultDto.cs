using System.Collections.Generic;
using API.Domain.Dtos.Result.Base;

namespace API.Domain.Dtos.Result
{
    public class ClassResultDto : NamedResultDto<int>
    {
        public IEnumerable<StudentResultDto> Students { get; set; }

        public IEnumerable<CourseResultDto> Courses { get; set; }
    }
}
