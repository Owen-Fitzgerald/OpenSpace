using Geraldine._4XEngine.Contracts;

namespace Geraldine._4XEngine.Loaders
{
    public class TemperatureLoader : DataLoader<Temperature, TemperatureLoader>
    {
        protected override string directoryName => "Temperatures";
        protected override string descendantName => "Temperature";
    }
}
