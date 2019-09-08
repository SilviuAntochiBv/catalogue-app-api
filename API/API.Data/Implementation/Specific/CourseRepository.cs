using API.Data.Interfaces.Specific;
using API.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Data.Implementation.Specific
{
    public class CourseRepository : Repository<Course>, ICourseRepository
    {
        public CourseRepository(DbContext databaseContext) : base(databaseContext)
        {
        }
    }
}
