using System.Text;
using DbScoutBaby.Models;

namespace DbScoutBaby.Services;

public class DatabaseSearchService
{
    private readonly MetadataCacheService _cache;

    public int MaximumResults { get; set; } = 1500;

    public DatabaseSearchService(
        MetadataCacheService cache)
    {
        _cache = cache;
    }

    public async Task<List<SearchResult>> SearchAsync(
        string database,
        string connectionString,
        string searchText,
        bool searchCode,
        int minimumSimilarity,
        CancellationToken cancellationToken = default)
    {
        DatabaseMetadata metadata =
            await _cache.GetMetadataAsync(
                database,
                connectionString,
                false,
                cancellationToken);

        var results =
            new List<SearchResult>();

        SearchObjects(
            metadata,
            results,
            database,
            searchText,
            searchCode,
            minimumSimilarity);

        SearchColumns(
            metadata,
            results,
            database,
            searchText,
            minimumSimilarity);

        SearchIndexes(
            metadata,
            results,
            database,
            searchText,
            minimumSimilarity);

        SearchUserTypes(
            metadata,
            results,
            database,
            searchText,
            minimumSimilarity);

        SearchAgentObjects(
            metadata,
            results,
            database,
            searchText,
            minimumSimilarity);

        return results
            .OrderByDescending(
                x => x.Similarity)
            .ThenBy(
                x => x.ResultType)
            .ThenBy(
                x => x.FullName)
            .Take(MaximumResults)
            .ToList();
    }

    public Task<DatabaseMetadata> RefreshMetadataAsync(
        string database,
        string connectionString,
        CancellationToken cancellationToken = default)
    {
        return _cache.GetMetadataAsync(
            database,
            connectionString,
            true,
            cancellationToken);
    }

    private static void SearchObjects(
        DatabaseMetadata metadata,
        List<SearchResult> results,
        string database,
        string searchText,
        bool searchCode,
        int minimumSimilarity)
    {
        foreach (DbObjectMetadata obj
                 in metadata.Objects)
        {
            int score =
                BestScore(
                    searchText,
                    obj.Name,
                    obj.ParentObject,
                    obj.Extra,
                    obj.TypeDescription,
                    $"{obj.Schema}.{obj.Name}");

            if (score >= minimumSimilarity)
            {
                results.Add(
                    new SearchResult
                    {
                        Database =
                            database,

                        Schema =
                            obj.Schema,

                        ObjectName =
                            obj.Name,

                        ParentObject =
                            obj.ParentObject,

                        Extra =
                            string.IsNullOrWhiteSpace(obj.Extra)
                                ? obj.TypeDescription
                                : obj.Extra,

                        Type =
                            obj.SearchType,

                        Match =
                            obj.Name,

                        Similarity =
                            score,

                        PreviewText =
                            BuildObjectPreview(obj)
                    });
            }

            if (!searchCode ||
                string.IsNullOrWhiteSpace(obj.Definition))
            {
                continue;
            }

            int codeScore =
                Math.Max(
                    obj.Definition.Contains(
                        searchText,
                        StringComparison.OrdinalIgnoreCase)
                        ? 100
                        : 0,

                    FuzzySearch.KeywordCoverageScore(
                        obj.Definition,
                        searchText));

            if (codeScore < minimumSimilarity)
                continue;

            results.Add(
                new SearchResult
                {
                    Database =
                        database,

                    Schema =
                        obj.Schema,

                    ObjectName =
                        obj.Name,

                    ParentObject =
                        obj.ParentObject,

                    Extra =
                        obj.TypeDescription,

                    Type =
                        SearchResultType.ObjectCode,

                    Match =
                        searchText,

                    Similarity =
                        codeScore,

                    PreviewText =
                        obj.Definition
                });
        }
    }

    private static void SearchColumns(
        DatabaseMetadata metadata,
        List<SearchResult> results,
        string database,
        string searchText,
        int minimumSimilarity)
    {
        foreach (ColumnMetadata column
                 in metadata.Columns)
        {
            int score =
                BestScore(
                    searchText,
                    column.Name,
                    column.ParentObject,
                    column.DataType,
                    $"{column.ParentObject}.{column.Name}",
                    $"{column.Schema}.{column.ParentObject}.{column.Name}");

            if (score < minimumSimilarity)
                continue;

            results.Add(
                new SearchResult
                {
                    Database =
                        database,

                    Schema =
                        column.Schema,

                    ObjectName =
                        column.Name,

                    ParentObject =
                        column.ParentObject,

                    DataType =
                        column.DataType,

                    Extra =
                        column.DataType,

                    Type =
                        SearchResultType.Column,

                    Match =
                        column.Name,

                    Similarity =
                        score,

                    PreviewText =
                        $"""
                        COLUMN
                        ─────────────────────────────────────
                        Database : {database}
                        Schema   : {column.Schema}
                        Parent   : {column.ParentObject}
                        Column   : {column.Name}
                        Type     : {column.DataType}

                        Example SQL
                        ─────────────────────────────────────
                        SELECT TOP (100)
                            [{Escape(column.Name)}]
                        FROM
                            [{Escape(column.Schema)}].[{Escape(column.ParentObject)}];
                        """
                });
        }
    }

