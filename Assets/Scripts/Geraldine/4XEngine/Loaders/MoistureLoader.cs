using Geraldine._4XEngine.Contracts;

namespace Geraldine._4XEngine.Loaders
{
    public class MoistureLoader : DataLoader<Moisture, MoistureLoader>
    {
        protected override string directoryName => "Moistures";
        protected override string descendantName => "Moisture";
    }
}
