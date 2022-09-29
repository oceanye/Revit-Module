namespace CreateFamilyManager
{
    partial class FamilyFrame
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FamilyFrame));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.DirectoryTree = new System.Windows.Forms.TreeView();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.listView1 = new System.Windows.Forms.ListView();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabControl2 = new System.Windows.Forms.TabControl();
            this.imageList2 = new System.Windows.Forms.ImageList(this.components);
            this.imageList3 = new System.Windows.Forms.ImageList(this.components);
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.显示族文件ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.显示相关项目文件ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tabControl1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Location = new System.Drawing.Point(1, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(883, 432);
            this.tabControl1.TabIndex = 0;
            this.tabControl1.Click += new System.EventHandler(this.tabControl1_Click);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.splitContainer1);
            this.tabPage2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(875, 406);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "加载模型库";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(3, 3);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.DirectoryTree);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.listView1);
            this.splitContainer1.Size = new System.Drawing.Size(869, 400);
            this.splitContainer1.SplitterDistance = 289;
            this.splitContainer1.TabIndex = 0;
            // 
            // DirectoryTree
            // 
            this.DirectoryTree.ImageIndex = 0;
            this.DirectoryTree.ImageList = this.imageList1;
            this.DirectoryTree.Location = new System.Drawing.Point(3, 3);
            this.DirectoryTree.Name = "DirectoryTree";
            this.DirectoryTree.SelectedImageIndex = 0;
            this.DirectoryTree.Size = new System.Drawing.Size(283, 394);
            this.DirectoryTree.TabIndex = 0;
            this.DirectoryTree.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.DirectoryTree_NodeMouseDoubleClick);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "Document.jpeg");
            this.imageList1.Images.SetKeyName(1, "6da6152cb0d8f716fb7cc62a84963ef2.jpeg");
            this.imageList1.Images.SetKeyName(2, "564b5cf0ddee8d8a4e5154230741310e.jpeg");
            // 
            // listView1
            // 
            this.listView1.BackColor = System.Drawing.SystemColors.Menu;
            this.listView1.ForeColor = System.Drawing.SystemColors.WindowText;
            this.listView1.HoverSelection = true;
            this.listView1.Location = new System.Drawing.Point(3, 3);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(570, 394);
            this.listView1.TabIndex = 0;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listView1_MouseDoubleClick);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.tabControl2);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Size = new System.Drawing.Size(875, 406);
            this.tabPage1.TabIndex = 2;
            this.tabPage1.Text = "浏览模型库";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabControl2
            // 
            this.tabControl2.Location = new System.Drawing.Point(3, 3);
            this.tabControl2.Name = "tabControl2";
            this.tabControl2.SelectedIndex = 0;
            this.tabControl2.Size = new System.Drawing.Size(876, 400);
            this.tabControl2.TabIndex = 0;
            this.tabControl2.Click += new System.EventHandler(this.tabControl2_Click);
            // 
            // imageList2
            // 
            this.imageList2.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList2.ImageStream")));
            this.imageList2.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList2.Images.SetKeyName(0, "SJ-MK-LT-1236-2815.png");
            this.imageList2.Images.SetKeyName(1, "SJ-MK-LT-1236-3015.png");
            this.imageList2.Images.SetKeyName(2, "SJ-MK-LT-1239-2815.png");
            this.imageList2.Images.SetKeyName(3, "SJ-MK-LT-1239-3015.png");
            this.imageList2.Images.SetKeyName(4, "SJ-MK-LT-1242-2815.png");
            this.imageList2.Images.SetKeyName(5, "SJ-MK-LT-1242-3015.png");
            this.imageList2.Images.SetKeyName(6, "SJ-MK-LT-1836-2815.png");
            this.imageList2.Images.SetKeyName(7, "SJ-MK-LT-1836-3015.png");
            this.imageList2.Images.SetKeyName(8, "SJ-MK-LT-1839-2815.png");
            this.imageList2.Images.SetKeyName(9, "SJ-MK-LT-1839-3015.png");
            this.imageList2.Images.SetKeyName(10, "SJ-MK-LT-1842-2815.png");
            this.imageList2.Images.SetKeyName(11, "SJ-MK-LT-1842-3015.png");
            this.imageList2.Images.SetKeyName(12, "SJ-MK-PJ-8196B.png");
            this.imageList2.Images.SetKeyName(13, "SJ-MK-PJ-8196C.png");
            this.imageList2.Images.SetKeyName(14, "SJ-MK-PJ-8496B.png");
            this.imageList2.Images.SetKeyName(15, "SJ-MK-PJ-8496C.png");
            this.imageList2.Images.SetKeyName(16, "SJ-MK-PJ-9696B.png");
            this.imageList2.Images.SetKeyName(17, "SJ-MK-PJ-9696C.png");
            this.imageList2.Images.SetKeyName(18, "SJ-MK-WC-8160A.png");
            this.imageList2.Images.SetKeyName(19, "SJ-MK-XZ-8196A.png");
            this.imageList2.Images.SetKeyName(20, "SJ-MK-XZ-8196B.png");
            this.imageList2.Images.SetKeyName(21, "SJ-MK-ZJ-84125A-地理.png");
            this.imageList2.Images.SetKeyName(22, "SJ-MK-ZJ-84125A-合班.png");
            this.imageList2.Images.SetKeyName(23, "SJ-MK-ZJ-84125A-劳技.png");
            this.imageList2.Images.SetKeyName(24, "SJ-MK-ZJ-84125A-美术.png");
            this.imageList2.Images.SetKeyName(25, "SJ-MK-ZJ-84125A-书法.png");
            this.imageList2.Images.SetKeyName(26, "SJ-MK-ZJ-84125A-物理.png");
            this.imageList2.Images.SetKeyName(27, "SJ-MK-ZJ-84125A-音乐.png");
            this.imageList2.Images.SetKeyName(28, "SJ-MK-ZJ-84125A-语言.png");
            this.imageList2.Images.SetKeyName(29, "SJ-MK-ZJ-84125B.png");
            this.imageList2.Images.SetKeyName(30, "SJ-MK-ZJ-84125C.png");
            this.imageList2.Images.SetKeyName(31, "SJ-MK-ZJ-84125D-计算机.png");
            this.imageList2.Images.SetKeyName(32, "XH-MK-PJ-8196A.png");
            this.imageList2.Images.SetKeyName(33, "XH-MK-PJ-8496A .png");
            this.imageList2.Images.SetKeyName(34, "XH-MK-PJ-9696A.png");
            // 
            // imageList3
            // 
            this.imageList3.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imageList3.ImageSize = new System.Drawing.Size(150, 150);
            this.imageList3.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.显示族文件ToolStripMenuItem,
            this.显示相关项目文件ToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(173, 48);
            // 
            // 显示族文件ToolStripMenuItem
            // 
            this.显示族文件ToolStripMenuItem.Name = "显示族文件ToolStripMenuItem";
            this.显示族文件ToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.显示族文件ToolStripMenuItem.Text = "显示族文件";
            // 
            // 显示相关项目文件ToolStripMenuItem
            // 
            this.显示相关项目文件ToolStripMenuItem.Name = "显示相关项目文件ToolStripMenuItem";
            this.显示相关项目文件ToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.显示相关项目文件ToolStripMenuItem.Text = "显示相关项目文件";
            // 
            // FamilyFrame
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(896, 456);
            this.Controls.Add(this.tabControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FamilyFrame";
            this.Text = "模型库加载";
            this.Load += new System.EventHandler(this.FamilyFrame_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ImageList imageList1;
        public System.Windows.Forms.ImageList imageList2;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabControl tabControl2;
        private System.Windows.Forms.ImageList imageList3;
        private System.Windows.Forms.ToolStripMenuItem 显示族文件ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 显示相关项目文件ToolStripMenuItem;
        public System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.TreeView DirectoryTree;
        private System.Windows.Forms.ListView listView1;
    }
}