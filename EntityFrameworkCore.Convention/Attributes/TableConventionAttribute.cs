namespace EntityFrameworkCore.Convention.Attributes
{
    public class TableConventionAttribute : System.Attribute, INamingConvention
    {
        public string Prefix { get; set; }

        public string Suffix { get; set; }
    }
}