using System.Collections.Generic;

namespace API.Domain.Entities
{
    public class Class : BaseEntity<int>
    {
        public string Name { get; set; }

        public ICollection<Student> EnrolledStudents { get; set; }

        public ICollection<ClassCourseAssociation> AssociatedCourses { get; set; }
    }
}
