# EntityFrameworkCore.Convention
> Life is short, use `EntityFrameworkCore.Convention`

[![Build status](https://ci.appveyor.com/api/projects/status/7lsoshhc7ju8ug09/branch/master?svg=true)](https://ci.appveyor.com/project/enif-lee/entityframeworkcore-convention/branch/master)

We have always done same thing when we started every EntityFrameworkCore project.
This is a library that collects frequently used / implemented functions as below.

### Features

#### Naming Convention

- [ ] Global Table Default Schema
- [ ] Global Table Prefix/Suffix
- [ ] Global/Table Scope Column Prefix/Suffix
- [ ] Column Naming Convention { built in kebob, snake, camel, Pascal }

```csharp
optionsBuilder
    .UseNamingConvention(Conventions.Pascal)
    .UseGlobalTablePrefix("ef")
    .UseGlobalColumnPrefix("c")
;

// Entity Classes

[Table]
[ColumnNaming(Prefix: "uh")]
public class UserHistory
{
    public long Id { get; set; } // will be uh_id

    [ColumnNaming(string.Empty)
    public long IgnoredColumn { get; set; } // will be ignored_column
}

```

#### BaseEntity

- [ ] Provide CreateAt, UpdateAt Interface
- [ ] Provide Creator, Updater Interface

```csharp
optionsBuilder
    .UseAutoUpdateDateTime()
    .UseAutoUpdateAuthor(() => Http.Context.Session['userId'])
;

// Entity Class
public class Something : ICreateAt, IUpdateAt, ICreator
{
    public long Id { get; set; }
}
```

#### Enum Conversion Helper

- [ ] Provide Enum with attribute conversion helper

```csharp
[Conversion]
public enum UserStatus
{
    [Value("BL")] Blocked,
    [Value("LK")] Locked,
    [Value("NM")] Normal,
    [Value("DT")] Deleted,
    [Value("HD")] Hidden
}

public DatabaseContext : ConventionDbContext
{
    public DbSet<User> Users { get; set; }
}

// Startup.cs

optionsBuilder
    .UseInMemoryDatabase()
    .UseEnumConversion();
```

### Contribute Guide

First of all, We need to conversation through issue or PR about some your great idea.

(Please let us know your issue before register PR so that your thoughts will not be in vain.)

### License

MIT
