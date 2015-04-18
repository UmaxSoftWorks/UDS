using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

using System.IO;
using System.Resources;
using System.Diagnostics;

namespace Doorway_Studio
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] StartParameters)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Разбор параметров запуска
            for (int i = 0; i < StartParameters.Length; i++)
            {
                if (StartParameters[i] == "-auto")
                {
                    MainSettings.MinimizedStart = true;
                }
                else if (StartParameters[i] == "-debug")
                {
                    MainSettings.Debug = true;
                }
                else if (StartParameters[i] == "-nofxcheck")
                {
                    MainSettings.NoFxCheck = true;
                }
            }
            //Проверка наличия библиотек
            if (!File.Exists("XPTable.dll"))
            {
                MessageBox.Show("File XPTable.dll not found!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (!File.Exists("ZedGraph.dll"))
            {
                MessageBox.Show("File ZedGraph.dll not found!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Checking if sniffers active
            if (BlackList.CheckDebuggers())
            {
                MessageBox.Show("Debugger detected!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            //Запуск
            if (BlackList.CheckStudio() > 1)
            {
                MessageBox.Show("The Doorway Studio already running!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (BlackList.CheckAutomator() > 0)
            {
                MessageBox.Show("Automator already running!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Application.Run(new StartUp());
        }
    }

    struct SharedData
    {
        public static bool Exit;

        public static int SelectedWorkSpace;
        public static DateTime SelectedDate;
        public static List<WorkSpace> WorkSpaces;

        public static string ProjectNews;
    }

    struct MainSettings
    {
        /// <summary>
        /// Старт с Windows
        /// </summary>
        public static bool StartWithWindows;
        /// <summary>
        /// Старт с виндой или запуск в свернутом состоянии
        /// </summary>
        public static bool MinimizedStart;
        /// <summary>
        /// Максимум одновременно выполняющихся заданий
        /// </summary>
        public static int MaxParallelTaks;
        /// <summary>
        /// Количество дней, через которые нужно удалить выполеные задания
        /// </summary>
        public static int DeleteFinishedTasksAfterDays;
        /// <summary>
        /// Удаление содержимого папки куда должны быть сохранены дорвеи
        /// </summary>
        public static bool ClearFolderWhereDoorwaysMastBeSaved;
        /// <summary>
        /// Отображение всплывающих сообщений
        /// </summary>
        public static bool ShowBaloons;
        /// <summary>
        /// Время отображения всплывающих сообщений
        /// </summary>
        public static int ShowBaloonsTime;
        /// <summary>
        /// Дебаг режим для лога
        /// </summary>
        public static bool Debug;
        /// <summary>
        /// Автоматическая проверка обновлений при старте программы
        /// </summary>
        public static bool UpdateAtStartUp;
        /// <summary>
        /// Отмена проверки версии фреймворка
        /// </summary>
        public static bool NoFxCheck;
    }

    public struct View
    {
        /// <summary>
        /// Оьображение новостей
        /// </summary>
        public static bool ShowNews;
        /// <summary>
        /// Отображение подсказок
        /// </summary>
        public static bool ShowTips;

        /// <summary>
        /// Язык: 0 - английский; 1 - русский
        /// </summary>
        public static int UILanguage;
        public static ResourceManager UILanguageResources;
    }
}
