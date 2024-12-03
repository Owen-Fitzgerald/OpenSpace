using Geraldine._4XEngine.Contracts;
using Geraldine._4XEngine.Loaders.Interfaces;
using Geraldine._4XEngine.Util;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Xml.Linq;

namespace Geraldine._4XEngine.Loaders
{
    public class PlanetClassLoader : DataLoader<PlanetClass, PlanetClassLoader>
    {
        protected override string directoryName => "Planet Classes";
        protected override string descendantName => "planet_class";
    }
}