namespace EntityFrameworkCore.Convention.Attributes
{
    public class ColumnConventionAttribute : System.Attribute, INamingConvention
    {
        public string Prefix { get; set; }
        public string Suffix { get; set; }
    }
}