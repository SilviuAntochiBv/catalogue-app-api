using System.Collections.Generic;

namespace API.Domain.Entities
{
    public class Subject : BaseEntity<int>
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public virtual ICollection<TeacherSubjectAssociation> Teachers { get; set; }

        public virtual ICollection<Course> AssociatedCourses { get; set; }
    }
}
