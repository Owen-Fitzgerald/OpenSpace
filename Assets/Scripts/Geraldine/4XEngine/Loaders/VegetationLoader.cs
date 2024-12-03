using Geraldine._4XEngine.Contracts;

namespace Geraldine._4XEngine.Loaders
{
    public class VegetationLoader : DataLoader<Vegetation, VegetationLoader>
    {
        protected override string directoryName => "Vegetations";
        protected override string descendantName => "Vegetation";
    }
}
