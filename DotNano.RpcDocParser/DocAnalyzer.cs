using DotNano.Shared.DataTypes;
using DotNano.Shared.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;

namespace DotNano.RpcDocParser
{
    public class DocAnalyzer
    {
        private readonly JsonAnalyzer _jsonAnalyzer;

        public DocAnalyzer(JsonAnalyzer jsonAnalyzer)
        {
            _jsonAnalyzer = jsonAnalyzer;
        }

        public IEnumerable<RpcCallCodeDefinition> Analyze(IEnumerable<RpcCallDocDefinition> rpcDocCalls)
        {
            foreach (var rpcDocCall in rpcDocCalls)
            {
                var requestFields = new Dictionary<string, Type>();
                var parsedRequests = new List<SimpleJson>();
                foreach (var json in rpcDocCall.JsonRequests)
                {
                    var simpleJson = _jsonAnalyzer.Convert(json);
                    simpleJson.Fields.ToList().ForEach(x => requestFields[x.Key] = x.Value);
                    parsedRequests.Add(simpleJson);
                }

                foreach (var optionalParam in rpcDocCall.OptionalParameters)
                {
                    if (!requestFields.ContainsKey(optionalParam.Key))
                    {
                        var type = GetTypeFromParamDescription(optionalParam.Value);
                        requestFields[optionalParam.Key] = type;
                    }
                }

                var requiredFields = requestFields.Where(x => !rpcDocCall.OptionalParameters.ContainsKey(x.Key) && parsedRequests.All(y => y.Fields.ContainsKey(x.Key)))
                    .ToDictionary(x => x.Key, x => x.Value);
                var optionalFields = requestFields.Except(requiredFields).ToDictionary(x => x.Key, x => x.Value);

                var responseJsons = new List<SimpleJson>();
                rpcDocCall.JsonResponses.ForEach(x => responseJsons.Add(_jsonAnalyzer.Convert(x)));
                var responseJson = Merge(responseJsons);
                var rpcCodeCall = new RpcCallCodeDefinition(rpcDocCall.MethodName, requiredFields, optionalFields, responseJson, rpcDocCall);

                yield return rpcCodeCall;
            }
        }

        private SimpleJson Merge(IEnumerable<SimpleJson> responseJsons)
        {
            if (responseJsons.Count() == 1)
                return responseJsons.Single();

            var resultJson = new SimpleJson();
            var allFields = responseJsons.SelectMany(x => x.Fields).GroupBy(x => x.Key);
            foreach (var field in allFields)
            {
                var type = field.First().Value;
                if (field.Count() < responseJsons.Count() && type.IsValueType)
                    resultJson.Fields.Add(field.Key, typeof(Nullable<>).MakeGenericType(type));
                else
                    resultJson.Fields.Add(field.Key, type);
            }

            var allInnerObjects = responseJsons.SelectMany(x => x.InnerJsons).GroupBy(x => x.Key);
            foreach (var innerObject in allInnerObjects)
            {
                resultJson.InnerJsons.Add(innerObject.Key, Merge(innerObject.Select(x => x.Value)));
            }

            var allArrays = responseJsons.SelectMany(x => x.ArraysOfObjects).GroupBy(x => x.Key);
            foreach (var array in allArrays)
            {
                resultJson.InnerJsons.Add(array.Key, Merge(array.Select(x => x.Value)));
            }

            return resultJson;
        }

        private Type GetTypeFromParamDescription(string description)
        {
            description = description.ToLower();
            if (description.Contains("bool"))
                return typeof(bool);
            if (description.Contains("false") || description.Contains("true"))
                return typeof(bool);
            if (description.Contains("256 bit"))
                return typeof(HexKey64);
            if (description.Contains("hash"))
                return typeof(HexKey64);
            if (description.Contains("number"))
                return typeof(BigInteger);
            if (description.Contains("account"))
                return typeof(PublicAddress);
            if (description.Contains("string"))
                return typeof(string);

            return typeof(string);
        }
    }
}
