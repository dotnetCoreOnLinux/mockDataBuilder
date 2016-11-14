using System;
using Microsoft.Extensions.Configuration;

namespace MockDataBuilder
{
    public class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                var builder =  new ConfigurationBuilder().AddJsonFile("appSettings.json");
                var configuration = builder.Build();
                var tableName = ShowPGSqlSchema(configuration["pgSQLConnection"]);
                StaticDataAvailability();
                MapStaticDataToTable(tableName);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }
        }

        //Method        :   ShowPGSqlSchema
        //Description   :   Create Query to fetch table schema and execute against pgsql
        //Input(s)      :   
        //  -connectionString  :   pgsql connection string
        //Output        :   string - table name 
        private static string ShowPGSqlSchema(string connectionString)
        {
            Console.Write("Table Name:");
            var tableName = Console.ReadLine();
            var schemaQuery = "select column_name, data_type from INFORMATION_SCHEMA.COLUMNS where table_name = '"
                                + tableName + "'";
            ConnectPGSql conObj = new ConnectPGSql(connectionString);
            conObj.FetchStatement(schemaQuery,2);
            return tableName;
        }

        //Method        :   StaticDataAvailability
        //Description   :   Displays status of dummy data in the current system
        //Input(s)      :   none
        //Output        :   none 
        private static void StaticDataAvailability()
        {
            Console.WriteLine("");
            Console.WriteLine("Current system has 4k of different:");
            Console.WriteLine("1:Name");
            Console.WriteLine("2:AddressLine1");
            Console.WriteLine("3:City");
            Console.WriteLine("4:State");
            Console.WriteLine("5:Zip");
            Console.WriteLine("If you need more than 4k records 4001-8000 records will be appended with I then II,III,...");
            //todo: suggest disk space requirement
        }

        //Method        :   MapStaticDataToTable
        //Description   :   Map static data to pgsql table column
        //Input(s)      :   
        //  tableName   :   name of pgsql table
        //Output        :   none 
        private static void MapStaticDataToTable(string tableName)
        {
            Console.Write("Now, say how many dummmy records you need in " + tableName + " table:");
            var dummyRecCount = Console.ReadLine();
        }
    }
}
