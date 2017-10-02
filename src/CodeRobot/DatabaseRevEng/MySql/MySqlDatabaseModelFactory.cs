// Copyright (c) rigofunc (xuyingting). All rights reserved

using System.Collections.Generic;
using System.Data;
using System.Data.Common;

namespace CodeRobot.Database.RevEng
{
    /// <summary>
    /// Represents the MySQL implementation of the <see cref="IDatabaseModelFactory"/> interface.
    /// </summary>
    public class MySqlDatabaseModelFactory : IDatabaseModelFactory
    {
        private TableSelectionSet _tableSelectionSet;
        private DbConnection _connection;
        private DatabaseModel _databaseModel;
        private Dictionary<string, TableModel> _tables;

        private static string TableKey(TableModel table) => TableKey(table.Name, table.Schema);
        private static string TableKey(string name, string schema) => $"`{name}`";

        private void ResetState()
        {
            _connection = null;
            _databaseModel = new DatabaseModel();
            _tables = new Dictionary<string, TableModel>();
        }

        /// <summary>
        /// Creates a new instance of <see cref="DatabaseModel"/> by the specified <paramref name="connection"/> and <paramref name="tableSelectionSet"/>.
        /// </summary>
        /// <param name="connection">The db connection.</param>
        /// <param name="tableSelectionSet">The table selection set.</param>
        /// <returns>The instance of <see cref="DatabaseModel"/></returns>
        public DatabaseModel Create(DbConnection connection, TableSelectionSet tableSelectionSet)
        {
            ResetState();

            _tableSelectionSet = tableSelectionSet;
            _connection = connection;

            var connectionStartedOpen = _connection.State == ConnectionState.Open;
            if (!connectionStartedOpen)
            {
                _connection.Open();
            }

            try
            {
                _databaseModel.Name = _connection.Database;
                _databaseModel.Schema = null;

                GetTables();
                GetColumns();

                return _databaseModel;
            }
            finally
            {
                if (!connectionStartedOpen)
                {
                    _connection.Close();
                }
            }
        }

        const string GetTablesQuery = @"SHOW FULL TABLES WHERE TABLE_TYPE = 'BASE TABLE'";

        void GetTables()
        {
            using (var command = _connection.CreateCommand())
            {
                command.CommandText = GetTablesQuery;
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var table = new TableModel
                        {
                            Database = _databaseModel,
                            Schema = null,
                            Name = reader.GetString(0)
                        };

                        if (_tableSelectionSet.Allows(table.Schema, table.Name))
                        {
                            _databaseModel.Tables.Add(table);
                            _tables[TableKey(table)] = table;
                        }
                    }
                }
            }
        }

        const string GetColumnsQuery = @"SELECT COLUMN_NAME, DATA_TYPE, IS_NULLABLE, COLUMN_KEY, COLUMN_DEFAULT, COLUMN_COMMENT FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_SCHEMA='{0}' AND TABLE_NAME='{1}'";

        void GetColumns()
        {
            foreach (var x in _tables)
            {
                var table = x.Value;
                using (var command = _connection.CreateCommand())
                {
                    command.CommandText = string.Format(GetColumnsQuery, table.Database.Name, table.Name);

                    using (var reader = command.ExecuteReader())
                        while (reader.Read())
                        {
                            var column = new ColumnModel
                            {
                                Table = x.Value,
                                PrimaryKeyOrdinal = reader[3].ToString() == "PRI" ? (int?)1 : null,
                                Name = reader.GetString(0),
                                DataType = reader.GetString(1),
                                IsNullable = reader.GetString(2) == "YES",
                                DefaultValue = reader[4].ToString() == "" ? null : reader[4].ToString(),
                                Comment = reader.GetString(5),
                            };
                            x.Value.Columns.Add(column);
                        }
                }
            }
        }
    }
}
