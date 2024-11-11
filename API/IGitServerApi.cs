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
      public void CreateProject(string project_name, string project_description, string group_id, string path);
      public void DeleteProject();

      public void DownloadProject(string project_url, string temp_folder_path);
      public void UploadProject(string project_name, string group_name, string path, string tmp_folder_path);
      public void DeleteTempfolder(string temp_folder_path);
      public List<Group> GetAllGroups();

      public void CreateGroup(string super_group_id, string group_name, string group_description, string path);
      public void DeleteGroup();
   }
}
