using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Text;

using SagaLib;
using SagaDB.Actor;
using SagaDB.Item;

namespace SagaValidation
{
    public class Configuration : Singleton<Configuration>
    {
        string dbhost, dbuser, dbpass, dbname;
        int dbport, port, loglevel, dbType;
        string encoding;
        string password = "saga";
        string servername, serverip;
        string clientgameversion = "All";

        SagaLib.Version version;
        public string DBHost { get { return this.dbhost; } set { this.dbhost = value; } }
        public string DBUser { get { return this.dbuser; } set { this.dbuser = value; } }
        public string DBPass { get { return this.dbpass; } set { this.dbpass = value; } }
        public string DBName { get { return this.dbname; } set { this.dbname = value; } }
        public string Password { get { return this.password; } set { this.password = value; } }
        public int DBPort { get { return this.dbport; } set { this.dbport = value; } }
        public int Port { get { return this.port; } set { this.port = value; } }
        public int DBType { get { return this.dbType; } set { this.dbType = value; } }
        public string ClientGameVersion { get { return this.clientgameversion; } set { this.clientgameversion = value; } }

        public string ServerName{get{return this.servername;} set { this.servername=value;}}
        public string ServerIP { get { return this.serverip; } set { this.serverip = value; } }
        public SagaLib.Version Version { get { return this.version; } set { this.version = value; } }

        public string DBEncoding
        {
            get
            {
                if (this.encoding == null)
                {
                    Logger.ShowDebug("DB Encoding not set, set to default value: GBK", Logger.CurrentLogger);
                    this.encoding = "GBK";
                }
                return this.encoding;
            }
            set { this.encoding = value; }
        }

        public int LogLevel { get { return this.loglevel; } set { this.loglevel = value; } }

        public void Initialization(string path)
        {
            XmlDocument xml = new XmlDocument();
            try
            {
                XmlElement root;
                XmlNodeList list;
                bool nullClientGameVersion = false;
                xml.Load(path);
                root = xml["SagaValidation"];
                list = root.ChildNodes;
                foreach (object j in list)
                {
                    XmlElement i;
                    if (j.GetType() != typeof(XmlElement)) continue;
                    i = (XmlElement)j;
                    switch (i.Name.ToLower())
                    {
                        case "dbtype":
                            this.dbType = int.Parse(i.InnerText);
                            break;
                        case "port":
                            this.port = int.Parse(i.InnerText);
                            break;
                        case "dbhost":
                            this.dbhost = i.InnerText;
                            break;
                        case "dbport":
                            this.dbport = int.Parse(i.InnerText);
                            break;
                        case "dbuser":
                            this.dbuser = i.InnerText;
                            break;
                        case "dbpass":
                            this.dbpass = i.InnerText;
                            break;
                        case "dbname":
                            this.dbname = i.InnerText;
                            break;
                        case "dbencoding":
                            this.encoding = i.InnerText;
                            break;
                        case "password":
                            this.password = i.InnerText;
                            break;
                        case "loglevel":
                            this.loglevel = int.Parse(i.InnerText);
                            break;
                        case "version":
                            try
                            {
                                this.version = (SagaLib.Version)Enum.Parse(typeof(SagaLib.Version), i.InnerText);
                            }
                            catch
                            {
                                Logger.ShowWarning(string.Format("Cannot find Version:[{0}], using default version:[{1}]", i.InnerText, this.version));
                            }
                            break;
                        case "servername":
                            this.servername = i.InnerText;
                            break;
                        case "clientgameversion":
                            this.clientgameversion = i.InnerText;
                            if (this.clientgameversion == "All" || this.clientgameversion == "")
                            {
                                this.clientgameversion = "All";
                                nullClientGameVersion = true;
                            }
                            break;
                        case "serverip":
                            this.serverip = i.InnerText;
                            break;
                    }
                }
                if (nullClientGameVersion)
                    Logger.ShowWarning("Cannot find ClientGameVersion, accepting all version.");
                Logger.ShowInfo("Done reading configuration...");
            }
            catch (Exception ex)
            {
                Logger.ShowError(ex);
            }

        }
    }
}
