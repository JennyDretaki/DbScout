namespace DbScoutBaby;

public static class DatabaseSettings
{
    // Server supplied by the user.
    // This configuration assumes Windows Authentication.
    public const string ServerName = "Server";

    public static string DevConnectionString =
        $@"Server={ServerName};
           Database=DEV;
           Trusted_Connection=True;
           TrustServerCertificate=True;";

    public static string CtCollectConnectionString =
        $@"Server={ServerName};
           Database=CTCOLLECT;
           Trusted_Connection=True;
           TrustServerCertificate=True;";

    public static string GetConnectionString(string database) =>
        database.ToUpperInvariant() switch
        {
            "DEV" => DevConnectionString,
            "CTCOLLECT" => CtCollectConnectionString,
            _ => throw new ArgumentException($"Unknown database: {database}")
        };
}
