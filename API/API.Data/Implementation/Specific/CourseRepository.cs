using API.Data.Interfaces.Specific;
using API.Domain.Entities;

namespace API.Data.Implementation.Specific
{
    public class CourseRepository : Repository<Course>, ICourseRepository
    {
        public CourseRepository(APIDbContext databaseContext) : base(databaseContext)
        {
        }
    }
}
