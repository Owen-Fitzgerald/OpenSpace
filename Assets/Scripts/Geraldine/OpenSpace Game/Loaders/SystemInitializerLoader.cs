using Geraldine._4XEngine.Contracts;
using Geraldine._4XEngine.Initializers;
using Geraldine._4XEngine.Loaders.Interfaces;
using Geraldine._4XEngine.Util;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Xml.Linq;
using UnityEngine.Rendering.VirtualTexturing;

namespace Geraldine._4XEngine.Loaders
{
    public class SystemInitializerLoader : DataLoader<SystemInitializer, SystemInitializerLoader>
    {
        protected override string directoryName => "System Inits";
        protected override string descendantName => "Predefined_System";
    }
}
