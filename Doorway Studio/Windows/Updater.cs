using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.Net;
using System.Threading;
using System.IO;

namespace Doorway_Studio
{
    public partial class Updater : Form
    {
        public Updater()
        {
            InitializeComponent();
        }

        private string url;

        public string URL
        {
            get
            {
                return url;
            }
            set
            {
                this.url = value;
            }
        }

        private void AutoUpdate_Load(object sender, EventArgs e)
        {
            this.Icon = Resource.MainIcon;
            //
            this.Text = View.UILanguageResources.GetString("S0000256");
            cancelButton.Text = View.UILanguageResources.GetString("S0000060");
            openFolderCheckBox.Text = View.UILanguageResources.GetString("S0000257");
            //Старт загрузки
            WorkData.Done = false;
            WorkData.Error = false;
            WorkData.MainThread = new Thread(Work);
            WorkData.MainThread.Start();

            mainTimer.Start();
        }

        private struct WData
        {
            public bool Done;
            public bool Error;
            public long FileLength;
            public long FileDownloaded;

            public Thread MainThread;
        }

        private WData WorkData;

        private void Work()
        {
            Stream strResponse = null;
            FileStream strLocal = null;
            HttpWebRequest webRequest = null;
            HttpWebResponse webResponse = null;
            try
            {
                //Скачивание
                // Create a request to the file we are downloading
                webRequest = (HttpWebRequest)WebRequest.Create(url);
                // Set default authentication for retrieving the file
                webRequest.Credentials = CredentialCache.DefaultCredentials;
                // Retrieve the response from the server
                webResponse = (HttpWebResponse)webRequest.GetResponse();
                // Ask the server for the file size and store it
                WorkData.FileLength = (int)webResponse.ContentLength;
                // Open the URL for download 
                strResponse = webResponse.GetResponseStream();
                //strResponse = wcDownload.OpenRead(WorkData.Files[FileIndex]);
                // Create a new file stream where we will be saving the data (local drive)

                string fileFolder = Application.StartupPath;
                if (!fileFolder.EndsWith("\\"))
                {
                    fileFolder += "\\";
                }
                strLocal = new FileStream(fileFolder + url.Substring(url.LastIndexOf("/") + 1), FileMode.Create, FileAccess.Write, FileShare.None);

                // It will store the current number of bytes we retrieved from the server
                int bytesSize = 0;
                // A buffer for storing and writing the data retrieved from the server
                byte[] downBuffer = new byte[2048];

                // Loop through the buffer until the buffer is empty
                while ((bytesSize = strResponse.Read(downBuffer, 0, downBuffer.Length)) > 0)
                {
                    // Write the data from the buffer to the local hard drive
                    strLocal.Write(downBuffer, 0, bytesSize);
                    // Invoke the method that updates the form's label and progress bar
                    WorkData.FileDownloaded += bytesSize;
                }
            }
            catch (Exception)
            {
                WorkData.Error = true;
            }
            finally
            {
                // When the above code has ended, close the streams
                if (strResponse != null)
                {
                    strResponse.Close();
                }
                if (strLocal != null)
                {
                    strLocal.Close();
                }
                if (webRequest != null)
                {
                    webRequest.Abort();
                    webRequest = null;
                }
                if (webResponse != null)
                {
                    webResponse.Close();
                    webResponse = null;
                }
            }
            WorkData.Done = true;
        }

        private void OKButton_Click(object sender, EventArgs e)
        {
            if (openFolderCheckBox.Checked)
            {
                System.Diagnostics.Process.Start(Application.StartupPath);
            }
            Close();
        }

        private void mainTimer_Tick(object sender, EventArgs e)
        {
            if (WorkData.Done)
            {
                mainTimer.Stop();
                if (WorkData.Error)
                {
                    MessageBox.Show(View.UILanguageResources.GetString("S0000030"), View.UILanguageResources.GetString("S0000030"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    OKButton.Enabled = true;
                }
            }
            try
            {
                statusLabel.Text = View.UILanguageResources.GetString("S0000005") + " " + (WorkData.FileDownloaded / 1024).ToString() + "/" + (WorkData.FileLength / 1024).ToString() + View.UILanguageResources.GetString("S0000258");

                statusProgressBar.Maximum = (int)WorkData.FileLength;
                statusProgressBar.Value = (int)WorkData.FileDownloaded;
            }
            catch (Exception)
            {
            }
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            Stop();
            Close();
        }

        private void AutoUpdate_FormClosing(object sender, FormClosingEventArgs e)
        {
            Stop();
        }

        private void Stop()
        {
            try
            {
                mainTimer.Stop();
                WorkData.MainThread.Abort();
            }
            catch (Exception)
            {
            }
        }

    }
}
