using API.Domain.Logging;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace API.Web.Controllers
{
    public abstract class AppControllerBase<TService, TController> : ControllerBase, ILoggable<TController>
    {
        protected TService Service { get; }

        public ILogger<TController> Logger { get; }

        public AppControllerBase(TService service, ILogger<TController> logger)
        {
            Service = service;
            Logger = logger;
        }
    }
}
