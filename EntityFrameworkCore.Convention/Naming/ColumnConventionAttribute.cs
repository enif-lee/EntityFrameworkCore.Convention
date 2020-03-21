using System;

namespace EntityFrameworkCore.Convention.Naming
{
    public class ColumnConventionAttribute : Attribute, INamingConventionAttribute
    {
        public string Prefix { get; set; }
        public string Suffix { get; set; }
    }
}