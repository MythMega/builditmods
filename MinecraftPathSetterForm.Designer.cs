namespace BuildItModsSelector
{
    partial class MinecraftPathSetterForm
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
            this.txbLocation = new System.Windows.Forms.TextBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnCHangeMinecraftLocation = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txbLocation
            // 
            this.txbLocation.Location = new System.Drawing.Point(13, 13);
            this.txbLocation.Name = "txbLocation";
            this.txbLocation.Size = new System.Drawing.Size(502, 20);
            this.txbLocation.TabIndex = 0;
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(13, 40);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(250, 23);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "button1";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnCHangeMinecraftLocation
            // 
            this.btnCHangeMinecraftLocation.Location = new System.Drawing.Point(270, 40);
            this.btnCHangeMinecraftLocation.Name = "btnCHangeMinecraftLocation";
            this.btnCHangeMinecraftLocation.Size = new System.Drawing.Size(245, 23);
            this.btnCHangeMinecraftLocation.TabIndex = 2;
            this.btnCHangeMinecraftLocation.Text = "button2";
            this.btnCHangeMinecraftLocation.UseVisualStyleBackColor = true;
            this.btnCHangeMinecraftLocation.Click += new System.EventHandler(this.btnCHangeMinecraftLocation_Click);
            // 
            // MinecraftPathSetterForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(527, 72);
            this.Controls.Add(this.btnCHangeMinecraftLocation);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.txbLocation);
            this.Name = "MinecraftPathSetterForm";
            this.Text = "Location";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.TextBox txbLocation;
        public System.Windows.Forms.Button btnCancel;
        public System.Windows.Forms.Button btnCHangeMinecraftLocation;
    }
}