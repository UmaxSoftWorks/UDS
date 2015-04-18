using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Win32;

namespace Doorway_Studio
{
    class IESound
    {
        private static string MusicFilePath;
        private static void GetMusicFilePath()
        {
            RegistryKey key = Registry.CurrentUser.OpenSubKey(@"AppEvents\Schemes\Apps\Explorer\Navigating\.Current");
            MusicFilePath = (string)key.GetValue(null);
        }
        /// <summary>
        /// Включение/выключение звуков в браузере
        /// </summary>
        public static bool Enabled
        {
            get
            {
                return String.IsNullOrEmpty(MusicFilePath) == false && MusicFilePath != "\"\"";
            }
            set
            {
                string keyValue = string.Empty;

                if (String.IsNullOrEmpty(MusicFilePath))
                {
                    GetMusicFilePath();
                }

                if (value)
                {
                    keyValue = MusicFilePath;
                }
                else
                {
                    keyValue = "\"\"";
                }

                RegistryKey key = Registry.CurrentUser.OpenSubKey(@"AppEvents\Schemes\Apps\Explorer\Navigating\.Current", true);
                key.SetValue(null, keyValue, RegistryValueKind.ExpandString);
            }
        }
    }
}
