using System.Data;
using System.Data.SqlClient;

namespace Customer.Data
{
    public class DatabaseHandler : IDatabaseHandler
    {
        private readonly IConfiguration _configuration;

        public DatabaseHandler(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IDbConnection GetDbConnection()
        {
            return new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
        }
    }
}
