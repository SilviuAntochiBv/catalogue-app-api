using API.Data.Interfaces.Specific;
using API.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Data.Implementation.Specific
{
    public class SubjectRepository : Repository<Subject>, ISubjectRepository
    {
        public SubjectRepository(DbContext databaseContext) : base(databaseContext)
        {
        }
    }
}
