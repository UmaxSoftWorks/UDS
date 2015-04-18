using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Automator.Properties;

namespace Automator.Windows
{
    public partial class URLS : Form
    {
        public URLS()
        {
            InitializeComponent();
            this.URLs = new List<string>();
        }

        public List<string> URLs { get; set; }

        private void URLSLoad(object sender, EventArgs e)
        {
            this.Icon = Resources.umaxsoft;

            this.InitializeUI();

            StringBuilder builder = new StringBuilder();

            for (int i = 0; i < this.URLs.Count; i++)
            {
                builder.AppendLine(this.URLs[i]);
            }

            mainTextBox.Text = builder.ToString();
        }

        private void InitializeUI()
        {
            this.Text = UI.Manager.GetString("S000020");

            openButton.Text = UI.Manager.GetString("S000011");
            cancelButton.Text = UI.Manager.GetString("S000023");
        }

        private void cancelButtonClick(object sender, EventArgs e)
        {
            Close();
        }

        private void okButtonClick(object sender, EventArgs e)
        {
            this.URLs = mainTextBox.Lines.ToList();

            Close();
        }

        private void openButtonClick(object sender, EventArgs e)
        {
            mainOpenFileDialog.FileName = string.Empty;
            mainOpenFileDialog.ShowDialog();
            if (mainOpenFileDialog.FileName == string.Empty)
            {
                return;
            }

            mainTextBox.Text = File.ReadAllText(mainOpenFileDialog.FileName, Encoding.Default);
        }
    }
}
