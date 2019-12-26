using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace DotNano.Shared
{
    public class JsonSerializationSettings
    {
        public static JsonSerializerSettings PascalCaseSettings = new JsonSerializerSettings()
        {
            ContractResolver = new UnderscorePropertyNamesContractResolver(),
            MissingMemberHandling = MissingMemberHandling.Error
        };
    }

    public class UnderscorePropertyNamesContractResolver : DefaultContractResolver
    {
        public UnderscorePropertyNamesContractResolver()
        {
            NamingStrategy = new SnakeCaseNamingStrategy();
        }
    }
}
