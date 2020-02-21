using DataLibrary.Models;
using DataLibrary.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLibrary.BusinessLogic
{
    public static class IssueProcessor
    {
        public static int CreateIssue(int projectID, string issueName, string issueDescription)
        {

            IssueModel data = new IssueModel
            {
                ProjectID = projectID,
                IssueName = issueName,
                IssueDescription = issueDescription
            };

            string sql = @"insert into dbo.Issue (ProjectID, IssueName, IssueDescription)
                        values (@ProjectID, @IssueName, @IssueDescription);";

            return SqlDataAccess.SaveData(sql, data);
        }
      
        public static List<IssueModel> LoadIssue()
        {
            string sql = @"select ProjectID, IssueName, IssueDescription
                           from dbo.Issue;";

            return SqlDataAccess.LoadData<IssueModel>(sql);
        }

    }
}
