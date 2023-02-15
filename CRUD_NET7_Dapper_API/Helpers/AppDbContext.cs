using System.Data;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;

namespace CRUD_NET7_Dapper_API.Helpers
{
    public class AppDbContext
    {
        private DbConnectionSettings _dbSettings;

        public AppDbContext(IOptions<DbConnectionSettings> dbSettings)
        {
            _dbSettings = dbSettings.Value;
        }

        public IDbConnection CreateConnection()
        {
            var connectionString = $"Server={_dbSettings.Server}; Database={_dbSettings.Database}; User Id={_dbSettings.UserId}; Password={_dbSettings.Password};";
            //var connectionString = $"Server={_dbSettings.Server};Database={_dbSettings.Database};User ID={_dbSettings.UserId};Password={_dbSettings.Password};Trusted_Connection={_dbSettings.TrustedConnection};TrustServerCertificate={_dbSettings.TrustServerCertificate};Encrypt={_dbSettings.Encrypt};MultipleActiveResultSets={_dbSettings.MultipleActiveResultSets}; Persist Security Info={_dbSettings.PersistSecurityInfo}; Connection Timeout={_dbSettings.ConnectionTimeout};";
            //var connectionString = $"Server=SHOHAG-PC;Database=DapperNet7DB;User ID=sa;Password=SqlDev2019;MultipleActiveResultSets=true;";
            //var connectionString = $"Data Source=SHOHAG-PC;Initial Catalog=DapperNet7DB;Integrated Security=False;Persist Security Info=False;User ID=sa;Password=SqlDev2019";
            //var connectionString = $"Server=SHOHAG-PC;Database=DapperNet7DB;Trusted_Connection=True;encrypt=false";
            return new SqlConnection(connectionString);
        }

        public async Task Init()
        {
            await _initDatabase();
            await _initTables();
        }

        private async Task _initDatabase()
        {
            // create database if it doesn't exist
            var connectionString = $"Server={_dbSettings.Server}; Database=master; User Id={_dbSettings.UserId}; Password={_dbSettings.Password};";
            using var connection = new SqlConnection(connectionString);
            var sql = $"IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = '{_dbSettings.Database}') CREATE DATABASE [{_dbSettings.Database}];";
            await connection.ExecuteAsync(sql);
        }

        private async Task _initTables()
        {
            // create tables if they don't exist
            using var connection = CreateConnection();
            await _initUsers();

            async Task _initUsers()
            {
                var sql = """
                    IF OBJECT_ID('Users', 'U') IS NULL
                    CREATE TABLE Users (
                        Id INT NOT NULL PRIMARY KEY IDENTITY,
                        Title NVARCHAR(MAX),
                        FirstName NVARCHAR(MAX),
                        LastName NVARCHAR(MAX),
                        Email NVARCHAR(MAX),
                        Role INT,
                        PasswordHash NVARCHAR(MAX)
                    );
                 """;
                await connection.ExecuteAsync(sql);
            }
        }
    }
}
