using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GAPResources
{
    public static class GAResourceManager
    {
        public static string GetResourceString(string key)
        {
            return GAPResources.LocalResorces.GAResource.ResourceManager.GetString(key);
        }

        public static string GetResourceString(string key, CultureInfo cultureInfo)
        {
            return GAPResources.LocalResorces.GAResource.ResourceManager.GetString(key, cultureInfo);
        }
    }
}
