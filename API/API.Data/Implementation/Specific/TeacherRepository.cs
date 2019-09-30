using API.Data.Interfaces.Specific;
using API.Domain.Entities;

namespace API.Data.Implementation.Specific
{
    public class TeacherRepository : Repository<Teacher>, ITeacherRepository
    {
        public TeacherRepository(APIDbContext databaseContext) : base(databaseContext)
        {
        }
    }
}
