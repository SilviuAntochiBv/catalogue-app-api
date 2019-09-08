using API.Data.Interfaces.Specific;
using API.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Data.Implementation.Specific
{
    public class TeacherRepository : Repository<Teacher>, ITeacherRepository
    {
        public TeacherRepository(DbContext databaseContext) : base(databaseContext)
        {
        }
    }
}
