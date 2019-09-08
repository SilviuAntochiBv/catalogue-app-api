namespace API.Domain.Dtos.Result
{
    public class StudentResultDto : PersonDto<int>
    {
        public short Age { get; set; }
    }
}
