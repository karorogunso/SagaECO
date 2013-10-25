using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Text;

using SagaLib;

namespace SagaMap
{
    public class Configuration : Singleton<Configuration>
    {
        string dbhost, dbuser, dbpass, dbname, language;
        int dbport,port, loglevel;

        public string DBHost { get { return this.dbhost; } set { this.dbhost = value; } }
        public string DBUser { get { return this.dbuser; } set { this.dbuser = value; } }
        public string DBPass { get { return this.dbpass; } set { this.dbpass = value; } }
        public string DBName { get { return this.dbname; } set { this.dbname = value; } }
        public int DBPort { get { return this.dbport; } set { this.dbport = value; } }
        public int Port { get { return this.port; } set { this.port = value; } }

        public string Language { get { return this.language; } set { this.language = value; } }

        public int LogLevel { get { return this.loglevel; } set { this.loglevel = value; } }

        public Configuration()
        {
        }

        public void Initialization(string path)
        {
            XmlDocument xml = new XmlDocument();
            try
            {
                XmlElement root;
                XmlNodeList list;
                xml.Load(path);
                root = xml["SagaMap"];
                list = root.ChildNodes;
                foreach (object j in list)
                {
                    XmlElement i;
                    if (j.GetType() != typeof(XmlElement)) continue;
                    i = (XmlElement)j;
                    switch (i.Name.ToLower())
                    {
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
                        case "loglevel":
                            this.loglevel = int.Parse(i.InnerText);
                            break;
                        case "language":
                            this.language = i.InnerText;
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
