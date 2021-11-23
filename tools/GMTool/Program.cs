using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace GMTool
{
    public static class Program
    {
        public static System.Globalization.CultureInfo culture = null;
        /// <summary>
        /// 應用程式的主要進入點。
        /// </summary>
        [STAThread]
        static void Main()
        {
            try
            {

                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new Main());
                while (culture != null)
                {
                    Application.Run(new Main());
                }
                System.Environment.Exit(System.Environment.ExitCode);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
