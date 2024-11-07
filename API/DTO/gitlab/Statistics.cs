namespace GitMigrater.API.DTO.gitlab
{
   public class Statistics
   {
      public int CommitCount { get; set; }
      public long StorageSize { get; set; }
      public long RepositorySize { get; set; }
      public long LfsObjectsSize { get; set; }
      public long JobArtifactsSize { get; set; }
   }
}
