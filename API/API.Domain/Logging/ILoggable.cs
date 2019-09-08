using Microsoft.Extensions.Logging;

namespace API.Domain.Logging
{
    public interface ILoggable<out T>
    {
        ILogger<T> Logger { get; }
    }
}