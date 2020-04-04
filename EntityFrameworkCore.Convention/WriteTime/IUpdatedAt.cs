using System;

namespace EntityFrameworkCore.Convention.WriteTime
{
    public interface IUpdatedAt
    {
        DateTime UpdatedAt { get; set; }
    }
}