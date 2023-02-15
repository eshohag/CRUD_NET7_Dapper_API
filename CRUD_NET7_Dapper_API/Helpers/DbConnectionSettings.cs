namespace CRUD_NET7_Dapper_API.Helpers;

public class DbConnectionSettings
{
    public string Server { get; set; }
    public string Database { get; set; }
    public string UserId { get; set; }
    public string Password { get; set; }
    public string TrustedConnection { get; set; }
    public string TrustServerCertificate { get; set; }
    public string MultipleActiveResultSets { get; set; }
    public string PersistSecurityInfo { get; set; }
    public string ConnectionTimeout { get; set; }
}