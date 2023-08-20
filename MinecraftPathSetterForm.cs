using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BuildItModsSelector
{
    public partial class MinecraftPathSetterForm : Form
    {
        private string profileFilePath = Path.Combine("sys", "profile.txt");
        public MinecraftPathSetterForm()
        {
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnCHangeMinecraftLocation_Click(object sender, EventArgs e)
        {
            SetProfileValue("mc", txbLocation.Text);
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
    }
}
