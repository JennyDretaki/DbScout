using System.Collections.Concurrent;
using DbScoutBaby.Models;
using Microsoft.Data.SqlClient;

namespace DbScoutBaby.Services;

public class MetadataCacheService
{
    private readonly ConcurrentDictionary<string, DatabaseMetadata> _cache =
        new(StringComparer.OrdinalIgnoreCase);

    public bool HasCache(string database) =>
        _cache.ContainsKey(database);

    public async Task<DatabaseMetadata> GetMetadataAsync(
        string database,
        string connectionString,
        bool forceRefresh = false,
        CancellationToken cancellationToken = default)
    {
        if (!forceRefresh &&
            _cache.TryGetValue(
                database,
                out DatabaseMetadata? cached))
        {
            return cached;
        }

        DatabaseMetadata metadata =
            await LoadMetadataAsync(
                connectionString,
                cancellationToken);

        _cache[database] =
            metadata;

        return metadata;
    }

    private static async Task<DatabaseMetadata> LoadMetadataAsync(
        string connectionString,
        CancellationToken cancellationToken)
    {
        var metadata =
            new DatabaseMetadata();

        await using var connection =
            new SqlConnection(connectionString);

        await connection.OpenAsync(
            cancellationToken);

        await LoadObjectsAsync(
            connection,
            metadata,
            cancellationToken);

        await LoadColumnsAsync(
            connection,
            metadata,
            cancellationToken);

        await LoadIndexesAsync(
            connection,
            metadata,
            cancellationToken);

        await LoadUserTypesAsync(
            connection,
            metadata,
            cancellationToken);

        await LoadAgentJobsAsync(
            connection,
            metadata,
            cancellationToken);

        await LoadAgentAlertsAsync(
            connection,
            metadata,
            cancellationToken);

        await LoadAgentOperatorsAsync(
            connection,
            metadata,
            cancellationToken);

        metadata.LoadedAt =
            DateTime.Now;

        return metadata;
    }

    private static async Task LoadObjectsAsync(
        SqlConnection connection,
        DatabaseMetadata metadata,
        CancellationToken cancellationToken)
    {
        const string sql = """
        SELECT
            s.name AS SchemaName,
            o.name AS ObjectName,
            ISNULL(OBJECT_NAME(o.parent_object_id), '') AS ParentObject,
            o.type,
            o.type_desc,
            ISNULL(m.definition, '') AS Definition,
            CASE
                WHEN o.type = 'SN'
                    THEN ISNULL(sn.base_object_name, '')
                ELSE ''
            END AS Extra
        FROM sys.objects o
        INNER JOIN sys.schemas s
            ON s.schema_id = o.schema_id
        LEFT JOIN sys.sql_modules m
            ON m.object_id = o.object_id
        LEFT JOIN sys.synonyms sn
            ON sn.object_id = o.object_id
        WHERE
            o.is_ms_shipped = 0
            AND o.type IN
            (
                'U',
                'V',
                'P',
                'FN',
                'IF',
                'TF',
                'TR',
                'SN',
                'SO',
                'C',
                'D',
                'F',
                'PK',
                'UQ'
            )
        ORDER BY
            s.name,
            o.name;
        """;

        await using var command =
            new SqlCommand(
                sql,
                connection);

        await using var reader =
            await command.ExecuteReaderAsync(
                cancellationToken);

        while (await reader.ReadAsync(
                   cancellationToken))
        {
            string sqlType =
                reader.GetString(3);

            SearchResultType searchType =
                sqlType switch
                {
                    "U" =>
                        SearchResultType.Table,

                    "V" =>
                        SearchResultType.View,

                    "P" =>
                        SearchResultType.StoredProcedure,

                    "FN" or "IF" or "TF" =>
                        SearchResultType.Function,

                    "TR" =>
                        SearchResultType.Trigger,

                    "SN" =>
                        SearchResultType.Synonym,

                    "SO" =>
                        SearchResultType.Sequence,

                    "C" or "D" or "F" or "PK" or "UQ" =>
                        SearchResultType.Constraint,

                    _ =>
                        SearchResultType.Table
                };

            metadata.Objects.Add(
                new DbObjectMetadata
                {
                    Schema =
                        reader.GetString(0),

                    Name =
                        reader.GetString(1),

                    ParentObject =
                        reader.GetString(2),

                    TypeDescription =
                        reader.GetString(4),

                    Definition =
                        reader.GetString(5),

                    Extra =
                        reader.GetString(6),

                    SearchType =
                        searchType
                });
        }
    }

