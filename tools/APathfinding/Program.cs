using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace APathfinding
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            SagaLib.VirtualFileSystem.VirtualFileSystemManager.Instance.Init(SagaLib.VirtualFileSystem.FileSystems.Real, ""); 
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
            
        }
    }
}
