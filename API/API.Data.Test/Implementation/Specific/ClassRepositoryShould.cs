using API.Data.Implementation.Specific;
using API.Data.Interfaces.Specific;
using API.Domain.Entities;

namespace API.Data.Test.Implementation.Specific
{
    public class ClassRepositoryShould : RepositoryShould<IClassRepository, Class, int>
    {
        protected override int CreateKey(bool other = false)
        {
            return other ? 1 : 2;
        }

        protected override IClassRepository CreateRepository(APIDbContext inMemoryDbContext)
        {
            return new ClassRepository(inMemoryDbContext);
        }
    }
}
