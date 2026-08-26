namespace DbScoutBaby.Models;

public class DatabaseMetadata
{
    public List<DbObjectMetadata> Objects { get; set; } = new();
    public List<ColumnMetadata> Columns { get; set; } = new();
    public List<IndexMetadata> Indexes { get; set; } = new();
    public List<UserTypeMetadata> UserTypes { get; set; } = new();
    public List<AgentObjectMetadata> AgentObjects { get; set; } = new();

    public DateTime LoadedAt { get; set; } = DateTime.Now;
}

public class DbObjectMetadata
{
    public string Schema { get; set; } = "";
    public string Name { get; set; } = "";
    public string ParentObject { get; set; } = "";
    public string TypeDescription { get; set; } = "";
    public string Definition { get; set; } = "";
    public string Extra { get; set; } = "";
    public SearchResultType SearchType { get; set; }
}

public class ColumnMetadata
{
    public string Schema { get; set; } = "";
    public string ParentObject { get; set; } = "";
    public string Name { get; set; } = "";
    public string DataType { get; set; } = "";
}

public class IndexMetadata
{
    public string Schema { get; set; } = "";
    public string Table { get; set; } = "";
    public string Name { get; set; } = "";
    public string TypeDescription { get; set; } = "";
    public bool IsUnique { get; set; }
}

public class UserTypeMetadata
{
    public string Schema { get; set; } = "";
    public string Name { get; set; } = "";
    public string BaseType { get; set; } = "";
}

public class AgentObjectMetadata
{
    public SearchResultType Type { get; set; }
    public string Name { get; set; } = "";
    public string Extra { get; set; } = "";
    public string Definition { get; set; } = "";
}
