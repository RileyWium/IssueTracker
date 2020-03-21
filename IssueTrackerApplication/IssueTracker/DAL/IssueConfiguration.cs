using System.Data.Entity;
using System.Data.Entity.SqlServer;

namespace IssueTracker.DAL
{
    public class IssueConfiguration: DbConfiguration
    {
        public IssueConfiguration()
        {
            SetExecutionStrategy("System.Data.SqlClient", () => new SqlAzureExecutionStrategy());
        }
    }
}