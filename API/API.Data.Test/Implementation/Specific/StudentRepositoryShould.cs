using API.Data.Implementation.Specific;
using API.Data.Interfaces.Specific;
using API.Domain.Entities;

namespace API.Data.Test.Implementation.Specific
{
    public class StudentRepositoryShould : RepositoryShould<IStudentRepository, Student, int>
    {
        protected override int CreateKey(bool other = false)
        {
            return other ? 1 : 2;
        }

        protected override IStudentRepository CreateRepository(APIDbContext inMemoryDbContext)
        {
            return new StudentRepository(inMemoryDbContext);
        }
    }
}
