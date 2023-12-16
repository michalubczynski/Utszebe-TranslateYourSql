using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using Utszebe.Core.Entities.Database;

namespace Utszebe.Infrastracture.Data
{
    public class DatabaseRepository : IDatabaseRepository
    {
        private readonly string _connectionString = "Data Source=hackathon.db;";
        private readonly string _sqlFilePath = "..\\Utszebe.Infrastracture\\DatabaseFiles\\ddl.sql";
        private readonly string  _filesDirectory = "..\\Utszebe.Infrastracture\\DatabaseFiles\\";

        //private readonly StoreContext _context;

        public DatabaseRepository()//StoreContext context)
        {
            //_context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<bool> CreateDatabaseAsync()
        {
            await Task.Delay(0);

            // Check if SQL file exists
            if (!File.Exists(_sqlFilePath))
            {
                Console.WriteLine($"SQL file '{_sqlFilePath}' not found!");

                if (Debugger.IsAttached)
                    Debugger.Break();
            }

            //Return true if database is already created
            if (File.Exists(_connectionString))
            {
                Console.WriteLine($"Database '{_connectionString}' already created!");
                if (Debugger.IsAttached)
                    Debugger.Break();

                return false;
            }

            // Read SQL file content
            string sqlForDatabaseCreation = File.ReadAllText(_sqlFilePath);

            //// Create or open SQLite database
            //SQLiteConnection.CreateFile(_connectionStringTesting);

            // Execute SQL content on SQLite database
            using (var connection = new SQLiteConnection(_connectionString))
            {
                connection.Open();


                using (var command = new SQLiteCommand(sqlForDatabaseCreation, connection))
                {
                    command.ExecuteNonQuery();
                }

                DirectoryInfo d = new DirectoryInfo(_filesDirectory); //Assuming Test is your Folder

                List<FileInfo> files = d.GetFiles("*.sql").ToList();

                var ddl = files.Where(f => f.Name == "ddl.sql").FirstOrDefault();
                if (ddl == null)
                {
                    if (Debugger.IsAttached)
                        Debugger.Break();

                    return false;
                }

                files.Remove(ddl);
                files.OrderBy(f => d.Name);

                foreach (FileInfo file in files)
                {
                    var query = File.ReadAllText(file.FullName);
                    using (var command = new SQLiteCommand(query, connection))
                    {
                        command.ExecuteNonQuery();
                    }
                }
            }


            return true;
        }


        public async Task<IEnumerable<Column>> GetAllColumnsAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Table>> GetAllTablesAsync()
        {
            await Task.Delay(0);
            var tables = new List<Table>();

            var conn = new SqliteConnection(_connectionString);
            //var conn = _context.Database.GetDbConnection();
            try
            {
                conn.Open();
                using (var command = conn.CreateCommand())
                {
                    string query = @"SELECT name, sql FROM sqlite_master WHERE type='table';";

                    command.CommandText = query;
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var tableName = reader.GetString(0);

                            if (!tables.Any(t => t.Name == tableName))
                                tables.Add(new Table() { Name = tableName });
                        }
                    }
                }
            }
            finally
            {
                conn.Close();
            }

            return tables;
        }

    
        public async Task<IEnumerable<TableWithColumns>> GetTablesAndColumnsAsync()
        {
            var tables = await GetAllTablesAsync();
            var tablesToReturn = new List<TableWithColumns>();

            foreach (var table in tables)
            {
                tablesToReturn.Add(new TableWithColumns()
                {
                    Name = table.Name,
                    Columns = await GetColumnsInTableAsync(table)
                });
            }

            return tablesToReturn;
        }

        public async Task<IEnumerable<Column>> GetColumnsInTableAsync(Table table)
        {
            await Task.Delay(0);
            var columns = new List<Column>();

            var conn = new SqliteConnection(_connectionString);
            //var conn = _context.Database.GetDbConnection();
            try
            {
                conn.Open();
                using (var command = conn.CreateCommand())
                {
                    string query = $"PRAGMA table_info({table.Name});";

                    command.CommandText = query;
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var columnName = reader.GetString(1);


                            if (!columns.Any(t => t.Name == columnName))
                                columns.Add(new Column() { Name = columnName });
                        }
                    }
                }
            }
            finally
            {
                conn.Close();
            }

            return columns;
        }
    }
}
