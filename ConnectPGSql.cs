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

        //Method        :   ExecuteStatement
        //Description   :   Connects to pgsql and executes a non query statement
        //Input(s)      :   
        //  -statement  :   statment to execute
        //Output        :   int - no. of record affected 
        public int? ExecuteStatement(string statement)
        {
            try
            {
                using (var conn = new NpgsqlConnection(ConnectionString))
                {
                    conn.Open();
                    using (var cmd = new NpgsqlCommand())
                    {
                        cmd.Connection = conn;
                        cmd.CommandText = statement;
                        return cmd.ExecuteNonQuery();
                    }
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }
            return null;
        }

        //Method        :   FetchStatement
        //Description   :   Connects to pgsql and executes a select statement
        //Input(s)      :   
        //  -statement  :   select statment to execute
        //  -columnCount:   requested no. of columns
        //Output        :   int - display in console 
        public void FetchStatement(string statement,int columnCount)
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
                            int rows =0;
                            while (reader.Read())
                            {
                                Console.Write("Row "+ ++rows + ": ");
                                for(int i=0;i<columnCount;i++)
                                {                                    
                                    Console.Write(reader.GetString(i));
                                    Console.Write('|');
                                }
                                Console.WriteLine();
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
