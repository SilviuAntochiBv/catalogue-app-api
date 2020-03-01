using AutoMapper;
using API.Business.Mappers;

namespace API.Business.Test.Utilities
{
    public static class AutoMapperUtilities
    {
        public static IMapper MapperInstance { get; }

        static AutoMapperUtilities()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<StudentMapperProfile>();
                cfg.AddProfile<SubjectMapperProfile>();
            });

            MapperInstance = config.CreateMapper();
        }
    }
}
