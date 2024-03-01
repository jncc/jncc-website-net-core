using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JNCC.PublicWebsite.Core.Utilities
{
    internal static class JsonUtility
    {
        public static bool TryParseJson<T>(this string obj, out T result)
        {
            try
            {
                // Validate missing fields of object
                var settings = new JsonSerializerSettings
                {
                    MissingMemberHandling = MissingMemberHandling.Error,
                    PreserveReferencesHandling = PreserveReferencesHandling.Objects
                };

                result = JsonConvert.DeserializeObject<T>(obj, settings);
                return true;
            }
            catch
            {
                result = default(T);
                return false;
            }
        }
    }
}
