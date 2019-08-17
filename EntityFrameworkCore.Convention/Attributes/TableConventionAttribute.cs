using System;

namespace EntityFrameworkCore.Convention.Attributes
{
    public class TableConventionAttribute : Attribute, INamingConvention
    {
        public string Prefix { get; set; }

        public string Suffix { get; set; }
    }
}