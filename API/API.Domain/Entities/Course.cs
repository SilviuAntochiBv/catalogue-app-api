using System.Collections.Generic;

namespace API.Domain.Entities
{
    public class Course : BaseEntity<int>
    {
        public string Name { get; set; }

        public Subject Subject { get; set; }

        public Teacher Teacher { get; set; }

        public virtual ICollection<ClassCourseAssociation> AssociatedClasses { get; set; }
    }
}
