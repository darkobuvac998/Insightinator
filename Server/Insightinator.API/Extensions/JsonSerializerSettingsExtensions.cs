using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Insightinator.API.Extensions
{
    public static class JsonSerializerSettingsExtensions
    {
        public static JsonSerializerSettings DefaultSettings(this JsonSerializerSettings settings)
        {
            settings.NullValueHandling = NullValueHandling.Ignore;
            settings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            settings.ContractResolver = new DefaultContractResolver
            {
                NamingStrategy = new CamelCaseNamingStrategy { OverrideSpecifiedNames = false }
            };

            return settings;
        }
    }
}
