using Geraldine._4XEngine.GraphTree.Interfaces;
using System.IO;

namespace Geraldine._4XEngine.Units.Interfaces
{
    public interface IExistOnNodeGraph
    {
        public INodeGraph ParentGraph { get; set; }

        public INode Location { get; set; }

        public float Orientation { get; set; }

        public void ValidateLocation();

        public void Kill();

        public void Save(BinaryWriter writer);
    }
}
