using GitMigrater.API;
using GitMigrater.API.DTO.gitlab;
using Newtonsoft.Json;
using System.Linq.Expressions;

namespace GitMigrater
{
   public partial class Form1 : Form
   {
      string src_url = "";
      string src_at = "";

      string dest_url = "";
      string dest_at = "";

      string src_path = "";
      string dest_path = "";
      Project src_project;
      Group src_group;
      Group dest_group;


      public Form1()
      {
         InitializeComponent();
      }
      private void saveServers()
      {
         using (StreamWriter sw = new StreamWriter("./config.ini"))
         {
            sw.WriteLine(tb_src_url.Text);
            sw.WriteLine(tb_src_access_token.Text);
            sw.WriteLine(tb_dest_url.Text);
            sw.WriteLine(tb_dest_access_token.Text);
         }


      }
      private void loadServers()
      {
         if (File.Exists("./config.ini"))
         {
            using (StreamReader sr = new StreamReader("./config.ini"))
            {
               src_url = sr.ReadLine();
               src_at = sr.ReadLine();
               dest_url = sr.ReadLine();
               dest_at = sr.ReadLine();
            }
         }
         tb_src_url.Text = src_url;
         tb_src_access_token.Text = src_at;
         tb_dest_url.Text = dest_url;
         tb_dest_access_token.Text = dest_at;
      }
      private void checkSrcServer(object sender, EventArgs e)
      {
         saveServers();
         src_url = tb_src_url.Text;
         src_at = tb_src_access_token.Text;
         if (src_url.Trim().Equals("") || src_at.Trim().Equals(""))
         {
            btn_ck_src.Text = "Please input";
         }
         else
         {
            btn_ck_src.Text = "checking";
            this.tb_src_url.Enabled = false;
            this.tb_src_access_token.Enabled = false;
            if (!bgw_src_checker.IsBusy)
               bgw_src_checker.RunWorkerAsync();
         }
      }
      private void checkDestServer(object sender, EventArgs e)
      {
         saveServers();
         dest_url = tb_dest_url.Text;
         dest_at = tb_dest_access_token.Text;
         if (dest_url.Trim().Equals("") || dest_at.Trim().Equals(""))
         {
            btn_ck_dest.Text = "Please input";
         }
         else
         {
            btn_ck_dest.Text = "checking";
            tb_dest_url.Enabled = false;
            tb_dest_access_token.Enabled = false;
            if (!bgw_dest_checker.IsBusy)
               bgw_dest_checker.RunWorkerAsync();
         }
      }
      private List<BaseGroupAndProjectObject> BuildBaseGroupAndProjectObjectsTree(GitLabServerApi api, bool withProjects = true)
      {
         List<Group> groups = api.GetAllGroups();
         List<BaseGroupAndProjectObject> groupsAndProjects = new List<BaseGroupAndProjectObject>();
         int count = groups.Count;
         int current = 0;
         foreach (Group group in groups)
         {
            current++;
            if (group.ParentId == null)
            {
               groupsAndProjects.Add(group);//the first level group is the root group.
            }
            else
            {
               foreach (Group parentGroup in groups)
               {
                  if (parentGroup.Id == group.ParentId)//find the parent group of the current group.
                  {
                     parentGroup.SubGroupAndProjectObjects.Add(group);
                  }
               }

            }
            if (withProjects)
            {
               showLoadMsg("(" + current + "/" + count + ")Loading projects of group " + group.Name);
               List<Project> projects = api.GetAllGroupProjects(group.Id.ToString());
               foreach (Project project in projects)
               {
                  group.SubGroupAndProjectObjects.Add(project);
               }
            }
         }
         return groupsAndProjects;
      }
      private void bgw_src_checker_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
      {
         if (src_url.Trim().Equals("") || src_at.Trim().Equals(""))
         {
            bgw_src_checker.ReportProgress(0, "Please input");
         }
         else
         {
            bgw_src_checker.ReportProgress(0, "Checking...");
            try
            {
               GitLabServerApi api = new GitLabServerApi(src_url, src_at);
               List<BaseGroupAndProjectObject> groupsAndProjects = BuildBaseGroupAndProjectObjectsTree(api);
               bgw_src_checker.ReportProgress(1, groupsAndProjects);
               bgw_src_checker.ReportProgress(2, "Src Server OK");
            }
            catch (Exception ex)
            {
               bgw_src_checker.ReportProgress(0, "ERROR:" + ex.Message);
            }
         }
      }

