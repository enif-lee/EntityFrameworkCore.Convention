# EntityFrameworkCore.Convention
> Life is short, use `EntityFrameworkCore.Convention`

![Build&Test](https://github.com/enif-lee/EntityFrameworkCore.Convention/workflows/Build&Test/badge.svg)

We have always done same thing when we started every EntityFrameworkCore project.
This is a library that collects frequently used / implemented functions as below.

### Features

#### Naming Convention

- [ ] Global Table Default Schema
- [ ] Global Table Prefix/Suffix
- [ ] Global/Table Scope Column Prefix/Suffix
- [ ] Configure Specific Table Naming Convention
- [ ] Configure Specific Column Naming Convention { built in kebob, snake, camel, Pascal }

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

- [ ] Provide ICreateAt, IUpdateAt Interface
- [ ] Provide ICreator, IUpdater Interface

```csharp
optionsBuilder
    .UseAutoUpdateDateTime()
    .UseAutoUpdateAuthor(() => Http.Context.Session['userId'])
;

protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
{
    optionsBuilder.UseEnumConversion<UserStatus>();
}

// Entity Class
public class Something : ICreateAt, IUpdateAt, ICreator
{
    public long Id { get; set; }
}
```

#### Enum String Value Converter

If you don't want to store enum value as int(number) value and you want to store enum value as string,
you just attach `EnumValue` to all enum values with unique value and just call extension method `ModelBuilder.ApplyEnumValueConverter`
then the `ApplyEnumValueConverter` method find properties that defined enum type of type parameter and apply 1:1 value converter.

Additionally, this feature also help automatically maximum string length of original property. 
If maximum length of `EnumValue` is changed, the model metadata of model type also will be changed.
 
```csharp
public enum UserStatus
{
    [EnumValue("BL")] 
    Blocked,

    [EnumValue("LK")] 
    Locked,

    [EnumValue("NM")] 
    Normal,

    [EnumValue("DT")] 
    Deleted,

    [EnumValue("HD")] 
    Hidden

}

public DatabaseContext : DbContext
{
    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyEnumValueConverter<UserStatus>();
    }
}
```

### Table Audit

#### Features
-  Track and store changes of row for specific assigned tables.
-  Configure tracking strategy for each entity.
-  Support for project contain multiple `DbContext` type.

### Contribution Guide

First of all, We need to conversation through issue or PR about some your great idea.

(Please let us know your issue before register PR so that your thoughts will not be in vain.)

### License

MIT
