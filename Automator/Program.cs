using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Windows.Forms;
using Doorway_Studio;

namespace Automator
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            
            // Checking if sniffers active
            if (BlackList.CheckDebuggers())
            {
                MessageBox.Show("Debugger detected!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            //Запуск
            if (BlackList.CheckStudio() > 0)
            {
                MessageBox.Show("The Doorway Studio already running!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (BlackList.CheckAutomator() > 1)
            {
                MessageBox.Show("Automator already running!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Application.Run(new StartUp());
        }
    }

    struct UI
    {
        public static ResourceManager Manager;
    }
}