    private static async Task LoadColumnsAsync(
        SqlConnection connection,
        DatabaseMetadata metadata,
        CancellationToken cancellationToken)
    {
        const string sql = """
        SELECT
            s.name,
            o.name,
            c.name,
            ty.name
        FROM sys.columns c
        INNER JOIN sys.objects o
            ON o.object_id = c.object_id
        INNER JOIN sys.schemas s
            ON s.schema_id = o.schema_id
        INNER JOIN sys.types ty
            ON ty.user_type_id = c.user_type_id
        WHERE
            o.is_ms_shipped = 0
            AND o.type IN ('U', 'V')
        ORDER BY
            s.name,
            o.name,
            c.column_id;
        """;

        await using var command =
            new SqlCommand(
                sql,
                connection);

        await using var reader =
            await command.ExecuteReaderAsync(
                cancellationToken);

        while (await reader.ReadAsync(
                   cancellationToken))
        {
            metadata.Columns.Add(
                new ColumnMetadata
                {
                    Schema =
                        reader.GetString(0),

                    ParentObject =
                        reader.GetString(1),

                    Name =
                        reader.GetString(2),

                    DataType =
                        reader.GetString(3)
                });
        }
    }

    private static async Task LoadIndexesAsync(
        SqlConnection connection,
        DatabaseMetadata metadata,
        CancellationToken cancellationToken)
    {
        const string sql = """
        SELECT
            s.name,
            t.name,
            i.name,
            i.type_desc,
            i.is_unique
        FROM sys.indexes i
        INNER JOIN sys.tables t
            ON t.object_id = i.object_id
        INNER JOIN sys.schemas s
            ON s.schema_id = t.schema_id
        WHERE
            i.name IS NOT NULL
            AND t.is_ms_shipped = 0
        ORDER BY
            s.name,
            t.name,
            i.name;
        """;

        await using var command =
            new SqlCommand(
                sql,
                connection);

        await using var reader =
            await command.ExecuteReaderAsync(
                cancellationToken);

        while (await reader.ReadAsync(
                   cancellationToken))
        {
            metadata.Indexes.Add(
                new IndexMetadata
                {
                    Schema =
                        reader.GetString(0),

                    Table =
                        reader.GetString(1),

                    Name =
                        reader.GetString(2),

                    TypeDescription =
                        reader.GetString(3),

                    IsUnique =
                        reader.GetBoolean(4)
                });
        }
    }

    private static async Task LoadUserTypesAsync(
        SqlConnection connection,
        DatabaseMetadata metadata,
        CancellationToken cancellationToken)
    {
        const string sql = """
        SELECT
            s.name,
            t.name,
            TYPE_NAME(t.system_type_id)
        FROM sys.types t
        INNER JOIN sys.schemas s
            ON s.schema_id = t.schema_id
        WHERE
            t.is_user_defined = 1
        ORDER BY
            s.name,
            t.name;
        """;

        await using var command =
            new SqlCommand(
                sql,
                connection);

        await using var reader =
            await command.ExecuteReaderAsync(
                cancellationToken);

        while (await reader.ReadAsync(
                   cancellationToken))
        {
            metadata.UserTypes.Add(
                new UserTypeMetadata
                {
                    Schema =
                        reader.GetString(0),

                    Name =
                        reader.GetString(1),

                    BaseType =
                        reader.IsDBNull(2)
                            ? ""
                            : reader.GetString(2)
                });
        }
    }

