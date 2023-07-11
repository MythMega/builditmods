using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace BuildItModsSelector
{
    public partial class Main : Form
    {
        private WebClient webClient;
        private Dictionary<string, string> filesToDownload = new Dictionary<string, string>();
        private string version = "0.1e";

        public Main()
        {
            InitializeComponent();
            //crée un dossier sys si il n'existe pas
            if (!Directory.Exists(Path.Combine(Application.StartupPath, "sys"))) { Directory.CreateDirectory(Path.Combine(Application.StartupPath, "sys")); }
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
                chbIsometricRenderer,
            };

            return list;
        }

        private void btnDesactiverTout_Click(object sender, EventArgs e)
        {
            foreach (CheckBox chk in settings())
            {
                chk.Checked = false;
            }
        }

        private void btnActiverTout_Click(object sender, EventArgs e)
        {
            foreach (CheckBox chk in settings())
            {
                chk.Checked = true;
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

                if (chbIsometricRenderer.Checked) { filePath = Path.Combine(Application.StartupPath, "sys/bats/isorender_enable.bat"); }
                else { filePath = Path.Combine(Application.StartupPath, "sys/bats/isorender_disable.bat"); }
                process.StartInfo.FileName = filePath; process.Start();

                if (cbCosmetiquesJeu.Checked) { filePath = Path.Combine(Application.StartupPath, "sys/bats/cosmetique_jeu_enable.bat"); }
                else { filePath = Path.Combine(Application.StartupPath, "sys/bats/cosmetique_jeu_disable.bat"); }
                process.StartInfo.FileName = filePath; process.Start();

                MessageBox.Show("Tout s'est déroulé avec succés !");
            }
            catch
            {
                MessageBox.Show("Une erreur s'est produite :( attention ce logiciel doit être placé dans le dossier mods de votre .minecraft !");
            }

        }

        private void btnUpdateMods_Click(object sender, EventArgs e)
        {
            DownloadFileSyncInSys("modlist.config", "https://jmdbymyth.000webhostapp.com/modlist.config");
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
            while(!moving){
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
            DownloadFiles("sys/"+file, url);
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
                        MessageBox.Show("Timeout Error");
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
                { "Blur", 1 },
                { "Continuity", 0 },
                { "Falling Leaves", 3 },
            };

            showModstats(modlist);
        }

        private void lblCosmetiqueSkins_Click(object sender, EventArgs e)
        {
            Dictionary<string, int> modlist = new Dictionary<string, int> {
                { "3D Skin Layer", 2 },
                { "Capes", 2 },
            };

            showModstats(modlist);
        }

        private void lblJourneyMap_Click(object sender, EventArgs e)
        {
            Dictionary<string, int> modlist = new Dictionary<string, int> {
                { "JourneyMap", 10 },
            };

            showModstats(modlist);
        }

        private void lblLitematica_Click(object sender, EventArgs e)
        {
            Dictionary<string, int> modlist = new Dictionary<string, int> {
                { "Litematica", 4 },
                { "Malilib", 0 },
                { "Litematica Enderchest", 1 },
            };

            showModstats(modlist);
        }

        private void lblReplaymod_Click(object sender, EventArgs e)
        {
            Dictionary<string, int> modlist = new Dictionary<string, int> {
                { "ReplayMod", 4 },
                { "Bobby", 6 },
            };

            showModstats(modlist);
        }

        private void lblWorldEdit_Click(object sender, EventArgs e)
        {
            Dictionary<string, int> modlist = new Dictionary<string, int> {
                { "World Edit", 15 },
            };

            showModstats(modlist);
        }
        private void btnIsometry_Click(object sender, EventArgs e)
        {
            Dictionary<string, int> modlist = new Dictionary<string, int> {
                { "Isometric Renderer", 11 },
                { "owo-lib", 0 },
            };

            showModstats(modlist);
        }

        private void showModstats(Dictionary<string, int> modlist)
        {
            FormInfos leForm = new FormInfos();
            string modNames = string.Join("\n", modlist.Keys);
            int poidsMods = modlist.Values.Sum();
            string impact = getImpact(poidsMods);
            leForm.impactSet(impact);
            leForm.modlistSet(modNames);
            leForm.ShowDialog();
        }

        private string getImpact(int poidsMods)
        {
            if (poidsMods < 0)
            {
                return "Optimisation";
            }
            if (poidsMods >= 0 && poidsMods < 5)
            {
                return "Ultra léger";
            }
            if (poidsMods >= 5 && poidsMods < 10)
            {
                return "Léger";
            }
            if (poidsMods >= 10 && poidsMods < 15)
            {
                return "Moyen";
            }
            else
            {
                return "Lourd";
            }
        }

        private void btnUpdateMods_MouseHover(object sender, EventArgs e)
        {
            ToolTip toolTip = new ToolTip();
            toolTip.SetToolTip(btnUpdateMods, "Git doit être installé pour que cette fonctionnalité fonctionne.");
        }

        private void btnFabric_MouseHover(object sender, EventArgs e)
        {
            ToolTip toolTip = new ToolTip();
            toolTip.SetToolTip(btnFabric, "Ouvre l'URL pour télécharger Fabric.");
        }

        private void btnOpenRepos_MouseHover(object sender, EventArgs e)
        {
            ToolTip toolTip = new ToolTip();
            toolTip.SetToolTip(btnOpenRepos, "Ouvre le repository GitHub.");
        }

        private void btnModsChangelog_MouseHover(object sender, EventArgs e)
        {
            ToolTip toolTip = new ToolTip();
            toolTip.SetToolTip(btnModsChangelog, "Ouvre l'URL du changelog, résumé des changement par mises à jour.");
        }

        private void btnInstallGit_MouseHover(object sender, EventArgs e)
        {
            ToolTip toolTip = new ToolTip();
            toolTip.SetToolTip(btnReport, "Installer Git, lancer le setup et tout laisser par défaut.");
        }

        private void btnQuitter_MouseHover(object sender, EventArgs e)
        {
            ToolTip toolTip = new ToolTip();
            toolTip.SetToolTip(btnQuitter, "Regardes ce que ça fait.");
        }

        private void btnDeleteAll_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Cette fonctionalité n'est pas encore gérée");
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
    }
}
