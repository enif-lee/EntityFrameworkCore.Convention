namespace EntityFrameworkCore.Convention.Attributes
{
    public interface INamingConventionAttribute
    {
        string Prefix { get; set; }

        string Suffix { get; set; }
    }
}