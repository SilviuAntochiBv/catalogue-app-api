using API.Data.Implementation.Specific;
using API.Data.Interfaces.Specific;
using API.Domain.Entities;

namespace API.Data.Test.Implementation
{
    public class TeacherRepositoryShould : RepositoryShould<ITeacherRepository, Teacher, int>
    {
        protected override int CreateKey(bool other = false)
        {
            return other ? 1 : 2;
        }

        protected override ITeacherRepository CreateRepository(APIDbContext inMemoryDbContext)
        {
            return new TeacherRepository(inMemoryDbContext);
        }
    }
}
