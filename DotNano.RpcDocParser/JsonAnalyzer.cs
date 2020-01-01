using DotNano.Shared;
using DotNano.Shared.DataTypes;
using DotNano.Shared.Model;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace DotNano.RpcDocParser
{
    public class JsonAnalyzer
    {
        public SimpleJson Convert(string json)
        {
            var parsed = JObject.Parse(json);
            return Convert(parsed);
        }

        private SimpleJson Convert(JObject jObject)
        {
            var targetJson = new SimpleJson();
            foreach (var token in jObject)
            {
                if (token.Value is JValue jValue)
                {
                    var type = TypeFromJValue(jValue);
                    var keyType = TypeFromValue(token.Key);
                    var finalFieldType = VerifyFieldType(type, token.Key);
                    var fieldName = GetFieldName(token.Key, keyType);

                    if (!targetJson.Fields.ContainsKey(fieldName) && fieldName != "action")
                        targetJson.Fields.Add(fieldName, finalFieldType);
                    continue;
                }

                if (token.Value is JObject nestedObject)
                {
                    var type = TypeFromValue(token.Key);
                    var fieldName = GetFieldName(token.Key, type);

                    if (IsPredefinedType(type))
                        targetJson.Fields.Add(token.Key, type);
                    else if (!targetJson.InnerJsons.ContainsKey(fieldName))
                    {
                        var innerJson = Convert(nestedObject);
                        if (IsPredefinedType(innerJson, out var predefinedType))
                            targetJson.Fields.Add(fieldName, predefinedType);
                        else
                            targetJson.InnerJsons.Add(fieldName, innerJson);
                    }
                    continue;
                }

                if (token.Value is JArray jArray)
                {
                    if (jArray.Count == 0)
                        targetJson.Fields.Add(token.Key, typeof(IEnumerable<>));
                    else
                    {
                        if (jArray.First is JObject arrayJObject)
                        {
                            var elementObject = Convert(arrayJObject);
                            targetJson.ArraysOfObjects.Add(token.Key, elementObject);
                        }
                        else
                        {
                            var type = TypeFromJValue((JValue)jArray.First);
                            var enumerableType = typeof(IEnumerable<>).MakeGenericType(type);
                            targetJson.Fields.Add(token.Key, enumerableType);
                        }
                    }
                }
            }

            return targetJson;
        }
        
        private bool IsPredefinedType(SimpleJson json, out Type predefinedType)
        {
            predefinedType = null;

            var blockContentProperties = typeof(BlockContent).GetProperties().Select(x => x.Name.ToLowerInvariant());
            if (!json.InnerJsons.Any() && json.Fields.Count() > 3 && json.Fields.Select(x => Tools.ToPascalCase(x.Key).ToLowerInvariant()).All(x => blockContentProperties.Contains(x)))
            {
                predefinedType = typeof(BlockContent);
                return true;
            }

            return false;
        }

        private bool IsPredefinedType(Type type) => type == typeof(BlockContent);

        private string GetFieldName(string actualFieldName, Type type)
        {
            if (type == typeof(HexKey64))
                return HexKey64.Default;
            else if (type == typeof(HexKey128))
                return HexKey128.Default;
            else if (type == typeof(PublicAddress))
                return PublicAddress.Default;

            return actualFieldName;
        }

        private Type VerifyFieldType(Type type, string fieldName)
        {
            if (type == typeof(long) && (fieldName == "balance" || fieldName == "pending" || fieldName == "weight"))
                return typeof(BigInteger);

            return type;
        }

        private Type TypeFromJValue(JValue jValue)
        {
            var value = jValue.Value<string>();
            return TypeFromValue(value);
        }

        private Type TypeFromValue(string value)
        {
            if(value == "block" || value == "contents")
                return typeof(BlockContent);
            else
                return Tools.TypeFromValue(value);
        }
    }
}
