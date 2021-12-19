using System;

namespace Candle.Model.Enums.EnumExtensions.Attributes
{
    [AttributeUsage(AttributeTargets.Field)]
    public class ValueAttribute : Attribute
    {
        public string Value { get; }

        public ValueAttribute(string value)
        {
            Value = value;
        }

    }
}