      private void bgw_dest_checker_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
      {
         if (dest_url.Equals("") || dest_at.Equals(""))
         {
            bgw_dest_checker.ReportProgress(10, "Please input");
         }
         else
         {
            bgw_dest_checker.ReportProgress(10, "Checking...");
            try
            {
               GitLabServerApi api = new GitLabServerApi(dest_url, dest_at);
               List<BaseGroupAndProjectObject> groups = BuildBaseGroupAndProjectObjectsTree(api, false);
               bgw_dest_checker.ReportProgress(11, groups);
               bgw_dest_checker.ReportProgress(12, "Dest Server OK");
            }
            catch (Exception ex)
            {
               bgw_dest_checker.ReportProgress(10, "Error:" + ex.Message);
            }
         }
      }
      private void buildNods(TreeNode node, BaseGroupAndProjectObject baseObject)
      {
         if (baseObject.HasSubGroups)
         {
            foreach (BaseGroupAndProjectObject subBaseObject in baseObject.SubGroupAndProjectObjects)
            {
               TreeNode subTreeNode = new TreeNode((subBaseObject is Group ? "G:" : "P:") + subBaseObject.Name);
               subTreeNode.Tag = subBaseObject;
               node.Nodes.Add(subTreeNode);
               buildNods(subTreeNode, subBaseObject);
            }
         }
      }
      private void showLoadMsg(string msg)
      {
         if (this.InvokeRequired)
         {
            this.Invoke(new Action<string>(showLoadMsg), msg);
         }
         else
         {
            lb_loadmsg.Text = msg;
         }
      }
      private void bgw_ProgressChanged(object sender, System.ComponentModel.ProgressChangedEventArgs e)
      {
         switch (e.ProgressPercentage)
         {
            case 0:
               this.btn_ck_src.Text = e.UserState.ToString();
               lb_loadmsg.Text = e.UserState.ToString();
               break;
            case 1:
               List<BaseGroupAndProjectObject> groups = e.UserState as List<BaseGroupAndProjectObject>;
               tv_tree_src.Nodes.Clear();
               foreach (BaseGroupAndProjectObject baseObject in groups)
               {
                  TreeNode node = new TreeNode((baseObject is Group ? "G:" : "P:") + baseObject.Name);
                  node.Tag = baseObject;
                  buildNods(node, baseObject);
                  tv_tree_src.Nodes.Add(node);
               }
               break;
            case 2:
               this.btn_ck_src.Text = e.UserState.ToString();
               this.tb_src_url.Enabled = true;
               this.tb_src_access_token.Enabled = true;
               MessageBox.Show("CheckOver");
               break;
            case 10:
               this.btn_ck_dest.Text = e.UserState.ToString();
               lb_loadmsg.Text = e.UserState.ToString();
               break;
            case 11:
               List<BaseGroupAndProjectObject> groups2 = e.UserState as List<BaseGroupAndProjectObject>;
               tv_tree_dest.Nodes.Clear();
               foreach (BaseGroupAndProjectObject baseObject in groups2)
               {
                  TreeNode node = new TreeNode((baseObject is Group ? "G:" : "P:") + baseObject.Name);
                  node.Tag = baseObject;
                  buildNods(node, baseObject);
                  tv_tree_dest.Nodes.Add(node);
               }
               break;
            case 12:
               this.btn_ck_dest.Text = e.UserState.ToString();
               this.tb_dest_url.Enabled = true;
               this.tb_dest_access_token.Enabled = true;
               MessageBox.Show("CheckOver");
               break;
            default:
               break;

         }
      }
      private void migrateProject(Project project, Group group)
      {
         GitLabServerApi srcApi = new GitLabServerApi(src_url, src_at);
         GitLabServerApi destApi = new GitLabServerApi(dest_url, dest_at);
         //   //1¡¢ git clone --bare oldurl
         showLoadMsg("Downloading project " + project.Name);
         srcApi.DownloadProject(project.PathWithNamespace);
         //   //2¡¢ create project on new server
         destApi.CreateProject(project.Name, project.Description == null ? "" : project.Description, group.Id.ToString());
         //   //3¡¢ git push --bare new url
         showLoadMsg("Uploading project " + project.Name);
         destApi.UploadProject(project.Name, group.FullPath);
      }
      private void bgw_migrater_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
      {
         if (src_group != null)
         {
            foreach (BaseGroupAndProjectObject baseObject in src_group.SubGroupAndProjectObjects)
            {
               if (baseObject is Project)
               {
                  migrateProject(baseObject as Project, dest_group);
               }
            }
         }
         if (src_project != null)
         {
            migrateProject(src_project, dest_group);
         }



      }

      private void bgw_monitor_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
      {
      }

      private void bgw_monitor_ProgressChanged(object sender, System.ComponentModel.ProgressChangedEventArgs e)
      {

      }

      private void Form1_FormClosing(object sender, FormClosingEventArgs e)
      {
         bgw_src_checker.CancelAsync();
         bgw_dest_checker.CancelAsync();
         bgw_monitor.CancelAsync();
      }

      private void btn_migrate_Click(object sender, EventArgs e)
      {
         bgw_migrater.RunWorkerAsync();
      }

      private void Form1_Load(object sender, EventArgs e)
      {
         loadServers();
      }
      private void checkCanMigrate()
      {
         if (src_path != "" && dest_path != "")
         {
            lb_msg.Text = src_path + "     ==>>>     " + dest_path;
            btn_migrate.Enabled = true;
         }
         else
         {
            if (src_path != "") lb_msg.Text = src_path;
            if (dest_path != "") lb_msg.Text = dest_path;
            btn_migrate.Enabled = false;

         }
      }
      private void tv_tree_src_AfterSelect(object sender, TreeViewEventArgs e)
      {
         if (e.Node.Tag is Group)
         {
            src_group = e.Node.Tag as Group;
            src_project = null;
            src_path = src_group.WebUrl;
         }
         else
         {
            Project p = e.Node.Tag as Project;
            src_project = p;
            src_group = null;
            src_path = p.HttpUrlToRepo;

         }
         checkCanMigrate();
      }

      private void tv_tree_dest_AfterSelect(object sender, TreeViewEventArgs e)
      {
         if (e.Node.Tag is Group)
         {
            Group group = e.Node.Tag as Group;
            dest_group = group;
            dest_path = group.WebUrl;
         }
         else
         {
         }
         checkCanMigrate();
      }

      private void bgw_migrater_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
      {
         if(bgw_migrater.IsBusy == true)
         {
            bgw_migrater.CancelAsync();
         }
         MessageBox.Show("Migrate Over");
      }
   }
}
