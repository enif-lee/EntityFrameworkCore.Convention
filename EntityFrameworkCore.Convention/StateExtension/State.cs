namespace EntityFrameworkCore.Convention.StateExtension
{
    public enum State
    {
        [EnumValue("C")]
        Created = 0x01,

        [EnumValue("U")]
        Updated = 0x02,

        [EnumValue("D")]
        Deleted = 0x04
    }
}