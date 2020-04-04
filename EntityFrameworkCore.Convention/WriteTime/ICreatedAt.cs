using System;

namespace EntityFrameworkCore.Convention.WriteTime
{
    public interface ICreatedAt
    {
        DateTime CreatedAt { get; set; }
    }
}