using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Text;

using SagaLib;
using SagaDB.Actor;
using SagaDB.Item;

namespace SagaAPI
{
    public class Configuration : Singleton<Configuration>
    {
        string dbhost, dbuser, dbpass, dbname;
        int dbport, loglevel, dbType;
        string encoding;
        string password = "saga";
        string whitelist = "127.0.0.1";
        string prefixes;
        string port;
        string apikey;

        public string DBHost { get { return this.dbhost; } set { this.dbhost = value; } }
        public string DBUser { get { return this.dbuser; } set { this.dbuser = value; } }
        public string DBPass { get { return this.dbpass; } set { this.dbpass = value; } }
        public string DBName { get { return this.dbname; } set { this.dbname = value; } }
        public string Password { get { return this.password; } set { this.password = value; } }
        public int DBPort { get { return this.dbport; } set { this.dbport = value; } }
        public int DBType { get { return this.dbType; } set { this.dbType = value; } }
        public string APIKey { get { return this.apikey; }set { this.apikey = value; } }
        public string Port
        {
            get
            {
                if (this.port == null)
                {
                    Logger.ShowWarning("PORT ARE NOT SET.USE DEFAULT PORT (8080).");
                    this.port = "8080";
                }
                return this.port;
            }
            set { this.port = value; }
        }
        public string Prefixes {
            get {
                if(this.prefixes == null)
                {
                    Logger.ShowWarning("PREFIXES ARE NOT SET.USE DEFAULT PREFIXES (localhost).");
                    this.prefixes = "http://localhost";
                }
                return this.prefixes;
            }
            set {this.prefixes = value;}
        }

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
                xml.Load(path);
                root = xml["SagaAPI"];
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
                            this.port = i.InnerText;
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
                        case "whitelist":
                            this.whitelist = i.InnerText;
                            break;
                        case "prefixes":
                            this.prefixes = i.InnerText;
                            break;
                        case "apikey":
                            this.apikey = i.InnerText;
                            break;
                    }
                }
                Logger.ShowInfo("Done reading configuration...");
            }
            catch (Exception ex)
            {
                Logger.ShowError(ex);
            }

        }
    }
}