    private static void SearchIndexes(
        DatabaseMetadata metadata,
        List<SearchResult> results,
        string database,
        string searchText,
        int minimumSimilarity)
    {
        foreach (IndexMetadata index
                 in metadata.Indexes)
        {
            int score =
                BestScore(
                    searchText,
                    index.Name,
                    index.Table,
                    index.TypeDescription);

            if (score < minimumSimilarity)
                continue;

            results.Add(
                new SearchResult
                {
                    Database =
                        database,

                    Schema =
                        index.Schema,

                    ObjectName =
                        index.Name,

                    ParentObject =
                        index.Table,

                    Extra =
                        $"{index.TypeDescription}; Unique={index.IsUnique}",

                    Type =
                        SearchResultType.Index,

                    Match =
                        index.Name,

                    Similarity =
                        score,

                    PreviewText =
                        $"""
                        INDEX
                        ─────────────────────────────────────
                        Database : {database}
                        Schema   : {index.Schema}
                        Table    : {index.Table}
                        Index    : {index.Name}
                        Type     : {index.TypeDescription}
                        Unique   : {index.IsUnique}
                        """
                });
        }
    }

    private static void SearchUserTypes(
        DatabaseMetadata metadata,
        List<SearchResult> results,
        string database,
        string searchText,
        int minimumSimilarity)
    {
        foreach (UserTypeMetadata type
                 in metadata.UserTypes)
        {
            int score =
                BestScore(
                    searchText,
                    type.Name,
                    type.BaseType);

            if (score < minimumSimilarity)
                continue;

            results.Add(
                new SearchResult
                {
                    Database =
                        database,

                    Schema =
                        type.Schema,

                    ObjectName =
                        type.Name,

                    Extra =
                        type.BaseType,

                    Type =
                        SearchResultType.UserType,

                    Match =
                        type.Name,

                    Similarity =
                        score,

                    PreviewText =
                        $"""
                        USER TYPE
                        ─────────────────────────────────────
                        Database  : {database}
                        Schema    : {type.Schema}
                        Name      : {type.Name}
                        Base Type : {type.BaseType}
                        """
                });
        }
    }

    private static void SearchAgentObjects(
        DatabaseMetadata metadata,
        List<SearchResult> results,
        string database,
        string searchText,
        int minimumSimilarity)
    {
        foreach (AgentObjectMetadata agent
                 in metadata.AgentObjects)
        {
            int score =
                BestScore(
                    searchText,
                    agent.Name,
                    agent.Extra,
                    agent.Definition);

            if (score < minimumSimilarity)
                continue;

            results.Add(
                new SearchResult
                {
                    Database =
                        database,

                    ObjectName =
                        agent.Name,

                    Extra =
                        agent.Extra,

                    Type =
                        agent.Type,

                    Match =
                        agent.Name,

                    Similarity =
                        score,

                    PreviewText =
                        $"""
                        {agent.Type}
                        ─────────────────────────────────────
                        Server    : {DatabaseSettings.ServerName}
                        Source DB : msdb
                        Name      : {agent.Name}

                        {agent.Extra}

                        {agent.Definition}
                        """
                });
        }
    }

    private static int BestScore(
        string searchText,
        params string[] values)
    {
        int best = 0;

        foreach (string value
                 in values)
        {
            if (string.IsNullOrWhiteSpace(value))
                continue;

            best =
                Math.Max(
                    best,
                    FuzzySearch.Similarity(
                        searchText,
                        value));
        }

        return best;
    }

    private static string BuildObjectPreview(
        DbObjectMetadata obj)
    {
        var builder =
            new StringBuilder();

        builder.AppendLine(
            $"{obj.SearchType.ToString().ToUpperInvariant()}");

        builder.AppendLine(
            "─────────────────────────────────────");

        builder.AppendLine(
            $"Schema : {obj.Schema}");

        builder.AppendLine(
            $"Name   : {obj.Name}");

        builder.AppendLine(
            $"Type   : {obj.TypeDescription}");

        if (!string.IsNullOrWhiteSpace(
                obj.ParentObject))
        {
            builder.AppendLine(
                $"Parent : {obj.ParentObject}");
        }

        if (!string.IsNullOrWhiteSpace(
                obj.Extra))
        {
            builder.AppendLine(
                $"Extra  : {obj.Extra}");
        }

        if (!string.IsNullOrWhiteSpace(
                obj.Definition))
        {
            builder.AppendLine();
            builder.AppendLine(
                "DEFINITION");

            builder.AppendLine(
                "─────────────────────────────────────");

            builder.AppendLine(
                obj.Definition);
        }

        return builder.ToString();
    }

    private static string Escape(
        string value)
    {
        return value.Replace(
            "]",
            "]]");
    }
}
