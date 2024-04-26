namespace DroneManagement.Health;

public class SqlServerHealthSettings
{
    public string ConnectionString { get; set; } = "Server=(localhost);Database=Devices;Trusted_Connection=True;";

    //Query database version from the 'Version' table
    public string Query { get; set; } = "SELECT dbVersion FROM Version WHERE ID =1";

    public string ExpectedVersionInSettingFile { get; set; } = "1.0.0";
}