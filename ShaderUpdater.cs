using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BuildItModsSelector
{
    public partial class ShaderUpdater : Form
    {
        private Dictionary<string, string> filesToDownload = new Dictionary<string, string>();
        private WebClient webClient;
        private string shaderFilePath = Path.Combine("sys", "shaderlist.config");
        private Main temoinMain = new Main();
        private List<CheckBox> checkBoxes = new List<CheckBox>();

        public ShaderUpdater()
        {
            InitializeComponent();
        }

        private void ShaderUpdater_Load(object sender, EventArgs e)
        {
            DownloadFileSyncInSys("shaderlist.config", "https://jmdbymyth.000webhostapp.com/prj/mysurvivaltool/shaderlist.config");
            Thread.Sleep(1000);
            loadLayout();
            temoinMain.updateTheme(this);
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
                        MessageBox.Show(temoinMain.translatedText("ERR_TIMEOUT"));
                        return;
                    }
                }
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

        private void loadLayout()
        {
            // Lecture du contenu du fichier itemlist.cfg
            string[] lines = File.ReadAllLines(shaderFilePath);

            // Création d'un dictionnaire de groupes avec des listes de cases à cocher
            Dictionary<string, List<CheckBox>> groupes = new Dictionary<string, List<CheckBox>>();

            // Parcourir les lignes pour remplir le dictionnaire
            foreach (string line in lines)
            {
                string[] parts = line.Split('|');
                string groupe = parts[0];
                string item = parts[1];

                if (!groupes.ContainsKey(groupe))
                {
                    groupes[groupe] = new List<CheckBox>();
                }

                CheckBox checkBox = new CheckBox();
                checkBox.Text = item;
                checkBoxes.Add(checkBox);
                groupes[groupe].Add(checkBox);
            }

            int xSpacing = 10; // Espacement horizontal entre les GroupBox
            int ySpacing = 10; // Espacement vertical entre les GroupBox
            int groupBoxWidth = 300;
            int groupBoxHeight = 150;

            int x = xSpacing; // Position horizontale de départ
            int y = ySpacing; // Position verticale de départ

            int groupBoxCount = 0;
            int groupBoxsPerRow = 3;

            // Création des groupes de checkboxes
            foreach (var groupe in groupes)
            {
                GroupBox groupBox = new GroupBox();
                groupBox.Text = groupe.Key;
                groupBox.Width = groupBoxWidth;
                groupBox.Height = groupBoxHeight;
                groupBox.Location = new Point(x, y);
                this.Controls.Add(groupBox);

                int checkBoxY = 20; // Position verticale de départ pour les cases à cocher
                int checkBoxSpacing = 30; // Espacement vertical entre les cases à cocher

                foreach (CheckBox checkBox in groupe.Value)
                {
                    checkBox.Location = new Point(10, checkBoxY); // Définir la position
                    checkBox.Width = groupBox.Width - 20; // Ajuster la largeur en fonction de la GroupBox
                    groupBox.Controls.Add(checkBox);

                    checkBoxY += checkBoxSpacing; // Augmenter la position verticale pour la prochaine case à cocher
                }


                // Position verticale pour les boutons Sélectionner Tout et Désélectionner Tout
                int buttonsY = groupBox.Height - 60; // 60 pour prendre en compte la hauteur des boutons

                // Bouton tout selectionner
                Button selectAllButton = new Button();
                selectAllButton.Text = temoinMain.translatedText("LBL_TOUT_SELECTIONNER");
                selectAllButton.Location = new Point(10, buttonsY+15);
                selectAllButton.Width = 130;
                selectAllButton.FlatAppearance.BorderSize = 0;
                selectAllButton.FlatStyle = FlatStyle.Flat;
                selectAllButton.Click += (s, eventArgs) =>
                {
                    foreach (CheckBox checkBox in groupe.Value)
                    {
                        checkBox.Checked = true;
                    }
                };
                groupBox.Controls.Add(selectAllButton);

                // Bouton tout deselectionner
                Button deselectAllButton = new Button();
                deselectAllButton.Text = temoinMain.translatedText("LBL_TOUT_DESELECTIONNER");
                deselectAllButton.Location = new Point(155, buttonsY+15);
                deselectAllButton.Width = 130;
                deselectAllButton.FlatAppearance.BorderSize = 0;
                deselectAllButton.FlatStyle = FlatStyle.Flat;
                deselectAllButton.Click += (s, eventArgs) =>
                {
                    foreach (CheckBox checkBox in groupe.Value)
                    {
                        checkBox.Checked = false;
                    }
                };
                groupBox.Controls.Add(deselectAllButton);

                groupBoxCount++;
                x += groupBoxWidth + xSpacing;

                // Passer à la ligne suivante après chaque groupe dans la même ligne
                if (groupBoxCount % groupBoxsPerRow == 0)
                {
                    x = xSpacing;
                    y += groupBoxHeight + ySpacing;
                }
            }
            int sizeOneLine = (groupBoxHeight + ySpacing);
            int nbrLigne = (groupBoxCount - (groupBoxCount % 3))/groupBoxsPerRow;
            if(groupBoxCount%groupBoxsPerRow!=0) { nbrLigne += 1; }
            this.Size = new Size((groupBoxWidth + xSpacing) * groupBoxsPerRow+20, (nbrLigne*sizeOneLine)+90);


            //btnUpdate
            int btnupdateX = (this.Width / 2) - 50;
            int btnupdateY = (this.Height) - 80;
            btnUpdateShader.Location = new Point(btnupdateX, btnupdateY);
        }


        private void btnUpdateShader_Click(object sender, EventArgs e)
        {
            List<string> selectedShaderItems = new List<string>();
            foreach(CheckBox cb in checkBoxes)
            {
                if(cb.Checked)
                {
                    selectedShaderItems.Add(cb.Text);
                }
                
            }

            DownloadAndMoveFiles(selectedShaderItems);
        }

        public void DownloadAndMoveFiles(List<string> selectedShaderItems)
        {
            string executableDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string shaderpacksDirectory = Path.Combine(Directory.GetParent(executableDirectory).FullName, "shaderpacks");

            if (!Directory.Exists(shaderpacksDirectory))
            {
                Directory.CreateDirectory(shaderpacksDirectory);
            }

            string[] lines = File.ReadAllLines(shaderFilePath);

            foreach (string line in lines)
            {
                string[] parts = line.Split('|');
                string categoryName = parts[0];
                string shaderName = parts[1];
                string downloadLink = parts[2];

                if (selectedShaderItems.Contains(shaderName))
                {
                    string targetFilePath = Path.Combine(shaderpacksDirectory, "../../shaderpacks/", $"{shaderName}.zip");

                    using (WebClient webClient = new WebClient())
                    {
                        Console.WriteLine($"Téléchargement de {shaderName} depuis {downloadLink}");
                        webClient.DownloadFile(downloadLink, targetFilePath);
                        Console.WriteLine($"Téléchargement de {shaderName} terminé");
                    }
                }
            }

            Console.WriteLine("Téléchargement et déplacement terminé.");
        }

    }
}
