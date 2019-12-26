using System;
using System.Collections.Generic;
using System.Text;

namespace DotNano.Shared.Model
{
    public class ParameterProperties
    {
        public string Name { get; }
        public ParameterType ParameterType { get; }
        public object DefaultValue { get; }
        public bool IsOptional { get; }

        public ParameterProperties(string name, ParameterType parameterType, object defaultValue, bool isOptional)
        {
            Name = name;
            ParameterType = parameterType;
            DefaultValue = defaultValue;
            IsOptional = isOptional;
        }

        public override string ToString()
        {
            return $"{ParameterType.ToString()} {Name}";
        }
    }
}
