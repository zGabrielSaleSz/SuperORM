## SuperORM
This is not a simple ORM, this is Actually a **SUPER ORM**, but simple

First of all, it's awesome how ORM can be able to understand your operations and convert that to SQL queries and handle your own objects based in configurations (and you should never doubt how good they are), but over the time you sometimes find yourself with performance issues and you do not even know what is going on. For that cases, you can use the most flexible way which is build your own queries using ADO.NET connections.

If you're a C# programmer and uses Entity framework you probably will notice that sometimes ORM builds the queries in uneffective ways - especially using Include - that occasionally will cause performance issues and hard work for database.

But that is a general issue about ORMs, in the most part of the time, you should know exactly how your ORM works, so you can write in the better way to impact in the final query result, so you basically get used to it.

## Target
The ideia of **SuperORM** is to allow you not to choose between implicit instructions(auto generated queries) or flexibillity, but using both as it's needed, using OOP to manage and create your queries and prevent errors, allowing a query construction that looks like the SQL sintax itself

With the base code, it becomes easy to support any other SQL relational database since that they should handle the most part of their condition, implementing few interfaces.
So, you don't have to find and learn from another ORM in case you're handling a different database type.

Currently the supported ones:
- MySQL 
- SQLServer

Planning
- Postgree
- SQLite
- MariaDB

## Examples

### To begin with

You should configurate your connection provider, you actually can configurate as many as you need, even with different SQL databases
```cs
ConnectionProvider connectionProvider = new MySql.ConnectionProvider("connectionStringHere");
```

### By implementing Repositories
```cs
public class UserRepository : BaseRepository<User, int> //Entity type and PrimaryKey type
{
    public override void Configurate()
    {
        SetTable("users");
        SetPrimaryKey(u => u.id);
    }
}
```

So you can use it as
```cs
// Singleton Settings will allow you to set preferences and default connections for your repositories 
Setting setting = Setting.GetInstance();
setting.SetConnection(connectionProvider);

ISelectable<User> selectable = userRepository.Select()
                .Where(u => u.ID != 1 && (u.Name == "Gabriel" || u.email.Contains("gabriel") && u.active == false)
                .OrderByDescending(u => u.id);
                
// Just build the query
string query = selectable.GetQuery();

// Executes
User[] user = selectable
    .AsEnumerable()
    .ToArray();

```
### By creating directly LINQ expressions
```cs
 Selectable<User> selectableSingle = new Selectable<User>(connectionProvider.GetConnection(), connectionProvider.GetQuerySintax());
 User specialUser = selectableSingle
    .From("users")
    .Where(u => u.id != 5)
    .OrderByDescending(u => u.Name)
    .Skip(10)
    .Take(1)
    .FirstOrDefault();
 
 Selectable<User> selectableMany = new Selectable<User>(connectionProvider.GetConnection(), connectionProvider.GetQuerySintax());
 IEnumerable<User> selectedUsers = selectableMany
    .From("users")
    .Where(u => u.id != 5)
    .OrderByDescending(u => u.Name)
    .Take(100)
    .AsEnumerable();
```
You can also build complex select with joins such as

```cs
var selectable = new Selectable<User>(connectionProvider.GetConnection(), connectionProvider.GetQuerySintax())
    .Select(
        a => a.id,
        a => a.Name,
        a => a.email,
        a => a.active
    )
    .Select<Document>(
        d => d.issueDate
    )
    .Select<DocumentType>(
        t => t.name
    )
    .From("users")
    .InnerJoin<Document>("documents", a => a.id, b => b.idUser)
    .LeftJoin<Document, DocumentType>("documentTypes", d => d.idDocumentType, t => t.id)
    .Where(u => u.Name.Contains("Gabriel"));
    
string result = selectable.GetQuery();
```

You can check more details through Tests project


## How it works
There are these principles to access your database with objects
- Repositories (recommended as usual use)
  - Implementation of BaseRepository\<T\>
  
- LINQ (recommended in case you need to be precise in your operations)
  - Insertable\<T\>
  - Updatable\<T\>
  - Deletable\<T\>
  - Selectable\<T\>
  
**Consider that Repositories will use LINQ operations to build the generated queries**
  
- QueryBuilders (not recommended)
  - InsertableBuilder
  - UpdatableBuilder
  - DeletableBuilder
  - SelectableBuilder
  
 **Consider that LINQ will use QueryBuilders to build the generated queries, this layer has no access to database execution**
 
 LINQ and QueryBuilder will provide you the following methods to run your query
 - string GetQuery()
 - ParameterizedQuery GetQueryWithParameters()

Parameterized queries are used in LINQ when Executes is called, the query is safe builded using ADO.NET parameters
 
