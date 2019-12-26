using System;
using System.Collections.Generic;
using System.Linq;

namespace DotNano.Shared.Model
{
    public class RpcCallCodeDefinition
    {
        public string MethodName { get; }
        public Dictionary<string, Type> RequiredParameters { get; }
        public Dictionary<string, Type> OptionalParameters { get; }
        public SimpleJson Response { get; }
        public RpcCallDocDefinition RpcCallDoc { get; }

        public RpcCallCodeDefinition(string methodName, Dictionary<string, Type> requiredParameters, Dictionary<string, Type> optionalParameters, SimpleJson response, RpcCallDocDefinition rpcCallDoc)
        {
            MethodName = methodName;
            RequiredParameters = requiredParameters;
            OptionalParameters = optionalParameters;
            Response = response;
            RpcCallDoc = rpcCallDoc;
        }

        public override string ToString()
        {
            //return $"{MethodName}\n{String.Join(',', RequiredParameters.Select(x => $"{x.Value.Name} {x.Key}"))}\n{String.Join(',', OptionalParameters.Select(x => $"{x.Value.Name} {x.Key}"))}\n-------------------------------";
            return $"{MethodName}\n{Response.ToString()}\n-------------------------------";
        }
    }
}
