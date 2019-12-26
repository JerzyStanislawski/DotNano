using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DotNano.Shared.Model
{
    public class SimpleJson
    {
        public Dictionary<string, Type> Fields { get; }  = new Dictionary<string, Type>();

        public Dictionary<string, string> FieldsOfGeneratedTypes { get; } = new Dictionary<string, string>();

        public Dictionary<string, SimpleJson> InnerJsons { get; } = new Dictionary<string, SimpleJson>();

        public Dictionary<string, SimpleJson> ArraysOfObjects { get; } = new Dictionary<string, SimpleJson>();

        public override string ToString()
        {
            return $"{{{String.Join('\n', Fields.Select(x => $"{x.Key}: {x.Value}"))}\n{String.Join('\n', InnerJsons.Select(x => $"{x.Key}: {x.Value.ToString()}"))}\n|||{String.Join('\n', ArraysOfObjects.Select(x => $"{x.Key}: {x.Value.ToString()}"))}}}";
        }
    }
}
