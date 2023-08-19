using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace BuildItModsSelector
{

    public partial class Main : Form
    {
        // Déclarez une instance de MaterialSkinManager
        private string profileFilePath = Path.Combine("sys", "profile.txt");
        private WebClient webClient;
        private Dictionary<string, string> filesToDownload = new Dictionary<string, string>();
        private string version = "1.1pre-release-1";
        private int app_build = 9; 

        public Main()
        {
            checkStartingFolder();
            InitializeComponent();
            //crée un dossier sys si il n'existe pas
            if (!Directory.Exists(Path.Combine(Application.StartupPath, "sys"))) { Directory.CreateDirectory(Path.Combine(Application.StartupPath, "sys")); }
            // Chemin du fichier profile.txt
            string filePath = profileFilePath;
            comboBoxLanguage.DropDownStyle = ComboBoxStyle.DropDownList;

            // Vérifier si le fichier existe
            if (!File.Exists(filePath))
            {
                // Contenu à écrire dans le fichier
                string fileContent = @"p1=profile 1=
p2=profile 2=
p3=profile 3=
p4=profile 4=
p5=profile 5=
p6=profile 6=
p7=profile 7=
p8=profile 8=
language=??
theme=DEFAULT";

                // Créer le fichier avec le contenu spécifié
                File.WriteAllText(filePath, fileContent);

                // Récupérer la langue du système
                if(new List<string>(){"FR", "EN", "PT", "ES", "IT", "AR", "RU", "IT" }.Contains((CultureInfo.InstalledUICulture).Name.Substring(0, 2).ToUpper()))
                {
                    SetProfileValue("language", (CultureInfo.InstalledUICulture).Name.Substring(0, 2).ToUpper());
                }
                else
                {
                    SetProfileValue("language", "EN");
                }
            }
            reloadProfiles();
            makeTranslation();
            updateTheme();
        }

        private void checkStartingFolder()
        {
            string appFolderPath = Path.GetDirectoryName(Application.ExecutablePath);

            if (Path.GetFileName(appFolderPath) != "mods")
            {
                DialogResult result = MessageBox.Show(
                    translatedText("WARN_MUSTSTARTINMODSFOLDER"),
                    translatedText("WARN_WRONGFOLDER"),
                    MessageBoxButtons.OKCancel,
                    MessageBoxIcon.Warning
                );

                if (result == DialogResult.Cancel)
                {
                    this.Close();
                }
            }
        }

        private void reloadProfiles()
        {
            List<string> profilesStuff = new List<string>()
            {
                "p1","p2","p3","p4","p5","p6","p7","p8"
            };
            foreach (string code in profilesStuff)
            {
                loadUniqueProfile(code);
            }
        }

        private void load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.Fixed3D;
            UpdateProfileFile();
            foreach (var item in comboBoxLanguage.Items)
            {
                if (item.ToString().StartsWith(GetProfileValue("language")))
                {
                    comboBoxLanguage.SelectedItem = item;
                    break;
                }
            }
        }

        public string GetProfileValue(string criterion)
        {
            string[] lines = File.ReadAllLines(profileFilePath);

            foreach (string line in lines)
            {
                string[] parts = line.Split('=');

                if (parts.Length == 2 && parts[0] == criterion)
                {
                    return parts[1];
                }
            }

            // Critère non trouvé, retourner une valeur par défaut ou une chaîne vide
            return string.Empty;
        }

        public void SetProfileValue(string criterion, string value)
        {
            string[] lines = File.ReadAllLines(profileFilePath);

            for (int i = 0; i < lines.Length; i++)
            {
                string line = lines[i];
                string[] parts = line.Split('=');

                if (parts.Length == 2 && parts[0] == criterion)
                {
                    lines[i] = $"{criterion}={value}";
                    File.WriteAllLines(profileFilePath, lines);
                    return;
                }
            }

            // Critère non trouvé, ajouter une nouvelle ligne avec le critère et la valeur
            string newLine = $"{criterion}={value}";
            File.AppendAllText(profileFilePath, Environment.NewLine + newLine);
        }

        private void btnQuitter_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        private List<CheckBox> settings()
        {
            List<CheckBox> list = new List<CheckBox>(){
                cbWorldEdit,
                cbReplaymod,
                cbLitematica,
                cbJourneymap,
                cbCosmetiquesSkins,
                cbCosmetiquesJeu,
                cbIsometricRenderer,
            };

            return list;
        }

        private void btnDesactiverTout_Click(object sender, EventArgs e)
        {
            foreach (CheckBox cb in GetCheckboxsDictionary().Values)
            {
                cb.Checked = false;
            }
        }

        private void btnActiverTout_Click(object sender, EventArgs e)
        {
            foreach (CheckBox cb in GetCheckboxsDictionary().Values)
            {
                cb.Checked = true;
            }
        }

        private void btnFabric_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://fabricmc.net/");
        }

        private void btnReport_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/MythMega/builditmods/issues/new");
        
        }

        private void btnOpenRepos_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/MythMega/builditmods");
        }

        private void btnModsChangelog_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/MythMega/builditmods/commits/master");
        }

        private void btnExec_Click(object sender, EventArgs e)
        {
            try
            {
                Process process = new Process();
                string filePath = "";
                process.StartInfo.WorkingDirectory = Application.StartupPath;
                process.StartInfo.CreateNoWindow = true; // Évite la création de fenêtre de console
                process.StartInfo.UseShellExecute = false; // N'utilise pas le shell pour exécuter le fichier

                if (cbWorldEdit.Checked) { filePath = Path.Combine(Application.StartupPath, "sys/bats/worldedit_enable.bat"); }
                else { filePath = Path.Combine(Application.StartupPath, "sys/bats/worldedit_disable.bat"); }
                process.StartInfo.FileName = filePath; process.Start();

                if (cbReplaymod.Checked) { filePath = Path.Combine(Application.StartupPath, "sys/bats/replaymod_enable.bat"); }
                else { filePath = Path.Combine(Application.StartupPath, "sys/bats/replaymod_disable.bat"); }
                process.StartInfo.FileName = filePath; process.Start();

                if (cbLitematica.Checked) { filePath = Path.Combine(Application.StartupPath, "sys/bats/litematica_enable.bat"); }
                else { filePath = Path.Combine(Application.StartupPath, "sys/bats/litematica_disable.bat"); }
                process.StartInfo.FileName = filePath; process.Start();

                if (cbJourneymap.Checked) { filePath = Path.Combine(Application.StartupPath, "sys/bats/journeymap_enable.bat"); }
                else { filePath = Path.Combine(Application.StartupPath, "sys/bats/journeymap_disable.bat"); }
                process.StartInfo.FileName = filePath; process.Start();

                if (cbCosmetiquesSkins.Checked) { filePath = Path.Combine(Application.StartupPath, "sys/bats/cosmetique_skin_enable.bat"); }
                else { filePath = Path.Combine(Application.StartupPath, "sys/bats/cosmetique_skin_disable.bat"); }
                process.StartInfo.FileName = filePath; process.Start();

                if (cbCosmetiquesGUI.Checked) { filePath = Path.Combine(Application.StartupPath, "sys/bats/cosmetique_gui_enable.bat"); }
                else { filePath = Path.Combine(Application.StartupPath, "sys/bats/cosmetique_gui_disable.bat"); }
                process.StartInfo.FileName = filePath; process.Start();

                if (cbIsometricRenderer.Checked) { filePath = Path.Combine(Application.StartupPath, "sys/bats/isorender_enable.bat"); }
                else { filePath = Path.Combine(Application.StartupPath, "sys/bats/isorender_disable.bat"); }
                process.StartInfo.FileName = filePath; process.Start();

                if (cbCosmetiquesJeu.Checked) { filePath = Path.Combine(Application.StartupPath, "sys/bats/cosmetique_jeu_enable.bat"); }
                else { filePath = Path.Combine(Application.StartupPath, "sys/bats/cosmetique_jeu_disable.bat"); }
                process.StartInfo.FileName = filePath; process.Start();

                MessageBox.Show(translatedText("INFO_SUCCESS"));
            }
            catch
            {
                MessageBox.Show(translatedText("ERR_NOMODS"));
            }

        }

        private void btnUpdateMods_Click(object sender, EventArgs e)
        {
            DownloadFileSyncInSys("modlist.config", "https://jmdbymyth.000webhostapp.com/modlist.config");
            Thread.Sleep(3000);
            string modlistFilePath = Path.Combine("sys", "modlist.config");
            enableAllMod();
            if (File.Exists(modlistFilePath))
            {
                string[] lines = File.ReadAllLines(modlistFilePath);

                foreach (string line in lines)
                {
                    // Diviser chaque ligne en nom de fichier et lien de fichier
                    string[] parts = line.Split('=');
                    if (parts.Length == 2)
                    {
                        string nomDeFichier = parts[0].Trim() + ".jar";
                        string lienDuFichier = parts[1].Trim();

                        // Appeler la fonction Download avec les paramètres appropriés
                        DownloadFiles(nomDeFichier, lienDuFichier);
                    }
                    else
                    {
                        Console.WriteLine($"Format de ligne invalide : {line}");
                    }
                }
            }
            else
            {
                Console.WriteLine($"Le fichier {modlistFilePath} n'existe pas.");
            }

            Console.WriteLine("Téléchargements terminés.");


            //batFile
            DownloadFileSyncInSys("batlist.config", "https://jmdbymyth.000webhostapp.com/batlist.config");
            string batlistFilePath = Path.Combine("sys", "batlist.config");
            if (File.Exists(batlistFilePath))
            {
                string[] lines = File.ReadAllLines(batlistFilePath);

                foreach (string line in lines)
                {
                    // Diviser chaque ligne en nom de fichier et lien de fichier
                    string[] parts = line.Split('=');
                    if (parts.Length == 2)
                    {
                        string nomDeFichier = parts[0].Trim() + ".bat";
                        string lienDuFichier = parts[1].Trim();

                        // Appeler la fonction Download avec les paramètres appropriés
                        DownloadFiles(nomDeFichier, lienDuFichier);
                    }
                    else
                    {
                        Console.WriteLine($"Format de ligne invalide : {line}");
                    }
                }
            }
            else
            {
                Console.WriteLine($"Le fichier {batlistFilePath} n'existe pas.");
            }

            Console.WriteLine("Téléchargements terminés.");

            Thread.Sleep(3000);

            bool moving = false;
            while (!moving) {
                try
                {
                    moveBatFiles();
                    moving = true;
                }
                catch
                {
                    Thread.Sleep(50);
                }
            }


        }

        private void DownloadFileSyncInSys(string file, string url)
        {
            DownloadFiles("sys/" + file, url);
            bool fileIsDownloaded = false;
            int debug_timetodownloadFile = 0;




            string sysFilePath = Path.Combine("sys", file);

            while (!fileIsDownloaded)
            {
                try
                {
                    string[] lines = File.ReadAllLines(sysFilePath);
                    fileIsDownloaded = true;
                }
                catch
                {
                    Thread.Sleep(50);
                    debug_timetodownloadFile += 50;
                    if (debug_timetodownloadFile == 5000)
                    {
                        MessageBox.Show(translatedText("ERR_TIMEOUT"));
                        return;
                    }
                }
            }
        }

        private void btnOuvrirMods_Click(object sender, EventArgs e)
        {
            string workingDirectory = Application.StartupPath;

            Process process = new Process();
            process.StartInfo.FileName = "explorer.exe";
            process.StartInfo.Arguments = workingDirectory;
            process.Start();
        }
        private void lblCosmetiquesJeu_Click(object sender, EventArgs e)
        {
            Dictionary<string, int> modlist = new Dictionary<string, int> {
                { "Continuity", 1 },
                { "Falling Leaves", 3 },
            };

            showModstats(modlist, getLanguageCode());
        }

        private void lblCommandsAdmins_Click(object sender, EventArgs e)
        {
            Dictionary<string, int> modlist = new Dictionary<string, int> {
                { "CommandMacros", 1 },
            };

            showModstats(modlist, getLanguageCode());
        }
        private void lblCosmetiqueSkins_Click(object sender, EventArgs e)
        {
            Dictionary<string, int> modlist = new Dictionary<string, int> {
                { "3D Skin Layer", 2 },
                { "Capes", 2 },
            };

            showModstats(modlist, getLanguageCode());
        }

        private void lblCosmetiqueGUI_Click(object sender, EventArgs e)
        {
            Dictionary<string, int> modlist = new Dictionary<string, int> {
                { "Blur", 1 },
                { "Chat Head", 0 },
            };

            showModstats(modlist, getLanguageCode());
        }

        private void lblJourneyMap_Click(object sender, EventArgs e)
        {
            Dictionary<string, int> modlist = new Dictionary<string, int> {
                { "JourneyMap", 10 },
            };

            showModstats(modlist, getLanguageCode());
        }

        private void lblLitematica_Click(object sender, EventArgs e)
        {
            Dictionary<string, int> modlist = new Dictionary<string, int> {
                { "Litematica", 4 },
                { "Malilib", 0 },
                { "Litematica Enderchest", 1 },
            };

            showModstats(modlist, getLanguageCode());
        }

        private void lblReplaymod_Click(object sender, EventArgs e)
        {
            Dictionary<string, int> modlist = new Dictionary<string, int> {
                { "ReplayMod", 4 },
                { "Bobby", 6 },
            };

            showModstats(modlist, getLanguageCode());
        }

        private void lblWorldEdit_Click(object sender, EventArgs e)
        {
            Dictionary<string, int> modlist = new Dictionary<string, int> {
                { "World Edit", 15 },
            };

            showModstats(modlist, getLanguageCode());
        }
        private void lblIsometry_Click(object sender, EventArgs e)
        {
            Dictionary<string, int> modlist = new Dictionary<string, int> {
                { "Isometric Renderer", 11 },
                { "owo-lib", 0 },
            };

            showModstats(modlist, getLanguageCode());
        }

        private void showModstats(Dictionary<string, int> modlist, string languageCode)
        {
            FormInfos leForm = new FormInfos();
            string modNames = string.Join("\n", modlist.Keys);
            int poidsMods = modlist.Values.Sum();
            leForm.modlistSet(modNames);
            leForm.makeTranslation(languageCode, poidsMods);
            leForm.ShowDialog();
        }

        private void btnUpdateMods_MouseHover(object sender, EventArgs e)
        {
            ToolTip toolTip = new ToolTip();
            toolTip.SetToolTip(btnUpdateMods, translatedText("MOUSEHOVER_BTNUPDATE"));
        }

        private void btnFabric_MouseHover(object sender, EventArgs e)
        {
            ToolTip toolTip = new ToolTip();
            toolTip.SetToolTip(btnFabric, translatedText("MOUSEHOVER_BTNFABRIC"));
        }

        private void btnOpenRepos_MouseHover(object sender, EventArgs e)
        {
            ToolTip toolTip = new ToolTip();
            toolTip.SetToolTip(btnOpenRepos, translatedText("MOUSEHOVER_BTNSOURCECODE"));
        }

        private void btnModsChangelog_MouseHover(object sender, EventArgs e)
        {
            ToolTip toolTip = new ToolTip();
            toolTip.SetToolTip(btnModsChangelog, translatedText("MOUSEHOVER_BTNCHANGELOG"));
        }

        private void btnReport_MouseHover(object sender, EventArgs e)
        {
            ToolTip toolTip = new ToolTip();
            toolTip.SetToolTip(btnReport, translatedText("MOUSEHOVER_BTNREPORT"));
        }

        private void btnQuitter_MouseHover(object sender, EventArgs e)
        {
            ToolTip toolTip = new ToolTip();
            toolTip.SetToolTip(btnQuitter, translatedText("MOUSEHOVER_BTNQUITTER"));
        }

        public static void enableAllMod()
        {
            string fileExtension = "dis";
            try
            {
                string applicationDirectory = AppDomain.CurrentDomain.BaseDirectory;
                string[] files = Directory.GetFiles(applicationDirectory, "*." + fileExtension);

                foreach (string filePath in files)
                {
                    string newFilePath = Path.ChangeExtension(filePath, "jar");
                    File.Move(filePath, newFilePath);
                }

                Console.WriteLine("Files renamed successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error renaming files: " + ex.Message);
            }
        }


        public void DownloadFiles(string filename, string link)
        {
            // Ajouter les fichiers à télécharger dans le dictionnaire
            filesToDownload.Add(filename, link);
            // Ajouter d'autres fichiers ici avec leurs noms et URLs correspondants

            // Télécharger les fichiers du dictionnaire
            foreach (var fileEntry in filesToDownload)
            {
                string fileName = fileEntry.Key;
                string url = fileEntry.Value;

                // Créer une instance de WebClient
                webClient = new WebClient();

                // S'abonner à l'événement DownloadFileCompleted
                webClient.DownloadFileCompleted += WebClient_DownloadFileCompleted;

                // Télécharger le fichier depuis l'URL spécifiée vers le chemin de destination
                webClient.DownloadFileAsync(new Uri(url), fileName);
            }
            filesToDownload.Clear();
        }

        private void WebClient_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            // Vérifier si le téléchargement a réussi
            if (!e.Cancelled && e.Error == null)
            {
                // Obtenir le nom de fichier à partir de UserState
                string fileName = (string)e.UserState;

                //MessageBox.Show($"Téléchargement terminé : {fileName}");
            }
            else if (e.Cancelled)
            {
                //MessageBox.Show("Le téléchargement a été annulé.");
            }
            else
            {
                //MessageBox.Show($"Une erreur s'est produite lors du téléchargement : {e.Error.Message}");
            }

            // Libérer les ressources du WebClient
            webClient.Dispose();
            filesToDownload.Clear();
        }

        public static void ExecuteBatchFile(string filePath)
        {
            try
            {
                Process process = new Process();
                process.StartInfo.FileName = filePath;
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.RedirectStandardOutput = true;
                process.Start();
                process.WaitForExit();
                string output = process.StandardOutput.ReadToEnd();
                Console.WriteLine(output);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Une erreur s'est produite lors de l'exécution du fichier batch : " + ex.Message);
            }
        }

        private void moveBatFiles()
        {
            string appDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string batsDirectory = Path.Combine(appDirectory, "sys", "bats");
            if (Directory.Exists(batsDirectory))
            {
                Directory.Delete(batsDirectory, true);
            }
            // Vérifier si le dossier de destination existe, sinon le créer
            if (!Directory.Exists(batsDirectory))
            {
                Directory.CreateDirectory(batsDirectory);
            }

            // Obtenir la liste des fichiers .bat dans le répertoire de l'application
            string[] batFiles = Directory.GetFiles(appDirectory, "*.bat");

            // Parcourir tous les fichiers .bat et les déplacer vers le dossier de destination
            foreach (string batFile in batFiles)
            {
                string fileName = Path.GetFileName(batFile);
                string destinationPath = Path.Combine(batsDirectory, fileName);

                // Déplacer le fichier vers le dossier de destination
                File.Move(batFile, destinationPath);
            }
        }

        private void btnDebug_Click(object sender, EventArgs e)
        {
            enableAllMod();
        }

        private List<Button> GetProfilesButtons()
        {
            return new List<Button>()
                    {
                        btnProfile1, btnProfile2, btnProfile3, btnProfile4, btnProfile5, btnProfile6, btnProfile7, btnProfile8
                    };
        }

        private List<CheckBox> GetModsCheckboxs()
        {
            return new List<CheckBox>()
                    {
                        cbCosmetiquesGUI, cbCosmetiquesJeu, cbCosmetiquesSkins, cbJourneymap, cbLitematica, cbReplaymod, cbWorldEdit, cbIsometricRenderer, cbCommandsAdmins
                    };
        }

        public Dictionary<string, Button> GetButtonDictionary()
        {
            Dictionary<string, Button> buttonDictionary = new Dictionary<string, Button>
            {
                { btnProfile1.Tag.ToString(), btnProfile1 },
                { btnProfile2.Tag.ToString(), btnProfile2 },
                { btnProfile3.Tag.ToString(), btnProfile3 },
                { btnProfile4.Tag.ToString(), btnProfile4 },
                { btnProfile5.Tag.ToString(), btnProfile5 },
                { btnProfile6.Tag.ToString(), btnProfile6 },
                { btnProfile7.Tag.ToString(), btnProfile7 },
                { btnProfile8.Tag.ToString(), btnProfile8 }
            };

            return buttonDictionary;
        }

        public Dictionary<string, CheckBox> GetCheckboxsDictionary()
        {
            Dictionary<string, CheckBox> checkboxDictionary = new Dictionary<string, CheckBox>
            {
                { cbCosmetiquesGUI.Tag.ToString(), cbCosmetiquesGUI },
                { cbCosmetiquesJeu.Tag.ToString(), cbCosmetiquesJeu },
                { cbCosmetiquesSkins.Tag.ToString(), cbCosmetiquesSkins },
                { cbJourneymap.Tag.ToString(), cbJourneymap },
                { cbLitematica.Tag.ToString(), cbLitematica },
                { cbReplaymod.Tag.ToString(), cbReplaymod },
                { cbWorldEdit.Tag.ToString(), cbWorldEdit },
                { cbCommandsAdmins.Tag.ToString(), cbCommandsAdmins },
                { cbIsometricRenderer.Tag.ToString(), cbIsometricRenderer }
            };

            return checkboxDictionary;
        }

        public Dictionary<string, ToolStripMenuItem> GetProfilToolStripMenuItems()
        {
            Dictionary<string, ToolStripMenuItem> toolStripMenuItems = new Dictionary<string, ToolStripMenuItem>
            {
                { profil1ToolStripMenuItem.Tag.ToString(), profil1ToolStripMenuItem },
                { profil2ToolStripMenuItem.Tag.ToString(), profil2ToolStripMenuItem },
                { profil3ToolStripMenuItem.Tag.ToString(), profil3ToolStripMenuItem },
                { profil4ToolStripMenuItem.Tag.ToString(), profil4ToolStripMenuItem },
                { profil5ToolStripMenuItem.Tag.ToString(), profil5ToolStripMenuItem },
                { profil6ToolStripMenuItem.Tag.ToString(), profil6ToolStripMenuItem },
                { profil7ToolStripMenuItem.Tag.ToString(), profil7ToolStripMenuItem },
                { profil8ToolStripMenuItem.Tag.ToString(), profil8ToolStripMenuItem }
            };

            return toolStripMenuItems;
        }

        private void profilSaveBtn(object sender, EventArgs e)
        {
            ToolStripMenuItem clickedButton = (ToolStripMenuItem)sender;
            if(txbProfileName.Text.Length < 1)
            {
                txbProfileName.Text = clickedButton.Text;
            }
            clickedButton.Text = txbProfileName.Text;
            Dictionary<string, Button> myBtns = GetButtonDictionary();
            myBtns[clickedButton.Tag.ToString()].Text = txbProfileName.Text;
            string lineSave = txbProfileName.Text + "=";
            foreach (CheckBox checkedElement in GetModsCheckboxs())
            {
                if (checkedElement.Checked)
                {
                    lineSave += checkedElement.Tag.ToString() + ",";
                }
            }
            WriteProfile(clickedButton.Tag.ToString(), lineSave.Substring(0, lineSave.Length - 1));

        }

        private void profilLoadBtn(object sender, EventArgs e)
        {
            Button btnLoad = (Button)sender;
            loadUniqueProfile(btnLoad.Tag.ToString());
            btnExec_Click(sender, e);
        }

        private void WriteProfile(string profileNumber, string lineSave)
        {
            string keyPrefix = profileNumber; // Clé souhaitée

            // Lire le contenu du fichier dans une liste de chaînes de caractères
            List<string> lines = File.ReadAllLines(profileFilePath).ToList();

            // Rechercher la ligne qui commence par la clé souhaitée
            int lineIndex = lines.FindIndex(line => line.StartsWith(keyPrefix));

            if (lineIndex != -1)
            {
                // Modifier cette ligne avec les nouvelles valeurs
                string newValue = lineSave; // Nouvelles valeurs
                lines[lineIndex] = $"{keyPrefix}={newValue}";
            }

            // Écrire le contenu mis à jour dans le fichier
            File.WriteAllLines(profileFilePath, lines);

        }

        private void quitterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public void loadUniqueProfile(string profileCode)
        {
            foreach(CheckBox checkBox in GetModsCheckboxs()) {
                checkBox.Checked = false;
            }
           

            // Vérifier si le fichier existe
            if (File.Exists(profileFilePath))
            {
                // Lire toutes les lignes du fichier
                string[] lines = File.ReadAllLines(profileFilePath);

                // Chercher la ligne correspondant au profil
                string profileLine = lines.FirstOrDefault(line => line.StartsWith(profileCode + "="));

                // Vérifier si la ligne a été trouvée
                if (profileLine != null)
                {
                    // Extraire le nom du profil
                    string profileName = profileLine.Substring(profileCode.Length + 1).Split('=')[0];
                    Dictionary<string, Button> myBtns = GetButtonDictionary();
                    Button btn = myBtns[profileCode];
                    btn.Text = profileName;
                    Dictionary<string, ToolStripMenuItem> myTSMIs = GetProfilToolStripMenuItems();
                    ToolStripMenuItem TSMI = myTSMIs[profileCode];
                    TSMI.Text = profileName;

                    // Extraire les éléments de la troisième partie de la ligne
                    string[] elements;
                    try
                    {
                        elements = profileLine.Split('=')[2].Split(',');
                    }
                    catch {
                        elements = new string[0];
                    }
                    

                    // Afficher chaque élément
                    foreach (string element in elements)
                    {
                        Dictionary<string, CheckBox> myCbs = GetCheckboxsDictionary();
                        if(element != "")
                        {
                            CheckBox myCb = myCbs[element];
                            myCb.Checked = true;
                        }
                        
                    }
                }
                else
                {
                    Console.WriteLine("Profil non trouvé : " + profileCode);
                }
            }
            else
            {
                Console.WriteLine("Fichier profile.txt introuvable.");
            }
        }

        public void changeLanguage(string languageCode)
        {
            string[] lines = File.ReadAllLines(profileFilePath);
            List<string> modifiedLines = new List<string>();

            foreach (string line in lines)
            {
                if (line.StartsWith("language="))
                {
                    modifiedLines.Add($"language={languageCode}");
                }
                else
                {
                    modifiedLines.Add(line);
                }
            }

            File.WriteAllLines(profileFilePath, modifiedLines);
        }

        public string getLanguageCode()
        {
            string ProfileFilePath = Path.Combine("sys", "profile.txt");
            string[] lines = File.ReadAllLines(ProfileFilePath);
            string code = "EN";

            foreach (string line in lines)
            {
                if (line.StartsWith("language="))
                {
                    string language = line.Substring(line.IndexOf('=') + 1);
                    Console.WriteLine($"Current language: {language}");
                    return language;
                }
            }
            return code;
        }

        public void UpdateProfileFile()
        {
            string[] lines = File.ReadAllLines(profileFilePath);

            // Vérifier si la ligne "language=" existe
            bool languageExists = false;
            // Vérifier si la ligne "theme=" existe
            bool themeExists = false;

            foreach (string line in lines)
            {
                if (line.StartsWith("language="))
                    languageExists = true;
                if (line.StartsWith("theme="))
                    themeExists = true;
            }

            // Si la ligne "language=" n'existe pas, l'ajouter à la fin du fichier
            if (!languageExists)
                File.AppendAllText(profileFilePath, "language=EN" + Environment.NewLine);

            // Si la ligne "theme=" n'existe pas, l'ajouter à la fin du fichier
            if (!themeExists)
                File.AppendAllText(profileFilePath, "theme=DEFAULT" + Environment.NewLine);
        }

        private void comboBoxLanguage_SelectedIndexChanged(object sender, EventArgs e)
        {
            changeLanguage(comboBoxLanguage.SelectedItem.ToString().Substring(0, 2));
            makeTranslation();
        }

        private void changeThemeClick(object sender, EventArgs e)
        {
            string theme = ((ToolStripMenuItem)sender).Text;
            SetProfileValue("theme", theme);
            updateTheme();
        }

        

        private void MenuItem_MouseEnter(object sender, EventArgs e)
        {
            if (sender is ToolStripMenuItem menuItem)
            {
                menuItem.BackColor = Color.Gray; // Modifier la couleur de fond lors du survol
            }
        }

        private void MenuItem_MouseLeave(object sender, EventArgs e)
        {
            if (sender is ToolStripMenuItem menuItem)
            {
                menuItem.BackColor = SystemColors.Control; // Rétablir la couleur de fond par défaut
            }
        }

        private void MenuItem_Paint(object sender, PaintEventArgs e)
        {
            if (sender is ToolStripMenuItem menuItem)
            {
                if (menuItem.Selected)
                {
                    e.Graphics.FillRectangle(Brushes.LightBlue, e.ClipRectangle); // Modifier la couleur de fond lors de la sélection
                }
            }
        }

        private void aProposToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show(translatedText("INFO_ABOUT"));
        }

        public void notYetImpletmentedFeature()
        {
            MessageBox.Show(translatedText("ERR_NOTYETIMPLEMENTED"));
        }

        private void mythMegaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show(translatedText("INFO_MYTHMEGA"));
        }

        private void wikiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/MythMega/mysurvivalmods/wiki");
        }

        private void rafraichirProfilsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            reloadProfiles();
        }

        private void réinitialiserProfilsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(translatedText("LBL_CONFIRMATION"), translatedText("LBL_WARNING"), MessageBoxButtons.YesNo);

            if (result == DialogResult.No)
            {
                return;
            }

            string[] lines = File.ReadAllLines(profileFilePath);

            List<string> updatedLines = new List<string>();
            int c = 1;

            foreach (string line in lines)
            {
                if (line.StartsWith("p") && char.IsDigit(line[1]))
                {
                    // Garder les 3 premiers caractères de la ligne
                    string updatedLine = line.Substring(0, 3) + $"Profile {c}";
                    c++;
                    updatedLines.Add(updatedLine);
                }
                else
                {
                    updatedLines.Add(line);
                }
            }

            File.WriteAllLines(profileFilePath, updatedLines);
            reloadProfiles();
        }

        private void btnLocateMinecraft_MouseHover(object sender, EventArgs e)
        {
            ToolTip toolTip = new ToolTip();
            toolTip.SetToolTip(btnUpdateMods, translatedText("MOUSEHOVER_BTNUPDATE"));
        }

        private Dictionary<string, string> getContributor()
        {
            Dictionary<string, string> keyValuePairs = new Dictionary<string, string>();
            return keyValuePairs;
        }

        private string getContributorsNamesAndLbl(){
            string res = string.Empty;
            return res;
        }

        private void contributeursToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //TODO
        }


        private void updateTheme()
        {
            string theme = GetProfileValue("theme");
            List<Control> allControls = new List<Control>();

            foreach (Control control in Controls)
            {
                allControls.Add(control);
                if (control is GroupBox)
                {
                    foreach (Control element in control.Controls)
                    {
                        allControls.Add(element);
                    }
                }
            }

            Color backColor = Color.FromArgb(255, 225, 225, 225);
            Color lightBackColor = Color.FromArgb(255, 225, 225, 245);
            Color lightMainColor = Color.FromArgb(255, 12, 20, 55);
            Color secondaryColor = Color.LightGray;
            Color textColor = Color.Black;

            switch (theme)
            {
                case "DEFAULT":
                    backColor = Color.FromArgb(255, 225, 225, 225);
                    lightBackColor = Color.FromArgb(255, 225, 225, 245);
                    lightMainColor = Color.FromArgb(255, 12, 20, 55);
                    secondaryColor = Color.LightGray;
                    textColor = Color.Black;
                    break;

                case "DARK":
                    backColor = Color.FromArgb(255, 10, 10, 14);
                    lightBackColor = Color.FromArgb(255, 15, 15, 20);
                    lightMainColor = Color.FromArgb(255, 17, 17, 23);
                    secondaryColor = Color.FromArgb(255, 50, 50, 70);
                    textColor = Color.White;
                    break;

                case "BUILDITGREEN":
                    backColor = Color.FromArgb(255, 30, 30, 30);
                    lightBackColor = Color.FromArgb(255, 40, 40, 40);
                    lightMainColor = Color.FromArgb(255, 12, 55, 20);
                    secondaryColor = Color.FromArgb(255, 50, 70, 50);
                    textColor = Color.FromArgb(255, 28, 239, 91);
                    break;

                case "BUILDITBLUE":
                    backColor = Color.FromArgb(255, 30, 30, 30);
                    lightBackColor = Color.FromArgb(255, 40, 40, 40);
                    lightMainColor = Color.FromArgb(255, 12, 20, 55);
                    secondaryColor = Color.FromArgb(255, 50, 50, 70);
                    textColor = Color.FromArgb(255, 50, 200, 244);
                    break;

                case "BUILDITYELLOW":
                    backColor = Color.FromArgb(30, 30, 30);
                    lightBackColor = Color.FromArgb(40, 40, 40);
                    lightMainColor = Color.FromArgb(12, 55, 20);
                    secondaryColor = Color.FromArgb(50, 70, 50);
                    textColor = Color.Yellow;
                    break;

                case "BUILDITPINK":
                    backColor = Color.FromArgb(30, 30, 30);
                    lightBackColor = Color.FromArgb(40, 40, 40);
                    lightMainColor = Color.FromArgb(12, 55, 20);
                    secondaryColor = Color.FromArgb(50, 70, 50);
                    textColor = Color.Pink;
                    break;

                case "BUILDITORANGE":
                    backColor = Color.FromArgb(30, 30, 30);
                    lightBackColor = Color.FromArgb(40, 40, 40);
                    lightMainColor = Color.FromArgb(12, 55, 20);
                    secondaryColor = Color.FromArgb(50, 70, 50);
                    textColor = Color.Orange;
                    break;

                case "CANDY":
                    backColor = Color.Pink;
                    lightBackColor = Color.Purple;
                    lightMainColor = Color.Blue;
                    secondaryColor = Color.Green;
                    textColor = Color.White;
                    break;

                case "MINT":
                    backColor = Color.LightGreen;
                    lightBackColor = Color.Green;
                    lightMainColor = Color.DarkGreen;
                    secondaryColor = Color.PaleGreen;
                    textColor = Color.Black;
                    break;

                case "PASTEL":
                    backColor = Color.FromArgb(240, 240, 240);
                    lightBackColor = Color.FromArgb(230, 230, 230);
                    lightMainColor = Color.FromArgb(200, 200, 200);
                    secondaryColor = Color.LightGray;
                    textColor = Color.White;
                    break;

                case "AQUA":
                    backColor = Color.FromArgb(100, 149, 237);
                    lightBackColor = Color.FromArgb(135, 206, 250);
                    lightMainColor = Color.FromArgb(0, 0, 128);
                    secondaryColor = Color.Cyan;
                    textColor = Color.White;
                    break;

            }
            menuStrip1.BackColor = backColor;
            this.BackColor = backColor;

            foreach (ToolStripMenuItem item in menuStrip1.Items.OfType<ToolStripMenuItem>())
            {
                item.BackColor = backColor;
                item.ForeColor = textColor;

                foreach (ToolStripItem subItem in item.DropDownItems)
                {
                    subItem.BackColor = lightBackColor;
                    subItem.ForeColor = textColor;
                    if (subItem is ToolStripMenuItem)
                    {
                        ToolStripMenuItem menuItem = (ToolStripMenuItem)subItem;
                        foreach (ToolStripItem againmoresubItem in menuItem.DropDownItems)
                        {
                            againmoresubItem.BackColor = lightBackColor;
                            againmoresubItem.ForeColor = textColor;
                        }
                    }
                }
            }
            foreach (Control control in allControls)
            {
                switch (control)
                {
                    case Label label:
                        // Action spécifique pour les labels
                        // ...
                        break;

                    case Button button:
                        button.BackColor = backColor;
                        button.ForeColor = textColor;
                        button.FlatAppearance.MouseOverBackColor = lightBackColor;
                        button.FlatAppearance.MouseDownBackColor = lightMainColor;
                        break;

                    case GroupBox groupBox:
                        groupBox.BackColor = backColor;
                        groupBox.ForeColor = textColor;
                        break;

                    case TextBox textBox:
                        textBox.BackColor = secondaryColor;
                        textBox.ForeColor = textColor;
                        break;

                    case ComboBox comboBox:
                        comboBox.BackColor = secondaryColor;
                        comboBox.ForeColor = textColor;
                        break;

                    default:
                        // Action par défaut pour les autres types de contrôles
                        // ...
                        break;
                }

            }

        }


        #region TRADUCTIONS

        public void makeTranslation()
        {
            string LanguageCode = getLanguageCode();
            string isEnabled = "!! Missing Translation !!";


            lblJourneyMap.Text = "JourneyMap";
            lblLitematica.Text = "Litematica";
            lblReplaymod.Text = "ReplayMod";
            lblWorldEdit.Text = "WorldEdit";
            lblIsometry.Text = "Isometric Renderer";
            gbMods.Text = "Mods";

            switch (LanguageCode)
            {
                // english
                case "EN":
                    // label mods
                    lblCosmetiqueGUI.Text = "GUI Cosmetics";
                    lblCosmetiquesJeu.Text = "Game Cosmetics";
                    lblCosmetiqueSkins.Text = "Skins Cosmetics";
                    lblCommandsAdmins.Text = "Commands/Admin";

                    // checkboxes
                    isEnabled = "Enabled?";

                    // buttons
                    btnActiverTout.Text = "Enable All";
                    btnDesactiverTout.Text = "Disable All";
                    btnExec.Text = "Execute";
                    btnFabric.Text = "Install Fabric";
                    btnModsChangelog.Text = "Update Notes";
                    btnOpenRepos.Text = "GitHub Source";
                    btnOuvrirMods.Text = "Open Mods Folder";
                    btnUpdateMods.Text = "Update/Download Mods";
                    btnReport.Text = "Report a Problem";
                    btnQuitter.Text = "Quit";
                    btnStartMinecraft.Text = "Launch Minecraft";

                    // section
                    gbFastProfiles.Text = "Fast Profiles";
                    gbSystem.Text = "System";

                    // menuItems
                    quitterToolStripMenuItem.Text = "Exit";
                    profilsToolStripMenuItem.Text = "Save Profil";
                    fichierToolStripMenuItem.Text = "File";
                    profilToolStripMenuItem.Text = "Profile";
                    rafraichirProfilsToolStripMenuItem.Text = "Refresh Profiles";
                    réinitialiserProfilsToolStripMenuItem.Text = "Reset Profiles";
                    configToolStripMenuItem.Text = "Settings";
                    aProposToolStripMenuItem.Text = "About";
                    themeToolStripMenuItem.Text = "Theme";
                    contributeursToolStripMenuItem.Text = "Contributors";
                    break;

                // french
                case "FR":
                    //label mods
                    lblCosmetiqueGUI.Text = "Cosmetiques GUI";
                    lblCosmetiquesJeu.Text = "Cosmetiques Jeu";
                    lblCosmetiqueSkins.Text = "Cosmetiques Skins";
                    lblCommandsAdmins.Text = "Commandes/Admin";

                    //checkboxes
                    isEnabled = "Activé ?";

                    //buttons
                    btnActiverTout.Text = "Activer tout";
                    btnDesactiverTout.Text = "Désactiver tout";
                    btnExec.Text = "Executer";
                    btnFabric.Text = "Installer Fabric";
                    btnModsChangelog.Text = "Notes de mises à jours";
                    btnOpenRepos.Text = "Source github";
                    btnOuvrirMods.Text = "Ouvrir Dossier mods";
                    btnUpdateMods.Text = "Mettre à jours/télécharger mods";
                    btnReport.Text = "Signaler un problème";
                    btnQuitter.Text = "Quitter";
                    btnStartMinecraft.Text = "Lancer Minecraft";

                    //section
                    gbFastProfiles.Text = "Profils rapides";
                    gbSystem.Text = "Système";

                    // menuItems
                    quitterToolStripMenuItem.Text = "Quitter";
                    profilsToolStripMenuItem.Text = "Save Profil";
                    fichierToolStripMenuItem.Text = "File";
                    profilToolStripMenuItem.Text = "Profil";
                    rafraichirProfilsToolStripMenuItem.Text = "Rafraichir Profils";
                    réinitialiserProfilsToolStripMenuItem.Text = "Réinitialiser Profils";
                    configToolStripMenuItem.Text = "Paramètres";
                    aProposToolStripMenuItem.Text = "À propos";
                    themeToolStripMenuItem.Text = "thème";
                    contributeursToolStripMenuItem.Text = "Contributeurs";

                    break;

                // espagnol
                case "ES":
                    // etiquetas de mods
                    lblCosmetiqueGUI.Text = "Cosméticos GUI";
                    lblCosmetiquesJeu.Text = "Cosméticos Juego";
                    lblCosmetiqueSkins.Text = "Cosméticos Skins";
                    lblCommandsAdmins.Text = "Comandos/Admin";

                    // checkboxes
                    isEnabled = "¿Activado?";

                    // botones
                    btnActiverTout.Text = "Activar Todo";
                    btnDesactiverTout.Text = "Desactivar Todo";
                    btnExec.Text = "Ejecutar";
                    btnFabric.Text = "Instalar Fabric";
                    btnModsChangelog.Text = "Notas de Actualización";
                    btnOpenRepos.Text = "Fuente GitHub";
                    btnOuvrirMods.Text = "Abrir Carpeta de Mods";
                    btnUpdateMods.Text = "Actualizar/Descargar Mods";
                    btnReport.Text = "Informar un Problema";
                    btnQuitter.Text = "Salir";
                    btnStartMinecraft.Text = "Iniciar Minecraft";

                    // sección
                    gbFastProfiles.Text = "Perfiles Rápidos";
                    gbSystem.Text = "Sistema";

                    // menuItems
                    quitterToolStripMenuItem.Text = "Salir";
                    profilsToolStripMenuItem.Text = "Guardar Perfil";
                    fichierToolStripMenuItem.Text = "Archivo";
                    profilToolStripMenuItem.Text = "Perfil";
                    rafraichirProfilsToolStripMenuItem.Text = "Actualizar Perfiles";
                    réinitialiserProfilsToolStripMenuItem.Text = "Restablecer Perfiles";
                    configToolStripMenuItem.Text = "Configuración";
                    aProposToolStripMenuItem.Text = "Acerca de";
                    themeToolStripMenuItem.Text = "tema";
                    contributeursToolStripMenuItem.Text = "Contribuidores";

                    break;

                // italiano
                case "IT":
                    // etichette mods
                    lblCosmetiqueGUI.Text = "Cosmetici GUI";
                    lblCosmetiquesJeu.Text = "Cosmetici Gioco";
                    lblCosmetiqueSkins.Text = "Cosmetici Skins";
                    lblCommandsAdmins.Text = "Comandi/Admin";

                    // checkboxes
                    isEnabled = "Attivo?";

                    // botones
                    btnActiverTout.Text = "Attiva Tutto";
                    btnDesactiverTout.Text = "Disattiva Tutto";
                    btnExec.Text = "Esegui";
                    btnFabric.Text = "Installa Fabric";
                    btnModsChangelog.Text = "Note di Aggiornamento";
                    btnOpenRepos.Text = "Sorgente GitHub";
                    btnOuvrirMods.Text = "Apri Cartella Mods";
                    btnUpdateMods.Text = "Aggiorna/Scarica Mods";
                    btnReport.Text = "Segnala un Problema";
                    btnQuitter.Text = "Esci";
                    btnStartMinecraft.Text = "Avvia Minecraft";

                    // sezione
                    gbFastProfiles.Text = "Profili Veloci";
                    gbSystem.Text = "Sistema";

                    // menuItems
                    quitterToolStripMenuItem.Text = "Esci";
                    profilsToolStripMenuItem.Text = "Salva Profilo";
                    fichierToolStripMenuItem.Text = "File";
                    profilToolStripMenuItem.Text = "Profilo";
                    rafraichirProfilsToolStripMenuItem.Text = "Aggiorna Profili";
                    réinitialiserProfilsToolStripMenuItem.Text = "Ripristina Profili";
                    configToolStripMenuItem.Text = "Impostazioni";
                    aProposToolStripMenuItem.Text = "Informazioni";
                    themeToolStripMenuItem.Text = "tema";
                    contributeursToolStripMenuItem.Text = "Contributori";

                    break;

                // german
                case "DE":
                    // Mod-Bezeichnungen
                    lblCosmetiqueGUI.Text = "GUI-Kosmetik";
                    lblCosmetiquesJeu.Text = "Spiel-Kosmetik";
                    lblCosmetiqueSkins.Text = "Skins-Kosmetik";
                    lblCommandsAdmins.Text = "Befehle/Admin";

                    // Kontrollkästchen
                    isEnabled = "Aktiviert?";

                    // Schaltflächen
                    btnActiverTout.Text = "Alle aktivieren";
                    btnDesactiverTout.Text = "Alle deaktivieren";
                    btnExec.Text = "Ausführen";
                    btnFabric.Text = "Fabric installieren";
                    btnModsChangelog.Text = "Update-Hinweise";
                    btnOpenRepos.Text = "GitHub-Quelle";
                    btnOuvrirMods.Text = "Mods-Ordner öffnen";
                    btnUpdateMods.Text = "Mods aktualisieren/herunterladen";
                    btnReport.Text = "Problem melden";
                    btnQuitter.Text = "Beenden";
                    btnStartMinecraft.Text = "Minecraft starten";

                    // Abschnitt
                    gbFastProfiles.Text = "Schnellprofile";
                    gbSystem.Text = "System";

                    // menuItems
                    quitterToolStripMenuItem.Text = "Beenden";
                    profilsToolStripMenuItem.Text = "Profil speichern";
                    fichierToolStripMenuItem.Text = "Datei";
                    profilToolStripMenuItem.Text = "Profil";
                    rafraichirProfilsToolStripMenuItem.Text = "Profile aktualisieren";
                    réinitialiserProfilsToolStripMenuItem.Text = "Profile zurücksetzen";
                    configToolStripMenuItem.Text = "Einstellungen";
                    aProposToolStripMenuItem.Text = "Über";
                    themeToolStripMenuItem.Text = "Thema";
                    contributeursToolStripMenuItem.Text = "Mitwirkende";

                    break;

                // portuguese
                case "PT":
                    // etiquetas de mods
                    lblCosmetiqueGUI.Text = "Cosméticos GUI";
                    lblCosmetiquesJeu.Text = "Cosméticos Jogo";
                    lblCosmetiqueSkins.Text = "Cosméticos Skins";
                    lblCommandsAdmins.Text = "Comandos/Admin";

                    // checkboxes
                    isEnabled = "Ativado?";

                    // botones
                    btnActiverTout.Text = "Ativar Tudo";
                    btnDesactiverTout.Text = "Desativar Tudo";
                    btnExec.Text = "Executar";
                    btnFabric.Text = "Instalar Fabric";
                    btnModsChangelog.Text = "Notas de Atualização";
                    btnOpenRepos.Text = "Fonte GitHub";
                    btnOuvrirMods.Text = "Abrir Pasta de Mods";
                    btnUpdateMods.Text = "Atualizar/Download de Mods";
                    btnReport.Text = "Reportar um Problema";
                    btnQuitter.Text = "Sair";
                    btnStartMinecraft.Text = "Iniciar Minecraft";

                    // seção
                    gbFastProfiles.Text = "Perfis Rápidos";
                    gbSystem.Text = "Sistema";

                    // menuItems
                    quitterToolStripMenuItem.Text = "Sair";
                    profilsToolStripMenuItem.Text = "Salvar Perfil";
                    fichierToolStripMenuItem.Text = "Arquivo";
                    profilToolStripMenuItem.Text = "Perfil";
                    rafraichirProfilsToolStripMenuItem.Text = "Atualizar Perfis";
                    réinitialiserProfilsToolStripMenuItem.Text = "Redefinir Perfis";
                    configToolStripMenuItem.Text = "Configurações";
                    aProposToolStripMenuItem.Text = "Sobre";
                    themeToolStripMenuItem.Text = "tema";
                    contributeursToolStripMenuItem.Text = "Contribuidores";

                    break;

                // russian
                case "RU":
                    // étiquettes des mods
                    lblCosmetiqueGUI.Text = "Косметические GUI";
                    lblCosmetiquesJeu.Text = "Косметические игры";
                    lblCosmetiqueSkins.Text = "Косметические скины";
                    lblCommandsAdmins.Text = "Команды/Админ";

                    // cases à cocher
                    isEnabled = "Включено?";

                    // boutons
                    btnActiverTout.Text = "Включить все";
                    btnDesactiverTout.Text = "Выключить все";
                    btnExec.Text = "Выполнить";
                    btnFabric.Text = "Установить Fabric";
                    btnModsChangelog.Text = "Заметки об обновлении";
                    btnOpenRepos.Text = "Исходный код на GitHub";
                    btnOuvrirMods.Text = "Открыть папку модов";
                    btnUpdateMods.Text = "Обновить/Загрузить моды";
                    btnReport.Text = "Сообщить о проблеме";
                    btnQuitter.Text = "Выйти";
                    btnStartMinecraft.Text = "Запустить Minecraft";

                    // section
                    gbFastProfiles.Text = "Быстрые профили";
                    gbSystem.Text = "Система";

                    // menuItems
                    quitterToolStripMenuItem.Text = "Выход";
                    profilsToolStripMenuItem.Text = "Сохранить профиль";
                    fichierToolStripMenuItem.Text = "Файл";
                    profilToolStripMenuItem.Text = "Профиль";
                    rafraichirProfilsToolStripMenuItem.Text = "Обновить профили";
                    réinitialiserProfilsToolStripMenuItem.Text = "Сбросить профили";
                    configToolStripMenuItem.Text = "Настройки";
                    aProposToolStripMenuItem.Text = "О программе";
                    themeToolStripMenuItem.Text = "тема";
                    contributeursToolStripMenuItem.Text = "Участники";

                    break;

                // arab
                case "AR":
                    // étiquettes des mods
                    lblCosmetiqueGUI.Text = "واجهة مستخدم تجميلية";
                    lblCosmetiquesJeu.Text = "ألعاب مستحضرات التجميل";
                    lblCosmetiqueSkins.Text = "تجميلات الشخصيات";
                    lblCommandsAdmins.Text = "الأوامر/المشرفين";

                    // cases à cocher
                    isEnabled = "مُمكّن؟";

                    // boutons
                    btnActiverTout.Text = "تفعيل الكل";
                    btnDesactiverTout.Text = "تعطيل الكل";
                    btnExec.Text = "تنفيذ";
                    btnFabric.Text = "تثبيت Fabric";
                    btnModsChangelog.Text = "ملاحظات التحديثات";
                    btnOpenRepos.Text = "مصدر GitHub";
                    btnOuvrirMods.Text = "فتح مجلد الـMods";
                    btnUpdateMods.Text = "تحديث/تنزيل الـMods";
                    btnReport.Text = "الإبلاغ عن مشكلة";
                    btnQuitter.Text = "الخروج";
                    btnStartMinecraft.Text = "بدء تشغيل ماين كرافت";

                    // section
                    gbFastProfiles.Text = "الملفات الشخصية السريعة";
                    gbSystem.Text = "النظام";

                    // menuItems
                    quitterToolStripMenuItem.Text = "الخروج";
                    profilsToolStripMenuItem.Text = "حفظ الملف الشخصي";
                    fichierToolStripMenuItem.Text = "ملف";
                    profilToolStripMenuItem.Text = "الملف الشخصي";
                    rafraichirProfilsToolStripMenuItem.Text = "تحديث الملفات الشخصية";
                    réinitialiserProfilsToolStripMenuItem.Text = "إعادة تعيين الملفات الشخصية";
                    configToolStripMenuItem.Text = "الإعدادات";
                    aProposToolStripMenuItem.Text = "حول";
                    themeToolStripMenuItem.Text = "السمة";
                    contributeursToolStripMenuItem.Text = "المساهمون";

                    break;

                // Tagalog
                case "TL":
                    // mga label na mod
                    lblCosmetiqueGUI.Text = "GUI ng Kosmetiko";
                    lblCosmetiquesJeu.Text = "Kosmetiko ng Laro";
                    lblCosmetiqueSkins.Text = "Kosmetiko ng Skins";
                    lblCommandsAdmins.Text = "Mga Utos/Admin";

                    // mga checkboxes
                    isEnabled = "Pinagana?";

                    // mga buttons
                    btnActiverTout.Text = "Paganahin lahat";
                    btnDesactiverTout.Text = "Huwag paganahin ang lahat";
                    btnExec.Text = "Isagawa";
                    btnFabric.Text = "Mag-install ng Fabric";
                    btnModsChangelog.Text = "Mga Pabago sa Update";
                    btnOpenRepos.Text = "Pinagmulan sa GitHub";
                    btnOuvrirMods.Text = "Buksan ang Mga Mod Folder";
                    btnUpdateMods.Text = "I-update/I-download ang mga Mod";
                    btnReport.Text = "I-ulat ang Isang Problema";
                    btnQuitter.Text = "Lumabas";
                    btnStartMinecraft.Text = "Simulan ang Minecraft";

                    // seksyon
                    gbFastProfiles.Text = "Mabilis na Mga Profile";
                    gbSystem.Text = "Systema";

                    // menuItems
                    quitterToolStripMenuItem.Text = "Lumabas";
                    profilsToolStripMenuItem.Text = "I-save ang Profil";
                    fichierToolStripMenuItem.Text = "File";
                    profilToolStripMenuItem.Text = "Profile";
                    rafraichirProfilsToolStripMenuItem.Text = "I-refresh ang Mga Profile";
                    réinitialiserProfilsToolStripMenuItem.Text = "I-reset ang Mga Profile";
                    configToolStripMenuItem.Text = "Mga Setting";
                    aProposToolStripMenuItem.Text = "Tungkol Dito";
                    themeToolStripMenuItem.Text = "Tema";
                    contributeursToolStripMenuItem.Text = "Mga Kontribyutor";

                    break;

                // Japanese
                case "JA":
                    // ラベルモッズ
                    lblCosmetiqueGUI.Text = "GUI コスメティック";
                    lblCosmetiquesJeu.Text = "ゲーム コスメティック";
                    lblCosmetiqueSkins.Text = "スキン コスメティック";
                    lblCommandsAdmins.Text = "コマンド/管理者";

                    // チェックボックス
                    isEnabled = "有効ですか？";

                    // ボタン
                    btnActiverTout.Text = "すべてを有効にする";
                    btnDesactiverTout.Text = "すべてを無効にする";
                    btnExec.Text = "実行";
                    btnFabric.Text = "Fabric をインストール";
                    btnModsChangelog.Text = "アップデートノート";
                    btnOpenRepos.Text = "GitHub ソース";
                    btnOuvrirMods.Text = "モッズフォルダを開く";
                    btnUpdateMods.Text = "モッズをアップデート/ダウンロード";
                    btnReport.Text = "問題を報告する";
                    btnQuitter.Text = "終了";
                    btnStartMinecraft.Text = "Minecraftを起動";

                    // セクション
                    gbFastProfiles.Text = "高速プロファイル";
                    gbSystem.Text = "システム";

                    // メニューアイテム
                    quitterToolStripMenuItem.Text = "終了";
                    profilsToolStripMenuItem.Text = "プロフィールを保存";
                    fichierToolStripMenuItem.Text = "ファイル";
                    profilToolStripMenuItem.Text = "プロフィール";
                    rafraichirProfilsToolStripMenuItem.Text = "プロフィールを更新";
                    réinitialiserProfilsToolStripMenuItem.Text = "プロフィールをリセット";
                    configToolStripMenuItem.Text = "設定";
                    aProposToolStripMenuItem.Text = "情報";
                    themeToolStripMenuItem.Text = "テーマ";
                    contributeursToolStripMenuItem.Text = "貢献者";

                    break;

                // Bengali
                case "BN":
                    // লেবেল মডস
                    lblCosmetiqueGUI.Text = "জিউআই কসমেটিক্স";
                    lblCosmetiquesJeu.Text = "গেম কসমেটিক্স";
                    lblCosmetiqueSkins.Text = "স্কিন কসমেটিক্স";
                    lblCommandsAdmins.Text = "কমান্ডগুলি / অ্যাডমিন";

                    // চেকবক্সগুলি
                    isEnabled = "সক্ষম?";

                    // বোতামগুলি
                    btnActiverTout.Text = "সব সক্ষম করুন";
                    btnDesactiverTout.Text = "সব অক্ষম করুন";
                    btnExec.Text = "সাক্ষাত্কার";
                    btnFabric.Text = "ফ্যাব্রিক ইনস্টল করুন";
                    btnModsChangelog.Text = "আপডেট নোটস";
                    btnOpenRepos.Text = "GitHub উত্স";
                    btnOuvrirMods.Text = "মডস ফোল্ডার খুলুন";
                    btnUpdateMods.Text = "মডস আপডেট / ডাউনলোড করুন";
                    btnReport.Text = "একটি সমস্যা রিপোর্ট করুন";
                    btnQuitter.Text = "বাহির";
                    btnStartMinecraft.Text = "মাইনক্রাফট চালান";

                    // সেকশন
                    gbFastProfiles.Text = "দ্রুত প্রোফাইল";
                    gbSystem.Text = "সিস্টেম";

                    // মেনুআইটেমস
                    quitterToolStripMenuItem.Text = "বাহির হোন";
                    profilsToolStripMenuItem.Text = "প্রোফাইল সংরক্ষণ করুন";
                    fichierToolStripMenuItem.Text = "ফাইল";
                    profilToolStripMenuItem.Text = "প্রোফাইল";
                    rafraichirProfilsToolStripMenuItem.Text = "প্রোফাইল রিফ্রেশ করুন";
                    réinitialiserProfilsToolStripMenuItem.Text = "প্রোফাইল রিসেট করুন";
                    configToolStripMenuItem.Text = "সেটিংস";
                    aProposToolStripMenuItem.Text = "সম্পর্কিত";
                    themeToolStripMenuItem.Text = "থিম";
                    contributeursToolStripMenuItem.Text = "অবদানকারীগণ";
                    
                    break;

                // Hindi
                case "HI":
                    // मोड लेबल
                    lblCosmetiqueGUI.Text = "GUI सौंदर्यिक उपयोग";
                    lblCosmetiquesJeu.Text = "गेम सौंदर्यिक उपयोग";
                    lblCosmetiqueSkins.Text = "स्किन सौंदर्यिक उपयोग";
                    lblCommandsAdmins.Text = "कमांड / प्रशासन";

                    // चेकबॉक्स
                    isEnabled = "सक्षम?";

                    // बटन
                    btnActiverTout.Text = "सभी को सक्षम करें";
                    btnDesactiverTout.Text = "सभी को अक्षम करें";
                    btnExec.Text = "क्रियान्वित करें";
                    btnFabric.Text = "फैब्रिक इंस्टॉल करें";
                    btnModsChangelog.Text = "अपडेट नोट्स";
                    btnOpenRepos.Text = "GitHub स्रोत";
                    btnOuvrirMods.Text = "मोड फ़ोल्डर खोलें";
                    btnUpdateMods.Text = "मोड अपडेट / डाउनलोड करें";
                    btnReport.Text = "समस्या की सूचना दें";
                    btnQuitter.Text = "बाहर जाएं";
                    btnStartMinecraft.Text = "माइनक्राफ्ट चलाएं";

                    // सेक्शन
                    gbFastProfiles.Text = "फ़ास्ट प्रोफ़ाइल";
                    gbSystem.Text = "सिस्टम";

                    // मेनूआइटम्स
                    quitterToolStripMenuItem.Text = "बाहर निकलें";
                    profilsToolStripMenuItem.Text = "प्रोफ़ाइल सहेजें";
                    fichierToolStripMenuItem.Text = "फ़ाइल";
                    profilToolStripMenuItem.Text = "प्रोफ़ाइल";
                    rafraichirProfilsToolStripMenuItem.Text = "प्रोफ़ाइल ताज़ा करें";
                    réinitialiserProfilsToolStripMenuItem.Text = "प्रोफ़ाइल रीसेट करें";
                    configToolStripMenuItem.Text = "सेटिंग्स";
                    aProposToolStripMenuItem.Text = "बारे में";
                    themeToolStripMenuItem.Text = "थीम";
                    contributeursToolStripMenuItem.Text = "योगदानकर्ता";

                    break;

                // Korean
                case "KO":
                    // 레이블 모드
                    lblCosmetiqueGUI.Text = "GUI 화장품";
                    lblCosmetiquesJeu.Text = "게임 화장품";
                    lblCosmetiqueSkins.Text = "스킨 화장품";
                    lblCommandsAdmins.Text = "명령어/관리자";

                    // 체크박스
                    isEnabled = "활성화됨?";

                    // 버튼
                    btnActiverTout.Text = "모두 활성화";
                    btnDesactiverTout.Text = "모두 비활성화";
                    btnExec.Text = "실행";
                    btnFabric.Text = "패브릭 설치";
                    btnModsChangelog.Text = "업데이트 노트";
                    btnOpenRepos.Text = "GitHub 소스";
                    btnOuvrirMods.Text = "모드 폴더 열기";
                    btnUpdateMods.Text = "모드 업데이트/다운로드";
                    btnReport.Text = "문제 신고";
                    btnQuitter.Text = "종료";
                    btnStartMinecraft.Text = "마인크래프트 시작";

                    // 섹션
                    gbFastProfiles.Text = "빠른 프로필";
                    gbSystem.Text = "시스템";

                    // 메뉴 아이템
                    quitterToolStripMenuItem.Text = "종료";
                    profilsToolStripMenuItem.Text = "프로필 저장";
                    fichierToolStripMenuItem.Text = "파일";
                    profilToolStripMenuItem.Text = "프로필";
                    rafraichirProfilsToolStripMenuItem.Text = "프로필 새로고침";
                    réinitialiserProfilsToolStripMenuItem.Text = "프로필 재설정";
                    configToolStripMenuItem.Text = "설정";
                    aProposToolStripMenuItem.Text = "소개";
                    themeToolStripMenuItem.Text = "테마";
                    contributeursToolStripMenuItem.Text = "기여자";

                    break;

                // Turkish
                case "TR":
                    // etiket değişiklikleri
                    lblCosmetiqueGUI.Text = "GUI Kozmetik";
                    lblCosmetiquesJeu.Text = "Oyun Kozmetikleri";
                    lblCosmetiqueSkins.Text = "Kil Kozmetikleri";
                    lblCommandsAdmins.Text = "Komutlar/Yönetici";

                    // onay kutuları
                    isEnabled = "Etkin mi?";

                    // düğmeler
                    btnActiverTout.Text = "Tümünü Etkinleştir";
                    btnDesactiverTout.Text = "Tümünü Devre Dışı Bırak";
                    btnExec.Text = "Çalıştır";
                    btnFabric.Text = "Fabric Yükle";
                    btnModsChangelog.Text = "Güncelleme Notları";
                    btnOpenRepos.Text = "GitHub Kaynağı";
                    btnOuvrirMods.Text = "Mod Klasörünü Aç";
                    btnUpdateMods.Text = "Modları Güncelle/İndir";
                    btnReport.Text = "Sorun Bildir";
                    btnQuitter.Text = "Çıkış";
                    btnStartMinecraft.Text = "Minecraft'ı Başlat";

                    // bölümler
                    gbFastProfiles.Text = "Hızlı Profiller";
                    gbSystem.Text = "Sistem";

                    // menü öğeleri
                    quitterToolStripMenuItem.Text = "Çıkış";
                    profilsToolStripMenuItem.Text = "Profil Kaydet";
                    fichierToolStripMenuItem.Text = "Dosya";
                    profilToolStripMenuItem.Text = "Profil";
                    rafraichirProfilsToolStripMenuItem.Text = "Profilleri Yenile";
                    réinitialiserProfilsToolStripMenuItem.Text = "Profilleri Sıfırla";
                    configToolStripMenuItem.Text = "Ayarlar";
                    aProposToolStripMenuItem.Text = "Hakkında";
                    themeToolStripMenuItem.Text = "Tema";
                    contributeursToolStripMenuItem.Text = "Katkıda Bulunanlar";

                    break;

                // Thai
                case "TH":
                    // แก้ไขตัวแปร
                    lblCosmetiqueGUI.Text = "GUI เครื่องสำอาง";
                    lblCosmetiquesJeu.Text = "เครื่องสำอางในเกม";
                    lblCosmetiqueSkins.Text = "เครื่องสำอางสกิน";
                    lblCommandsAdmins.Text = "คำสั่ง/ผู้ดูแล";

                    // เช็คบ็อกซ์
                    isEnabled = "เปิดใช้งาน?";

                    // ปุ่ม
                    btnActiverTout.Text = "เปิดใช้งานทั้งหมด";
                    btnDesactiverTout.Text = "ปิดใช้งานทั้งหมด";
                    btnExec.Text = "ดำเนินการ";
                    btnFabric.Text = "ติดตั้ง Fabric";
                    btnModsChangelog.Text = "บันทึกการอัพเดต";
                    btnOpenRepos.Text = "ที่มา GitHub";
                    btnOuvrirMods.Text = "เปิดโฟลเดอร์ Mods";
                    btnUpdateMods.Text = "อัพเดต/ดาวน์โหลด Mods";
                    btnReport.Text = "รายงานปัญหา";
                    btnQuitter.Text = "ออก";
                    btnStartMinecraft.Text = "เริ่ม Minecraft";

                    // ส่วน
                    gbFastProfiles.Text = "โปรไฟล์ด่วน";
                    gbSystem.Text = "ระบบ";

                    // เมนูไอเท็ม
                    quitterToolStripMenuItem.Text = "ออก";
                    profilsToolStripMenuItem.Text = "บันทึกโปรไฟล์";
                    fichierToolStripMenuItem.Text = "ไฟล์";
                    profilToolStripMenuItem.Text = "โปรไฟล์";
                    rafraichirProfilsToolStripMenuItem.Text = "รีเฟรชโปรไฟล์";
                    réinitialiserProfilsToolStripMenuItem.Text = "รีเซ็ตโปรไฟล์";
                    configToolStripMenuItem.Text = "การตั้งค่า";
                    aProposToolStripMenuItem.Text = "เกี่ยวกับ";
                    themeToolStripMenuItem.Text = "ธีม";
                    contributeursToolStripMenuItem.Text = "ผู้มีส่วนร่วม";

                    break;

                // Indonesian
                case "ID":
                    // modifikasi label
                    lblCosmetiqueGUI.Text = "GUI Kosmetik";
                    lblCosmetiquesJeu.Text = "Kosmetik Game";
                    lblCosmetiqueSkins.Text = "Kosmetik Skins";
                    lblCommandsAdmins.Text = "Perintah/Admin";

                    // kotak centang
                    isEnabled = "Diaktifkan?";

                    // tombol
                    btnActiverTout.Text = "Aktifkan Semua";
                    btnDesactiverTout.Text = "Nonaktifkan Semua";
                    btnExec.Text = "Eksekusi";
                    btnFabric.Text = "Pasang Fabric";
                    btnModsChangelog.Text = "Catatan Pembaruan";
                    btnOpenRepos.Text = "Sumber GitHub";
                    btnOuvrirMods.Text = "Buka Folder Mods";
                    btnUpdateMods.Text = "Perbarui/Unduh Mods";
                    btnReport.Text = " Laporkan Masalah";
                    btnQuitter.Text = "Keluar";
                    btnStartMinecraft.Text = "Mulai Minecraft";

                    // bagian
                    gbFastProfiles.Text = "Profil Cepat";
                    gbSystem.Text = "Sistem";

                    // item menu
                    quitterToolStripMenuItem.Text = "Keluar";
                    profilsToolStripMenuItem.Text = "Simpan Profil";
                    fichierToolStripMenuItem.Text = "Berkas";
                    profilToolStripMenuItem.Text = "Profil";
                    rafraichirProfilsToolStripMenuItem.Text = "Segarkan Profil";
                    réinitialiserProfilsToolStripMenuItem.Text = "Atur Ulang Profil";
                    configToolStripMenuItem.Text = "Pengaturan";
                    aProposToolStripMenuItem.Text = "Tentang";
                    themeToolStripMenuItem.Text = "Tema";
                    contributeursToolStripMenuItem.Text = "Kontributor";

                    break;


            }

            foreach (CheckBox cb in GetCheckboxsDictionary().Values)
            {
                cb.Text = isEnabled;
            }
        }

        public string translatedText(string textCode)
        {
            string resultat = "MISSING TRANSLATION";
            switch (getLanguageCode())
            {
                // french
                case "FR":
                    switch (textCode)
                    {
                        case "ERR_NOMODS": resultat = "Une erreur s'est produite :( attention ce logiciel doit être placé dans le dossier mods de votre .minecraft !\nVerifiez aussi que vous avez bien téléchargé les mods !"; break;
                        case "ERR_TIMEOUT": resultat = "Timeout - Verifiez votre connexion."; break;
                        case "ERR_NOTYETIMPLEMENTED": resultat = "Cette feature n'est pas encore implémentée"; break;
                        case "ERR_EXEC-BATCH": resultat = "Une erreur s'est produite lors de l'exécution du fichier batch : "; break;
                        case "ERR_NOMINECRAFEXE": resultat = "Il semblerait qu'il soit pas possible de lancer minecraft"; break;

                        case "WARN_WRONGFOLDER": resultat = "Mauvais dossier : "; break;
                        case "WARN_MUSTSTARTINMODSFOLDER": resultat = "Le logiciel doit être exécuté depuis le dossier 'mods' de votre .minecraft."; break;

                        case "MOUSEHOVER_BTNQUITTER": resultat = "Regarde ce que ça fait..."; break;
                        case "MOUSEHOVER_BTNREPORT": resultat = "Ouvrir le rapport de problème."; break;
                        case "MOUSEHOVER_BTNCHANGELOG": resultat = "Ouvre l'URL du changelog, résumé des changement par mises à jour."; break;
                        case "MOUSEHOVER_BTNSOURCECODE": resultat = "Ouvre le repository GitHub."; break;
                        case "MOUSEHOVER_BTNFABRIC": resultat = "Ouvre l'URL pour télécharger Fabric."; break;
                        case "MOUSEHOVER_BTNUPDATE": resultat = "Télécharger / Mettre à jour les mods"; break;

                        case "INFO_SUCCESS": resultat = "Tout s'est déroulé avec succés !"; break;
                        case "INFO_ABOUT": resultat = $"My Survival Mods\nby MythMega\n2023\nVersion :{version}\nBuild : {app_build}"; break;
                        case "INFO_MYTHMEGA": resultat = "Je suis MythMega (j'ai pas encore crée cette section, c'est moins urgent)."; break;
                        case "INFO_CONTRIBUTORS": resultat = $"Les contributeurs sont les suivants :\n{getContributorsNamesAndLbl()}\nMerci à eux !"; break;

                        case "CONTRIBUTORS": resultat = "Contributeurs"; break;
                        case "CONTRIB_TRANSLATOR": resultat = "Traduction"; break;
                        case "CONTRIB_DEV": resultat = "Developpement"; break;
                        case "CONTRIB_ARTIST": resultat = "Artiste/Design"; break;
                        case "CONTRIB_TESTER": resultat = "Testeur"; break;
                        case "CONTRIB_OTHER": resultat = "Autre"; break;

                        case "LBL_WARNING": resultat = "Attention !"; break;
                        case "LBL_CONFIRMATION": resultat = "ëtes vous sur de vouloir faire cette action ?"; break;
                        case "LBL_TOUT_SELECTIONNER": resultat = "Tout déselctionner"; break;
                        case "LBL_TOUT_DESELECTIONNER": resultat = "Tout selctionner"; break;
                        case "LBL_STOP": resultat = "Arrêter"; break;
                        case "LBL_OUI": resultat = "Oui"; break;
                        case "LBL_NON": resultat = "Non"; break;
                        case "LBL_ANNULER": resultat = "Annuler"; break;
                        case "LBL_LOCALISER": resultat = "Localiser"; break;

                        case "": resultat = ""; break;
                    }
                    break;

                // English
                case "EN":
                    switch (textCode)
                    {
                        case "ERR_NOMODS": resultat = "An error occurred :( Please ensure this software is placed in the 'mods' folder of your .minecraft!\nAlso, make sure you have downloaded the mods!"; break;
                        case "ERR_TIMEOUT": resultat = "Timeout - Check your connection."; break;
                        case "ERR_NOTYETIMPLEMENTED": resultat = "This feature is not yet implemented."; break;
                        case "ERR_EXEC-BATCH": resultat = "An error occurred while executing the batch file: "; break;
                        case "ERR_NOMINECRAFEXE": resultat = "It seems that launching Minecraft is not possible."; break;

                        case "WARN_WRONGFOLDER": resultat = "Wrong folder: "; break;
                        case "WARN_MUSTSTARTINMODSFOLDER": resultat = "The software must be executed from the 'mods' folder of your .minecraft."; break;

                        case "MOUSEHOVER_BTNQUITTER": resultat = "See what it does..."; break;
                        case "MOUSEHOVER_BTNREPORT": resultat = "Open problem report."; break;
                        case "MOUSEHOVER_BTNCHANGELOG": resultat = "Open the changelog URL, summary of changes per updates."; break;
                        case "MOUSEHOVER_BTNSOURCECODE": resultat = "Open the GitHub repository."; break;
                        case "MOUSEHOVER_BTNFABRIC": resultat = "Open the URL to download Fabric."; break;
                        case "MOUSEHOVER_BTNUPDATE": resultat = "Download / Update mods"; break;

                        case "INFO_SUCCESS": resultat = "Everything went successfully!"; break;
                        case "INFO_ABOUT": resultat = $"My Survival Mods\nby MythMega\n2023\nVersion: {version}\nBuild: {app_build}"; break;
                        case "INFO_MYTHMEGA": resultat = "I am MythMega (I haven't created this section yet, it's less urgent)."; break;
                        case "INFO_CONTRIBUTORS": resultat = $"The contributors are as follows:\n{getContributorsNamesAndLbl()}\nThanks to them!"; break;

                        case "CONTRIBUTORS": resultat = "Contributors"; break;
                        case "CONTRIB_TRANSLATOR": resultat = "Translation"; break;
                        case "CONTRIB_DEV": resultat = "Development"; break;
                        case "CONTRIB_ARTIST": resultat = "Artist/Design"; break;
                        case "CONTRIB_TESTER": resultat = "Tester"; break;
                        case "CONTRIB_OTHER": resultat = "Other"; break;

                        case "LBL_WARNING": resultat = "Warning!"; break;
                        case "LBL_CONFIRMATION": resultat = "Are you sure you want to perform this action?"; break;
                        case "LBL_TOUT_SELECTIONNER": resultat = "Deselect All"; break;
                        case "LBL_TOUT_DESELECTIONNER": resultat = "Select All"; break;
                        case "LBL_STOP": resultat = "Stop"; break;
                        case "LBL_OUI": resultat = "Yes"; break;
                        case "LBL_NON": resultat = "No"; break;
                        case "LBL_ANNULER": resultat = "Cancel"; break;
                        case "LBL_LOCALISER": resultat = "Locate"; break;

                        case "": resultat = ""; break;
                    }
                    break;

                // Spanish
                case "ES":
                    switch (textCode)
                    {
                        case "ERR_NOMODS": resultat = "¡Se ha producido un error! :( Asegúrate de que este software esté ubicado en la carpeta 'mods' de tu .minecraft.\n¡También verifica que hayas descargado los mods!"; break;
                        case "ERR_TIMEOUT": resultat = "Tiempo agotado - Verifica tu conexión."; break;
                        case "ERR_NOTYETIMPLEMENTED": resultat = "Esta característica aún no está implementada."; break;
                        case "ERR_EXEC-BATCH": resultat = "Se ha producido un error al ejecutar el archivo por lotes: "; break;
                        case "ERR_NOMINECRAFEXE": resultat = "Parece que no es posible ejecutar Minecraft."; break;

                        case "WARN_WRONGFOLDER": resultat = "Carpeta incorrecta: "; break;
                        case "WARN_MUSTSTARTINMODSFOLDER": resultat = "El software debe ejecutarse desde la carpeta 'mods' de tu .minecraft."; break;

                        case "MOUSEHOVER_BTNQUITTER": resultat = "Observa qué hace..."; break;
                        case "MOUSEHOVER_BTNREPORT": resultat = "Abrir el informe del problema."; break;
                        case "MOUSEHOVER_BTNCHANGELOG": resultat = "Abrir la URL del registro de cambios, resumen de los cambios por actualizaciones."; break;
                        case "MOUSEHOVER_BTNSOURCECODE": resultat = "Abrir el repositorio de GitHub."; break;
                        case "MOUSEHOVER_BTNFABRIC": resultat = "Abrir la URL para descargar Fabric."; break;
                        case "MOUSEHOVER_BTNUPDATE": resultat = "Descargar / Actualizar mods"; break;

                        case "INFO_SUCCESS": resultat = "¡Todo se realizó con éxito!"; break;
                        case "INFO_ABOUT": resultat = $"Mis Mods de Supervivencia\npor MythMega\n2023\nVersión: {version}\nCompilación: {app_build}"; break;
                        case "INFO_MYTHMEGA": resultat = "Soy MythMega (todavía no he creado esta sección, es menos urgente)."; break;
                        case "INFO_CONTRIBUTORS": resultat = $"Los colaboradores son los siguientes:\n{getContributorsNamesAndLbl()}\n¡Gracias a ellos!"; break;

                        case "CONTRIBUTORS": resultat = "Colaboradores"; break;
                        case "CONTRIB_TRANSLATOR": resultat = "Traducción"; break;
                        case "CONTRIB_DEV": resultat = "Desarrollo"; break;
                        case "CONTRIB_ARTIST": resultat = "Artista/Diseño"; break;
                        case "CONTRIB_TESTER": resultat = "Probador"; break;
                        case "CONTRIB_OTHER": resultat = "Otro"; break;

                        case "LBL_WARNING": resultat = "¡Advertencia!"; break;
                        case "LBL_CONFIRMATION": resultat = "¿Estás seguro de que quieres realizar esta acción?"; break;
                        case "LBL_TOUT_SELECTIONNER": resultat = "Deseleccionar Todo"; break;
                        case "LBL_TOUT_DESELECTIONNER": resultat = "Seleccionar Todo"; break;
                        case "LBL_STOP": resultat = "Detener"; break;
                        case "LBL_OUI": resultat = "Sí"; break;
                        case "LBL_NON": resultat = "No"; break;
                        case "LBL_ANNULER": resultat = "Cancelar"; break;
                        case "LBL_LOCALISER": resultat = "Localizar"; break;

                        case "": resultat = ""; break;
                    }
                    break;

                // German
                case "DE":
                    switch (textCode)
                    {
                        case "ERR_NOMODS": resultat = "Ein Fehler ist aufgetreten :( Stelle sicher, dass diese Software im 'mods'-Ordner deines .minecraft-Verzeichnisses platziert ist!\nStelle außerdem sicher, dass du die Mods heruntergeladen hast!"; break;
                        case "ERR_TIMEOUT": resultat = "Zeitüberschreitung - Überprüfe deine Verbindung."; break;
                        case "ERR_NOTYETIMPLEMENTED": resultat = "Diese Funktion ist noch nicht implementiert."; break;
                        case "ERR_EXEC-BATCH": resultat = "Ein Fehler ist während der Ausführung der Stapeldatei aufgetreten: "; break;
                        case "ERR_NOMINECRAFEXE": resultat = "Es scheint, dass das Starten von Minecraft nicht möglich ist."; break;

                        case "WARN_WRONGFOLDER": resultat = "Falscher Ordner: "; break;
                        case "WARN_MUSTSTARTINMODSFOLDER": resultat = "Die Software muss aus dem 'mods'-Ordner deines .minecraft-Verzeichnisses ausgeführt werden."; break;

                        case "MOUSEHOVER_BTNQUITTER": resultat = "Schau, was passiert..."; break;
                        case "MOUSEHOVER_BTNREPORT": resultat = "Problembericht öffnen."; break;
                        case "MOUSEHOVER_BTNCHANGELOG": resultat = "Öffne die URL des Changelogs, Zusammenfassung der Änderungen pro Update."; break;
                        case "MOUSEHOVER_BTNSOURCECODE": resultat = "GitHub-Repository öffnen."; break;
                        case "MOUSEHOVER_BTNFABRIC": resultat = "Öffne die URL zum Herunterladen von Fabric."; break;
                        case "MOUSEHOVER_BTNUPDATE": resultat = "Mods herunterladen / aktualisieren"; break;

                        case "INFO_SUCCESS": resultat = "Alles lief erfolgreich ab!"; break;
                        case "INFO_ABOUT": resultat = $"Meine Survival Mods\nvon MythMega\n2023\nVersion: {version}\nBuild: {app_build}"; break;
                        case "INFO_MYTHMEGA": resultat = "Ich bin MythMega (ich habe diesen Abschnitt noch nicht erstellt, es ist weniger dringend)."; break;
                        case "INFO_CONTRIBUTORS": resultat = $"Die Beitragenden sind wie folgt:\n{getContributorsNamesAndLbl()}\nDanke an sie!"; break;

                        case "CONTRIBUTORS": resultat = "Beitragende"; break;
                        case "CONTRIB_TRANSLATOR": resultat = "Übersetzung"; break;
                        case "CONTRIB_DEV": resultat = "Entwicklung"; break;
                        case "CONTRIB_ARTIST": resultat = "Künstler/Design"; break;
                        case "CONTRIB_TESTER": resultat = "Tester"; break;
                        case "CONTRIB_OTHER": resultat = "Andere"; break;

                        case "LBL_WARNING": resultat = "Warnung!"; break;
                        case "LBL_CONFIRMATION": resultat = "Bist du sicher, dass du diese Aktion durchführen möchtest?"; break;
                        case "LBL_TOUT_SELECTIONNER": resultat = "Alles abwählen"; break;
                        case "LBL_TOUT_DESELECTIONNER": resultat = "Alles auswählen"; break;
                        case "LBL_STOP": resultat = "Stoppen"; break;
                        case "LBL_OUI": resultat = "Ja"; break;
                        case "LBL_NON": resultat = "Nein"; break;
                        case "LBL_ANNULER": resultat = "Abbrechen"; break;
                        case "LBL_LOCALISER": resultat = "Lokalisieren"; break;

                        case "": resultat = ""; break;
                    }
                    break;

                // Italian
                case "IT":
                    switch (textCode)
                    {
                        case "ERR_NOMODS": resultat = "Si è verificato un errore :( Assicurati che questo software sia posizionato nella cartella 'mods' del tuo .minecraft!\nVerifica anche di aver scaricato le mod!"; break;
                        case "ERR_TIMEOUT": resultat = "Timeout - Verifica la tua connessione."; break;
                        case "ERR_NOTYETIMPLEMENTED": resultat = "Questa funzionalità non è ancora implementata."; break;
                        case "ERR_EXEC-BATCH": resultat = "Si è verificato un errore durante l'esecuzione del file batch: "; break;
                        case "ERR_NOMINECRAFEXE": resultat = "Sembra che non sia possibile avviare Minecraft."; break;

                        case "WARN_WRONGFOLDER": resultat = "Cartella errata: "; break;
                        case "WARN_MUSTSTARTINMODSFOLDER": resultat = "Il software deve essere eseguito dalla cartella 'mods' del tuo .minecraft."; break;

                        case "MOUSEHOVER_BTNQUITTER": resultat = "Guarda cosa fa..."; break;
                        case "MOUSEHOVER_BTNREPORT": resultat = "Apri il rapporto del problema."; break;
                        case "MOUSEHOVER_BTNCHANGELOG": resultat = "Apri l'URL del registro modifiche, riepilogo delle modifiche per aggiornamento."; break;
                        case "MOUSEHOVER_BTNSOURCECODE": resultat = "Apri il repository GitHub."; break;
                        case "MOUSEHOVER_BTNFABRIC": resultat = "Apri l'URL per scaricare Fabric."; break;
                        case "MOUSEHOVER_BTNUPDATE": resultat = "Scarica / Aggiorna le mod"; break;

                        case "INFO_SUCCESS": resultat = "Tutto è andato bene con successo!"; break;
                        case "INFO_ABOUT": resultat = $"Le Mie Mod per la Sopravvivenza\ndi MythMega\n2023\nVersione: {version}\nBuild: {app_build}"; break;
                        case "INFO_MYTHMEGA": resultat = "Sono MythMega (non ho ancora creato questa sezione, è meno urgente)."; break;
                        case "INFO_CONTRIBUTORS": resultat = $"I collaboratori sono i seguenti:\n{getContributorsNamesAndLbl()}\nGrazie a loro!"; break;

                        case "CONTRIBUTORS": resultat = "Collaboratori"; break;
                        case "CONTRIB_TRANSLATOR": resultat = "Traduzione"; break;
                        case "CONTRIB_DEV": resultat = "Sviluppo"; break;
                        case "CONTRIB_ARTIST": resultat = "Artista/Design"; break;
                        case "CONTRIB_TESTER": resultat = "Tester"; break;
                        case "CONTRIB_OTHER": resultat = "Altro"; break;

                        case "LBL_WARNING": resultat = "Attenzione!"; break;
                        case "LBL_CONFIRMATION": resultat = "Sei sicuro di voler eseguire questa azione?"; break;
                        case "LBL_TOUT_SELECTIONNER": resultat = "Deseleziona tutto"; break;
                        case "LBL_TOUT_DESELECTIONNER": resultat = "Seleziona tutto"; break;
                        case "LBL_STOP": resultat = "Ferma"; break;
                        case "LBL_OUI": resultat = "Sì"; break;
                        case "LBL_NON": resultat = "No"; break;
                        case "LBL_ANNULER": resultat = "Annulla"; break;
                        case "LBL_LOCALISER": resultat = "Trova"; break;

                        case "": resultat = ""; break;
                    }
                    break;

                // Portuguese
                case "PT":
                    switch (textCode)
                    {
                        case "ERR_NOMODS": resultat = "Ocorreu um erro :( Certifique-se de que este software está na pasta 'mods' do seu .minecraft!\nCertifique-se também de que baixou as mods!"; break;
                        case "ERR_TIMEOUT": resultat = "Timeout - Verifique a sua conexão."; break;
                        case "ERR_NOTYETIMPLEMENTED": resultat = "Esta funcionalidade ainda não foi implementada."; break;
                        case "ERR_EXEC-BATCH": resultat = "Ocorreu um erro durante a execução do arquivo em lote: "; break;
                        case "ERR_NOMINECRAFEXE": resultat = "Parece que não é possível iniciar o Minecraft."; break;

                        case "WARN_WRONGFOLDER": resultat = "Pasta incorreta: "; break;
                        case "WARN_MUSTSTARTINMODSFOLDER": resultat = "O software deve ser executado a partir da pasta 'mods' do seu .minecraft."; break;

                        case "MOUSEHOVER_BTNQUITTER": resultat = "Veja o que faz..."; break;
                        case "MOUSEHOVER_BTNREPORT": resultat = "Abrir relatório de problemas."; break;
                        case "MOUSEHOVER_BTNCHANGELOG": resultat = "Abrir URL do changelog, resumo das mudanças por atualização."; break;
                        case "MOUSEHOVER_BTNSOURCECODE": resultat = "Abrir repositório GitHub."; break;
                        case "MOUSEHOVER_BTNFABRIC": resultat = "Abrir URL para baixar o Fabric."; break;
                        case "MOUSEHOVER_BTNUPDATE": resultat = "Baixar / Atualizar mods"; break;

                        case "INFO_SUCCESS": resultat = "Tudo correu com sucesso!"; break;
                        case "INFO_ABOUT": resultat = $"As Minhas Mods de Sobrevivência\npor MythMega\n2023\nVersão: {version}\nCompilação: {app_build}"; break;
                        case "INFO_MYTHMEGA": resultat = "Eu sou o MythMega (ainda não criei esta seção, é menos urgente)."; break;
                        case "INFO_CONTRIBUTORS": resultat = $"Os contribuidores são os seguintes:\n{getContributorsNamesAndLbl()}\nAgradecemos a eles!"; break;

                        case "CONTRIBUTORS": resultat = "Contribuidores"; break;
                        case "CONTRIB_TRANSLATOR": resultat = "Tradução"; break;
                        case "CONTRIB_DEV": resultat = "Desenvolvimento"; break;
                        case "CONTRIB_ARTIST": resultat = "Artista/Design"; break;
                        case "CONTRIB_TESTER": resultat = "Testador"; break;
                        case "CONTRIB_OTHER": resultat = "Outro"; break;

                        case "LBL_WARNING": resultat = "Aviso!"; break;
                        case "LBL_CONFIRMATION": resultat = "Tem certeza de que deseja realizar esta ação?"; break;
                        case "LBL_TOUT_SELECTIONNER": resultat = "Desselecionar Tudo"; break;
                        case "LBL_TOUT_DESELECTIONNER": resultat = "Selecionar Tudo"; break;
                        case "LBL_STOP": resultat = "Parar"; break;
                        case "LBL_OUI": resultat = "Sim"; break;
                        case "LBL_NON": resultat = "Não"; break;
                        case "LBL_ANNULER": resultat = "Cancelar"; break;
                        case "LBL_LOCALISER": resultat = "Localizar"; break;

                        case "": resultat = ""; break;
                    }
                    break;

                // Arabic
                case "AR":
                    switch (textCode)
                    {
                        case "ERR_NOMODS": resultat = "حدث خطأ :( يُرجى التأكد من وضع هذا البرنامج في مجلد 'mods' في ملف .minecraft الخاص بك!\nتأكد أيضًا من تنزيل المودات!"; break;
                        case "ERR_TIMEOUT": resultat = "مهلة زمنية - تحقق من اتصالك."; break;
                        case "ERR_NOTYETIMPLEMENTED": resultat = "هذه الميزة لم تُنفذ بعد."; break;
                        case "ERR_EXEC-BATCH": resultat = "حدث خطأ أثناء تنفيذ ملف الدفع: "; break;
                        case "ERR_NOMINECRAFEXE": resultat = "يبدو أنه من غير الممكن تشغيل ماين كرافت."; break;

                        case "WARN_WRONGFOLDER": resultat = "مجلد غير صحيح: "; break;
                        case "WARN_MUSTSTARTINMODSFOLDER": resultat = "يجب تشغيل البرنامج من مجلد 'mods' في ملف .minecraft الخاص بك."; break;

                        case "MOUSEHOVER_BTNQUITTER": resultat = "انظر ماذا يحدث..."; break;
                        case "MOUSEHOVER_BTNREPORT": resultat = "فتح تقرير المشكلة."; break;
                        case "MOUSEHOVER_BTNCHANGELOG": resultat = "فتح عنوان URL لسجل التغييرات، ملخص للتغييرات حسب التحديثات."; break;
                        case "MOUSEHOVER_BTNSOURCECODE": resultat = "فتح مستودع GitHub."; break;
                        case "MOUSEHOVER_BTNFABRIC": resultat = "فتح العنوان URL لتنزيل Fabric."; break;
                        case "MOUSEHOVER_BTNUPDATE": resultat = "تنزيل / تحديث المودات"; break;

                        case "INFO_SUCCESS": resultat = "كل شيء تم بنجاح!"; break;
                        case "INFO_ABOUT": resultat = $"مودات البقاء الخاصة بي\nبواسطة MythMega\n2023\nالإصدار: {version}\nالبناء: {app_build}"; break;
                        case "INFO_MYTHMEGA": resultat = "أنا MythMega (لم أقم بإنشاء هذا القسم بعد، أمر أقل ضرورة)."; break;
                        case "INFO_CONTRIBUTORS": resultat = $"المساهمون هم على النحو التالي:\n{getContributorsNamesAndLbl()}\nشكراً لهم!"; break;

                        case "CONTRIBUTORS": resultat = "المساهمون"; break;
                        case "CONTRIB_TRANSLATOR": resultat = "الترجمة"; break;
                        case "CONTRIB_DEV": resultat = "التطوير"; break;
                        case "CONTRIB_ARTIST": resultat = "الفنان/التصميم"; break;
                        case "CONTRIB_TESTER": resultat = "المُختبر"; break;
                        case "CONTRIB_OTHER": resultat = "آخر"; break;

                        case "LBL_WARNING": resultat = "تحذير!"; break;
                        case "LBL_CONFIRMATION": resultat = "هل أنت متأكد من رغبتك في القيام بهذا الإجراء؟"; break;
                        case "LBL_TOUT_SELECTIONNER": resultat = "إلغاء تحديد الكل"; break;
                        case "LBL_TOUT_DESELECTIONNER": resultat = "تحديد الكل"; break;
                        case "LBL_STOP": resultat = "إيقاف"; break;
                        case "LBL_OUI": resultat = "نعم"; break;
                        case "LBL_NON": resultat = "لا"; break;
                        case "LBL_ANNULER": resultat = "إلغاء"; break;
                        case "LBL_LOCALISER": resultat = "تحديد الموقع"; break;

                        case "": resultat = ""; break;
                    }
                    break;

                // Russian
                case "RU":
                    switch (textCode)
                    {
                        case "ERR_NOMODS": resultat = "Произошла ошибка :( Пожалуйста, убедитесь, что это программное обеспечение находится в папке 'mods' вашей .minecraft!\nТакже убедитесь, что вы загрузили моды!"; break;
                        case "ERR_TIMEOUT": resultat = "Тайм-аут - Проверьте ваше соединение."; break;
                        case "ERR_NOTYETIMPLEMENTED": resultat = "Эта функция еще не реализована."; break;
                        case "ERR_EXEC-BATCH": resultat = "Произошла ошибка при выполнении пакетного файла: "; break;
                        case "ERR_NOMINECRAFEXE": resultat = "Похоже, что запуск Minecraft невозможен."; break;

                        case "WARN_WRONGFOLDER": resultat = "Неправильная папка: "; break;
                        case "WARN_MUSTSTARTINMODSFOLDER": resultat = "Программу следует запускать из папки 'mods' вашей .minecraft."; break;

                        case "MOUSEHOVER_BTNQUITTER": resultat = "Посмотрите, что это делает..."; break;
                        case "MOUSEHOVER_BTNREPORT": resultat = "Открыть отчет о проблеме."; break;
                        case "MOUSEHOVER_BTNCHANGELOG": resultat = "Открыть URL-адрес изменений, сводка изменений по обновлениям."; break;
                        case "MOUSEHOVER_BTNSOURCECODE": resultat = "Открыть репозиторий GitHub."; break;
                        case "MOUSEHOVER_BTNFABRIC": resultat = "Открыть URL-адрес для загрузки Fabric."; break;
                        case "MOUSEHOVER_BTNUPDATE": resultat = "Скачать / Обновить моды"; break;

                        case "INFO_SUCCESS": resultat = "Все прошло успешно!"; break;
                        case "INFO_ABOUT": resultat = $"Мои Моды Выживания\nот MythMega\n2023\nВерсия: {version}\nСборка: {app_build}"; break;
                        case "INFO_MYTHMEGA": resultat = "Я MythMega (я еще не создал этот раздел, это менее срочно)."; break;
                        case "INFO_CONTRIBUTORS": resultat = $"Следующие люди внесли вклад:\n{getContributorsNamesAndLbl()}\nСпасибо им!"; break;

                        case "CONTRIBUTORS": resultat = "Участники"; break;
                        case "CONTRIB_TRANSLATOR": resultat = "Переводчик"; break;
                        case "CONTRIB_DEV": resultat = "Разработка"; break;
                        case "CONTRIB_ARTIST": resultat = "Художник/Дизайнер"; break;
                        case "CONTRIB_TESTER": resultat = "Тестировщик"; break;
                        case "CONTRIB_OTHER": resultat = "Другое"; break;

                        case "LBL_WARNING": resultat = "Внимание!"; break;
                        case "LBL_CONFIRMATION": resultat = "Вы уверены, что хотите выполнить это действие?"; break;
                        case "LBL_TOUT_SELECTIONNER": resultat = "Снять все выделение"; break;
                        case "LBL_TOUT_DESELECTIONNER": resultat = "Выделить все"; break;
                        case "LBL_STOP": resultat = "Остановить"; break;
                        case "LBL_OUI": resultat = "Да"; break;
                        case "LBL_NON": resultat = "Нет"; break;
                        case "LBL_ANNULER": resultat = "Отмена"; break;
                        case "LBL_LOCALISER": resultat = "Найти"; break;

                        case "": resultat = ""; break;
                    }
                    break;

                // Tagalog
                case "TL":
                    switch (textCode)
                    {
                        case "ERR_NOMODS": resultat = "May naganap na error :( Tiyaking nasa 'mods' na folder ng iyong .minecraft ang software na ito!\nSiguruhin din na na-download mo ang mga mods!"; break;
                        case "ERR_TIMEOUT": resultat = "Timeout - Suriin ang iyong koneksyon."; break;
                        case "ERR_NOTYETIMPLEMENTED": resultat = "Ang feature na ito ay hindi pa naipapatupad."; break;
                        case "ERR_EXEC-BATCH": resultat = "May naganap na error habang inii-execute ang batch file: "; break;
                        case "ERR_NOMINECRAFEXE": resultat = "Mukhang hindi maaring buksan ang Minecraft."; break;

                        case "WARN_WRONGFOLDER": resultat = "Maling folder: "; break;
                        case "WARN_MUSTSTARTINMODSFOLDER": resultat = "Ang software ay dapat na ma-eexecute mula sa 'mods' na folder ng iyong .minecraft."; break;

                        case "MOUSEHOVER_BTNQUITTER": resultat = "Tingnan kung ano ang nagaganap..."; break;
                        case "MOUSEHOVER_BTNREPORT": resultat = "Buksan ang ulat ng problema."; break;
                        case "MOUSEHOVER_BTNCHANGELOG": resultat = "Buksan ang changelog URL, buod ng mga pagbabago sa bawat update."; break;
                        case "MOUSEHOVER_BTNSOURCECODE": resultat = "Buksan ang GitHub repository."; break;
                        case "MOUSEHOVER_BTNFABRIC": resultat = "Buksan ang URL para sa pag-download ng Fabric."; break;
                        case "MOUSEHOVER_BTNUPDATE": resultat = "I-download / I-update ang mga mods"; break;

                        case "INFO_SUCCESS": resultat = "Lahat ay naging matagumpay!"; break;
                        case "INFO_ABOUT": resultat = $"Aking Mga Mods sa Survival\nni MythMega\n2023\nBersyon: {version}\nBuild: {app_build}"; break;
                        case "INFO_MYTHMEGA": resultat = "Ako si MythMega (Hindi ko pa nabuo ang seksyon na ito, hindi pa ito urgent)."; break;
                        case "INFO_CONTRIBUTORS": resultat = $"Ang mga kontribyutor ay sumusunod:\n{getContributorsNamesAndLbl()}\nSalamat sa kanila!"; break;

                        case "CONTRIBUTORS": resultat = "Mga Kontribyutor"; break;
                        case "CONTRIB_TRANSLATOR": resultat = "Pagsasalin"; break;
                        case "CONTRIB_DEV": resultat = "Pag-develop"; break;
                        case "CONTRIB_ARTIST": resultat = "Artist/Disenyo"; break;
                        case "CONTRIB_TESTER": resultat = "Tagatasa"; break;
                        case "CONTRIB_OTHER": resultat = "Iba pa"; break;

                        case "LBL_WARNING": resultat = "Babala!"; break;
                        case "LBL_CONFIRMATION": resultat = "Sigurado ka bang nais mong gawin ang aksyong ito?"; break;
                        case "LBL_TOUT_SELECTIONNER": resultat = "Huwag Piliin Lahat"; break;
                        case "LBL_TOUT_DESELECTIONNER": resultat = "Piliin Lahat"; break;
                        case "LBL_STOP": resultat = "Itigil"; break;
                        case "LBL_OUI": resultat = "Oo"; break;
                        case "LBL_NON": resultat = "Hindi"; break;
                        case "LBL_ANNULER": resultat = "Kanselahin"; break;
                        case "LBL_LOCALISER": resultat = "Hanapin"; break;

                        case "": resultat = ""; break;
                    }
                    break;

                // Hindi
                case "HI":
                    switch (textCode)
                    {
                        case "ERR_NOMODS": resultat = "कुछ त्रुटि हुई है :( कृपया सुनिश्चित करें कि यह सॉफ़्टवेयर आपके .minecraft के 'mods' फ़ोल्डर में है!\nयह भी सुनिश्चित करें कि आपने मॉड डाउनलोड किए हैं!"; break;
                        case "ERR_TIMEOUT": resultat = "टाइमआउट - अपना कनेक्शन जांचें।"; break;
                        case "ERR_NOTYETIMPLEMENTED": resultat = "यह सुविधा अभी तक लागू नहीं की गई है।"; break;
                        case "ERR_EXEC-BATCH": resultat = "बैच फ़ाइल को निष्पादित करते समय कुछ त्रुटि हुई: "; break;
                        case "ERR_NOMINECRAFEXE": resultat = "लगता है कि Minecraft चलाना संभावित नहीं है।"; break;

                        case "WARN_WRONGFOLDER": resultat = "गलत फ़ोल्डर: "; break;
                        case "WARN_MUSTSTARTINMODSFOLDER": resultat = "सॉफ़्टवेयर को आपके .minecraft के 'mods' फ़ोल्डर से चलाया जाना चाहिए।"; break;

                        case "MOUSEHOVER_BTNQUITTER": resultat = "देखें कि यह क्या करता है..."; break;
                        case "MOUSEHOVER_BTNREPORT": resultat = "समस्या रिपोर्ट खोलें।"; break;
                        case "MOUSEHOVER_BTNCHANGELOG": resultat = "बदलाव URL खोलें, अपडेट्स के लिए परिवर्तनों का संक्षेप।"; break;
                        case "MOUSEHOVER_BTNSOURCECODE": resultat = "GitHub रिपॉजिटरी खोलें।"; break;
                        case "MOUSEHOVER_BTNFABRIC": resultat = "Fabric डाउनलोड करने के लिए URL खोलें।"; break;
                        case "MOUSEHOVER_BTNUPDATE": resultat = "मॉड डाउनलोड / अपडेट करें"; break;

                        case "INFO_SUCCESS": resultat = "सब कुछ सफलतापूर्वक हुआ!"; break;
                        case "INFO_ABOUT": resultat = $"मेरे Survival Mods\nनिर्माता: MythMega\n2023\nसंस्करण: {version}\nबिल्ड: {app_build}"; break;
                        case "INFO_MYTHMEGA": resultat = "मैं MythMega हूँ (मैंने इस खंड को अभी तक नहीं बनाया है, यह जरूरी नहीं है)।"; break;
                        case "INFO_CONTRIBUTORS": resultat = $"कोन्ट्रिब्यूटर्स निम्नलिखित हैं:\n{getContributorsNamesAndLbl()}\nउन्हें धन्यवाद!"; break;

                        case "CONTRIBUTORS": resultat = "कोन्ट्रिब्यूटर्स"; break;
                        case "CONTRIB_TRANSLATOR": resultat = "अनुवादक"; break;
                        case "CONTRIB_DEV": resultat = "डेवलपमेंट"; break;
                        case "CONTRIB_ARTIST": resultat = "कलाकार/डिज़ाइन"; break;
                        case "CONTRIB_TESTER": resultat = "टेस्टर"; break;
                        case "CONTRIB_OTHER": resultat = "अन्य"; break;

                        case "LBL_WARNING": resultat = "चेतावनी!"; break;
                        case "LBL_CONFIRMATION": resultat = "क्या आप वाकई इस क्रिया को करना चाहते हैं?"; break;
                        case "LBL_TOUT_SELECTIONNER": resultat = "सभी को अचयन करें"; break;
                        case "LBL_TOUT_DESELECTIONNER": resultat = "सभी को चयनित करें"; break;
                        case "LBL_STOP": resultat = "रोकें"; break;
                        case "LBL_OUI": resultat = "हाँ"; break;
                        case "LBL_NON": resultat = "नहीं"; break;
                        case "LBL_ANNULER": resultat = "रद्द करें"; break;
                        case "LBL_LOCALISER": resultat = "स्थानित करें"; break;

                        case "": resultat = ""; break;
                    }
                    break;

                // Bengali
                case "BN":
                    switch (textCode)
                    {
                        case "ERR_NOMODS": resultat = "একটি ত্রুটি ঘটেছে :( দয়া করে নিশ্চিত করুন যে এই সফটওয়্যারটি আপনার .minecraft ফোল্ডারের 'mods' ফোল্ডারে রয়েছে!\nএছাড়া, আপনি মডগুলি ডাউনলোড করেছেন তা নিশ্চিত করুন!"; break;
                        case "ERR_TIMEOUT": resultat = "টাইমআউট - আপনার সংযোগ পরীক্ষা করুন।"; break;
                        case "ERR_NOTYETIMPLEMENTED": resultat = "এই বৈশিষ্ট্যটি এখনও সম্পন্ন হয়নি।"; break;
                        case "ERR_EXEC-BATCH": resultat = "ব্যাচ ফাইল নিষ্পাদন করার সময় একটি ত্রুটি ঘটেছে: "; break;
                        case "ERR_NOMINECRAFEXE": resultat = "মাইনক্রাফট চালাতে সম্ভব হচ্ছে না।"; break;

                        case "WARN_WRONGFOLDER": resultat = "ভুল ফোল্ডার: "; break;
                        case "WARN_MUSTSTARTINMODSFOLDER": resultat = "সফটওয়্যারটি আপনার .minecraft এর 'mods' ফোল্ডার থেকে চালানো আবশ্যক।"; break;

                        case "MOUSEHOVER_BTNQUITTER": resultat = "এটি কী করে দেখুন..."; break;
                        case "MOUSEHOVER_BTNREPORT": resultat = "সমস্যা রিপোর্ট খুলুন।"; break;
                        case "MOUSEHOVER_BTNCHANGELOG": resultat = "চেঞ্জলগ URL খুলুন, আপডেটগুলির সারাংশ দেখানো হয়।"; break;
                        case "MOUSEHOVER_BTNSOURCECODE": resultat = "GitHub রিপোজিটরি খুলুন।"; break;
                        case "MOUSEHOVER_BTNFABRIC": resultat = "ফ্যাব্রিক ডাউনলোড করার জন্য URL খুলুন।"; break;
                        case "MOUSEHOVER_BTNUPDATE": resultat = "মডগুলি ডাউনলোড / আপডেট করুন"; break;

                        case "INFO_SUCCESS": resultat = "সব সফলভাবে গতিবদ্ধ হয়েছে!"; break;
                        case "INFO_ABOUT": resultat = $"আমার সার্ভাইভাল মডস\nলেখক: MythMega\n২০২৩\nসংস্করণ: {version}\nবিল্ড: {app_build}"; break;
                        case "INFO_MYTHMEGA": resultat = "আমি মিথমেগা (আমি এই বিভাগটি এখনও তৈরি করনি, এটি তাড়াতাড়ি প্রয়োজন নেই)।"; break;
                        case "INFO_CONTRIBUTORS": resultat = $"যারা অবদান দিয়েছেন তাদের তালিকা নিম্নলিখিত:\n{getContributorsNamesAndLbl()}\nতাদের জন্য ধন্যবাদ!"; break;

                        case "CONTRIBUTORS": resultat = "অবদানকারীগণ"; break;
                        case "CONTRIB_TRANSLATOR": resultat = "অনুবাদক"; break;
                        case "CONTRIB_DEV": resultat = "ডেভলপমেন্ট"; break;
                        case "CONTRIB_ARTIST": resultat = "শিল্পী/ডিজাইন"; break;
                        case "CONTRIB_TESTER": resultat = "টেস্টার"; break;
                        case "CONTRIB_OTHER": resultat = "অন্যান্য"; break;

                        case "LBL_WARNING": resultat = "সতর্কবার্তা!"; break;
                        case "LBL_CONFIRMATION": resultat = "আপনি কি নিশ্চিত যে আপনি এই ক্রিয়া পুনরায় চালাতে চান?"; break;
                        case "LBL_TOUT_SELECTIONNER": resultat = "সব অপশন বাদ দিন"; break;
                        case "LBL_TOUT_DESELECTIONNER": resultat = "সব চিহ্নিত করুন"; break;
                        case "LBL_STOP": resultat = "বন্ধ করুন"; break;
                        case "LBL_OUI": resultat = "হাঁ"; break;
                        case "LBL_NON": resultat = "না"; break;
                        case "LBL_ANNULER": resultat = "বাতিল করুন"; break;
                        case "LBL_LOCALISER": resultat = "স্থান নির্ধারণ করুন"; break;

                        case "": resultat = ""; break;
                    }
                    break;

                // Japanese
                case "JA":
                    switch (textCode)
                    {
                        case "ERR_NOMODS": resultat = "エラーが発生しました :( このソフトウェアが .minecraft の 'mods' フォルダに配置されていることを確認してください！\nまた、モッズをダウンロードしていることを確認してください！"; break;
                        case "ERR_TIMEOUT": resultat = "タイムアウト - 接続を確認してください。"; break;
                        case "ERR_NOTYETIMPLEMENTED": resultat = "この機能はまだ実装されていません。"; break;
                        case "ERR_EXEC-BATCH": resultat = "バッチファイルを実行する際にエラーが発生しました: "; break;
                        case "ERR_NOMINECRAFEXE": resultat = "Minecraft を起動することはできないようです。"; break;

                        case "WARN_WRONGFOLDER": resultat = "間違ったフォルダ："; break;
                        case "WARN_MUSTSTARTINMODSFOLDER": resultat = "このソフトウェアは .minecraft の 'mods' フォルダから実行する必要があります。"; break;

                        case "MOUSEHOVER_BTNQUITTER": resultat = "何をするかを見てみてください..."; break;
                        case "MOUSEHOVER_BTNREPORT": resultat = "問題報告を開く。"; break;
                        case "MOUSEHOVER_BTNCHANGELOG": resultat = "変更ログの URL を開く、アップデートごとの変更の概要。"; break;
                        case "MOUSEHOVER_BTNSOURCECODE": resultat = "GitHub リポジトリを開く。"; break;
                        case "MOUSEHOVER_BTNFABRIC": resultat = "Fabric をダウンロードするための URL を開く。"; break;
                        case "MOUSEHOVER_BTNUPDATE": resultat = "モッズをダウンロード / 更新"; break;

                        case "INFO_SUCCESS": resultat = "すべて正常に完了しました！"; break;
                        case "INFO_ABOUT": resultat = $"My Survival Mods\n作成者：MythMega\n2023\nバージョン：{version}\nビルド：{app_build}"; break;
                        case "INFO_MYTHMEGA": resultat = "私は MythMega です（このセクションはまだ作成していません、緊急性は低いです）。"; break;
                        case "INFO_CONTRIBUTORS": resultat = $"コントリビューターは次のとおりです：\n{getContributorsNamesAndLbl()}\n彼らに感謝！"; break;

                        case "CONTRIBUTORS": resultat = "コントリビューター"; break;
                        case "CONTRIB_TRANSLATOR": resultat = "翻訳者"; break;
                        case "CONTRIB_DEV": resultat = "開発者"; break;
                        case "CONTRIB_ARTIST": resultat = "アーティスト/デザイン"; break;
                        case "CONTRIB_TESTER": resultat = "テスター"; break;
                        case "CONTRIB_OTHER": resultat = "その他"; break;

                        case "LBL_WARNING": resultat = "警告！"; break;
                        case "LBL_CONFIRMATION": resultat = "このアクションを実行してもよろしいですか？"; break;
                        case "LBL_TOUT_SELECTIONNER": resultat = "すべて選択解除"; break;
                        case "LBL_TOUT_DESELECTIONNER": resultat = "すべて選択"; break;
                        case "LBL_STOP": resultat = "停止"; break;
                        case "LBL_OUI": resultat = "はい"; break;
                        case "LBL_NON": resultat = "いいえ"; break;
                        case "LBL_ANNULER": resultat = "キャンセル"; break;
                        case "LBL_LOCALISER": resultat = "位置を特定"; break;

                        case "": resultat = ""; break;
                    }
                    break;

                // Korean
                case "KO":
                    switch (textCode)
                    {
                        case "ERR_NOMODS": resultat = "오류가 발생했습니다 :( 이 소프트웨어를 .minecraft의 'mods' 폴더에 배치하십시오!\n또한 모드를 다운로드했는지 확인하십시오!"; break;
                        case "ERR_TIMEOUT": resultat = "타임아웃 - 연결을 확인하십시오."; break;
                        case "ERR_NOTYETIMPLEMENTED": resultat = "이 기능은 아직 구현되지 않았습니다."; break;
                        case "ERR_EXEC-BATCH": resultat = "일괄 파일을 실행하는 동안 오류가 발생했습니다: "; break;
                        case "ERR_NOMINECRAFEXE": resultat = "Minecraft을(를) 시작할 수 없는 것 같습니다."; break;

                        case "WARN_WRONGFOLDER": resultat = "잘못된 폴더: "; break;
                        case "WARN_MUSTSTARTINMODSFOLDER": resultat = "이 소프트웨어는 .minecraft의 'mods' 폴더에서 실행되어야 합니다."; break;

                        case "MOUSEHOVER_BTNQUITTER": resultat = "이게 무슨 일인지 확인해보세요..."; break;
                        case "MOUSEHOVER_BTNREPORT": resultat = "문제 보고서 열기."; break;
                        case "MOUSEHOVER_BTNCHANGELOG": resultat = "변경 내역 URL 열기, 업데이트별 변경 사항 요약."; break;
                        case "MOUSEHOVER_BTNSOURCECODE": resultat = "GitHub 저장소 열기."; break;
                        case "MOUSEHOVER_BTNFABRIC": resultat = "Fabric 다운로드용 URL 열기."; break;
                        case "MOUSEHOVER_BTNUPDATE": resultat = "모드 다운로드 / 업데이트"; break;

                        case "INFO_SUCCESS": resultat = "모든 작업이 성공적으로 완료되었습니다!"; break;
                        case "INFO_ABOUT": resultat = $"내 서바이벌 모드\n작성자: MythMega\n2023\n버전: {version}\n빌드: {app_build}"; break;
                        case "INFO_MYTHMEGA": resultat = "나는 MythMega입니다 (이 섹션을 아직 만들지 않았습니다, 긴급하지 않습니다)."; break;
                        case "INFO_CONTRIBUTORS": resultat = $"기여자 목록은 다음과 같습니다:\n{getContributorsNamesAndLbl()}\n그들에게 감사드립니다!"; break;

                        case "CONTRIBUTORS": resultat = "기여자"; break;
                        case "CONTRIB_TRANSLATOR": resultat = "번역자"; break;
                        case "CONTRIB_DEV": resultat = "개발자"; break;
                        case "CONTRIB_ARTIST": resultat = "아티스트/디자이너"; break;
                        case "CONTRIB_TESTER": resultat = "테스터"; break;
                        case "CONTRIB_OTHER": resultat = "기타"; break;

                        case "LBL_WARNING": resultat = "경고!"; break;
                        case "LBL_CONFIRMATION": resultat = "이 작업을 수행하시겠습니까?"; break;
                        case "LBL_TOUT_SELECTIONNER": resultat = "모두 선택 해제"; break;
                        case "LBL_TOUT_DESELECTIONNER": resultat = "모두 선택"; break;
                        case "LBL_STOP": resultat = "중지"; break;
                        case "LBL_OUI": resultat = "예"; break;
                        case "LBL_NON": resultat = "아니오"; break;
                        case "LBL_ANNULER": resultat = "취소"; break;
                        case "LBL_LOCALISER": resultat = "찾기"; break;

                        case "": resultat = ""; break;
                    }
                    break;

                // Turkish
                case "TR":
                    switch (textCode)
                    {
                        case "ERR_NOMODS": resultat = "Bir hata oluştu :( Lütfen bu yazılımı .minecraft klasörünüzün 'mods' klasörüne yerleştirdiğinizden emin olun!\nAyrıca, modları indirdiğinizden emin olun!"; break;
                        case "ERR_TIMEOUT": resultat = "Zaman aşımı - Bağlantınızı kontrol edin."; break;
                        case "ERR_NOTYETIMPLEMENTED": resultat = "Bu özellik henüz uygulanmadı."; break;
                        case "ERR_EXEC-BATCH": resultat = "Toplu işlem dosyası çalıştırılırken bir hata oluştu: "; break;
                        case "ERR_NOMINECRAFEXE": resultat = "Minecraft'u başlatmak mümkün görünmüyor."; break;

                        case "WARN_WRONGFOLDER": resultat = "Yanlış klasör: "; break;
                        case "WARN_MUSTSTARTINMODSFOLDER": resultat = "Yazılım, .minecraft'ınızın 'mods' klasöründen çalıştırılmalıdır."; break;

                        case "MOUSEHOVER_BTNQUITTER": resultat = "Ne yaptığını gör..."; break;
                        case "MOUSEHOVER_BTNREPORT": resultat = "Sorun raporunu aç."; break;
                        case "MOUSEHOVER_BTNCHANGELOG": resultat = "Değişiklik günlüğü URL'sini aç, güncellemelerin özetleri."; break;
                        case "MOUSEHOVER_BTNSOURCECODE": resultat = "GitHub deposunu aç."; break;
                        case "MOUSEHOVER_BTNFABRIC": resultat = "Fabric'i indirmek için URL'yi aç."; break;
                        case "MOUSEHOVER_BTNUPDATE": resultat = "Modları İndir / Güncelle"; break;

                        case "INFO_SUCCESS": resultat = "Her şey başarıyla gerçekleşti!"; break;
                        case "INFO_ABOUT": resultat = $"Benim Survival Modlarım\nMythMega tarafından\n2023\nSürüm: {version}\nDerleme: {app_build}"; break;
                        case "INFO_MYTHMEGA": resultat = "Ben MythMega'yım (Bu bölümü henüz oluşturmadım, daha az acil)."; break;
                        case "INFO_CONTRIBUTORS": resultat = $"Katkıda bulunanlar aşağıdaki gibidir:\n{getContributorsNamesAndLbl()}\nOnlara teşekkürler!"; break;

                        case "CONTRIBUTORS": resultat = "Katkıda Bulunanlar"; break;
                        case "CONTRIB_TRANSLATOR": resultat = "Çevirmen"; break;
                        case "CONTRIB_DEV": resultat = "Geliştirme"; break;
                        case "CONTRIB_ARTIST": resultat = "Sanatçı/Desen"; break;
                        case "CONTRIB_TESTER": resultat = "Testçi"; break;
                        case "CONTRIB_OTHER": resultat = "Diğer"; break;

                        case "LBL_WARNING": resultat = "Uyarı!"; break;
                        case "LBL_CONFIRMATION": resultat = "Bu eylemi gerçekleştirmek istediğinizden emin misiniz?"; break;
                        case "LBL_TOUT_SELECTIONNER": resultat = "Tümünü Kaldır"; break;
                        case "LBL_TOUT_DESELECTIONNER": resultat = "Tümünü Seç"; break;
                        case "LBL_STOP": resultat = "Durdur"; break;
                        case "LBL_OUI": resultat = "Evet"; break;
                        case "LBL_NON": resultat = "Hayır"; break;
                        case "LBL_ANNULER": resultat = "İptal"; break;
                        case "LBL_LOCALISER": resultat = "Konumlandır"; break;

                        case "": resultat = ""; break;
                    }
                    break;

                // Thai
                case "TH":
                    switch (textCode)
                    {
                        case "ERR_NOMODS": resultat = "เกิดข้อผิดพลาด :( โปรดตรวจสอบว่าซอฟต์แวร์นี้อยู่ในโฟลเดอร์ 'mods' ของ .minecraft ของคุณ!\nนอกจากนี้ โปรดตรวจสอบว่าคุณได้ดาวน์โหลด mod ทั้งหมด!"; break;
                        case "ERR_TIMEOUT": resultat = "หมดเวลา - ตรวจสอบการเชื่อมต่อของคุณ"; break;
                        case "ERR_NOTYETIMPLEMENTED": resultat = "คุณลักษณะนี้ยังไม่ได้รับการนำมาใช้งาน"; break;
                        case "ERR_EXEC-BATCH": resultat = "เกิดข้อผิดพลาดขณะดำเนินการแบทช์ไฟล์: "; break;
                        case "ERR_NOMINECRAFEXE": resultat = "ดูเหมือนว่าการเริ่ม Minecraft ไม่เป็นไปได้"; break;

                        case "WARN_WRONGFOLDER": resultat = "โฟลเดอร์ผิด: "; break;
                        case "WARN_MUSTSTARTINMODSFOLDER": resultat = "โปรแกรมต้องถูกเรียกใช้จากโฟลเดอร์ 'mods' ของ .minecraft ของคุณ"; break;

                        case "MOUSEHOVER_BTNQUITTER": resultat = "ดูว่ามันทำอะไร..."; break;
                        case "MOUSEHOVER_BTNREPORT": resultat = "เปิดรายงานปัญหา"; break;
                        case "MOUSEHOVER_BTNCHANGELOG": resultat = "เปิด URL ประวัติการเปลี่ยนแปลง, สรุปการเปลี่ยนแปลงต่อการอัปเดต"; break;
                        case "MOUSEHOVER_BTNSOURCECODE": resultat = "เปิด GitHub ที่เก็บข้อมูล"; break;
                        case "MOUSEHOVER_BTNFABRIC": resultat = "เปิด URL เพื่อดาวน์โหลด Fabric"; break;
                        case "MOUSEHOVER_BTNUPDATE": resultat = "ดาวน์โหลด / อัปเดต mod"; break;

                        case "INFO_SUCCESS": resultat = "ทุกอย่างเสร็จสมบูรณ์แล้ว!"; break;
                        case "INFO_ABOUT": resultat = $"My Survival Mods\nโดย MythMega\n2023\nเวอร์ชัน: {version}\nสร้าง: {app_build}"; break;
                        case "INFO_MYTHMEGA": resultat = "ฉันคือ MythMega (ฉันยังไม่ได้สร้างส่วนนี้, ไม่เร่งด่วน)"; break;
                        case "INFO_CONTRIBUTORS": resultat = $"ผู้มีส่วนร่วมคือดังต่อไปนี้:\n{getContributorsNamesAndLbl()}\nขอบคุณพวกเขา!"; break;

                        case "CONTRIBUTORS": resultat = "ผู้มีส่วนร่วม"; break;
                        case "CONTRIB_TRANSLATOR": resultat = "ผู้แปล"; break;
                        case "CONTRIB_DEV": resultat = "การพัฒนา"; break;
                        case "CONTRIB_ARTIST": resultat = "ศิลปิน/การออกแบบ"; break;
                        case "CONTRIB_TESTER": resultat = "ผู้ทดสอบ"; break;
                        case "CONTRIB_OTHER": resultat = "อื่น ๆ"; break;

                        case "LBL_WARNING": resultat = "คำเตือน!"; break;
                        case "LBL_CONFIRMATION": resultat = "คุณแน่ใจหรือไม่ว่าต้องการดำเนินการนี้?"; break;
                        case "LBL_TOUT_SELECTIONNER": resultat = "ไม่เลือกทั้งหมด"; break;
                        case "LBL_TOUT_DESELECTIONNER": resultat = "เลือกทั้งหมด"; break;
                        case "LBL_STOP": resultat = "หยุด"; break;
                        case "LBL_OUI": resultat = "ใช่"; break;
                        case "LBL_NON": resultat = "ไม่ใช่"; break;
                        case "LBL_ANNULER": resultat = "ยกเลิก"; break;
                        case "LBL_LOCALISER": resultat = "ค้นหาตำแหน่ง"; break;

                        case "": resultat = ""; break;
                    }
                    break;

                // Indonesian
                case "ID":
                    switch (textCode)
                    {
                        case "ERR_NOMODS": resultat = "Terjadi kesalahan :( Pastikan perangkat lunak ini ditempatkan di folder 'mods' dari .minecraft Anda!\nJuga, pastikan Anda telah mengunduh mod-modnya!"; break;
                        case "ERR_TIMEOUT": resultat = "Waktu habis - Periksa koneksi Anda."; break;
                        case "ERR_NOTYETIMPLEMENTED": resultat = "Fitur ini belum diimplementasikan."; break;
                        case "ERR_EXEC-BATCH": resultat = "Terjadi kesalahan saat menjalankan berkas batch: "; break;
                        case "ERR_NOMINECRAFEXE": resultat = "Sepertinya meluncurkan Minecraft tidak mungkin."; break;

                        case "WARN_WRONGFOLDER": resultat = "Folder yang Salah: "; break;
                        case "WARN_MUSTSTARTINMODSFOLDER": resultat = "Perangkat lunak ini harus dijalankan dari folder 'mods' dari .minecraft Anda."; break;

                        case "MOUSEHOVER_BTNQUITTER": resultat = "Lihat apa yang dilakukannya..."; break;
                        case "MOUSEHOVER_BTNREPORT": resultat = "Buka laporan masalah."; break;
                        case "MOUSEHOVER_BTNCHANGELOG": resultat = "Buka URL catatan perubahan, ringkasan perubahan per pembaruan."; break;
                        case "MOUSEHOVER_BTNSOURCECODE": resultat = "Buka repositori GitHub."; break;
                        case "MOUSEHOVER_BTNFABRIC": resultat = "Buka URL untuk mengunduh Fabric."; break;
                        case "MOUSEHOVER_BTNUPDATE": resultat = "Unduh / Perbarui mod"; break;

                        case "INFO_SUCCESS": resultat = "Semuanya berjalan dengan sukses!"; break;
                        case "INFO_ABOUT": resultat = $"My Survival Mods\noleh MythMega\n2023\nVersi: {version}\nBuild: {app_build}"; break;
                        case "INFO_MYTHMEGA": resultat = "Saya adalah MythMega (Saya belum membuat bagian ini, kurang mendesak)."; break;
                        case "INFO_CONTRIBUTORS": resultat = $"Para kontributor adalah sebagai berikut:\n{getContributorsNamesAndLbl()}\nTerima kasih kepada mereka!"; break;

                        case "CONTRIBUTORS": resultat = "Kontributor"; break;
                        case "CONTRIB_TRANSLATOR": resultat = "Penerjemah"; break;
                        case "CONTRIB_DEV": resultat = "Pengembang"; break;
                        case "CONTRIB_ARTIST": resultat = "Seniman/Desain"; break;
                        case "CONTRIB_TESTER": resultat = "Penguji"; break;
                        case "CONTRIB_OTHER": resultat = "Lainnya"; break;

                        case "LBL_WARNING": resultat = "Peringatan!"; break;
                        case "LBL_CONFIRMATION": resultat = "Apakah Anda yakin ingin melakukan tindakan ini?"; break;
                        case "LBL_TOUT_SELECTIONNER": resultat = "Tidak Pilih Semua"; break;
                        case "LBL_TOUT_DESELECTIONNER": resultat = "Pilih Semua"; break;
                        case "LBL_STOP": resultat = "Berhenti"; break;
                        case "LBL_OUI": resultat = "Ya"; break;
                        case "LBL_NON": resultat = "Tidak"; break;
                        case "LBL_ANNULER": resultat = "Batal"; break;
                        case "LBL_LOCALISER": resultat = "Temukan"; break;

                        case "": resultat = ""; break;
                    }
                    break;
            }
            return resultat;
        }

        #endregion

        
    }
}

