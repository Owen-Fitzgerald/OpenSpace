using Geraldine._4XEngine.Contracts;

namespace Geraldine._4XEngine.Loaders
{
    public class ElevationLoader : DataLoader<Elevation, ElevationLoader>
    {
        protected override string directoryName => "Elevations";
        protected override string descendantName => "Elevation";
    }
}
