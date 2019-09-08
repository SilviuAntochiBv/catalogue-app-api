namespace API.Domain.Entities
{
    public class ClassCourseAssociation
    {
        public int ClassId { get; set; }

        public int CourseId { get; set; }

        public virtual Class Class { get; set; }

        public virtual Course Course { get; set; }
    }
}
