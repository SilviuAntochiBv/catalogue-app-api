using System;

namespace API.Domain.Common
{
    public static class Precondition
    {
        public static void IsNotNull<T>(T input) where T : class
        {
            if (input == null)
            {
                throw new ArgumentException(nameof(input));
            }
        }
    }
}
