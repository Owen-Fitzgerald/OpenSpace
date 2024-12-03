using System.IO;

namespace Geraldine._4XEngine.Util
{
    /// <summary>
    /// Values that describe the contents of a cell.
    /// </summary>
    [System.Serializable]
    public struct HexValues
    {
        /// <summary>
        /// Seven values stored in 32 bits.
        /// XXXXXXXX XXXXXXXX XXXXXTTV VQQMMEEE.
        /// </summary>
        /// <remarks>Not readonly to support hot reloading in Unity.</remarks>
#pragma warning disable IDE0044 // Add readonly modifier
        int values;
#pragma warning restore IDE0044 // Add readonly modifier

        readonly int Get(int mask, int shift) =>
            (int)((uint)values >> shift) & mask;

        readonly HexValues With(int value, int mask, int shift) => new()
        {
            values = (values & ~(mask << shift)) | ((value & mask) << shift)
        };

        //Up to 8 possible accessed by index
        public readonly int Elevation => Get(7, 0);
        public readonly HexValues WithElevation(int value) => With(value, 7, 0);

        //Up to 4 possible accessed by index
        public readonly int Moisture => Get(3, 3);
        public readonly HexValues WithMoisture(int value) => With(value, 3, 3);

        //Up to 4 possible accessed by index
        public readonly int SoilQuality => Get(3, 5);
        public readonly HexValues WithSoilQuality(int value) => With(value, 3, 5);

        //Up to 4 possible accessed by index
        public readonly int Vegetation => Get(3, 7);
        public readonly HexValues WithVegetation(int value) => With(value, 3, 7);

        public readonly int Temperature => Get(3, 9);
        public readonly HexValues WithTemperature(int index) => With(index, 3, 9);

        //public readonly int ResourceIndex => Get(127, 17);
        //public readonly HexValues WithResourceIndex(int index) => With(index, 127, 17);

        //public readonly int ImprovementIndex => Get(255, 24);
        //public readonly HexValues WithImprovementIndex(int index) => With(index, 255, 24);

        /// <summary>
        /// Save the values.
        /// </summary>
        /// <param name="writer"><see cref="BinaryWriter"/> to use.</param>
        public readonly void Save(BinaryWriter writer)
        {
            writer.Write((byte)Elevation);
            writer.Write((byte)Moisture);
            writer.Write((byte)SoilQuality);
            writer.Write((byte)Vegetation);
            writer.Write((byte)Temperature);
            //writer.Write((byte)ResourceIndex);
            //writer.Write((byte)ImprovementIndex);
        }

        /// <summary>
        /// Load the values.
        /// </summary>
        /// <param name="reader"><see cref="BinaryReader"/> to use.</param>
        /// <param name="header">Header version.</param>
        public static HexValues Load(BinaryReader reader, int header)
        {
            HexValues values = default;
            values = values.WithElevation(reader.ReadByte());
            values = values.WithMoisture(reader.ReadByte());
            values = values.WithSoilQuality(reader.ReadByte());
            values = values.WithVegetation(reader.ReadByte());
            return values = values.WithTemperature(reader.ReadByte());
            //values = values.WithResourceIndex(reader.ReadByte());
            //return values.WithImprovementIndex(reader.ReadByte());
        }
    }
}