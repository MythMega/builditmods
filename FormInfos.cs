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

        public void impactSet(string impact)
        {
            this.lblImpact.Text = impact;
        }
        public void modlistSet(string modlist)
        {
            this.lblModList.Text = modlist;
        }
    }
}
