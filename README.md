# EntityFrameworkCore.Convention
> Life is short, use `EntityFrameworkCore.Convention`

![Build&Test](https://github.com/enif-lee/EntityFrameworkCore.Convention/workflows/Build&Test/badge.svg)

We have always done same thing when we started every EntityFrameworkCore project.
This is a library that collects frequently used / implemented functions as below.

### Features

#### Naming Convention

If you need to configure some naming convention for following legacy database. Follow below guide for applying global table/column convention

##### Preconfigured Table Naming Convention

```csharp
public class NamingDb : DbContext
{
    public DbSet<TestEntity> TestEntities { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // and TestEntity class will has 'test_entities' as name
        modelBuilder.UseNamingConvention(builder => builder.UseTableNamingConvention(NamingConvention.LowerSnakeCase));
    }
}
```

// Todo to write more example here

#### State Extension

If you want to log entity last state (New, Modified, Deleted) or apply logical deletion for entity, Here are extension for that.

```
public class SomeDb : DbContext
{
    // Define constructor and entity fiels.

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Enable logical deletion for IState Entities.
        modelBuilder.ApplyIgnoreDeletedStateEntitiesFromQuery();
        base.OnModelCreating(modelBuilder);
    }

    public override int SaveChanges(bool acceptAllChangesOnSuccess)
    {
        // Update entities that implemented IState.
        ChangeTracker.UpdateStateFields();
        return base.SaveChanges(acceptAllChangesOnSuccess);
    }

    public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = new CancellationToken())
    {
        // Update entities that implemented IState.
        ChangeTracker.UpdateStateFields();
        return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
    }
}
```

#### WriteTime Extension.

Entity need to record `created_at` or `updated_at` as database independent. Just implement `ICreatedAt` and `IUpdatedAt` in your entity class.
And call `UpdateWriteTimeFields` of `ChangeTracker` in override `SaveChanges` and `SaveChangesAsync`

```csharp
public SomeDb {
    public DbSet<ExampleEntity> Entities { get; set; }

    public override int SaveChanges(bool acceptAllChangesOnSuccess)
    {
        ChangeTracker.UpdateWriteTimeFields();
        return base.SaveChanges(acceptAllChangesOnSuccess);
    }

    public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = new CancellationToken())
    {
        ChangeTracker.UpdateWriteTimeFields();
        return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
    }
}

public class ExampleEntity : ICreatedAt, IUpdatedAt
{
    public long Id { get; set; }
    public string Message { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
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
