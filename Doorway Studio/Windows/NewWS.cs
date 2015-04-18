using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Doorway_Studio
{
    public partial class NewWS : Form
    {
        public NewWS()
        {
            InitializeComponent();
        }

        private bool ok;

        /// <summary>
        /// New WorkSpace was added
        /// </summary>
        public bool Ok
        {
            get
            {
                return ok;
            }
            set
            {
                ok = value;
            }
        }

        private void NewWS_Load(object sender, EventArgs e)
        {
            this.ok = false;
            this.Icon = Resource.MainIcon;
            FillOutForm();
            this.IDLabel.Text = GetNextWSID().ToString();
            tipsTimer.Start();
            tipsTimer_Tick(sender, e);
        }

        private void FillOutForm()
        {
            this.Text = View.UILanguageResources.GetString("S0000033");
            nameLabel.Text = View.UILanguageResources.GetString("S0000056");
            commLabel.Text = View.UILanguageResources.GetString("S0000053");
            cancelButton.Text = View.UILanguageResources.GetString("S0000060");
        }

        private int GetNextWSID()
        {
            if (SharedData.WorkSpaces.Count == 0)
            {
                return 0;
            }
            else
            {
                int max = SharedData.WorkSpaces[0].ID;
                for (int i = 1; i < SharedData.WorkSpaces.Count; i++)
                {
                    if (max < SharedData.WorkSpaces[i].ID)
                    {
                        max = SharedData.WorkSpaces[i].ID;
                    }
                }
                return max + 1;
            }
        }

        private void NewWS_FormClosing(object sender, FormClosingEventArgs e)
        {
            tipsTimer.Stop();
        }

        private void OKButton_Click(object sender, EventArgs e)
        {
            //Добавление
            AddWorkSpace();
        }

        private void AddWorkSpace()
        {
            if (WSUniqueName())
            {
                this.ok = true;
                SharedData.WorkSpaces.Add(new WorkSpace(int.Parse(this.IDLabel.Text), nameTextBox.Text, commTextBox.Text.Replace("\r\n", "")));
                SharedData.WorkSpaces[SharedData.WorkSpaces.Count - 1].Save();
                Close();
            }
            else
            {
                MessageBox.Show(View.UILanguageResources.GetString("S0000249"), View.UILanguageResources.GetString("S0000030"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool WSUniqueName()
        {
            for (int i = 0; i < SharedData.WorkSpaces.Count; i++)
            {
                if (nameTextBox.Text == SharedData.WorkSpaces[i].Name)
                {
                    return false;
                }
            }
            return true;
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void tipsTimer_Tick(object sender, EventArgs e)
        {
            TipsTextBox.Text = View.UILanguageResources.GetString("S000100" + new Random().Next(0, 5).ToString());
        }
    }
}
