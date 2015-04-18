using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Microsoft.Win32;

namespace Doorway_Studio
{
    public partial class StartUp : Form
    {
        public StartUp()
        {
            InitializeComponent();
        }
        
        int WorkType;
        bool Done;
        Thread WorkThread;

        private void startdemoButton_Click(object sender, EventArgs e)
        {
            //Загрузка настроек
            LoadOptions();
            //Старт
            Launch();
        }

        /// <summary>
        /// Загрузка данных
        /// </summary>
        private void Launch()
        {
            WorkType = 1;
            Done = false;
            mainProgressBar.Visible = true;
            WorkThread = new Thread(LoadData);
            WorkThread.Start();
            mainTimer.Start();
        }

        /// <summary>
        /// Запуск главного окна
        /// </summary>
        private void LaunchMainWindow()
        {
            Main MainWindow = new Main();
            MainWindow.ShowDialog();
        }

        /// <summary>
        /// Загрузка данных, непосредственная загрузка
        /// </summary>
        private void LoadData()
        {
            //Инициализация
            SharedData.WorkSpaces = new List<WorkSpace>();

            //Загрузка настроек
            if (!Directory.Exists(Path.Combine(Application.StartupPath, "Data")))
            {
                Directory.CreateDirectory(Path.Combine(Application.StartupPath, "Data"));
                File.SetAttributes(Path.Combine(Application.StartupPath, "Data"), FileAttributes.Normal);
            }
            DownloadNews();

            //Загрузка
            string[] directories = Directory.GetDirectories(Path.Combine(Application.StartupPath, "Data"), "*", SearchOption.TopDirectoryOnly);

            //Загрузка
            for (int i = 0; i < directories.Length; i++)
            {
                try
                {
                    SharedData.WorkSpaces.Add(new WorkSpace(int.Parse(directories[i].Substring(directories[i].LastIndexOf("\\") + 1))));
                    SharedData.WorkSpaces[i].Load();
                }
                catch (Exception)
                {
                }
            }
            Done = true;
        }

        private void DownloadNews()
        {
            WebClient downloader = new WebClient();
            try
            {
                //Загрузка новостей
                downloader.Encoding = Encoding.UTF8;
                SharedData.ProjectNews = downloader.DownloadString("http://umaxsoftworks.com/PrivateArea/News/UDS");
                SharedData.ProjectNews = SharedData.ProjectNews.Substring(SharedData.ProjectNews.IndexOf(">", SharedData.ProjectNews.IndexOf("<body")) + 1);
                SharedData.ProjectNews = SharedData.ProjectNews.Substring(0, SharedData.ProjectNews.IndexOf("</body>"));
            }
            catch (Exception)
            {
                SharedData.ProjectNews = string.Empty;
            }
        }

        private bool CheckFrameworkVersion()
        {
            if (MainSettings.NoFxCheck)
            {
                return true;
            }
            try
            {
                RegistryKey installed_versions = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\NET Framework Setup\NDP");
                string[] version_names = installed_versions.GetSubKeyNames();
                //version names start with 'v', eg, 'v3.5' which needs to be trimmed off before conversion
                double Framework = Convert.ToDouble(version_names[version_names.Length - 1].Remove(0, 1));
                //int SP = Convert.ToInt32(installed_versions.OpenSubKey(version_names[version_names.Length - 1]).GetValue("SP", 0));

                if (Framework >= 3.5)
                {
                    return true; 
                }
            }
            catch (Exception)
            {
            }
            return false;
        }

        private void StartUp_Load(object sender, EventArgs e)
        {
            this.Icon = Resource.MainIcon;
            //Проверка версии фреймворка
            if (!CheckFrameworkVersion())
            {
                if (MessageBox.Show(".Net Framework 3.5 doesn't installed. Please install it!\r\nIgnore this error?", "Error!", MessageBoxButtons.YesNo, MessageBoxIcon.Error) == DialogResult.No)
                {
                    Application.Exit();
                }
            }
            LoadViewSettings();
            FillOutForm();
            //Проверка EULA
            CheckEULA();
            //Запуск
            LoadOptions();
            Launch();
        }

        private void CheckEULA()
        {
            bool EULAAccepted = false;
            //Проверка
            if (File.Exists(Path.Combine(Application.UserAppDataPath, "EULA.txt")))
            {
                try
                {
                    if (bool.Parse(File.ReadAllText(Path.Combine(Application.UserAppDataPath, "EULA.txt"), Encoding.UTF8)))
                    {
                        EULAAccepted = true;
                    }
                }
                catch (Exception)
                {
                }
            }
            //Отображение окошка
            if (!EULAAccepted)
            {
                EULA EULAWindow = new EULA();
                EULAWindow.ShowDialog();
            }
        }

        /// <summary>
        /// Проверка лицензии
        /// </summary>
        private void CheckLicense()
        {
            //Загрузка настроек
            LoadOptions();

            Done = true;
        }

