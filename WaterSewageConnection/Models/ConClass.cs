using Microsoft.Data.SqlClient;

namespace WaterSewageConnection.Models
{
    public class ConClass
    {
        SqlConnection _connection = null;
        SqlCommand _command = null;

        public static IConfiguration Configuration { get; set; }

        public static string ConnectionString
        {
            get
            {
                var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json");

                Configuration = builder.Build();

                return Configuration.GetConnectionString("conn_string");
            }

        }
    }
}
