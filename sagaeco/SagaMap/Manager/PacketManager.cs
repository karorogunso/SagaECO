using System;
using System.Collections.Generic;
using System.CodeDom.Compiler;
using System.Reflection;
using Microsoft.CSharp;
using System.IO;
using System.Linq;
using System.Text;

using SagaLib;
using SagaDB.Actor;
using SagaMap.Localization;
using SagaMap.Localization.Languages;
using SagaMap.Scripting;

namespace SagaMap.Manager
{
    public class PacketManager : Singleton<PacketManager>
    {
        List<uint> packetsID = new List<uint>();
        string path;
        public List<uint> PacketsID { get { return this.packetsID; } }

        public PacketManager()
        {

        }

        public void LoadPacketFiles(string path)
        {
            Logger.ShowInfo("Loading uncompiled PacketFiles");
            Dictionary<string, string> dic = new Dictionary<string, string>() { { "CompilerVersion", "v3.5" } };
            CSharpCodeProvider provider = new CSharpCodeProvider(dic);
            Directory.SetCurrentDirectory(Directory.GetParent(path).FullName);
            path = Directory.GetCurrentDirectory();
            int i = path.LastIndexOf("\\");
            path = path.Substring(0, i);
            path = path + "\\SagaMap\\Packets\\Server";

            int Packetcount = 0;
            this.path = path;
            try
            {
                string[] files = Directory.GetFiles(path, "*.cs", SearchOption.AllDirectories);
                Assembly newAssembly;
                int tmp;
                if (files.Length > 0)
                {
                    newAssembly = CompilePacket(files, provider);
                    if (newAssembly != null)
                    {
                        tmp = LoadAssembly(newAssembly);
                        Logger.ShowInfo(string.Format("Containing {0} Events", tmp));
                        Packetcount += tmp;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.ShowError(ex);
            }
        }
        private Assembly CompilePacket(string[] Source, CodeDomProvider Provider)
        {
            CompilerParameters parms = new CompilerParameters();
            CompilerResults results;
            parms.CompilerOptions = "/target:library /optimize";
            parms.GenerateExecutable = false;
            parms.GenerateInMemory = true;
            parms.IncludeDebugInformation = true;
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
            return results.CompiledAssembly;
        }
        private int LoadAssembly(Assembly newAssembly)
        {
            Module[] newPackets = newAssembly.GetModules();
            int count = 0;
            foreach (Module newScript in newPackets)
            {
                Type[] types = newScript.GetTypes();
                foreach (Type npcType in types)
                {
                    try
                    {
                        if (npcType.IsAbstract == true) continue;
                        if (npcType.GetCustomAttributes(false).Length > 0) continue;
                        Packet newPacket;
                        try
                        {
                            newPacket = (Packet)Activator.CreateInstance(npcType);
                        }
                        catch (Exception)
                        {
                            continue;
                        }
                        if (!this.packetsID.Contains(newPacket.ID) && newPacket.ID != 0)
                        {
                            this.packetsID.Add(newPacket.ID);
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
    }
}