        /// <summary>
        /// Загрузка настроек
        /// </summary>
        private void LoadOptions()
        {
            //Загрузка настроек вида
            if (File.Exists(Path.Combine(Application.StartupPath, "options.ini")))
            {
                try
                {
                    string[] data = File.ReadAllLines(Path.Combine(Application.StartupPath, "options.ini"), Encoding.UTF8);
                    for (int i = 0; i < data.Length; i++)
                    {
                        if (data[i].StartsWith("MaxTasks="))
                        {
                            MainSettings.MaxParallelTaks = int.Parse(data[i].Substring(9));
                        }
                        else if (data[i].StartsWith("DeleteTasksAfter="))
                        {
                            MainSettings.DeleteFinishedTasksAfterDays = int.Parse(data[i].Substring(17));
                        }
                        else if (data[i].StartsWith("ClearFolders="))
                        {
                            MainSettings.ClearFolderWhereDoorwaysMastBeSaved = bool.Parse(data[i].Substring(13));
                        }
                        else if (data[i].StartsWith("WinStart="))
                        {
                            MainSettings.StartWithWindows = bool.Parse(data[i].Substring(9));
                        }
                        else if (data[i].StartsWith("MinStart="))
                        {
                            MainSettings.MinimizedStart = bool.Parse(data[i].Substring(9));
                            if (MainSettings.StartWithWindows)
                            {
                                MainSettings.MinimizedStart = true;
                            }
                        }
                        else if (data[i].StartsWith("UpdateOnStartUp="))
                        {
                            MainSettings.UpdateAtStartUp = bool.Parse(data[i].Substring(16));
                        }
                        else if (data[i].StartsWith("Baloons="))
                        {
                            MainSettings.ShowBaloons = bool.Parse(data[i].Substring(8));
                        }
                        else if (data[i].StartsWith("BaloonsTime="))
                        {
                            MainSettings.ShowBaloonsTime = int.Parse(data[i].Substring(12));
                        }
                    }
                }
                catch (Exception) {                }
            }
            else
            {
                MainSettings.DeleteFinishedTasksAfterDays = 25;
                MainSettings.MaxParallelTaks = 3;

                MainSettings.ShowBaloons = true;
                MainSettings.ShowBaloonsTime = 30;
            }
        }

        /// <summary>
        /// Загрузка настроек внешнего вида
        /// </summary>
        private void LoadViewSettings()
        {
            View.ShowNews = true;
            View.ShowTips = true;
            View.UILanguage = 0;
            //Загрузка настроек вида
            if (File.Exists(Path.Combine(Application.StartupPath, "view.ini")))
            {
                try
                {
                    string[] data = File.ReadAllLines(Path.Combine(Application.StartupPath, "view.ini"), Encoding.UTF8);
                    for (int i = 0; i < data.Length; i++)
                    {
                        if (data[i].StartsWith("News="))
                        {
                            try
                            {
                                View.ShowNews = bool.Parse(data[i].Substring(5));
                            }
                            catch (Exception)
                            {
                                View.ShowNews = true;
                            }
                        }
                        else if (data[i].StartsWith("Tips="))
                        {
                            try
                            {
                                View.ShowTips = bool.Parse(data[i].Substring(5));
                            }
                            catch (Exception)
                            {
                                View.ShowTips = true;
                            }
                        }
                        else if (data[i].StartsWith("Language="))
                        {
                            try
                            {
                                View.UILanguage = int.Parse(data[i].Substring(9));
                            }
                            catch (Exception)
                            {
                                View.UILanguage = 0;
                            }
                        }
                    }
                }
                catch (Exception)
                {
                }
            }

            switch (View.UILanguage)
            {
                case 0:
                    {
                        View.UILanguageResources = new System.Resources.ResourceManager("Doorway_Studio.English", typeof(English).Assembly);
                        break;
                    }
                case 1:
                    {
                        View.UILanguageResources = new System.Resources.ResourceManager("Doorway_Studio.Russian", typeof(Russian).Assembly);
                        break;
                    }
                default:
                    {
                        View.UILanguageResources = new System.Resources.ResourceManager("Doorway_Studio.English", typeof(English).Assembly);
                        break;
                    }
            }
        }

        /// <summary>
        /// Перевод текста элементов формы
        /// </summary>
        private void FillOutForm()
        {
            this.Text = View.UILanguageResources.GetString("S0000003");
            this.actionLabel.Text = View.UILanguageResources.GetString("S0000004");
            this.statusLabel.Text = View.UILanguageResources.GetString("S0000005");
            this.startdemoButton.Text = View.UILanguageResources.GetString("S0000006");
            this.retryButton.Text = View.UILanguageResources.GetString("S0000007");
            this.exitButton.Text = View.UILanguageResources.GetString("S0000008");
        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void mainTimer_Tick(object sender, EventArgs e)
        {
            switch (WorkType)
            {
                case 0:
                    {
                        if (Done)
                        {
                            mainTimer.Stop();
                            mainProgressBar.Visible = false;
                            startdemoButton.Enabled = true;
                            retryButton.Enabled = true;
                            Launch();
                        }
                        actionText.Text = View.UILanguageResources.GetString("S0000009");
                        statusText.Text = View.UILanguageResources.GetString("S0000009");
                        break;
                    }
                case 1:
                    {
                        if (Done)
                        {
                            this.Visible = false;
                            mainTimer.Stop();

                            LaunchMainWindow();
                            if (SharedData.Exit)
                            {
                                Application.Exit();
                            }
                        }
                        actionText.Text = View.UILanguageResources.GetString("S0000010");
                        statusText.Text = View.UILanguageResources.GetString("S0000011");
                        break;
                    }
            }
        }

        private void retryButton_Click(object sender, EventArgs e)
        {
            //Проверка лицензии
            WorkType = 0;
            Done = false;

            startdemoButton.Enabled = false;
            mainProgressBar.Visible = true;
            WorkThread = new Thread(CheckLicense);
            WorkThread.Start();
            mainTimer.Start();
        }
    }
}
