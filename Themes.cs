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
    public partial class Themes : Form
    {
        private Commons Commons = new Commons();
        
        public Themes()
        {
            InitializeComponent();
        }

        public void updateTheme(Form form)
        {
            string theme = Commons.GetProfileValue("theme");
            List<Control> allControls = new List<Control>();

            foreach (Control control in form.Controls)
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
                    
                    case MenuStrip menuStrip:
                        menuStrip.BackColor = backColor;
                        form.BackColor = backColor;

                        foreach (ToolStripMenuItem item in menuStrip.Items.OfType<ToolStripMenuItem>())
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
                        break;

                    default:
                        // Action par défaut pour les autres types de contrôles
                        // ...
                        break;
                }
            }
        }
    }
}
