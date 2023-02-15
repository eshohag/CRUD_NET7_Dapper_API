using System.Data;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;

namespace CRUD_NET7_Dapper_API.Helpers
{
    public class ApplicationDbContext
    {
        private DbConnectionSettings _dbSettings;

        public ApplicationDbContext(IOptions<DbConnectionSettings> dbSettings)
        {
            _dbSettings = dbSettings.Value;
        }

        public IDbConnection CreateConnection()
        {
            var connectionString = $"Server={_dbSettings.Server};Database={_dbSettings.Database};User ID={_dbSettings.UserId};Password={_dbSettings.Password};Trusted_Connection={_dbSettings.TrustedConnection};TrustServerCertificate={_dbSettings.TrustServerCertificate};MultipleActiveResultSets={_dbSettings.MultipleActiveResultSets}; Persist Security Info={_dbSettings.PersistSecurityInfo}; Connection Timeout={_dbSettings.ConnectionTimeout};";
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
            using var connection = CreateConnection();
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
