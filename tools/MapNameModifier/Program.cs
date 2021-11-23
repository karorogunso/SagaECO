using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace MapNameModifier
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new Form1());
            }
            else
            {
                /*
                 *     <Compile Include="Templetes\復活戰士.cs" />
    <Compile Include="Test.cs" />
    <Compile Include="Properties\AssemblyInfo.cs" />
*/
                string[] files = System.IO.Directory.GetFiles("./", "*.cs", System.IO.SearchOption.AllDirectories);
                System.IO.StreamReader sr = new System.IO.StreamReader(args[0] + ".txt");
                string format = sr.ReadToEnd();
                string pattern = "    <Compile Include=\"{0}\" />\r\n";
                string buf = "";
                foreach (string i in files)
                {
                    buf += string.Format(pattern, i.Replace("./", ""));
                }
                format = format.Replace('{', '[');
                format = format.Replace('}', ']');
                format = format.Replace('%', '{');
                format = format.Replace('&', '}');
                
                buf = string.Format(format, buf);
                buf = buf.Replace('[', '{');
                buf = buf.Replace(']', '}');
                System.IO.StreamWriter sw = new System.IO.StreamWriter(args[0] + ".csproj", false);
                sw.Write(buf);
                sw.Flush();
                sw.Close();
            }
        }
    }
}
