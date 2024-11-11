using GitMigrater.API.DTO.gitlab;
using GitMigrater.utils;
using Microsoft.VisualBasic.FileIO;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using static GitMigrater.utils.WebAccessor;

namespace GitMigrater.API
{
   public class GitLabServerApi : IGitServerApi
   {
      JsonSerializerSettings settings = new JsonSerializerSettings
      {
         ContractResolver = new DefaultContractResolver
         {
            NamingStrategy = new SnakeCaseNamingStrategy() // use Snake_Case naming strategy for JSON keys
         }
      };
      private String apiVersion = null;
      private bool checkApiVersionOK(String targetVersion)
      {
         Version t_v = new Version(targetVersion);
         if (apiVersion == null)
         {
            String response = WebAccessor.Instance.Request(ServerUrl + "/api/v4/version", "private_token=" + AccessToken, RequestMethod.GetMethod);
            ApiVersion version = JsonConvert.DeserializeObject<ApiVersion>(response, settings);
            apiVersion = version.Version;
         }
         Version v = new Version(apiVersion);

         if (v.Major < t_v.Major)
         {
            return false;
         }
         if (v.Minor < t_v.Minor)
         {
            return false;
         }

         return true;
      }

      public GitLabServerApi(string serverUrl, string accessToken)
      {
         AccessToken = accessToken;
         ServerUrl = serverUrl;
      }
      public string ServerUrl { get; set; }

      public String AccessToken { get; set; }
      public List<Project> GetAllGroupProjects(String group_id)
      {
         try
         {
            String response = WebAccessor.Instance.Request(ServerUrl + "/api/v4/groups/" + group_id + "/projects", "per_page=1000&page=1&private_token=" + AccessToken, RequestMethod.GetMethod);
            List<Project> projects = JsonConvert.DeserializeObject<List<Project>>(response, settings);

            return projects;
         }
         catch (Exception e)
         {
            Console.WriteLine(e.Message);
            return null;
         }
      }
      public void CreateGroup(string super_group_id, string group_name, string group_description, string path)
      {
         String response = WebAccessor.Instance.Request(ServerUrl + "/api/v4/groups", (super_group_id != "" ? "parent_id=" + super_group_id + "&" : "&") + "name=" + group_name + "&description=" + group_description + "&path=" + path + "&private_token=" + AccessToken, RequestMethod.PostMethod);
      }
      public void CreateProject(string project_name, string project_description, string group_id, string path)
      {
         String response = WebAccessor.Instance.Request(ServerUrl + "/api/v4/projects", "namespace_id=" + group_id + "&description=" + project_description + "&path=" + (path == null ? project_name : path) + "&private_token=" + AccessToken, RequestMethod.PostMethod);

         Project project = JsonConvert.DeserializeObject<Project>(response, settings);

         Console.WriteLine(project.Name);
      }

      public void DeleteGroup()
      {
         throw new NotImplementedException();
      }

      public void DeleteProject()
      {
         String response = WebAccessor.Instance.Request(ServerUrl + "/api/v4/projects/5605", "private_token=" + AccessToken, RequestMethod.DeleteMethod);
      }
      public void DownloadProject(string project_url,string tmp_folder)
      {
         int ret_code = process("git", "clone --bare " + ServerUrl + "/" + project_url + ".git "+ tmp_folder);
         if (ret_code != 0)
         {
            throw new Exception("Failed to download project from " + project_url);
         }

      }
      public List<Group> GetAllGroups()
      {
         String response = WebAccessor.Instance.Request(ServerUrl + "/api/v4/groups", "per_page=1000&page=1&private_token=" + AccessToken, RequestMethod.GetMethod);
         List<Group> groups = JsonConvert.DeserializeObject<List<Group>>(response, settings);
         return groups;
      }

      public void UploadProject(string project_name, string group_name, string path ,string tmp_folder)
      {

         int ret_code = process("git", "push --mirror " + ServerUrl + "/" + group_name + "/" + (path == null ? project_name : path) + ".git", tmp_folder);
         if (ret_code != 0)
         {
            throw new Exception("Failed to upload project from " + group_name);
         }
      }
      public void DeleteTempfolder(string temp_folder)
      {
         //Directory.Delete(project_name + ".git", true);
         Microsoft.VisualBasic.FileIO.FileSystem.DeleteDirectory(temp_folder , UIOption.OnlyErrorDialogs, RecycleOption.DeletePermanently);
      }
      public int process(string cmd, string args, string dir = null)
      {
         Process process = new Process();
         process.StartInfo.FileName = cmd;
         process.StartInfo.Arguments = args;
         if (dir != null)
         {
            process.StartInfo.WorkingDirectory = dir;
         }
         process.StartInfo.UseShellExecute = false;
         process.Start();
         process.WaitForExit();
         return process.ExitCode;
      }
   }
}
