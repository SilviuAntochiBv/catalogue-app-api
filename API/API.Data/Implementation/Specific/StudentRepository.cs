using API.Data.Interfaces.Specific;
using API.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Data.Implementation.Specific
{
    public class StudentRepository : Repository<Student>, IStudentRepository
    {
        public StudentRepository(DbContext databaseContext) : base(databaseContext)
        {
        }
    }
}
