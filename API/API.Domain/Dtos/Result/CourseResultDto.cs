using API.Domain.Dtos.Result.Base;

namespace API.Domain.Dtos.Result
{
    public class CourseResultDto : NamedResultDto<int>
    {
        public SubjectResultDto Subject { get; set; }

        public TeacherResultDto Teacher { get; set; }
    }
}
