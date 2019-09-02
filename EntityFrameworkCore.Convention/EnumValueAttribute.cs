using System;

namespace EntityFrameworkCore.Convention
{
    [AttributeUsage(AttributeTargets.Field)]
    public class EnumValueAttribute : Attribute
    {
        public string Value { get; set; }

        public EnumValueAttribute(string value)
        {
            Value = value;
        }
    }
}