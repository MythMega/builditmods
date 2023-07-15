using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BuildItModsSelector
{
    public partial class FormInfos : Form
    {
        public FormInfos()
        {
            InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public void modlistSet(string modlist)
        {
            this.lblModList.Text = modlist;
        }

        public void makeTranslation(string languageCode, int poidsMods)
        {
            switch (languageCode)
            {
                // code pour le cas "FR"
                case "FR":
                    lblImpact.Text = getImpact(poidsMods)[languageCode];
                    lblContainsMods.Text = "Contient les mods suivants :";
                    lblImpactOnPerf.Text = "Impact sur les performances :";
                    break;

                // code pour le cas "EN"
                case "EN":
                    lblImpact.Text = getImpact(poidsMods)[languageCode];
                    lblContainsMods.Text = "Contains the following mods :";
                    lblImpactOnPerf.Text = "Impact on performance :";
                    break;

                // code pour le cas "ES"
                case "ES":
                    lblImpact.Text = getImpact(poidsMods)[languageCode];
                    lblContainsMods.Text = "Contiene los siguientes mods :";
                    lblImpactOnPerf.Text = "Impacto en el rendimiento :";
                    break;

                // code pour le cas "DE"
                case "DE":
                    lblImpact.Text = getImpact(poidsMods)[languageCode];
                    lblContainsMods.Text = "Enthält folgende Mods :";
                    lblImpactOnPerf.Text = "Auswirkungen auf die Leistung :";
                    break;

                // code pour le cas "IT"
                case "IT":
                    lblImpact.Text = getImpact(poidsMods)[languageCode];
                    lblContainsMods.Text = "Contiene i seguenti mods :";
                    lblImpactOnPerf.Text = "Impatto sulle prestazioni :";
                    break;

                // code pour le cas "PT"
                case "PT":
                    lblImpact.Text = getImpact(poidsMods)[languageCode];
                    lblContainsMods.Text = "Contém os seguintes mods :";
                    lblImpactOnPerf.Text = "Impacto no desempenho :";
                    break;

                // code pour le cas "RU"
                case "RU":
                    lblImpact.Text = getImpact(poidsMods)[languageCode];
                    lblContainsMods.Text = "Содержит следующие моды :";
                    lblImpactOnPerf.Text = "Влияние на производительность :";
                    break;

                // code pour le cas "AR"
                case "AR":
                    lblImpact.Text = getImpact(poidsMods)[languageCode];
                    lblContainsMods.Text = "يحتوي على التعديلات التالية :";
                    lblImpactOnPerf.Text = "التأثير على الأداء :";
                    break;
            }
        }

        public Dictionary<string, string> getImpact(int poidsMods)
        {
            Dictionary<string, string> getImpact = new Dictionary<string, string>();
            if (poidsMods < 0)
            {
                return new Dictionary<string, string>()
                {
                    { "FR", "Optimisation" },
                    { "EN", "Optimization" },
                    { "ES", "Optimización" },
                    { "DE", "Optimierung" },
                    { "IT", "Ottimizzazione" },
                    { "PT", "Otimização" },
                    { "RU", "Оптимизация" },
                    { "AR", "تحسين" },
                };
            }
            if (poidsMods >= 0 && poidsMods < 5)
            {
                return new Dictionary<string, string>()
                {
                    { "FR", "Ultra léger"},
                    { "EN", "Ultra Light" },
                    { "ES", "Ultra Ligero" },
                    { "DE", "Ultra Leicht" },
                    { "IT", "Ultra Leggero" },
                    { "PT", "Ultra Leve" },
                    { "RU", "Ультра-легкий" },
                    { "AR", "خفيف جداً" },
                };
            }
            if (poidsMods >= 5 && poidsMods < 10)
            {
                return new Dictionary<string, string>()
                {
                    { "FR", "Léger"},
                    { "EN", "Light" },
                    { "ES", "Ligero" },
                    { "DE", "Leicht" },
                    { "IT", "Leggero" },
                    { "PT", "Leve" },
                    { "RU", "Легкий" },
                    { "AR", "خفيف" },
                };
            }
            if (poidsMods >= 10 && poidsMods < 15)
            {
                return new Dictionary<string, string>()
                {
                    { "FR", "Moyen"},
                    { "EN", "Medium" },
                    { "ES", "Medio" },
                    { "DE", "Mittel" },
                    { "IT", "Medio" },
                    { "PT", "Médio" },
                    { "RU", "Средний" },
                    { "AR", "متوسط" },
                };
            }
            else
            {
                return new Dictionary<string, string>()
                {
                    { "FR", "Lourd"},
                    { "EN", "Heavy" },
                    { "ES", "Pesado" },
                    { "DE", "Schwer" },
                    { "IT", "Pesante" },
                    { "PT", "Pesado" },
                    { "RU", "Тяжелый" },
                    { "AR", "ثقيل" },
                };
            }
        }
    }
}
