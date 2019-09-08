using API.Data.Implementation.Specific;
using API.Data.Interfaces.Specific;
using API.Domain.Entities;

namespace API.Data.Test.Implementation
{
    public class CourseRepositoryShould : RepositoryShould<ICourseRepository, Course, int>
    {
        protected override int CreateKey(bool other = false)
        {
            return other ? 1 : 2;
        }

        protected override ICourseRepository CreateRepository(APIDbContext inMemoryDbContext)
        {
            return new CourseRepository(inMemoryDbContext);
        }
    }
}
