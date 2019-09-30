using System.Collections.Generic;

namespace API.Domain.Entities
{
    public class Teacher : Person
    {
        public virtual ICollection<TeacherSubjectAssociation> Subjects { get; set; }

        public virtual ICollection<Course> Courses {  get; set; }
    }
}
