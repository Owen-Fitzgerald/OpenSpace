using Geraldine._4XEngine.Contracts;

namespace Geraldine._4XEngine.Loaders
{
    public class SoilQualityLoader : DataLoader<SoilQuality, SoilQualityLoader>
    {
        protected override string directoryName => "Soil Qualities";
        protected override string descendantName => "SoilQuality";
    }
}
