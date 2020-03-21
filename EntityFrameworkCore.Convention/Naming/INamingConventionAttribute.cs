namespace EntityFrameworkCore.Convention.Naming
{
    public interface INamingConventionAttribute
    {
        string Prefix { get; set; }

        string Suffix { get; set; }
    }
}