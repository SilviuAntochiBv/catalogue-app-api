using System.Collections.Generic;

namespace API.Domain.Dtos.Result
{
    public class ClassResultDto : NamedDto<int>
    {
        public IEnumerable<StudentResultDto> Students { get; set; }

        public IEnumerable<CourseResultDto> Courses { get; set; }
    }
}
