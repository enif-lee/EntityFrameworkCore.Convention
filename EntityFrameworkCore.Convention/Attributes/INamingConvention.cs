namespace EntityFrameworkCore.Convention.Attributes
{
    public interface INamingConvention
    {
        string Prefix { get; set; }

        string Suffix { get; set; }
    }
}