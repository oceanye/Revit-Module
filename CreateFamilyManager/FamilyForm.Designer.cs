namespace CreateFamilyManager
{
    partial class FamilyForm
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
            this.ListV = new System.Windows.Forms.ListView();
            this.SuspendLayout();
            // 
            // ListV
            // 
            this.ListV.Location = new System.Drawing.Point(1, 0);
            this.ListV.Name = "ListV";
            this.ListV.Size = new System.Drawing.Size(702, 388);
            this.ListV.TabIndex = 0;
            this.ListV.UseCompatibleStateImageBehavior = false;
            // 
            // FamilyForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(706, 387);
            this.Controls.Add(this.ListV);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "FamilyForm";
            this.Text = "族文件";
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.ListView ListV;
    }
}