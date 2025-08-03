using System;
using System.Collections.Generic;
using System.CodeDom.Compiler;
using System.Reflection;
using System.IO;
using System.Linq;
using System.Text;

using SagaLib;
using SagaDB.Actor;
using SagaMap.Localization;
using SagaMap.Localization.Languages;
using SagaMap.Scripting;
using Microsoft.CSharp;

namespace SagaMap.Manager
{
    public class ScriptManager : Singleton<ScriptManager>
    {
        Dictionary<uint, Event> events;
        string path;

        ActorPC variableHolder;
        Dictionary<string, MultiRunTask> timers = new Dictionary<string, MultiRunTask>();

        public Dictionary<uint, Event> Events { get { return this.events; } }
        public ActorPC VariableHolder { get { return this.variableHolder; } set { this.variableHolder = value; } }
        public Dictionary<string, MultiRunTask> Timers { get { return this.timers; } }
        
        public ScriptManager()
        {
            events = new Dictionary<uint, Event>();
        }

        public void ReloadScript()
        {
            ClientManager.noCheckDeadLock = true;
            try
            {
                this.events.Clear();
                LoadScript(path);
            }
            catch { }
            ClientManager.noCheckDeadLock = false;

        }

        public void LoadScript(string path)
        {
            Logger.ShowInfo("Loading uncompiled scripts");
            Dictionary<string, string> dic = new Dictionary<string, string>() { { "CompilerVersion", "v4.0" } };
            CSharpCodeProvider provider = new CSharpCodeProvider(dic);
            int eventcount = 0;
            this.path = path;
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
                        Logger.ShowInfo(string.Format("Containing {0} Events", tmp));
                        eventcount += tmp;
                    }
                }
                Logger.ShowInfo("Loading compiled scripts....");
                files = Directory.GetFiles(path, "*.dll", SearchOption.AllDirectories);
                foreach (string i in files)
                {
                    newAssembly = Assembly.LoadFile(System.IO.Path.GetFullPath(i));
                    if (newAssembly != null)
                    {
                        tmp = LoadAssembly(newAssembly);
                        Logger.ShowInfo(string.Format("Loading {1}, Containing {0} Events", tmp, i));
                        eventcount += tmp;
                    }
                }

                if (!events.ContainsKey(12001501))
                    events.Add(12001501, new Scripting.DungeonNorth());
                if (!events.ContainsKey(12001502))
                    events.Add(12001502, new Scripting.DungeonEast());
                if (!events.ContainsKey(12001503))
                    events.Add(12001503, new Scripting.DungeonSouth());
                if (!events.ContainsKey(12001504))
                    events.Add(12001504, new Scripting.DungeonWest());
                if (!events.ContainsKey(12001505))
                    events.Add(12001505, new Scripting.DungeonExit());
                if (!events.ContainsKey(0xF1000000))
                    events.Add(0xF1000000, new Scripting.WestFortGate());
                if (!events.ContainsKey(0xF1000001))
                    events.Add(0xF1000001, new Scripting.WestFortField());

            
            }
            catch (Exception ex)
            {
                Logger.ShowError(ex);
            }


            Logger.ShowInfo(string.Format("Totally {0} Events Added", eventcount));
        }

        private Assembly CompileScript(string[] Source, CSharpCodeProvider Provider)
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
            parms.ReferencedAssemblies.Add("SagaLib.dll");
            parms.ReferencedAssemblies.Add("SagaDB.dll");
            parms.ReferencedAssemblies.Add("SagaMap.exe");
            foreach (string i in Configuration.Instance.ScriptReference)
            {
                parms.ReferencedAssemblies.Add(i);
            }
            // Compile
            results = Provider.CompileAssemblyFromFile(parms, Source);
            if (results.Errors.HasErrors)
            {
                foreach (CompilerError error in results.Errors)
                {
                    if (!error.IsWarning)
                    {
                        Logger.ShowError("Compile Error:" + error.ErrorText, null);
                        Logger.ShowError("File:" + error.FileName + ":" + error.Line, null);
                    }
                }
                return null;
            }
            //get a hold of the actual assembly that was generated
            return results.CompiledAssembly;
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
                        Event newEvent;
                        try
                        {
                            newEvent = (Event)Activator.CreateInstance(npcType);
                        }
                        catch (Exception)
                        {
                            continue;
                        }
                        if (!this.events.ContainsKey(newEvent.EventID) && newEvent.EventID != 0)
                        {
                            this.events.Add(newEvent.EventID, newEvent);
                        }
                        else
                        {
                            if (newEvent.EventID != 0)
                                Logger.ShowWarning(string.Format("EventID:{0} already exists, Class:{1} droped", newEvent.EventID, npcType.FullName));
                        }
                    }
                    catch (Exception ex)
                    {
                        Logger.ShowError(ex);
                    }
                    count++;
                }
            }
            return count;
        }

        public uint GetFreeIDSince(uint since, int max)
        {
            for (uint i = since; i < since + max; i++)
            {
                if (!events.ContainsKey(i))
                    return i;
            }
            return 0xFFFFFFFF;
        }
    }
}
