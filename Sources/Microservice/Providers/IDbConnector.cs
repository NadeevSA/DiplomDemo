using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Microservice.Providers
{
    public interface IDbConnector
    {
        IDbConnection Connect(bool removeFromPoolOnDispose = false);
        Task<IDbConnection> ConnectAsync(bool removeFromPoolDispose = false);
    }
}
