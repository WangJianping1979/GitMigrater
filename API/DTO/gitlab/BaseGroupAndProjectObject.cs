using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GitMigrater.API.DTO.gitlab
{
    public class BaseGroupAndProjectObject
    {
      public int Id { get; set; } // 对应 "id"
      public string Name { get; set; } // 对应 "name"
      public string Path { get; set; } // 对应 "path"
      public string Description { get; set; } // 对应 "description"
      public string Visibility { get; set; }


      private List<BaseGroupAndProjectObject> subGroupAndProjectObjects;
      public List<BaseGroupAndProjectObject> SubGroupAndProjectObjects { get { if (subGroupAndProjectObjects == null) { subGroupAndProjectObjects = new List<BaseGroupAndProjectObject>(); } return subGroupAndProjectObjects; } }
      public bool HasSubGroups { get { return subGroupAndProjectObjects == null ? false : subGroupAndProjectObjects.Count > 0; } }
   }
}
