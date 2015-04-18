using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;
using Automator.Properties;
using Settings;

namespace Automator.Windows
{
    public partial class FTP : Form
    {
        public FTP()
        {
            InitializeComponent();

            this.Settings = new List<FTPSettings>();
        }

        public List<FTPSettings> Settings { get; set; }

        private void FTPLoad(object sender, EventArgs e)
        {
            this.Icon = Resources.umaxsoft;

            this.InitializeUI();

            mainDataGridView.Rows.Clear();

            for (int i = 0; i < this.Settings.Count; i++)
            {
                mainDataGridView.Rows.Add(this.Settings[i].Host, this.Settings[i].Login, this.Settings[i].Password, this.Settings[i].Folder);
            }
        }

        private void InitializeUI()
        {
            openButton.Text = UI.Manager.GetString("S000011");
            cancelButton.Text = UI.Manager.GetString("S000023");

            ftpServerColumn.HeaderText = UI.Manager.GetString("S000024");
            ftpAccountColumn.HeaderText = UI.Manager.GetString("S000025");
            ftpPasswordColumn.HeaderText = UI.Manager.GetString("S000026");
            ftpRemoteFolderColumn.HeaderText = UI.Manager.GetString("S000027");
        }

        private void openButtonClick(object sender, EventArgs e)
        {
            this.OpenFile();
        }

        private void okButtonClick(object sender, EventArgs e)
        {
            this.Settings = new List<FTPSettings>();
            for (int i = 0; i < mainDataGridView.Rows.Count - 1; i++)
            {
                this.Settings.Add(new FTPSettings()
                                      {
                                          Host = (mainDataGridView.Rows[i].Cells[0].Value != null) ? mainDataGridView.Rows[i].Cells[0].Value.ToString() : string.Empty,
                                          Login = (mainDataGridView.Rows[i].Cells[1].Value != null) ? mainDataGridView.Rows[i].Cells[1].Value.ToString() : string.Empty,
                                          Password = (mainDataGridView.Rows[i].Cells[2].Value != null) ? mainDataGridView.Rows[i].Cells[2].Value.ToString() : string.Empty,
                                          Folder = (mainDataGridView.Rows[i].Cells[3].Value != null) ? mainDataGridView.Rows[i].Cells[3].Value.ToString() : string.Empty
                                      });
            }

            Close();
        }

        private void OpenFile()
        {
            mainOpenFileDialog.FileName = string.Empty;
            mainOpenFileDialog.ShowDialog();
            if (mainOpenFileDialog.FileName == string.Empty)
            {
                return;
            }

            mainDataGridView.Rows.Clear();

            try
            {
                string[] ftpData = File.ReadAllLines(mainOpenFileDialog.FileName, Encoding.Default);

                for (int i = 0; i < ftpData.Length; i++)
                {
                    try
                    {
                        string[] rowData = ftpData[i].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                        if (rowData.Length == 4 || rowData.Length == 3)
                        {
                            mainDataGridView.Rows.Add();
                            for (int k = 0; k < rowData.Length; k++)
                            {
                                mainDataGridView.Rows[mainDataGridView.Rows.Count - 2].Cells[k].Value = rowData[k];
                            }
                            if (rowData.Length == 3)
                            {
                                mainDataGridView.Rows[mainDataGridView.Rows.Count - 2].Cells[3].Value = string.Empty;
                            }
                        }
                    }
                    catch (Exception)
                    {
                    }
                }
            }
            catch (Exception)
            {
                mainDataGridView.Rows.Clear();
            }
        }

        private void cancelButtonClick(object sender, EventArgs e)
        {
            Close();
        }
    }
}
