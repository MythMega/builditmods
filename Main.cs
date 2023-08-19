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
                        case "CONTRIB_TRANSLATOR": resultat = ""; break;
                        case "CONTRIB_DEV": resultat = ""; break;
                        case "CONTRIB_ARTIST": resultat = ""; break;
                        case "CONTRIB_TESTER": resultat = ""; break;
                        case "CONTRIB_OTHER": resultat = ""; break;

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
                        case "ERR_NOMODS": resultat = "An error occurred :( Make sure this software is placed in the mods folder of your .minecraft!\nAlso, check that you have downloaded the mods!"; break;
                        case "ERR_TIMEOUT": resultat = "Timeout - Check your connection."; break;
                        case "ERR_NOTYETIMPLEMENTED": resultat = "This feature is not yet implemented"; break;
                        case "ERR_EXEC-BATCH": resultat = "An error occurred while executing the batch file: "; break;

                        case "MOUSEHOVER_BTNQUITTER": resultat = "See what it does..."; break;
                        case "MOUSEHOVER_BTNREPORT": resultat = "Open the problem report."; break;
                        case "MOUSEHOVER_BTNCHANGELOG": resultat = "Open the changelog URL, a summary of changes by updates."; break;
                        case "MOUSEHOVER_BTNSOURCECODE": resultat = "Open the GitHub repository."; break;
                        case "MOUSEHOVER_BTNFABRIC": resultat = "Open the URL to download Fabric."; break;
                        case "MOUSEHOVER_BTNUPDATE": resultat = "Download / Update the mods"; break;

                        case "INFO_SUCCESS": resultat = "Everything went smoothly!"; break;
                        case "INFO_ABOUT": resultat = $"My Survival Mods\nby MythMega\n2023\nVersion :{version}\nBuild : {app_build}"; break;
                        case "INFO_MYTHMEGA": resultat = "I am MythMega (I haven't created this section yet, it's less urgent)."; break;


                        case "LBL_WARNING": resultat = "Warning!"; break;
                        case "LBL_CONFIRMATION": resultat = "Are you sure you want to perform this action?"; break;

                        case "": resultat = ""; break;
                    }
                    break;

                // Spanish
                case "ES":
                    switch (textCode)
                    {
                        case "ERR_NOMODS": resultat = "Se produjo un error :( Asegúrate de que este software esté ubicado en la carpeta mods de tu .minecraft!\n¡También verifica que hayas descargado los mods!"; break;
                        case "ERR_TIMEOUT": resultat = "Tiempo de espera - Verifica tu conexión."; break;
                        case "ERR_NOTYETIMPLEMENTED": resultat = "Esta función aún no está implementada"; break;
                        case "ERR_EXEC-BATCH": resultat = "Se produjo un error al ejecutar el archivo por lotes: "; break;

                        case "MOUSEHOVER_BTNQUITTER": resultat = "Mira lo que hace..."; break;
                        case "MOUSEHOVER_BTNREPORT": resultat = "Abrir el informe de problemas."; break;
                        case "MOUSEHOVER_BTNCHANGELOG": resultat = "Abrir la URL del registro de cambios, un resumen de los cambios por actualizaciones."; break;
                        case "MOUSEHOVER_BTNSOURCECODE": resultat = "Abrir el repositorio de GitHub."; break;
                        case "MOUSEHOVER_BTNFABRIC": resultat = "Abrir la URL para descargar Fabric."; break;
                        case "MOUSEHOVER_BTNUPDATE": resultat = "Descargar / Actualizar los mods"; break;

                        case "INFO_SUCCESS": resultat = "¡Todo salió bien!"; break;
                        case "INFO_ABOUT": resultat = $"Mis Mods de Supervivencia\npor MythMega\n2023\nVersión :{version}\nBuild : {app_build}"; break;
                        case "INFO_MYTHMEGA": resultat = "Soy MythMega (todavía no he creado esta sección, es menos urgente)."; break;

                        case "LBL_WARNING": resultat = "¡Advertencia!"; break;
                        case "LBL_CONFIRMATION": resultat = "¿Estás seguro de que deseas realizar esta acción?"; break;

                        case "": resultat = ""; break;
                    }
                    break;

                // German
                case "DE":
                    switch (textCode)
                    {
                        case "ERR_NOMODS": resultat = "Es ist ein Fehler aufgetreten :( Stellen Sie sicher, dass diese Software im mods-Ordner Ihrer .minecraft-Installation platziert ist!\nÜberprüfen Sie auch, ob Sie die Mods heruntergeladen haben!"; break;
                        case "ERR_TIMEOUT": resultat = "Timeout - Überprüfen Sie Ihre Verbindung."; break;
                        case "ERR_NOTYETIMPLEMENTED": resultat = "Diese Funktion ist noch nicht implementiert"; break;
                        case "ERR_EXEC-BATCH": resultat = "Es ist ein Fehler beim Ausführen der Batch-Datei aufgetreten: "; break;

                        case "MOUSEHOVER_BTNQUITTER": resultat = "Schauen Sie, was es tut..."; break;
                        case "MOUSEHOVER_BTNREPORT": resultat = "Öffnen Sie den Fehlerbericht."; break;
                        case "MOUSEHOVER_BTNCHANGELOG": resultat = "Öffnen Sie die URL des Änderungsprotokolls, eine Zusammenfassung der Änderungen nach Updates."; break;
                        case "MOUSEHOVER_BTNSOURCECODE": resultat = "Öffnen Sie das GitHub-Repository."; break;
                        case "MOUSEHOVER_BTNFABRIC": resultat = "Öffnen Sie die URL zum Herunterladen von Fabric."; break;
                        case "MOUSEHOVER_BTNUPDATE": resultat = "Mods herunterladen / aktualisieren"; break;

                        case "INFO_SUCCESS": resultat = "Alles verlief reibungslos!"; break;
                        case "INFO_ABOUT": resultat = $"Meine Survival-Mods\nvon MythMega\n2023\nVersion :{version}\nBuild : {app_build}"; break;
                        case "INFO_MYTHMEGA": resultat = "Ich bin MythMega (ich habe diesen Abschnitt noch nicht erstellt, es ist weniger dringend)."; break;

                        case "LBL_WARNING": resultat = "Achtung!"; break;
                        case "LBL_CONFIRMATION": resultat = "Sind Sie sicher, dass Sie diese Aktion ausführen möchten?"; break;

                        case "": resultat = ""; break;
                    }
                    break;

                // Italian
                case "IT":
                    switch (textCode)
                    {
                        case "ERR_NOMODS": resultat = "Si è verificato un errore :( Assicurati che questo software sia posizionato nella cartella mods della tua .minecraft!\nVerifica anche di aver scaricato le mod!"; break;
                        case "ERR_TIMEOUT": resultat = "Timeout - Verifica la tua connessione."; break;
                        case "ERR_NOTYETIMPLEMENTED": resultat = "Questa funzionalità non è ancora implementata"; break;
                        case "ERR_EXEC-BATCH": resultat = "Si è verificato un errore durante l'esecuzione del file batch: "; break;

                        case "MOUSEHOVER_BTNQUITTER": resultat = "Guarda cosa fa..."; break;
                        case "MOUSEHOVER_BTNREPORT": resultat = "Apri il rapporto del problema."; break;
                        case "MOUSEHOVER_BTNCHANGELOG": resultat = "Apri l'URL del changelog, un riepilogo delle modifiche per gli aggiornamenti."; break;
                        case "MOUSEHOVER_BTNSOURCECODE": resultat = "Apri il repository di GitHub."; break;
                        case "MOUSEHOVER_BTNFABRIC": resultat = "Apri l'URL per scaricare Fabric."; break;
                        case "MOUSEHOVER_BTNUPDATE": resultat = "Scarica / Aggiorna le mod"; break;

                        case "INFO_SUCCESS": resultat = "Tutto è andato bene!"; break;
                        case "INFO_ABOUT": resultat = $"Le mie mod di sopravvivenza\ndi MythMega\n2023\nVersione :{version}\nBuild : {app_build}"; break;
                        case "INFO_MYTHMEGA": resultat = "Sono MythMega (non ho ancora creato questa sezione, è meno urgente)."; break;

                        case "LBL_WARNING": resultat = "Attenzione!"; break;
                        case "LBL_CONFIRMATION": resultat = "Sei sicuro di voler eseguire questa azione?"; break;

                        case "": resultat = ""; break;
                    }
                    break;

                // Portuguese
                case "PT":
                    switch (textCode)
                    {
                        case "ERR_NOMODS": resultat = "Ocorreu um erro :( Certifique-se de que este software está localizado na pasta mods do seu .minecraft!\nVerifique também se você baixou os mods!"; break;
                        case "ERR_TIMEOUT": resultat = "Tempo esgotado - Verifique sua conexão."; break;
                        case "ERR_NOTYETIMPLEMENTED": resultat = "Esta funcionalidade ainda não está implementada"; break;
                        case "ERR_EXEC-BATCH": resultat = "Ocorreu um erro ao executar o arquivo em lote: "; break;

                        case "MOUSEHOVER_BTNQUITTER": resultat = "Veja o que faz..."; break;
                        case "MOUSEHOVER_BTNREPORT": resultat = "Abrir o relatório de problemas."; break;
                        case "MOUSEHOVER_BTNCHANGELOG": resultat = "Abrir a URL do changelog, um resumo das alterações por atualizações."; break;
                        case "MOUSEHOVER_BTNSOURCECODE": resultat = "Abrir o repositório do GitHub."; break;
                        case "MOUSEHOVER_BTNFABRIC": resultat = "Abrir a URL para baixar o Fabric."; break;
                        case "MOUSEHOVER_BTNUPDATE": resultat = "Baixar / Atualizar os mods"; break;

                        case "INFO_SUCCESS": resultat = "Tudo ocorreu com sucesso!"; break;
                        case "INFO_ABOUT": resultat = $"Minhas Mods de Sobrevivência\npor MythMega\n2023\nVersão :{version}\nBuild : {app_build}"; break;
                        case "INFO_MYTHMEGA": resultat = "Eu sou o MythMega (ainda não criei esta seção, é menos urgente)."; break;

                        case "LBL_WARNING": resultat = "Atenção!"; break;
                        case "LBL_CONFIRMATION": resultat = "Tem certeza de que deseja executar esta ação?"; break;

                        case "": resultat = ""; break;
                    }
                    break;

                // Arabic
                case "AR":
                    switch (textCode)
                    {
                        case "ERR_NOMODS": resultat = "حدث خطأ :( تأكد من وضع هذا البرنامج في مجلد mods الخاص بـ .minecraft!\nتحقق أيضًا مما إذا قمت بتنزيل الـ mods!"; break;
                        case "ERR_TIMEOUT": resultat = "مهلة المزامنة - تحقق من اتصالك."; break;
                        case "ERR_NOTYETIMPLEMENTED": resultat = "هذه الميزة لم تُنفّذ بعد"; break;
                        case "ERR_EXEC-BATCH": resultat = "حدث خطأ أثناء تنفيذ الملف الدفعي: "; break;

                        case "MOUSEHOVER_BTNQUITTER": resultat = "انظر ما يحدث..."; break;
                        case "MOUSEHOVER_BTNREPORT": resultat = "افتح تقرير المشكلة."; break;
                        case "MOUSEHOVER_BTNCHANGELOG": resultat = "افتح رابط تغيير السجل، وهو ملخص للتغييرات حسب التحديثات."; break;
                        case "MOUSEHOVER_BTNSOURCECODE": resultat = "افتح مستودع GitHub."; break;
                        case "MOUSEHOVER_BTNFABRIC": resultat = "افتح الرابط لتحميل Fabric."; break;
                        case "MOUSEHOVER_BTNUPDATE": resultat = "تنزيل / تحديث الـ mods"; break;

                        case "INFO_SUCCESS": resultat = "كل شيء تم بنجاح!"; break;
                        case "INFO_ABOUT": resultat = $"My Survival Mods\nby MythMega\n2023\nالإصدار :{version}\nBuild : {app_build}"; break;
                        case "INFO_MYTHMEGA": resultat = "أنا MythMega (لم أنشئ هذا القسم بعد، غير ضروري)."; break;

                        case "LBL_WARNING": resultat = "تحذير!"; break;
                        case "LBL_CONFIRMATION": resultat = "هل أنت متأكد من رغبتك في تنفيذ هذا الإجراء؟"; break;

                        case "": resultat = ""; break;
                    }
                    break;

                // Russian
                case "RU":
                    switch (textCode)
                    {
                        case "ERR_NOMODS": resultat = "Произошла ошибка :( Убедитесь, что это программное обеспечение находится в папке mods вашей .minecraft!\nТакже проверьте, что вы загрузили моды!"; break;
                        case "ERR_TIMEOUT": resultat = "Тайм-аут - Проверьте ваше соединение."; break;
                        case "ERR_NOTYETIMPLEMENTED": resultat = "Эта функция еще не реализована"; break;
                        case "ERR_EXEC-BATCH": resultat = "Произошла ошибка при выполнении пакетного файла: "; break;

                        case "MOUSEHOVER_BTNQUITTER": resultat = "Посмотрите, что это делает..."; break;
                        case "MOUSEHOVER_BTNREPORT": resultat = "Открыть отчет об ошибке."; break;
                        case "MOUSEHOVER_BTNCHANGELOG": resultat = "Открыть URL изменений, сводку изменений по обновлениям."; break;
                        case "MOUSEHOVER_BTNSOURCECODE": resultat = "Открыть репозиторий GitHub."; break;
                        case "MOUSEHOVER_BTNFABRIC": resultat = "Открыть URL для загрузки Fabric."; break;
                        case "MOUSEHOVER_BTNUPDATE": resultat = "Загрузить / Обновить моды"; break;

                        case "INFO_SUCCESS": resultat = "Все прошло успешно!"; break;
                        case "INFO_ABOUT": resultat = $"My Survival Mods\nот MythMega\n2023\nВерсия :{version}\nBuild : {app_build}"; break;
                        case "INFO_MYTHMEGA": resultat = "Я MythMega (я еще не создал этот раздел, это менее срочно)."; break;

                        case "LBL_WARNING": resultat = "Внимание!"; break;
                        case "LBL_CONFIRMATION": resultat = "Вы уверены, что хотите выполнить это действие?"; break;

                        case "": resultat = ""; break;
                    }
                    break;
            }
            return resultat;
        }

        #endregion

        
    }
}

