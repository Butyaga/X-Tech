using Abstract;
using Microsoft.Data.Sqlite;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SQLite
{
    public class SQLiteDB : IDataBase
    {
        private readonly string _dbName;
        private bool _isTableCreated = false;

        public SQLiteDB(string dbName)
        {
            _dbName = dbName;
        }

        public async Task SaveAsync(IEnumerable<INetData> data)
        {
            using SqliteConnection connection = new SqliteConnection($"Data Source={_dbName}.db");
            connection.Open();
            CreateTable(connection);

            SqliteCommand command = connection.CreateCommand();
            command.CommandText = GetCommandText(data);
            try
            {
                await command.ExecuteNonQueryAsync();
            }
            catch { }
        }

        private void CreateTable(SqliteConnection connection)
        {
            if (_isTableCreated) { return; }

            SqliteCommand command = connection.CreateCommand();
            command.CommandText = $"CREATE TABLE {_dbName}( id INTEGER PRIMARY KEY AUTOINCREMENT, name TEXT NOT NULL, strength INTEGER NOT NULL)";
            try
            {
                command.ExecuteNonQuery();
            }
            catch { }
            _isTableCreated = true;
        }

        private string GetCommandText(IEnumerable<INetData> data)
        {
            StringBuilder stringBuilder = new StringBuilder($"INSERT INTO {_dbName} (name, strength) VALUES ");
            foreach (INetData netData in data)
            {
                stringBuilder.Append($"('{netData.Name}', {netData.Strength}), ");
            }
            stringBuilder.Remove(stringBuilder.Length - 2, 2);
            return stringBuilder.ToString();
        }
    }
}
