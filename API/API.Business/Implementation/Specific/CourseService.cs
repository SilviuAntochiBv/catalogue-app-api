using API.Business.Interfaces;
using API.Data.Interfaces;
using API.Data.Interfaces.Specific;
using API.Domain.Entities;
using AutoMapper;
using FluentValidation;
using Microsoft.Extensions.Logging;

namespace API.Business.Implementation.Specific
{
    public class CourseService : Service<Course, ICourseRepository>, ICourseService
    { 
        public ILogger<ICourseService> Logger { get; }

        public CourseService(
            IUnitOfWork unitOfWork, 
            IValidator<Course> validator, 
            IMapper mapper, 
            ILogger<ICourseService> logger)
            : base(unitOfWork, validator, mapper)
        {
            Logger = logger;
        }
    }
}
