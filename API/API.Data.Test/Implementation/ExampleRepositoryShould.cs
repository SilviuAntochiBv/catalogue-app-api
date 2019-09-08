using API.Data.Implementation.Specific;
using API.Data.Interfaces.Specific;
using API.Domain.Entities;

namespace API.Data.Test.Implementation
{
    public class ExampleRepositoryShould : RepositoryShould<IExampleRepository, BaseEntity<long>, long>
    {
        protected override long CreateKey(bool other = false)
        {
            return other ? 2L : 1L;
        }

        protected override IExampleRepository CreateRepository(APIDbContext inMemoryDbContext)
        {
            return new ExampleRepository(inMemoryDbContext);
        }
    }
}
