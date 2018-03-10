using Dapper;
using System;
using System.Data.SqlClient;

namespace JustOnePgn.Tests.IntegrationTests
{
    public abstract class TestBase : IDisposable
    {
        protected TestBase()
        {
            using (var db = new SqlConnection(TestFixture.ConnectionString))
            {
                db.Execute("DELETE FROM Games;");
            }
        }

        public void Dispose()
        {
            using (var db = new SqlConnection(TestFixture.ConnectionString))
            {
                db.Execute("DELETE FROM Games;");
            }
        }
    }
}
