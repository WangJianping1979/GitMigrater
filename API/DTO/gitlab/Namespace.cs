namespace GitMigrater.API.DTO.gitlab
{
   public class Namespace
   {
      public int Id { get; set; }
      public string Name { get; set; }
      public string Path { get; set; }
      public string Kind { get; set; }
      public string FullPath { get; set; }
   }
}
