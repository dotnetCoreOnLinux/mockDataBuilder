using Microsoft.Extensions.Configuration;

namespace MockDataBuilder
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder =  new ConfigurationBuilder().AddJsonFile("appSettings.json");
            var configuration = builder.Build();
            ConnectPGSql conObj = new ConnectPGSql(configuration["pgSQLConnection"]);
            conObj.ExecuteStatement("INSERT INTO customer (id,name, salary, nationalid) VALUES (3,'Claudine Joseph', 0, 'AXUUJH456')");
            conObj.FetchStatement("select name from customer");
        }
    }
}
