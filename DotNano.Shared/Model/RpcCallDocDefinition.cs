using System;
using System.Collections.Generic;
using System.Linq;

namespace DotNano.Shared.Model
{
    public class RpcCallDocDefinition
    {
        public string MethodName { get; }
        public string Description { get; set; }
        public List<string> JsonRequests { get; } = new List<string>();
        public List<string> JsonResponses { get; } = new List<string>();
        public Dictionary<string, string> OptionalParameters { get; } = new Dictionary<string, string>();

        public RpcCallDocDefinition(string methodName)
        {
            MethodName = methodName;
        }

        public override string ToString()
        {
            return $"{MethodName}\nRequests:\n{JsonRequests.Aggregate((x, y) => $"{x}\n----------\n{y}")}\nResponses:\n{JsonResponses.Aggregate((x, y) => $"{x}\n----------\n{y}")}\n{(OptionalParameters.Any() ? OptionalParameters.Select(x => x.Key).Aggregate((x, y) => $"{x}, {y}") : String.Empty)}";
        }
    }
}
