using AutoMapper;
using API.Business.Mappers;

namespace API.Business.Test.Utilities
{
    public static class AutoMapperUtilities
    {
        private static readonly object Lock;

        private static IMapper Mapper;

        public static bool IsInitialiazed { get; set; }

        static AutoMapperUtilities()
        {
            Lock = new object();
            IsInitialiazed = false;
        }

        public static IMapper Init()
        {
            lock (Lock)
            {
                if (!IsInitialiazed)
                {
                    var config = new MapperConfiguration(cfg =>
                    {
                        cfg.AddProfile<StudentMapperProfile>();
                    });

                    Mapper = config.CreateMapper();
                    IsInitialiazed = true;
                }

                return Mapper;
            }
        }
    }
}
