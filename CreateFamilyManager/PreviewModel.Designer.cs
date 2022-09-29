namespace CreateFamilyManager
{
    partial class PreviewModel
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
            this.panel2 = new System.Windows.Forms.Panel();
            this._cbViews = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this._elementHostWPF = new System.Windows.Forms.Integration.ElementHost();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.Controls.Add(this._cbViews);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Location = new System.Drawing.Point(3, 431);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(600, 82);
            this.panel2.TabIndex = 5;
            // 
            // _cbViews
            // 
            this._cbViews.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this._cbViews.FormattingEnabled = true;
            this._cbViews.Location = new System.Drawing.Point(9, 50);
            this._cbViews.Name = "_cbViews";
            this._cbViews.Size = new System.Drawing.Size(183, 20);
            this._cbViews.TabIndex = 1;
            this._cbViews.SelectedIndexChanged += new System.EventHandler(this.cbViews_SelIdxChanged);
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 12);
            this.label2.TabIndex = 4;
            this.label2.Text = "请选择视图:";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this._elementHostWPF);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(606, 513);
            this.panel1.TabIndex = 1;
            // 
            // _elementHostWPF
            // 
            this._elementHostWPF.Location = new System.Drawing.Point(0, 0);
            this._elementHostWPF.Name = "_elementHostWPF";
            this._elementHostWPF.Size = new System.Drawing.Size(606, 434);
            this._elementHostWPF.TabIndex = 6;
            this._elementHostWPF.Text = "elementHost1";
            this._elementHostWPF.Child = null;
            // 
            // PreviewModel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(606, 513);
            this.Controls.Add(this.panel1);
            this.Name = "PreviewModel";
            this.Text = "PreviewModel";
            this.Load += new System.EventHandler(this.PreviewModel_Load);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.ComboBox _cbViews;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Integration.ElementHost _elementHostWPF;
    }
}