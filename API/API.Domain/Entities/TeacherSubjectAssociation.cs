namespace API.Domain.Entities
{
    public class TeacherSubjectAssociation
    {
        public int SubjectId { get; set; }

        public int TeacherId { get; set; }

        public Subject Subject { get; set; }

        public Teacher Teacher { get; set; }
    }
}
