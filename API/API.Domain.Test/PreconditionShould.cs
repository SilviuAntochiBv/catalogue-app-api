using System;
using API.Domain.Common;
using Xunit;

namespace API.Domain.Test
{
    public class PreconditionShould
    {
        [Fact]
        public void ThrowArgumentExceptionInCaseOfNull()
        {
            Assert.Throws<ArgumentException>(() => Precondition.IsNotNull<object>(null));
        }

        [Fact]
        public void NotThrowArgumentExceptionInCaseOfNotNullObject()
        {
            Precondition.IsNotNull(new object());
        }
    }
}
