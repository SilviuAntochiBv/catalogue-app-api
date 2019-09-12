using API.Data.Interfaces.Specific;
using API.Domain.Entities;

namespace API.Data.Implementation.Specific
{
    public class StudentRepository : Repository<Student>, IStudentRepository
    {
        public StudentRepository(APIDbContext databaseContext) : base(databaseContext)
        {
        }
    }
}
