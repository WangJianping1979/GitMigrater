namespace GitMigrater
{
   partial class Form1
   {
      /// <summary>
      ///  Required designer variable.
      /// </summary>
      private System.ComponentModel.IContainer components = null;

      /// <summary>
      ///  Clean up any resources being used.
      /// </summary>
      /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
      protected override void Dispose(bool disposing)
      {
         if (disposing && (components != null))
         {
            components.Dispose();
         }
         base.Dispose(disposing);
      }

      #region Windows Form Designer generated code

      /// <summary>
      ///  Required method for Designer support - do not modify
      ///  the contents of this method with the code editor.
      /// </summary>
      private void InitializeComponent()
      {
         label1 = new Label();
         groupBox1 = new GroupBox();
         btn_ck_dest = new Button();
         btn_ck_src = new Button();
         tb_dest_access_token = new TextBox();
         tb_src_access_token = new TextBox();
         label4 = new Label();
         label3 = new Label();
         tb_dest_url = new TextBox();
         tb_src_url = new TextBox();
         label2 = new Label();
         label5 = new Label();
         btn_migrate = new Button();
         bgw_src_checker = new System.ComponentModel.BackgroundWorker();
         bgw_dest_checker = new System.ComponentModel.BackgroundWorker();
         bgw_migrater = new System.ComponentModel.BackgroundWorker();
         bgw_monitor = new System.ComponentModel.BackgroundWorker();
         tv_tree_src = new TreeView();
         tv_tree_dest = new TreeView();
         splitContainer1 = new SplitContainer();
         label6 = new Label();
         lb_loadmsg = new Label();
         lb_msg = new Label();
         groupBox1.SuspendLayout();
         ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
         splitContainer1.Panel1.SuspendLayout();
         splitContainer1.Panel2.SuspendLayout();
         splitContainer1.SuspendLayout();
         SuspendLayout();
         // 
         // label1
         // 
         label1.AutoSize = true;
         label1.Location = new Point(21, 30);
         label1.Name = "label1";
         label1.Size = new Size(159, 28);
         label1.TabIndex = 0;
         label1.Text = "Src Server URL";
         // 
         // groupBox1
         // 
         groupBox1.Controls.Add(btn_ck_dest);
         groupBox1.Controls.Add(btn_ck_src);
         groupBox1.Controls.Add(tb_dest_access_token);
         groupBox1.Controls.Add(tb_src_access_token);
         groupBox1.Controls.Add(label4);
         groupBox1.Controls.Add(label3);
         groupBox1.Controls.Add(tb_dest_url);
         groupBox1.Controls.Add(tb_src_url);
         groupBox1.Controls.Add(label2);
         groupBox1.Controls.Add(label1);
         groupBox1.Location = new Point(12, 12);
         groupBox1.Name = "groupBox1";
         groupBox1.Size = new Size(1257, 127);
         groupBox1.TabIndex = 1;
         groupBox1.TabStop = false;
         groupBox1.Text = "Servers information";
         // 
         // btn_ck_dest
         // 
         btn_ck_dest.Location = new Point(962, 64);
         btn_ck_dest.Name = "btn_ck_dest";
         btn_ck_dest.Size = new Size(216, 40);
         btn_ck_dest.TabIndex = 9;
         btn_ck_dest.Text = "checkDestServer";
         btn_ck_dest.UseVisualStyleBackColor = true;
         btn_ck_dest.Click += checkDestServer;
         // 
         // btn_ck_src
         // 
         btn_ck_src.Location = new Point(962, 24);
         btn_ck_src.Name = "btn_ck_src";
         btn_ck_src.Size = new Size(216, 40);
         btn_ck_src.TabIndex = 8;
         btn_ck_src.Text = "checkSrcServer";
         btn_ck_src.UseVisualStyleBackColor = true;
         btn_ck_src.Click += checkSrcServer;
         // 
         // tb_dest_access_token
         // 
         tb_dest_access_token.Location = new Point(655, 70);
         tb_dest_access_token.Name = "tb_dest_access_token";
         tb_dest_access_token.Size = new Size(301, 34);
         tb_dest_access_token.TabIndex = 7;
         // 
         // tb_src_access_token
         // 
         tb_src_access_token.Location = new Point(655, 27);
         tb_src_access_token.Name = "tb_src_access_token";
         tb_src_access_token.Size = new Size(301, 34);
         tb_src_access_token.TabIndex = 6;
         // 
         // label4
         // 
         label4.AutoSize = true;
         label4.Location = new Point(507, 70);
         label4.Name = "label4";
         label4.Size = new Size(142, 28);
         label4.TabIndex = 5;
         label4.Text = "AccessToken";
         // 
         // label3
         // 
         label3.AutoSize = true;
         label3.Location = new Point(507, 30);
         label3.Name = "label3";
         label3.Size = new Size(142, 28);
         label3.TabIndex = 4;
         label3.Text = "AccessToken";
         // 
         // tb_dest_url
         // 
         tb_dest_url.Location = new Point(186, 67);
         tb_dest_url.Name = "tb_dest_url";
         tb_dest_url.Size = new Size(301, 34);
         tb_dest_url.TabIndex = 3;
         // 
         // tb_src_url
         // 
         tb_src_url.Location = new Point(186, 27);
         tb_src_url.Name = "tb_src_url";
         tb_src_url.Size = new Size(301, 34);
         tb_src_url.TabIndex = 2;
         // 
         // label2
         // 
         label2.AutoSize = true;
         label2.Location = new Point(14, 70);
         label2.Name = "label2";
         label2.Size = new Size(166, 28);
         label2.TabIndex = 1;
         label2.Text = "Des Server URL";
         // 
         // label5
         // 
         label5.AutoSize = true;
         label5.Location = new Point(12, 142);
         label5.Name = "label5";
         label5.Size = new Size(911, 28);
         label5.TabIndex = 4;
         label5.Text = "The left sid is project list from src serrver And the right side is  group list from dest Server";
         // 
         // btn_migrate
         // 
         btn_migrate.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
         btn_migrate.Location = new Point(1132, 607);
         btn_migrate.Name = "btn_migrate";
         btn_migrate.Size = new Size(131, 40);
         btn_migrate.TabIndex = 8;
         btn_migrate.Text = "Migrate!!";
         btn_migrate.UseVisualStyleBackColor = true;
         btn_migrate.Click += btn_migrate_Click;
         // 
         // bgw_src_checker
         // 
         bgw_src_checker.WorkerReportsProgress = true;
         bgw_src_checker.WorkerSupportsCancellation = true;
         bgw_src_checker.DoWork += bgw_src_checker_DoWork;
         bgw_src_checker.ProgressChanged += bgw_ProgressChanged;
         // 
         // bgw_dest_checker
         // 
         bgw_dest_checker.WorkerReportsProgress = true;
         bgw_dest_checker.WorkerSupportsCancellation = true;
         bgw_dest_checker.DoWork += bgw_dest_checker_DoWork;
         bgw_dest_checker.ProgressChanged += bgw_ProgressChanged;
         // 
         // bgw_migrater
         // 
         bgw_migrater.WorkerReportsProgress = true;
         bgw_migrater.WorkerSupportsCancellation = true;
         bgw_migrater.DoWork += bgw_migrater_DoWork;
         bgw_migrater.RunWorkerCompleted += bgw_migrater_RunWorkerCompleted;
         // 
         // bgw_monitor
         // 
         bgw_monitor.WorkerReportsProgress = true;
         bgw_monitor.WorkerSupportsCancellation = true;
         bgw_monitor.DoWork += bgw_monitor_DoWork;
         bgw_monitor.ProgressChanged += bgw_monitor_ProgressChanged;
         // 
         // tv_tree_src
         // 
         tv_tree_src.Dock = DockStyle.Fill;
         tv_tree_src.HideSelection = false;
         tv_tree_src.Location = new Point(0, 0);
         tv_tree_src.Name = "tv_tree_src";
         tv_tree_src.Size = new Size(624, 428);
         tv_tree_src.TabIndex = 9;
         tv_tree_src.AfterSelect += tv_tree_src_AfterSelect;
         // 
         // tv_tree_dest
         // 
         tv_tree_dest.Dock = DockStyle.Fill;
         tv_tree_dest.HideSelection = false;
         tv_tree_dest.Location = new Point(0, 0);
         tv_tree_dest.Name = "tv_tree_dest";
         tv_tree_dest.Size = new Size(629, 428);
         tv_tree_dest.TabIndex = 10;
         tv_tree_dest.AfterSelect += tv_tree_dest_AfterSelect;
         // 
         // splitContainer1
         // 
         splitContainer1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
         splitContainer1.Location = new Point(12, 173);
         splitContainer1.Name = "splitContainer1";
         // 
         // splitContainer1.Panel1
         // 
         splitContainer1.Panel1.Controls.Add(tv_tree_src);
         // 
         // splitContainer1.Panel2
         // 
         splitContainer1.Panel2.Controls.Add(tv_tree_dest);
         splitContainer1.Size = new Size(1257, 428);
         splitContainer1.SplitterDistance = 624;
         splitContainer1.TabIndex = 12;
         // 
         // label6
         // 
         label6.AutoSize = true;
         label6.Location = new Point(964, 142);
         label6.Name = "label6";
         label6.Size = new Size(299, 28);
         label6.TabIndex = 13;
         label6.Text = "Please check the server first!";
         // 
         // lb_loadmsg
         // 
         lb_loadmsg.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
         lb_loadmsg.AutoSize = true;
         lb_loadmsg.Location = new Point(12, 676);
         lb_loadmsg.Name = "lb_loadmsg";
         lb_loadmsg.Size = new Size(0, 28);
         lb_loadmsg.TabIndex = 15;
         // 
         // lb_msg
         // 
         lb_msg.AutoSize = true;
         lb_msg.Location = new Point(5, 622);
         lb_msg.Name = "lb_msg";
         lb_msg.Size = new Size(0, 28);
         lb_msg.TabIndex = 16;
         // 
         // Form1
         // 
         AutoScaleDimensions = new SizeF(13F, 28F);
         AutoScaleMode = AutoScaleMode.Font;
         ClientSize = new Size(1281, 904);
         Controls.Add(lb_msg);
         Controls.Add(lb_loadmsg);
         Controls.Add(label6);
         Controls.Add(splitContainer1);
         Controls.Add(btn_migrate);
         Controls.Add(label5);
         Controls.Add(groupBox1);
         Name = "Form1";
         Text = "GitMigrater";
         FormClosing += Form1_FormClosing;
         Load += Form1_Load;
         groupBox1.ResumeLayout(false);
         groupBox1.PerformLayout();
         splitContainer1.Panel1.ResumeLayout(false);
         splitContainer1.Panel2.ResumeLayout(false);
         ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
         splitContainer1.ResumeLayout(false);
         ResumeLayout(false);
         PerformLayout();
      }

      #endregion

      private Label label1;
      private GroupBox groupBox1;
      private Label label2;
      private TextBox tb_dest_url;
      private TextBox tb_src_url;
      private TextBox tb_dest_access_token;
      private TextBox tb_src_access_token;
      private Label label4;
      private Label label3;
      private Label label5;
      private Button btn_migrate;
      private System.ComponentModel.BackgroundWorker bgw_src_checker;
      private System.ComponentModel.BackgroundWorker bgw_dest_checker;
      private System.ComponentModel.BackgroundWorker bgw_migrater;
      private System.ComponentModel.BackgroundWorker bgw_monitor;
      private Button btn_ck_src;
      private Button btn_ck_dest;
      private TreeView tv_tree_src;
      private TreeView tv_tree_dest;
      private SplitContainer splitContainer1;
      private Label label6;
      private Label lb_loadmsg;
      private Label lb_msg;
   }
}