    private static async Task LoadAgentJobsAsync(
        SqlConnection connection,
        DatabaseMetadata metadata,
        CancellationToken cancellationToken)
    {
        try
        {
            const string sql = """
            SELECT
                j.name,
                ISNULL(j.description, ''),
                ISNULL
                (
                    (
                        SELECT STRING_AGG
                        (
                            CAST
                            (
                                CONCAT
                                (
                                    'STEP ',
                                    js.step_id,
                                    ': ',
                                    js.step_name,
                                    CHAR(13),
                                    CHAR(10),
                                    js.command
                                )
                                AS nvarchar(max)
                            ),
                            CHAR(13) + CHAR(10) + CHAR(13) + CHAR(10)
                        )
                        FROM msdb.dbo.sysjobsteps js
                        WHERE js.job_id = j.job_id
                    ),
                    ''
                )
            FROM msdb.dbo.sysjobs j
            ORDER BY j.name;
            """;

            await using var command =
                new SqlCommand(
                    sql,
                    connection);

            await using var reader =
                await command.ExecuteReaderAsync(
                    cancellationToken);

            while (await reader.ReadAsync(
                       cancellationToken))
            {
                metadata.AgentObjects.Add(
                    new AgentObjectMetadata
                    {
                        Type =
                            SearchResultType.AgentJob,

                        Name =
                            reader.GetString(0),

                        Extra =
                            reader.GetString(1),

                        Definition =
                            reader.GetString(2)
                    });
            }
        }
        catch (SqlException)
        {
            // Lack of msdb/SQL Agent permissions must not
            // break normal database metadata searching.
        }
    }

    private static async Task LoadAgentAlertsAsync(
        SqlConnection connection,
        DatabaseMetadata metadata,
        CancellationToken cancellationToken)
    {
        try
        {
            const string sql = """
            SELECT
                name,
                ISNULL(message_id, 0),
                ISNULL(severity, 0)
            FROM msdb.dbo.sysalerts
            ORDER BY name;
            """;

            await using var command =
                new SqlCommand(
                    sql,
                    connection);

            await using var reader =
                await command.ExecuteReaderAsync(
                    cancellationToken);

            while (await reader.ReadAsync(
                       cancellationToken))
            {
                metadata.AgentObjects.Add(
                    new AgentObjectMetadata
                    {
                        Type =
                            SearchResultType.AgentAlert,

                        Name =
                            reader.GetString(0),

                        Extra =
                            $"MessageId={Convert.ToInt32(reader.GetValue(1))}, " +
                            $"Severity={Convert.ToInt32(reader.GetValue(2))}"
                    });
            }
        }
        catch (SqlException)
        {
        }
    }

    private static async Task LoadAgentOperatorsAsync(
        SqlConnection connection,
        DatabaseMetadata metadata,
        CancellationToken cancellationToken)
    {
        try
        {
            // sysoperators.enabled can be returned as tinyint on some
            // SQL Server versions/configurations. CAST it explicitly to bit.
            const string sql = """
            SELECT
                name,
                ISNULL(email_address, ''),
                CAST(enabled AS bit) AS enabled
            FROM msdb.dbo.sysoperators
            ORDER BY name;
            """;

            await using var command =
                new SqlCommand(
                    sql,
                    connection);

            await using var reader =
                await command.ExecuteReaderAsync(
                    cancellationToken);

            while (await reader.ReadAsync(
                       cancellationToken))
            {
                bool enabled =
                    Convert.ToBoolean(
                        reader.GetValue(2));

                metadata.AgentObjects.Add(
                    new AgentObjectMetadata
                    {
                        Type =
                            SearchResultType.AgentOperator,

                        Name =
                            reader.GetString(0),

                        Extra =
                            $"Email={reader.GetString(1)}, Enabled={enabled}"
                    });
            }
        }
        catch (SqlException)
        {
        }
    }
}
