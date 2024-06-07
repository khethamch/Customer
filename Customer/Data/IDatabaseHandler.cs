using System.Data;

namespace Customer.Data
{
    public interface IDatabaseHandler
    {
        IDbConnection GetDbConnection();
    }
}
