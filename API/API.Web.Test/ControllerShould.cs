using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

namespace API.Web.Test
{
    public abstract class ControllerShould<TInternalService, TController>
        where TInternalService: class
        where TController: ControllerBase
    {
        protected Mock<TInternalService> ServiceMock { get; }

        protected Mock<ILogger<TController>> LoggerMock { get; }

        protected ControllerShould()
        {
            ServiceMock = new Mock<TInternalService>();

            LoggerMock = new Mock<ILogger<TController>>();
        }
    }
}
