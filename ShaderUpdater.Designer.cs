﻿namespace BuildItModsSelector
{
    partial class ShaderUpdater
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ShaderUpdater));
            this.btnUpdateShader = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnUpdateShader
            // 
            this.btnUpdateShader.FlatAppearance.BorderSize = 0;
            this.btnUpdateShader.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUpdateShader.Font = new System.Drawing.Font("Montserrat Medium", 8.249999F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUpdateShader.Location = new System.Drawing.Point(371, 391);
            this.btnUpdateShader.Name = "btnUpdateShader";
            this.btnUpdateShader.Size = new System.Drawing.Size(75, 23);
            this.btnUpdateShader.TabIndex = 1;
            this.btnUpdateShader.Text = "UPDATE";
            this.btnUpdateShader.UseVisualStyleBackColor = true;
            this.btnUpdateShader.Click += new System.EventHandler(this.btnUpdateShader_Click);
            // 
            // ShaderUpdater
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnUpdateShader);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ShaderUpdater";
            this.ShowInTaskbar = false;
            this.Text = "ShaderSelectors";
            this.Load += new System.EventHandler(this.ShaderUpdater_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnUpdateShader;
    }
}