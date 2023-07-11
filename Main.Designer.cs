namespace BuildItModsSelector
{
    partial class Main
    {
        /// <summary>
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur Windows Form

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnIsometry = new System.Windows.Forms.Label();
            this.chbIsometricRenderer = new System.Windows.Forms.CheckBox();
            this.btnActiverTout = new System.Windows.Forms.Button();
            this.btnDesactiverTout = new System.Windows.Forms.Button();
            this.btnExec = new System.Windows.Forms.Button();
            this.cbWorldEdit = new System.Windows.Forms.CheckBox();
            this.cbReplaymod = new System.Windows.Forms.CheckBox();
            this.cbLitematica = new System.Windows.Forms.CheckBox();
            this.cbJourneymap = new System.Windows.Forms.CheckBox();
            this.cbCosmetiquesSkins = new System.Windows.Forms.CheckBox();
            this.cbCosmetiquesJeu = new System.Windows.Forms.CheckBox();
            this.lblReplaymod = new System.Windows.Forms.Label();
            this.lblWorldEdit = new System.Windows.Forms.Label();
            this.lblLitematica = new System.Windows.Forms.Label();
            this.lblJourneyMap = new System.Windows.Forms.Label();
            this.lblCosmetiqueSkins = new System.Windows.Forms.Label();
            this.lblCosmetiquesJeu = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnOuvrirMods = new System.Windows.Forms.Button();
            this.btnModsChangelog = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btnQuitter = new System.Windows.Forms.Button();
            this.btnUpdateMods = new System.Windows.Forms.Button();
            this.btnOpenRepos = new System.Windows.Forms.Button();
            this.btnFabric = new System.Windows.Forms.Button();
            this.btnReport = new System.Windows.Forms.Button();
            this.btnDebug = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.groupBox1.Controls.Add(this.btnIsometry);
            this.groupBox1.Controls.Add(this.chbIsometricRenderer);
            this.groupBox1.Controls.Add(this.btnActiverTout);
            this.groupBox1.Controls.Add(this.btnDesactiverTout);
            this.groupBox1.Controls.Add(this.btnExec);
            this.groupBox1.Controls.Add(this.cbWorldEdit);
            this.groupBox1.Controls.Add(this.cbReplaymod);
            this.groupBox1.Controls.Add(this.cbLitematica);
            this.groupBox1.Controls.Add(this.cbJourneymap);
            this.groupBox1.Controls.Add(this.cbCosmetiquesSkins);
            this.groupBox1.Controls.Add(this.cbCosmetiquesJeu);
            this.groupBox1.Controls.Add(this.lblReplaymod);
            this.groupBox1.Controls.Add(this.lblWorldEdit);
            this.groupBox1.Controls.Add(this.lblLitematica);
            this.groupBox1.Controls.Add(this.lblJourneyMap);
            this.groupBox1.Controls.Add(this.lblCosmetiqueSkins);
            this.groupBox1.Controls.Add(this.lblCosmetiquesJeu);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(241, 369);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Mods";
            // 
            // btnIsometry
            // 
            this.btnIsometry.AutoSize = true;
            this.btnIsometry.Location = new System.Drawing.Point(25, 228);
            this.btnIsometry.Name = "btnIsometry";
            this.btnIsometry.Size = new System.Drawing.Size(96, 13);
            this.btnIsometry.TabIndex = 16;
            this.btnIsometry.Text = "Isometric Renderer";
            this.btnIsometry.Click += new System.EventHandler(this.btnIsometry_Click);
            // 
            // chbIsometricRenderer
            // 
            this.chbIsometricRenderer.AutoSize = true;
            this.chbIsometricRenderer.Location = new System.Drawing.Point(145, 227);
            this.chbIsometricRenderer.Name = "chbIsometricRenderer";
            this.chbIsometricRenderer.Size = new System.Drawing.Size(65, 17);
            this.chbIsometricRenderer.TabIndex = 15;
            this.chbIsometricRenderer.Text = "Activé ?";
            this.chbIsometricRenderer.UseVisualStyleBackColor = true;
            // 
            // btnActiverTout
            // 
            this.btnActiverTout.Location = new System.Drawing.Point(9, 340);
            this.btnActiverTout.Name = "btnActiverTout";
            this.btnActiverTout.Size = new System.Drawing.Size(95, 23);
            this.btnActiverTout.TabIndex = 14;
            this.btnActiverTout.Text = "Activer tout";
            this.btnActiverTout.UseVisualStyleBackColor = true;
            this.btnActiverTout.Click += new System.EventHandler(this.btnActiverTout_Click);
            // 
            // btnDesactiverTout
            // 
            this.btnDesactiverTout.Location = new System.Drawing.Point(9, 311);
            this.btnDesactiverTout.Name = "btnDesactiverTout";
            this.btnDesactiverTout.Size = new System.Drawing.Size(95, 23);
            this.btnDesactiverTout.TabIndex = 13;
            this.btnDesactiverTout.Text = "Désactiver tout";
            this.btnDesactiverTout.UseVisualStyleBackColor = true;
            this.btnDesactiverTout.Click += new System.EventHandler(this.btnDesactiverTout_Click);
            // 
            // btnExec
            // 
            this.btnExec.Location = new System.Drawing.Point(118, 311);
            this.btnExec.Name = "btnExec";
            this.btnExec.Size = new System.Drawing.Size(109, 52);
            this.btnExec.TabIndex = 12;
            this.btnExec.Text = "Executer";
            this.btnExec.UseVisualStyleBackColor = true;
            this.btnExec.Click += new System.EventHandler(this.btnExec_Click);
            // 
            // cbWorldEdit
            // 
            this.cbWorldEdit.AutoSize = true;
            this.cbWorldEdit.Location = new System.Drawing.Point(145, 197);
            this.cbWorldEdit.Name = "cbWorldEdit";
            this.cbWorldEdit.Size = new System.Drawing.Size(65, 17);
            this.cbWorldEdit.TabIndex = 11;
            this.cbWorldEdit.Text = "Activé ?";
            this.cbWorldEdit.UseVisualStyleBackColor = true;
            // 
            // cbReplaymod
            // 
            this.cbReplaymod.AutoSize = true;
            this.cbReplaymod.Location = new System.Drawing.Point(145, 166);
            this.cbReplaymod.Name = "cbReplaymod";
            this.cbReplaymod.Size = new System.Drawing.Size(65, 17);
            this.cbReplaymod.TabIndex = 10;
            this.cbReplaymod.Text = "Activé ?";
            this.cbReplaymod.UseVisualStyleBackColor = true;
            // 
            // cbLitematica
            // 
            this.cbLitematica.AutoSize = true;
            this.cbLitematica.Location = new System.Drawing.Point(145, 132);
            this.cbLitematica.Name = "cbLitematica";
            this.cbLitematica.Size = new System.Drawing.Size(65, 17);
            this.cbLitematica.TabIndex = 9;
            this.cbLitematica.Text = "Activé ?";
            this.cbLitematica.UseVisualStyleBackColor = true;
            // 
            // cbJourneymap
            // 
            this.cbJourneymap.AutoSize = true;
            this.cbJourneymap.Location = new System.Drawing.Point(145, 102);
            this.cbJourneymap.Name = "cbJourneymap";
            this.cbJourneymap.Size = new System.Drawing.Size(65, 17);
            this.cbJourneymap.TabIndex = 8;
            this.cbJourneymap.Text = "Activé ?";
            this.cbJourneymap.UseVisualStyleBackColor = true;
            // 
            // cbCosmetiquesSkins
            // 
            this.cbCosmetiquesSkins.AutoSize = true;
            this.cbCosmetiquesSkins.Location = new System.Drawing.Point(145, 72);
            this.cbCosmetiquesSkins.Name = "cbCosmetiquesSkins";
            this.cbCosmetiquesSkins.Size = new System.Drawing.Size(65, 17);
            this.cbCosmetiquesSkins.TabIndex = 7;
            this.cbCosmetiquesSkins.Text = "Activé ?";
            this.cbCosmetiquesSkins.UseVisualStyleBackColor = true;
            // 
            // cbCosmetiquesJeu
            // 
            this.cbCosmetiquesJeu.AutoSize = true;
            this.cbCosmetiquesJeu.Location = new System.Drawing.Point(145, 43);
            this.cbCosmetiquesJeu.Name = "cbCosmetiquesJeu";
            this.cbCosmetiquesJeu.Size = new System.Drawing.Size(65, 17);
            this.cbCosmetiquesJeu.TabIndex = 6;
            this.cbCosmetiquesJeu.Text = "Activé ?";
            this.cbCosmetiquesJeu.UseVisualStyleBackColor = true;
            // 
            // lblReplaymod
            // 
            this.lblReplaymod.AutoSize = true;
            this.lblReplaymod.Location = new System.Drawing.Point(25, 166);
            this.lblReplaymod.Name = "lblReplaymod";
            this.lblReplaymod.Size = new System.Drawing.Size(61, 13);
            this.lblReplaymod.TabIndex = 5;
            this.lblReplaymod.Text = "ReplayMod";
            this.lblReplaymod.Click += new System.EventHandler(this.lblReplaymod_Click);
            // 
            // lblWorldEdit
            // 
            this.lblWorldEdit.AutoSize = true;
            this.lblWorldEdit.Location = new System.Drawing.Point(25, 197);
            this.lblWorldEdit.Name = "lblWorldEdit";
            this.lblWorldEdit.Size = new System.Drawing.Size(53, 13);
            this.lblWorldEdit.TabIndex = 4;
            this.lblWorldEdit.Text = "WorldEdit";
            this.lblWorldEdit.Click += new System.EventHandler(this.lblWorldEdit_Click);
            // 
            // lblLitematica
            // 
            this.lblLitematica.AutoSize = true;
            this.lblLitematica.Location = new System.Drawing.Point(25, 132);
            this.lblLitematica.Name = "lblLitematica";
            this.lblLitematica.Size = new System.Drawing.Size(55, 13);
            this.lblLitematica.TabIndex = 3;
            this.lblLitematica.Text = "Litematica";
            this.lblLitematica.Click += new System.EventHandler(this.lblLitematica_Click);
            // 
            // lblJourneyMap
            // 
            this.lblJourneyMap.AutoSize = true;
            this.lblJourneyMap.Location = new System.Drawing.Point(25, 102);
            this.lblJourneyMap.Name = "lblJourneyMap";
            this.lblJourneyMap.Size = new System.Drawing.Size(65, 13);
            this.lblJourneyMap.TabIndex = 2;
            this.lblJourneyMap.Text = "JourneyMap";
            this.lblJourneyMap.Click += new System.EventHandler(this.lblJourneyMap_Click);
            // 
            // lblCosmetiqueSkins
            // 
            this.lblCosmetiqueSkins.AutoSize = true;
            this.lblCosmetiqueSkins.Location = new System.Drawing.Point(25, 72);
            this.lblCosmetiqueSkins.Name = "lblCosmetiqueSkins";
            this.lblCosmetiqueSkins.Size = new System.Drawing.Size(91, 13);
            this.lblCosmetiqueSkins.TabIndex = 1;
            this.lblCosmetiqueSkins.Text = "Cométiques Skins";
            this.lblCosmetiqueSkins.Click += new System.EventHandler(this.lblCosmetiqueSkins_Click);
            // 
            // lblCosmetiquesJeu
            // 
            this.lblCosmetiquesJeu.AutoSize = true;
            this.lblCosmetiquesJeu.Location = new System.Drawing.Point(25, 43);
            this.lblCosmetiquesJeu.Name = "lblCosmetiquesJeu";
            this.lblCosmetiquesJeu.Size = new System.Drawing.Size(87, 13);
            this.lblCosmetiquesJeu.TabIndex = 0;
            this.lblCosmetiquesJeu.Text = "Cosmetiques Jeu";
            this.lblCosmetiquesJeu.Click += new System.EventHandler(this.lblCosmetiquesJeu_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.groupBox2.Controls.Add(this.btnDebug);
            this.groupBox2.Controls.Add(this.btnOuvrirMods);
            this.groupBox2.Controls.Add(this.btnModsChangelog);
            this.groupBox2.Controls.Add(this.pictureBox1);
            this.groupBox2.Controls.Add(this.btnQuitter);
            this.groupBox2.Controls.Add(this.btnUpdateMods);
            this.groupBox2.Controls.Add(this.btnOpenRepos);
            this.groupBox2.Controls.Add(this.btnReport);
            this.groupBox2.Controls.Add(this.btnFabric);
            this.groupBox2.Location = new System.Drawing.Point(263, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(200, 368);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Système";
            // 
            // btnOuvrirMods
            // 
            this.btnOuvrirMods.Location = new System.Drawing.Point(8, 181);
            this.btnOuvrirMods.Name = "btnOuvrirMods";
            this.btnOuvrirMods.Size = new System.Drawing.Size(186, 23);
            this.btnOuvrirMods.TabIndex = 8;
            this.btnOuvrirMods.Text = "Ouvrir dossier mods";
            this.btnOuvrirMods.UseVisualStyleBackColor = true;
            this.btnOuvrirMods.Click += new System.EventHandler(this.btnOuvrirMods_Click);
            // 
            // btnModsChangelog
            // 
            this.btnModsChangelog.Location = new System.Drawing.Point(8, 89);
            this.btnModsChangelog.Name = "btnModsChangelog";
            this.btnModsChangelog.Size = new System.Drawing.Size(185, 23);
            this.btnModsChangelog.TabIndex = 7;
            this.btnModsChangelog.Text = "Changelog mods";
            this.btnModsChangelog.UseVisualStyleBackColor = true;
            this.btnModsChangelog.Click += new System.EventHandler(this.btnModsChangelog_Click);
            this.btnModsChangelog.MouseHover += new System.EventHandler(this.btnModsChangelog_MouseHover);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::BuildItModsSelector.Properties.Resources.logobi5transparent;
            this.pictureBox1.Location = new System.Drawing.Point(69, 298);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(64, 64);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 6;
            this.pictureBox1.TabStop = false;
            // 
            // btnQuitter
            // 
            this.btnQuitter.Location = new System.Drawing.Point(8, 267);
            this.btnQuitter.Name = "btnQuitter";
            this.btnQuitter.Size = new System.Drawing.Size(185, 23);
            this.btnQuitter.TabIndex = 5;
            this.btnQuitter.Text = "Quitter";
            this.btnQuitter.UseVisualStyleBackColor = true;
            this.btnQuitter.Click += new System.EventHandler(this.btnQuitter_Click);
            this.btnQuitter.MouseHover += new System.EventHandler(this.btnQuitter_MouseHover);
            // 
            // btnUpdateMods
            // 
            this.btnUpdateMods.Location = new System.Drawing.Point(8, 151);
            this.btnUpdateMods.Name = "btnUpdateMods";
            this.btnUpdateMods.Size = new System.Drawing.Size(186, 23);
            this.btnUpdateMods.TabIndex = 3;
            this.btnUpdateMods.Text = "Mettre à jour les mods";
            this.btnUpdateMods.UseVisualStyleBackColor = true;
            this.btnUpdateMods.Click += new System.EventHandler(this.btnUpdateMods_Click);
            this.btnUpdateMods.MouseHover += new System.EventHandler(this.btnUpdateMods_MouseHover);
            // 
            // btnOpenRepos
            // 
            this.btnOpenRepos.Location = new System.Drawing.Point(7, 59);
            this.btnOpenRepos.Name = "btnOpenRepos";
            this.btnOpenRepos.Size = new System.Drawing.Size(187, 23);
            this.btnOpenRepos.TabIndex = 2;
            this.btnOpenRepos.Text = "Repos Github";
            this.btnOpenRepos.UseVisualStyleBackColor = true;
            this.btnOpenRepos.Click += new System.EventHandler(this.btnOpenRepos_Click);
            this.btnOpenRepos.MouseHover += new System.EventHandler(this.btnOpenRepos_MouseHover);
            // 
            // btnFabric
            // 
            this.btnFabric.Location = new System.Drawing.Point(6, 32);
            this.btnFabric.Name = "btnFabric";
            this.btnFabric.Size = new System.Drawing.Size(187, 23);
            this.btnFabric.TabIndex = 0;
            this.btnFabric.Text = "Installer Fabric";
            this.btnFabric.UseVisualStyleBackColor = true;
            this.btnFabric.Click += new System.EventHandler(this.btnFabric_Click);
            this.btnFabric.MouseHover += new System.EventHandler(this.btnFabric_MouseHover);
            // 
            // btnReport
            // 
            this.btnReport.Location = new System.Drawing.Point(7, 240);
            this.btnReport.Name = "btnReport";
            this.btnReport.Size = new System.Drawing.Size(187, 23);
            this.btnReport.TabIndex = 1;
            this.btnReport.Text = "Report un problème";
            this.btnReport.UseVisualStyleBackColor = true;
            this.btnReport.Click += new System.EventHandler(this.btnReport_Click);
            this.btnReport.MouseHover += new System.EventHandler(this.btnInstallGit_MouseHover);
            // 
            // btnDebug
            // 
            this.btnDebug.Location = new System.Drawing.Point(118, 210);
            this.btnDebug.Name = "btnDebug";
            this.btnDebug.Size = new System.Drawing.Size(75, 23);
            this.btnDebug.TabIndex = 9;
            this.btnDebug.Text = "Debug";
            this.btnDebug.UseVisualStyleBackColor = true;
            this.btnDebug.Visible = false;
            this.btnDebug.Click += new System.EventHandler(this.btnDebug_Click);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.BackgroundImage = global::BuildItModsSelector.Properties.Resources.bokeh_sparkle_white_texture;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(478, 393);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Main";
            this.Text = "Build It Saison 5 Mods - 1.0";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox cbWorldEdit;
        private System.Windows.Forms.CheckBox cbReplaymod;
        private System.Windows.Forms.CheckBox cbLitematica;
        private System.Windows.Forms.CheckBox cbJourneymap;
        private System.Windows.Forms.CheckBox cbCosmetiquesSkins;
        private System.Windows.Forms.CheckBox cbCosmetiquesJeu;
        private System.Windows.Forms.Label lblReplaymod;
        private System.Windows.Forms.Label lblWorldEdit;
        private System.Windows.Forms.Label lblLitematica;
        private System.Windows.Forms.Label lblJourneyMap;
        private System.Windows.Forms.Label lblCosmetiqueSkins;
        private System.Windows.Forms.Label lblCosmetiquesJeu;
        private System.Windows.Forms.Button btnActiverTout;
        private System.Windows.Forms.Button btnDesactiverTout;
        private System.Windows.Forms.Button btnExec;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnOpenRepos;
        private System.Windows.Forms.Button btnFabric;
        private System.Windows.Forms.Button btnQuitter;
        private System.Windows.Forms.Button btnUpdateMods;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button btnModsChangelog;
        private System.Windows.Forms.Button btnOuvrirMods;
        private System.Windows.Forms.Label btnIsometry;
        private System.Windows.Forms.CheckBox chbIsometricRenderer;
        private System.Windows.Forms.Button btnReport;
        private System.Windows.Forms.Button btnDebug;
    }
}

