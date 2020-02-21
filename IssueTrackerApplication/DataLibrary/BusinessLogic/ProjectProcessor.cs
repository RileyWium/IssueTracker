using DataLibrary.DataAccess;
using DataLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLibrary.BusinessLogic
{
    public static class ProjectProcessor
    {
        public static int CreateProject(string projectName, string projectKey)
        {
            ProjectModel data = new ProjectModel
            {
                ProjectName = projectName,
                ProjectKey = projectKey
            };

            string sql = @"insert into dbo.Project (ProjectName, ProjectKey)
                        values (@ProjectName, @ProjectKey);";

            return SqlDataAccess.SaveData(sql, data);
        }

        public static List<ProjectModel> LoadProject()
        {
            string sql = @"select ProjectName, ProjectKey
                           from dbo.Project;";

            return SqlDataAccess.LoadData<ProjectModel>(sql);
        }
    }
}
