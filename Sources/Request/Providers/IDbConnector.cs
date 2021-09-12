using System.Data;
using System.Threading.Tasks;

namespace Request.Providers
{
    public interface IDbConnector
    {
        IDbConnection Connect(bool removeFromPoolOnDispose = false);
        Task<IDbConnection> ConnectAsync(bool removeFromPoolDispose = false);
    }
}
