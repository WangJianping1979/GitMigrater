using GitMigrater.API.DTO.gitlab;
using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GitMigrater.API
{
    interface IGitServerApi
    {
      public List<Project> GetAllGroupProjects(String group_id);
      public void CreateProject(string project_name, string project_description, string group_id);
      public void DeleteProject();

      public void DownloadProject(string project_url);
      public void UploadProject(string project_name, string group_name);
      public List<Group> GetAllGroups();

      public void CreateGroup();
      public void DeleteGroup();
    }
}
