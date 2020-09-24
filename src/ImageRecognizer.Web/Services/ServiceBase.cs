using Microsoft.Extensions.Logging;

namespace ImageRecognizer.Web.Services
{
    public abstract class ServiceBase<TInstance>
    {
        protected ILogger<TInstance> Logger { get; }

        protected ServiceBase(ILogger<TInstance> logger)
        {
            Logger = logger;
        }
    }
}