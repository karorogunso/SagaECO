using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.CSharp;
using System.Reflection;
using System.Windows.Forms;
using System.CodeDom.Compiler;
using System.IO;

namespace GMTool
{
    public class QueryManager : SagaLib.Singleton<QueryManager>
    {
        Dictionary<string, Query> querys;
        public Dictionary<string, Query> Querys { get { return this.querys; } }

        public QueryManager()
        {
            querys = new Dictionary<string, Query>();
        }

        public int LoadScript(string path)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>() { { "CompilerVersion", "v3.5" } };
            CSharpCodeProvider provider = new CSharpCodeProvider(dic);
            int eventcount = 0;
            try
            {
                string[] files = Directory.GetFiles(path, "*.cs", SearchOption.AllDirectories);
                Assembly newAssembly;
                int tmp;
                if (files.Length > 0)
                {
                    newAssembly = CompileScript(files, provider);
                    if (newAssembly != null)
                    {
                        tmp = LoadAssembly(newAssembly);
                        eventcount += tmp;
                    }
                }
                files = Directory.GetFiles(path, "*.dll", SearchOption.AllDirectories);
                foreach (string i in files)
                {
                    newAssembly = Assembly.LoadFile(System.IO.Path.GetFullPath(i));
                    if (newAssembly != null)
                    {
                        tmp = LoadAssembly(newAssembly);
                        eventcount += tmp;
                    }
                }
            }
            catch (Exception)
            {
                return -1;
            }
            return eventcount;
        }
        public Query CompileString(string SourceCode)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>() { { "CompilerVersion", "v3.5" } };
            CSharpCodeProvider provider = new CSharpCodeProvider(dic);
            try
            {
                string[] src={SourceCode};
                Assembly newAssembly = CompileScriptString(src, provider);
                return GetAssembly(newAssembly);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        private Assembly CompileScript(string[] Source, CodeDomProvider Provider)
        {
            //ICodeCompiler compiler = Provider.;
            CompilerParameters parms = new CompilerParameters();
            CompilerResults results;

            // Configure parameters
            parms.CompilerOptions = "/target:library /optimize";
            parms.GenerateExecutable = false;
            parms.GenerateInMemory = true;
            parms.IncludeDebugInformation = true;
            parms.ReferencedAssemblies.Add(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles) + @"\Reference Assemblies\Microsoft\Framework\v3.5\System.Data.DataSetExtensions.dll");
            parms.ReferencedAssemblies.Add(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles) + @"\Reference Assemblies\Microsoft\Framework\v3.5\System.Core.dll");
            parms.ReferencedAssemblies.Add(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles) + @"\Reference Assemblies\Microsoft\Framework\v3.5\System.Xml.Linq.dll");
            parms.ReferencedAssemblies.Add("System.dll");
            parms.ReferencedAssemblies.Add("System.Data.dll");
            parms.ReferencedAssemblies.Add("System.Xml.dll");
            parms.ReferencedAssemblies.Add("System.Windows.Forms.dll");
            parms.ReferencedAssemblies.Add("GMTool.exe");
            //parms.ReferencedAssemblies.Add("SagaLib.dll");
            //parms.ReferencedAssemblies.Add("SagaDB.dll");
            //parms.ReferencedAssemblies.Add("SagaMap.exe");
            // Compile
            results = Provider.CompileAssemblyFromFile(parms, Source);
            if (results.Errors.Count > 0)
            {
                foreach (CompilerError error in results.Errors)
                {
                    MessageBox.Show("Compile Error:" + error.ErrorText);
                    MessageBox.Show("File:" + error.FileName + ":" + error.Line);
                }
                return null;
            }
            //get a hold of the actual assembly that was generated
            return results.CompiledAssembly;
        }
        private Assembly CompileScriptString(string[] SourceCode, CodeDomProvider Provider)
        {
            //ICodeCompiler compiler = Provider.;
            CompilerParameters parms = new CompilerParameters();
            CompilerResults results;

            // Configure parameters
            parms.CompilerOptions = "/target:library /optimize";
            parms.GenerateExecutable = false;
            parms.GenerateInMemory = true;
            parms.IncludeDebugInformation = true;
            parms.ReferencedAssemblies.Add(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles) + @"\Reference Assemblies\Microsoft\Framework\v3.5\System.Data.DataSetExtensions.dll");
            parms.ReferencedAssemblies.Add(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles) + @"\Reference Assemblies\Microsoft\Framework\v3.5\System.Core.dll");
            parms.ReferencedAssemblies.Add(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles) + @"\Reference Assemblies\Microsoft\Framework\v3.5\System.Xml.Linq.dll");
            parms.ReferencedAssemblies.Add("System.dll");
            parms.ReferencedAssemblies.Add("System.Data.dll");
            parms.ReferencedAssemblies.Add("System.Xml.dll");
            parms.ReferencedAssemblies.Add("System.Windows.Forms.dll");
            parms.ReferencedAssemblies.Add("GMTool.exe");
            //parms.ReferencedAssemblies.Add("SagaLib.dll");
            //parms.ReferencedAssemblies.Add("SagaDB.dll");
            //parms.ReferencedAssemblies.Add("SagaMap.exe");
            // Compile
            results = Provider.CompileAssemblyFromSource(parms, SourceCode);
            if (results.Errors.Count > 0)
            {
                foreach (CompilerError error in results.Errors)
                {
                    MessageBox.Show("Compile Error:" + error.ErrorText);
                }
                return null;
            }
            //get a hold of the actual assembly that was generated
            return results.CompiledAssembly;
        }
        private Query GetAssembly(Assembly newAssembly)
        {
            Module[] newScripts = newAssembly.GetModules();
            foreach (Module newScript in newScripts)
            {
                Type[] types = newScript.GetTypes();
                foreach (Type npcType in types)
                {
                    try
                    {
                        if (npcType.IsAbstract == true) continue;
                        if (npcType.GetCustomAttributes(false).Length > 0) continue;
                        Query newEvent;
                        try
                        {
                            newEvent = (Query)newAssembly.CreateInstance(npcType.FullName);
                            return newEvent;
                        }
                        catch (Exception)
                        {
                            continue;
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                }
            }
            return null;
        }
        private int LoadAssembly(Assembly newAssembly)
        {
            Module[] newScripts = newAssembly.GetModules();
            int count = 0;
            foreach (Module newScript in newScripts)
            {
                Type[] types = newScript.GetTypes();
                foreach (Type npcType in types)
                {
                    try
                    {
                        if (npcType.IsAbstract == true) continue;
                        if (npcType.GetCustomAttributes(false).Length > 0) continue;
                        Query newEvent;
                        try
                        {
                            newEvent = (Query)newAssembly.CreateInstance(npcType.FullName);
                        }
                        catch (Exception)
                        {
                            continue;
                        }
                        if (!this.querys.ContainsKey(newEvent.QueryTitle) && newEvent.QueryTitle != "")
                        {
                            this.querys.Add(newEvent.QueryTitle, newEvent);
                        }
                        else
                        {
                            //重複，吃掉
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                    count++;
                }
            }
            return count;
        }
    }
}
