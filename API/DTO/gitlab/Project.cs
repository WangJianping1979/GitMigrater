using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GitMigrater.API.DTO.gitlab
{
   public class Project :  BaseGroupAndProjectObject
   {
      public string DefaultBranch { get; set; }
      public string SshUrlToRepo { get; set; }
      public string HttpUrlToRepo { get; set; }
      public string WebUrl { get; set; }
      public string ReadmeUrl { get; set; }
      public List<string> TagList { get; set; }
      public Owner Owner { get; set; }
      public string NameWithNamespace { get; set; }
      public string PathWithNamespace { get; set; }
      public bool IssuesEnabled { get; set; }
      public int OpenIssuesCount { get; set; }
      public bool MergeRequestsEnabled { get; set; }
      public bool JobsEnabled { get; set; }
      public bool WikiEnabled { get; set; }
      public bool SnippetsEnabled { get; set; }
      public bool ResolveOutdatedDiffDiscussions { get; set; }
      public bool? ContainerRegistryEnabled { get; set; }
      public DateTime CreatedAt { get; set; }
      public DateTime LastActivityAt { get; set; }
      public int CreatorId { get; set; }
      public Namespace Namespace { get; set; }
      public string ImportStatus { get; set; }
      public bool Archived { get; set; }
      public string AvatarUrl { get; set; }
      public bool SharedRunnersEnabled { get; set; }
      public int ForksCount { get; set; }
      public int StarCount { get; set; }
      public string RunnersToken { get; set; }
      public bool PublicJobs { get; set; }
      public List<object> SharedWithGroups { get; set; } // 如果有明确结构，请更改为合适的类型
      public bool OnlyAllowMergeIfPipelineSucceeds { get; set; }
      public bool OnlyAllowMergeIfAllDiscussionsAreResolved { get; set; }
      public bool RequestAccessEnabled { get; set; }
      public string MergeMethod { get; set; }
      public Statistics Statistics { get; set; }
      public Links Links { get; set; }
      public Permissions Permissions { get; set; } // 如果没有对应的权限结构，可以删除
   }
}
