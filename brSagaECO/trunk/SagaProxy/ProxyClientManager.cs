using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;

using System.Reflection;
using Microsoft.CSharp;
using System.IO;
using System.CodeDom.Compiler;

using SagaLib;
namespace SagaProxy
{
    public class ProxyClientManager : ClientManager
    {
        List<ProxyClient> clients;
        public Thread check;
        public string IP;
        public int port;
        public bool ready = false;
        public List<Packet> packetContainerClient;
        public List<Packet> packetContainerServer;
        public List<Packet> packets;
        public ushort firstlvlen = 2;
        public ProxyServer.ServerType type;

        public Dictionary<int, PP> proxyIDList = new Dictionary<int, PP>();
        string path;

        public class PP
        {
            public string name;
            public int length;
        }
        public static ProxyClientManager LoginInstance
        {
            get
            {
                return Nested.loginInstance;
            }
        }
        public static ProxyClientManager MapInstance
        {
            get
            {
                return Nested.mapInstance;
            }
        }
        public static ProxyClientManager ValidationInstance
        {
            get
            {
                return Nested.validationInstance;
            }
        }
        class Nested
        {
            static Nested()
            {
            }
            internal static readonly ProxyClientManager mapInstance = new ProxyClientManager();
            internal static readonly ProxyClientManager loginInstance = new ProxyClientManager();
            internal static readonly ProxyClientManager validationInstance = new ProxyClientManager();
        }
        public ProxyClientManager()
        {
            this.clients = new List<ProxyClient>();
            this.commandTable = new Dictionary<ushort, Packet>();
            this.commandTable.Add(0x0001, new Packets.Client.SendVersion());
            this.commandTable.Add(0xFFFF, new Packets.Client.SendUniversal());
            this.waitressQueue = new AutoResetEvent(true);

            check = new Thread(new ThreadStart(this.checkCriticalArea));
            check.Name = string.Format("DeadLock checker({0})", check.ManagedThreadId);
#if DeadLockCheck
            ckeck.Start();
#endif
        }
        public override void NetworkLoop(int maxNewConnections)
        {
            try
            {
                for (int i = 0; listener.Pending() && i < maxNewConnections; i++)
                {
                    Socket sock = listener.AcceptSocket();
                    string ip = sock.RemoteEndPoint.ToString().Substring(0, sock.RemoteEndPoint.ToString().IndexOf(':'));
                    MainWindow.Instance.Dispatcher.Invoke(() => MainWindow.Instance.Message.AppendText("\r\n......"));
                    ProxyClient client = new ProxyClient(sock, this.commandTable, this.IP, port, this.packetContainerClient, this.packetContainerServer, this.packets, this.firstlvlen, this.type);
                    MainWindow.Instance.Dispatcher.Invoke(() => MainWindow.Instance.Message.AppendText($"\r\nNew client form:{sock.RemoteEndPoint.ToString()}"));
                    switch (this.type)
                    {
                        case ProxyServer.ServerType.Login:
                            MainWindow.Instance.CurrentLoginClient=client;
                            break;
                        case ProxyServer.ServerType.Map:
                            MainWindow.Instance.CurrentMapClient = client;
                            break;
                        case ProxyServer.ServerType.Validation:
                            MainWindow.Instance.CurrentValidationClient = client;
                            break;
                    }
                    clients.Add(client);
                }
            }
            catch (Exception ex)
            {
                MainWindow.Instance.Dispatcher.Invoke(() => MainWindow.Instance.Message.AppendText(ex.ToString()));
            }
        }
        public override void OnClientDisconnect(Client client_t)
        {
            clients.Remove((ProxyClient)client_t);
        }
        public Dictionary<int, PP> LoadProxyListFromLocal(string path)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>() { { "CompilerVersion", "v3.5" } };
            CSharpCodeProvider provider = new CSharpCodeProvider(dic);
            int pcount = 0;
            this.path = path;
            try
            {
                string[] files = Directory.GetFiles(path, "*.cs", SearchOption.AllDirectories);
                Assembly newAssembly;
                int tmp;
                if (files.Length > 0)
                {
                    newAssembly = Compile(files, provider);
                    if (newAssembly != null)
                    {
                        tmp = LoadAssembly(newAssembly);
                        //Logger.ShowInfo(string.Format("Containing {0} Events", tmp));
                        pcount += tmp;
                    }
                }
                return proxyIDList;
            }
            catch (Exception ex)
            {
                MainWindow.Instance.Dispatcher.Invoke(() => MainWindow.Instance.Message.AppendText(ex.ToString()));
                return proxyIDList;
            }

        }
        private Assembly Compile(string[] Source, CodeDomProvider Provider)
        {
            //ICodeCompiler compiler = Provider.;
            CompilerParameters parms = new CompilerParameters();
            CompilerResults results;
            List<string> Reference = new List<string>();
            // Configure parameters
            parms.CompilerOptions = "/target:library /optimize";
            parms.GenerateExecutable = false;
            parms.GenerateInMemory = true;
            parms.IncludeDebugInformation = true;
            parms.ReferencedAssemblies.Add("System.dll");
            parms.ReferencedAssemblies.Add("System.Core.dll");
            parms.ReferencedAssemblies.Add("System.Xml.dll");
            parms.ReferencedAssemblies.Add("System.Xml.Linq.dll");
            parms.ReferencedAssemblies.Add("SagaLib.dll");
            parms.ReferencedAssemblies.Add("SagaDB.dll");
            parms.ReferencedAssemblies.Add("SagaMap.exe");
            parms.ReferencedAssemblies.Add("System.Data.dll");
            parms.ReferencedAssemblies.Add("Mysql.Data.dll");
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
                        Packet newPacket;
                        try
                        {
                            newPacket = (Packet)Activator.CreateInstance(npcType);
                        }
                        catch (Exception)
                        {
                            continue;
                        }
                        if (!this.proxyIDList.Keys.Contains(newPacket.ID) && newPacket.ID != 0)
                        {
                            PP p = new PP();
                            p.name = newPacket.ToString();
                            p.length = newPacket.data.Length;
                            this.proxyIDList.Add(newPacket.ID,p);
                        }
                        else
                        {
                            //if (newEvent.ID != 0)
                                //Logger.ShowWarning(string.Format("EventID:{0} already exists, Class:{1} droped", newEvent.ID, npcType.FullName));
                        }
                    }
                    catch (Exception ex)
                    {
                        MainWindow.Instance.Dispatcher.Invoke(() => MainWindow.Instance.Message.AppendText(ex.ToString()));
                    }
                    count++;
                }
            }
            return count;
        }
    }
}
