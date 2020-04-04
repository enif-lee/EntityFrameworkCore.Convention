namespace EntityFrameworkCore.Convention.Naming
{
    public interface INamingConvention
    {
        string Convert(NameMeta nameMeta);
    }
}