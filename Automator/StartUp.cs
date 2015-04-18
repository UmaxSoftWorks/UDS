using System;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;
using Automator.Localization;
using Automator.Properties;
using Doorway_Studio;

namespace Automator
{
    public partial class StartUp : Form
    {
        public StartUp()
        {
            InitializeComponent();

            if (File.Exists("view.ini"))
            {
                string[] items = File.ReadAllLines("view.ini");

                for (int i = 0; i < items.Length; i++)
                {
                    if (items[i] == "Language=0")
                    {
                        UI.Manager = English.ResourceManager;
                    }

                    if (items[i] == "Language=1")
                    {
                        UI.Manager = Russian.ResourceManager;
                    }
                }
            }
            else
            {
                UI.Manager = English.ResourceManager;
            }
        }

        private void StartUpLoad(object sender, EventArgs e)
        {
            this.Icon = Resources.umaxsoft;

            this.Text = UI.Manager.GetString("S000001");
            actionText.Text = UI.Manager.GetString("S000002");

            retryButton.Text = UI.Manager.GetString("S000003");
            exitButton.Text = UI.Manager.GetString("S000004");

            licenseBackgroundWorker.RunWorkerAsync();
        }

        private void mainBackgroundWorkerRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.Visible = false;
            Application.DoEvents();

            if (Data.Instance.Items.Count == 0)
            {
                MessageBox.Show(UI.Manager.GetString("S000008"), UI.Manager.GetString("S000006"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                Main mainWindow = new Main();
                mainWindow.ShowDialog();
            }

            Close();
        }

        private void Launch()
        {
            actionText.Text = UI.Manager.GetString("S000005");
            mainBackgroundWorker.RunWorkerAsync();
        }

        private void retryButtonClick(object sender, EventArgs e)
        {
            licenseBackgroundWorker.CancelAsync();
            mainBackgroundWorker.CancelAsync();

            actionText.Text = UI.Manager.GetString("S000002");
            licenseBackgroundWorker.RunWorkerAsync();
        }

        private void exitButtonClick(object sender, EventArgs e)
        {
            licenseBackgroundWorker.CancelAsync();
            mainBackgroundWorker.CancelAsync();

            Close();
        }

        private void mainBackgroundWorkerDoWork(object sender, DoWorkEventArgs e)
        {
            Data data = Data.Instance;
        }

        private void licenseBackgroundWorkerDoWork(object sender, DoWorkEventArgs e)
        {
        }

        private void licenseBackgroundWorkerRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.Launch();
        }
    }
}
