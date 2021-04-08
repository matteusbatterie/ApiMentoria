using System;
using System.Data;

namespace Repository
{
    public sealed class DbSession : IDisposable
    {
        public readonly IDbConnection Connection;
        public IDbTransaction Transaction { get; set; }

        public DbSession(IDbConnection connection)
        {
            Connection = connection;
            Connection.Open();
        }

        public void Dispose() => Connection?.Dispose();
    }
}