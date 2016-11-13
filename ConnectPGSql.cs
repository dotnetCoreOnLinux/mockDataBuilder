using System;
using Npgsql;
namespace MockDataBuilder{
    public class ConnectPGSql
    {
        #region private properties
        private string ConnectionString { get; set; }
        
        #endregion private properties

        public ConnectPGSql (string connectionString)
        {
            this.ConnectionString = connectionString;
        }

        public void ExecuteStatement(string statement)
        {
            try
            {
                using (var conn = new NpgsqlConnection(ConnectionString))
                {
                    conn.Open();
                    using (var cmd = new NpgsqlCommand())
                    {
                        cmd.Connection = conn;
                        // Insert some data
                        cmd.CommandText = statement;
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }
        }

        public void FetchStatement(string statement)
        {
            try{
                using (var conn = new NpgsqlConnection(ConnectionString))
                {
                    conn.Open();
                    using (var cmd = new NpgsqlCommand())
                    {
                        cmd.Connection = conn;
                        // Retrieve all rows
                        cmd.CommandText = statement;
                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Console.WriteLine(reader.GetString(0));
                            }
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }
        }
    }
}
