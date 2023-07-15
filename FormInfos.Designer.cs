namespace BuildItModsSelector
{
    partial class FormInfos
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
            this.btnOK = new System.Windows.Forms.Button();
            this.lblContainsMods = new System.Windows.Forms.Label();
            this.lblImpactOnPerf = new System.Windows.Forms.Label();
            this.lblImpact = new System.Windows.Forms.Label();
            this.lblModList = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(77, 134);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 0;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // lblContainsMods
            // 
            this.lblContainsMods.AutoSize = true;
            this.lblContainsMods.Location = new System.Drawing.Point(12, 55);
            this.lblContainsMods.Name = "lblContainsMods";
            this.lblContainsMods.Size = new System.Drawing.Size(138, 13);
            this.lblContainsMods.TabIndex = 1;
            this.lblContainsMods.Text = "Contient les mods suivants :";
            // 
            // lblImpactOnPerf
            // 
            this.lblImpactOnPerf.AutoSize = true;
            this.lblImpactOnPerf.Location = new System.Drawing.Point(12, 9);
            this.lblImpactOnPerf.Name = "lblImpactOnPerf";
            this.lblImpactOnPerf.Size = new System.Drawing.Size(139, 13);
            this.lblImpactOnPerf.TabIndex = 2;
            this.lblImpactOnPerf.Text = "Impact sur les performances";
            // 
            // lblImpact
            // 
            this.lblImpact.AutoSize = true;
            this.lblImpact.Location = new System.Drawing.Point(40, 32);
            this.lblImpact.Name = "lblImpact";
            this.lblImpact.Size = new System.Drawing.Size(49, 13);
            this.lblImpact.TabIndex = 3;
            this.lblImpact.Text = "lblImpact";
            // 
            // lblModList
            // 
            this.lblModList.AutoSize = true;
            this.lblModList.Location = new System.Drawing.Point(40, 81);
            this.lblModList.Name = "lblModList";
            this.lblModList.Size = new System.Drawing.Size(54, 13);
            this.lblModList.TabIndex = 4;
            this.lblModList.Text = "lblModList";
            // 
            // FormInfos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(218, 169);
            this.Controls.Add(this.lblModList);
            this.Controls.Add(this.lblImpact);
            this.Controls.Add(this.lblImpactOnPerf);
            this.Controls.Add(this.lblContainsMods);
            this.Controls.Add(this.btnOK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "FormInfos";
            this.Text = "Infos";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Label lblContainsMods;
        private System.Windows.Forms.Label lblImpactOnPerf;
        private System.Windows.Forms.Label lblImpact;
        private System.Windows.Forms.Label lblModList;
    }
}