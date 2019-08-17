namespace EntityFrameworkCore.Convention
{
    public interface INamingConvention
    {
        string Convert(NameMeta nameMeta);
    }
}