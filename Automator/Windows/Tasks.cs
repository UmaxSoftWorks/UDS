using System;
using System.Windows.Forms;
using Automator.Items;
using Automator.Properties;

namespace Automator.Windows
{
    public partial class Tasks : Form
    {
        public Tasks()
        {
            InitializeComponent();
        }

        public int WorkSpaceIndex { get; set; }
        public Task Task { get; set; }

        private void TasksLoad(object sender, EventArgs e)
        {
            this.Icon = Resources.umaxsoft;

            this.InitializeUI();

            for (int i = 0; i < Data.Instance.Items.Count; i++)
            {
                TreeNode item = new TreeNode(Data.Instance.Items[i].Name, 0, 0);
                for (int k = 0; k < Data.Instance.Items[i].Tasks.Count; k++)
                {
                    item.Nodes.Add(new TreeNode(Data.Instance.Items[i].Tasks[k].Name, 1, 1));
                }

                mainTreeView.Nodes.Add(item);
            }

            mainTreeView.ExpandAll();
        }

        private void InitializeUI()
        {
            this.Text = UI.Manager.GetString("S000019");

            cancelButton.Text = UI.Manager.GetString("S000023");
        }

        private void mainTreeViewNodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Node.Parent == null)
            {
                return;
            }

            this.WorkSpaceIndex = e.Node.Parent.Index;
            this.Task = Data.Instance.Items[e.Node.Parent.Index].Tasks[e.Node.Index];
        }

        private void okButtonClick(object sender, EventArgs e)
        {
            Close();
        }

        private void cancelButtonClick(object sender, EventArgs e)
        {
            this.Task = null;
            Close();
        }
    }
}
