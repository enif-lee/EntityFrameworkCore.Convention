using System;

namespace EntityFrameworkCore.Convention
{
    public interface ICreatedAt
    {
        DateTime CreatedAt { get; set; }
    }
}