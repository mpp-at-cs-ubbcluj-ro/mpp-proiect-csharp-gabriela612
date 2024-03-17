using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Data.SQLite;

namespace Utils
{
    public class DBUtils
    {
        private Dictionary<string, string> props;

        public DBUtils(Dictionary<string, string> props)
        {
            this.props = props;
        }

        private SQLiteConnection instance = null;

        private SQLiteConnection GetNewConnection()
        {
            String connectionString = props["ConnectionString"];
            SQLiteConnection sqLiteConnection = new SQLiteConnection(connectionString);
            sqLiteConnection.Open();
            return sqLiteConnection;
        }

        public SQLiteConnection GetConnection()
        {
            try
            {
                if (instance == null || instance.State == ConnectionState.Closed)
                {
                    instance = GetNewConnection();
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine($"Error DB: {e}");
            }
            return instance;
        }
    }
}