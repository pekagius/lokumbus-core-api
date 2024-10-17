using System.Reflection;
using Mapster;

namespace Lokumbus.CoreAPI.Configuration.Mapster
{
    public static class MappingRegistrar
    {
        public static void RegisterMappings()
        {
            var config = TypeAdapterConfig.GlobalSettings;
            config.Scan(Assembly.GetExecutingAssembly());
        }
    }
}