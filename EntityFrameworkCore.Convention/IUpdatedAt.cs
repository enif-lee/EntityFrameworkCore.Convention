using System;

namespace EntityFrameworkCore.Convention
{
    public interface IUpdatedAt
    {
        DateTime UpdatedAt { get; set; }
    }
}