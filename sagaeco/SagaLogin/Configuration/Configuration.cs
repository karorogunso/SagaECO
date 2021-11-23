using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Text;

using SagaLib;
using SagaDB.Actor;
using SagaDB.Item;
using SagaLogin.Configurations;

namespace SagaLogin
{
    public class Configuration : Singleton<Configuration>
    {
        string dbhost, dbuser, dbpass, dbname;
        int dbport, port, loglevel,dbType;
        string encoding;
        string password = "saga";
        SagaLib.Version version;

        Dictionary<PC_RACE, StartupSetting> startup = new Dictionary<PC_RACE, StartupSetting>();
        Dictionary<PC_RACE, Dictionary<PC_GENDER, List<StartItem>>> startitem = new Dictionary<PC_RACE, Dictionary<PC_GENDER, List<StartItem>>>();
        
        public string DBHost { get { return this.dbhost; } set { this.dbhost = value; } }
        public string DBUser { get { return this.dbuser; } set { this.dbuser = value; } }
        public string DBPass { get { return this.dbpass; } set { this.dbpass = value; } }
        public string DBName { get { return this.dbname; } set { this.dbname = value; } }
        public string Password { get { return this.password; } set { this.password = value; } }
        public int DBPort { get { return this.dbport; } set { this.dbport = value; } }
        public int Port { get { return this.port; } set { this.port = value; } }
        public int DBType { get { return this.dbType; } set { this.dbType = value; } }
        public Dictionary<PC_RACE, StartupSetting> StartupSetting { get { return this.startup; } set { this.startup = value; } }
        public Dictionary<PC_RACE, Dictionary<PC_GENDER, List<StartItem>>> StartItem { get { return this.startitem; } set { this.startitem = value; } }
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

        public Configuration()
        {
            Dictionary<PC_GENDER, List<StartItem>> list = new Dictionary<PC_GENDER, List<StartItem>>();
            startitem.Add(PC_RACE.EMIL, list);
            list = new Dictionary<PC_GENDER, List<StartItem>>();
            startitem.Add(PC_RACE.TITANIA, list);
            list = new Dictionary<PC_GENDER, List<StartItem>>();
            startitem.Add(PC_RACE.DOMINION, list);
            list = new Dictionary<PC_GENDER, List<StartItem>>();
            startitem.Add(PC_RACE.DEM, list);
        }

        public void Initialization(string path)
        {
            XmlDocument xml = new XmlDocument();
            try
            {
                XmlElement root;
                XmlNodeList list;
                bool getVersion = false;
                xml.Load(path);
                root = xml["SagaLogin"];
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
                                getVersion = true;
                            }
                            catch
                            {
                                Logger.ShowWarning(string.Format("Cannot find Version:[{0}], using default version:[{1}]", i.InnerText, this.version));
                            }
                            break;
                        case "startstatus":
                            PC_RACE race = PC_RACE.EMIL;
                            switch (i.Attributes["race"].Value.ToUpper())
                            {
                                case "EMIL":
                                    race = PC_RACE.EMIL;
                                    break;
                                case "TITANIA":
                                    race = PC_RACE.TITANIA;
                                    break;
                                case "DOMINION":
                                    race = PC_RACE.DOMINION;
                                    break;
                                case "DEM":
                                    race = PC_RACE.DEM;
                                    break;
                            }
                            Configurations.StartupSetting setting = new StartupSetting();
                            XmlNodeList childs = i.ChildNodes;
                            foreach (object l in childs)
                            {
                                XmlElement k;
                                if (l.GetType() != typeof(XmlElement)) continue;
                                k = (XmlElement)l;
                                switch (k.Name.ToLower())
                                {
                                    case "str":
                                        setting.Str = ushort.Parse(k.InnerText);
                                        break;
                                    case "dex":
                                        setting.Dex = ushort.Parse(k.InnerText);
                                        break;
                                    case "int":
                                        setting.Int = ushort.Parse(k.InnerText);
                                        break;
                                    case "vit":
                                        setting.Vit = ushort.Parse(k.InnerText);
                                        break;
                                    case "agi":
                                        setting.Agi = ushort.Parse(k.InnerText);
                                        break;
                                    case "mag":
                                        setting.Mag = ushort.Parse(k.InnerText);
                                        break;
                                    case "startmap":
                                        setting.StartMap = uint.Parse(k.InnerText);
                                        break;
                                    case "startx":
                                        setting.X = byte.Parse(k.InnerText);
                                        break;
                                    case "starty":
                                        setting.Y = byte.Parse(k.InnerText);
                                        break;
                                }
                            }
                            startup.Add(race, setting);
                            break;
                        case "startitem":
                            Dictionary<PC_GENDER, List<Configurations.StartItem>> items = null;
                            PC_GENDER gender = PC_GENDER.FEMALE;
                            switch (i.Attributes["race"].Value.ToUpper())
                            {
                                case "EMIL":
                                    items = this.startitem[PC_RACE.EMIL];
                                    break;
                                case "TITANIA":
                                    items = this.startitem[PC_RACE.TITANIA];
                                    break;
                                case "DOMINION":
                                    items = this.startitem[PC_RACE.DOMINION];
                                    break;
                                case "DEM":
                                    items = this.startitem[PC_RACE.DEM];
                                    break;
                            }
                            switch (i.Attributes["gender"].Value.ToUpper())
                            {
                                case "MALE":
                                    gender = PC_GENDER.MALE;
                                    break;
                                case "FEMALE":
                                    gender = PC_GENDER.FEMALE;
                                    break;
                            }
                            List<Configurations.StartItem> list2 = new List<StartItem>();
                            items.Add(gender, list2);
                            XmlNodeList childs2 = i.ChildNodes;
                            foreach (object o in childs2)
                            {
                                XmlElement p;
                                if (o.GetType() != typeof(XmlElement)) continue;
                                p = (XmlElement)o;
                                Configurations.StartItem startitem = new StartItem();
                                XmlNodeList childs3 = p.ChildNodes;
                                foreach (object n in childs3)
                                {
                                    XmlElement m;
                                    if (n.GetType() != typeof(XmlElement)) continue;
                                    m = (XmlElement)n;
                                    switch (m.Name.ToLower())
                                    {
                                        case "itemid":
                                            startitem.ItemID = uint.Parse(m.InnerText);
                                            break;
                                        case "slot":
                                            startitem.Slot = (ContainerType)Enum.Parse(typeof(ContainerType), m.InnerText.ToUpper());
                                            break;
                                        case "count":
                                            startitem.Count = byte.Parse(m.InnerText);
                                            break;
                                    }
                                }
                                list2.Add(startitem);
                            }
                            break;
                    }
                }
                if (!getVersion)
                    Logger.ShowWarning(string.Format("Packet Version not set, using default version:[{0}], \r\n         please change Config/SagaMap.xml to set version", this.version));
                Logger.ShowInfo("Done reading configuration...");
            }
            catch (Exception ex)
            {
                Logger.ShowError(ex);
            }
        }
    }
}
