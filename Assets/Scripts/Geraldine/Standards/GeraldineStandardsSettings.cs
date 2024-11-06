using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Geraldine
{
    public static class GeraldineStandardsSettings
    {
        public const char TextBodyIndexSeperator = '~';

        public const string InfoResourcesFilePath = "Infos";

        public static List<string> LocalizationFiles = new List<string>() { "text-concepts",
                                                                            "text-height",
                                                                            "text-improvements",
                                                                            "text-resources",
                                                                            "text-tech",
                                                                            "text-terrain",
                                                                            "text-vegetation",
                                                                            "text-yields",
                                                                            };
    }
}
