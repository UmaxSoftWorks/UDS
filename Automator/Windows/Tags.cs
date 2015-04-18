using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;
using Automator.Properties;
using Settings;

namespace Automator.Windows
{
    public partial class Tags : Form
    {
        public Tags()
        {
            InitializeComponent();
        }

        public List<TagSettings> Settings { get; set; }

        private void TagsLoad(object sender, EventArgs e)
        {
            this.Icon = Resources.umaxsoft;

            this.InitializeUI();

            dataGridView.Rows.Clear();

            for (int i = 0; i < this.Settings.Count; i++)
            {
                dataGridView.Rows.Add(this.Settings[i].File, this.Settings[i].EncodingType);
            }
        }

        private void InitializeUI()
        {
            this.Text = UI.Manager.GetString("S000045");

            pathColumn.HeaderText = UI.Manager.GetString("S000029");
            encodingColumn.HeaderText = UI.Manager.GetString("S000031");

            openButton.Text = UI.Manager.GetString("S000011");
            cancelButton.Text = UI.Manager.GetString("S000023");
        }

        private void openButtonClick(object sender, EventArgs e)
        {
            mainOpenFileDialog.FileName = string.Empty;
            mainOpenFileDialog.ShowDialog();
            if (mainOpenFileDialog.FileName == string.Empty)
            {
                return;
            }

            dataGridView.Rows.Clear();

            try
            {
                string[] data = File.ReadAllLines(mainOpenFileDialog.FileName, Encoding.Default);

                for (int i = 0; i < data.Length; i++)
                {
                    try
                    {
                        string[] rowData = data[i].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                        if (rowData.Length == 1 || rowData.Length == 2)
                        {
                            dataGridView.Rows.Add();
                            if (rowData.Length == 1)
                            {
                                dataGridView.Rows[dataGridView.Rows.Count - 2].Cells[0].Value = rowData[0];
                                dataGridView.Rows[dataGridView.Rows.Count - 2].Cells[1].Value = "0";
                            }
                            else if (rowData.Length == 2)
                            {
                                dataGridView.Rows[dataGridView.Rows.Count - 2].Cells[0].Value = rowData[0];
                                dataGridView.Rows[dataGridView.Rows.Count - 2].Cells[1].Value = rowData[1];
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
                dataGridView.Rows.Clear();
            }
        }

        private void okButtonClick(object sender, EventArgs e)
        {
            this.Settings = new List<TagSettings>();
            for (int i = 0; i < dataGridView.Rows.Count - 1; i++)
            {
                this.Settings.Add(new TagSettings()
                {
                    File = (dataGridView.Rows[i].Cells[0].Value != null) ? dataGridView.Rows[i].Cells[0].Value.ToString() : string.Empty,
                    EncodingType = (dataGridView.Rows[i].Cells[1].Value != null) ? int.Parse(dataGridView.Rows[i].Cells[1].Value.ToString()) : 0
                });
            }

            Close();
        }

        private void cancelButtonClick(object sender, EventArgs e)
        {
            Close();
        }
    }
}
