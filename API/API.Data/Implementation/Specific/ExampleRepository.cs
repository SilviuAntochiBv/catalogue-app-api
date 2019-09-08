using API.Data.Interfaces.Specific;
using API.Domain.Entities;

namespace API.Data.Implementation.Specific
{
    public class ExampleRepository : Repository<BaseEntity<long>>, IExampleRepository
    {
        public ExampleRepository(APIDbContext databaseContext)
            : base(databaseContext)
        {
        }
    }
}
