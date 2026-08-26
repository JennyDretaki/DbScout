namespace DbScoutBaby.Models;

public enum SearchResultType
{
    Table,
    Column,
    View,
    StoredProcedure,
    Function,
    Trigger,
    Synonym,
    Sequence,
    Constraint,
    Index,
    UserType,
    AgentJob,
    AgentAlert,
    AgentOperator,
    ObjectCode
}

public class SearchResult
{
    public string Database { get; set; } = "";
    public string Schema { get; set; } = "";
    public string ObjectName { get; set; } = "";
    public string ParentObject { get; set; } = "";
    public string Extra { get; set; } = "";
    public string DataType { get; set; } = "";
    public SearchResultType Type { get; set; }

    public string Match { get; set; } = "";
    public int Similarity { get; set; }
    public string PreviewText { get; set; } = "";

    public string ResultType => Type switch
    {
        SearchResultType.StoredProcedure => "Stored Procedure",
        SearchResultType.AgentJob => "Agent Job",
        SearchResultType.AgentAlert => "Agent Alert",
        SearchResultType.AgentOperator => "Agent Operator",
        SearchResultType.UserType => "User Type",
        SearchResultType.ObjectCode => "Object Code",
        _ => Type.ToString()
    };

    public string FullName =>
        string.IsNullOrWhiteSpace(Schema)
            ? ObjectName
            : $"{Schema}.{ObjectName}";
}
