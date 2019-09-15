using API.Data.Interfaces.Specific;
using API.Domain.Entities;

namespace API.Data.Implementation.Specific
{
    public class ClassRepository : Repository<Class>, IClassRepository
    {
        public ClassRepository(APIDbContext databaseContext) : base(databaseContext)
        {
        }
    }
}
