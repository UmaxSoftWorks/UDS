using System.Diagnostics;

namespace Doorway_Studio
{
    class BlackList
    {
        /// <summary>
        /// Checking if sniffers running
        /// </summary>
        /// <returns>true - sniffers running; false - all ok</returns>
        public static bool CheckDebuggers()
        {
            // Wireshark
            Process[] processes = Process.GetProcessesByName("wireshark");
            if (processes.Length > 0)
            {
                return true;
            }

            // Microsoft network monitor
            processes = Process.GetProcessesByName("netmon");
            if (processes.Length > 0)
            {
                return true;
            }

            return false;
        }

        public static int CheckStudio()
        {
            return Process.GetProcessesByName("Umax Doorway Studio").Length;
        }

        public static int CheckAutomator()
        {
            return Process.GetProcessesByName("Automator").Length;
        }
    }
}
