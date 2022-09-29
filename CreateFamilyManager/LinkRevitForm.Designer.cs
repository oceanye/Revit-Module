namespace CreateFamilyManager
{
    partial class LinkRevitForm
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
            this.LView = new System.Windows.Forms.ListView();
            this.SuspendLayout();
            // 
            // LView
            // 
            this.LView.Location = new System.Drawing.Point(1, 2);
            this.LView.Name = "LView";
            this.LView.Size = new System.Drawing.Size(670, 399);
            this.LView.TabIndex = 0;
            this.LView.UseCompatibleStateImageBehavior = false;
            // 
            // LinkRevitForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(675, 400);
            this.Controls.Add(this.LView);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "LinkRevitForm";
            this.Text = "部品部件";
            this.Load += new System.EventHandler(this.LinkRevitForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.ListView LView;
    }
}