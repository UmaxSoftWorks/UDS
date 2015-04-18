using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Automator.Properties;
using Settings;

namespace Automator.Windows
{
    public partial class Tokens : Form
    {
        public Tokens()
        {
            InitializeComponent();

            this.Settings = new List<FileMacross>();
        }

        public List<FileMacross> Settings { get; set; }

        private void TokensLoad(object sender, EventArgs e)
        {
            this.Icon = Resources.umaxsoft;

            this.InitializeUI();

            fileMacrossEncodingComboBox.SelectedIndex = 0;
            fileMacrossTypeComboBox.SelectedIndex = 0;

            for (int i = 0; i < this.Settings.Count; i++)
            {
                mainDataGridView.Rows.Add(this.Settings[i].Macross, this.Settings[i].File, this.Settings[i].EncodingType, this.Settings[i].Type);
            }
        }

        private void InitializeUI()
        {
            this.Text = UI.Manager.GetString("S000021");

            actionsGroupBox.Text = UI.Manager.GetString("S000028");
            fileMacrossFileLabel.Text = UI.Manager.GetString("S000029");
            fileMacrossLabel.Text = UI.Manager.GetString("S000030");
            fileMacrossEncodingLabel.Text = UI.Manager.GetString("S000031");
            fileMacrossTypeLabel.Text = UI.Manager.GetString("S000032");
            fileMacrossAddFileMacrossButton.Text = UI.Manager.GetString("S000033");
            fileMacrossDeleteFileMacrossButton.Text = UI.Manager.GetString("S000034");

            fileMacrossTypeComboBox.Items.Clear();
            fileMacrossTypeComboBox.Items.Add(UI.Manager.GetString("S000035"));
            fileMacrossTypeComboBox.Items.Add(UI.Manager.GetString("S000036"));
            fileMacrossTypeComboBox.Items.Add(UI.Manager.GetString("S000037"));
            fileMacrossTypeComboBox.Items.Add(UI.Manager.GetString("S000038"));

            mainGroupBox.Text = UI.Manager.GetString("S000021");
            fileMacrossMacrossColumn.HeaderText = UI.Manager.GetString("S000030");
            fileMacrossPathColumn.HeaderText = UI.Manager.GetString("S000029");
            fileMacrossEncodingColumn.HeaderText = UI.Manager.GetString("S000031");
            fileMacrossTypeColumn.HeaderText = UI.Manager.GetString("S000032");

            cancelButton.Text = UI.Manager.GetString("S000023");
        }

        private void okButtonClick(object sender, EventArgs e)
        {
            this.Settings = new List<FileMacross>();
            for (int i = 0; i < mainDataGridView.Rows.Count; i++)
            {
                this.Settings.Add(new FileMacross()
                                      {
                                          Macross = (mainDataGridView.Rows[i].Cells[0].Value != null) ? mainDataGridView.Rows[i].Cells[0].Value.ToString() : string.Empty,
                                          File = (mainDataGridView.Rows[i].Cells[1].Value != null) ? mainDataGridView.Rows[i].Cells[1].Value.ToString() : string.Empty,
                                          EncodingType = (mainDataGridView.Rows[i].Cells[2].Value != null) ? int.Parse(mainDataGridView.Rows[i].Cells[2].Value.ToString()) : 0,
                                          Type = (mainDataGridView.Rows[i].Cells[3].Value != null) ? int.Parse(mainDataGridView.Rows[i].Cells[3].Value.ToString()) : 0
                                      });
            }

            Close();
        }

        private void cancelButtonClick(object sender, EventArgs e)
        {
            Close();
        }

        private void fileSelectButtonClick(object sender, EventArgs e)
        {
            mainOpenFileDialog.FileName = string.Empty;
            mainOpenFileDialog.ShowDialog();
            if (mainOpenFileDialog.FileName == string.Empty)
            {
                return;
            }

            fileMacrossPathTextBox.Text = mainOpenFileDialog.FileName;
        }

        private void addFileMacrossButtonClick(object sender, EventArgs e)
        {
            if (fileMacrossPathTextBox.Text != string.Empty && fileMacrossTextBox.Text != string.Empty)
            {
                // Добавление
                mainDataGridView.Rows.Add();

                mainDataGridView.Rows[mainDataGridView.Rows.Count - 1].Cells[0].Value = fileMacrossTextBox.Text;
                mainDataGridView.Rows[mainDataGridView.Rows.Count - 1].Cells[1].Value = fileMacrossPathTextBox.Text;
                mainDataGridView.Rows[mainDataGridView.Rows.Count - 1].Cells[2].Value = fileMacrossEncodingComboBox.SelectedIndex.ToString();
                mainDataGridView.Rows[mainDataGridView.Rows.Count - 1].Cells[3].Value = fileMacrossTypeComboBox.SelectedIndex.ToString();

                // Очистка
                fileMacrossPathTextBox.Text = string.Empty;
                fileMacrossTextBox.Text = string.Empty;
                fileMacrossEncodingComboBox.SelectedIndex = 0;
                fileMacrossTypeComboBox.SelectedIndex = 0;
            }
        }

        private void deleteFileMacrossButtonClick(object sender, EventArgs e)
        {
            if (mainDataGridView.SelectedRows.Count > 0)
            {
                DataGridViewSelectedRowCollection selectedRow = mainDataGridView.SelectedRows;
                mainDataGridView.Rows.Remove(selectedRow[0]);
            }
        }
    }
}
