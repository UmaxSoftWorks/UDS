using System;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace Doorway_Studio
{
    public partial class EULA : Form
    {
        public EULA()
        {
            InitializeComponent();
        }

        private void EULA_Load(object sender, EventArgs e)
        {
            this.Icon = Resource.MainIcon;
            FillOutForm();
        }

        private void FillOutForm()
        {
            this.Text = View.UILanguageResources.GetString("S0000061");
            EULALinkLabel.Text = View.UILanguageResources.GetString("S0000061");
            EULACheckBox.Text = View.UILanguageResources.GetString("S0000062");
            OLALabel.Text = View.UILanguageResources.GetString("S0000063");
        }

        private void OKButton_Click(object sender, EventArgs e)
        {
            WriteFile();
            Close();
        }

        private void EULACheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (EULACheckBox.Checked)
            {
                OKButton.Enabled = true;
            }
            else
            {
                OKButton.Enabled = false;
            }
            WriteFile();
        }

        /// <summary>
        /// Запись файлика
        /// </summary>
        private void WriteFile()
        {
            File.WriteAllText(Application.UserAppDataPath + "\\EULA.txt", EULACheckBox.Checked.ToString(), Encoding.UTF8);
        }

        private void EULA_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!EULACheckBox.Checked)
            {
                e.Cancel = true;
            }
        }

        private void EULALinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://umaxsoft.com/projects/bin/uds-eula.html");
        }
    }
}
