using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GitMigrater.API.DTO.gitlab
{
   public class Group : BaseGroupAndProjectObject
   {      public bool LfsEnabled { get; set; } // 对应 "lfs_enabled"
      public string AvatarUrl { get; set; } // 对应 "avatar_url"
      public string WebUrl { get; set; } // 对应 "web_url"
      public bool RequestAccessEnabled { get; set; } // 对应 "request_access_enabled"
      public string FullName { get; set; } // 对应 "full_name"
      public string FullPath { get; set; } // 对应 "full_path"
      public int FileTemplateProjectId { get; set; } // 对应 "file_template_project_id"
      public int? ParentId { get; set; } // 对应 "parent_id"，nullable 类型，因为可能为 null
   }
}
