using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace JungleTimer
{
    public static class GlobalVar
    {
        public static int Mode = 0;
        public static bool isSwitchingMode = true;
    }
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
            while (GlobalVar.isSwitchingMode)
            {
                GlobalVar.isSwitchingMode = false;
                if (GlobalVar.Mode == 0) { Application.Run(new JungleTimer()); }
                else if (GlobalVar.Mode == 1) { Application.Run(new JungleTimer_Compact()); }
                else { Application.Run(new JungleTimer_Hanger()); }
            }
        }
    }
}
