namespace API.Domain.Dtos.Result
{
    public class CourseResultDto : NamedDto<int>
    {
        public SubjectResultDto Subject { get; set; }

        public TeacherResultDto Teacher { get; set; }
    }
}
