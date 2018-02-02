using Microsoft.Extensions.Configuration;
using SimpleInjector;

namespace MyCasts.Domain
{
    public interface IModule
    {
        void Initialize(Container container, IConfigurationRoot configuration);
    }
}