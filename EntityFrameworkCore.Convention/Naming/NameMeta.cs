namespace EntityFrameworkCore.Convention.Naming
{
    public class NameMeta
    {
        public string Prefix { get; set; }

        public string Name { get; set; }

        public string Suffix { get; set; }

        public static implicit operator NameMeta(string name)
        {
            return new NameMeta
            {
                Name = name
            };
        }
    }
}