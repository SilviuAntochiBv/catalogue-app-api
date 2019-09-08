using AutoMapper;
using API.Business.Mappers;

namespace API.Business.Test.Utilities
{
    public static class AutoMapperUtilities
    {
        private static readonly object Lock;

        public static bool IsInitialiazed { get; set; }

        static AutoMapperUtilities()
        {
            Lock = new object();
            IsInitialiazed = false;
        }

        public static void Init()
        {
            lock (Lock)
            {
                if (IsInitialiazed)
                {
                    return;
                }

                Mapper.Initialize(config =>
                {
                    config.AddProfile<ExampleProfile>();
                });

                IsInitialiazed = true;
            }
        }
    }
}
