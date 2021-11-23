#define Text
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Text;

using SagaLib;
using SagaDB.Actor;
using SagaLib.VirtualFileSystem;
namespace SagaMap
{
    public enum RateOverrideType
    {
        GMLv,
        CLevel
    }
    public class RateOverrideItem
    {
        RateOverrideType type;

        public RateOverrideType Type
        {
            get { return type; }
            set { type = value; }
        }
        int value;

        public int Value
        {
            get { return this.value; }
            set { this.value = value; }
        }
        float exprate, questrate, questGoldRate, stampDropRate;

        public float ExpRate
        {
            get { return exprate; }
            set { exprate = value; }
        }

        public float QuestRate
        {
            get { return questrate; }
            set { questrate = value; }
        }

        public float QuestGoldRate
        {
            get { return questGoldRate; }
            set { questGoldRate = value; }
        }

        public float StampDropRate
        {
            get { return stampDropRate; }
            set { stampDropRate = value; }
        }
        float globalDropRate = 1f, specialDropRate = 1f;

        public float GlobalDropRate
        {
            get { return globalDropRate; }
            set { globalDropRate = value; }
        }

        public float SpecialDropRate
        {
            get { return specialDropRate; }
            set { specialDropRate = value; }
        }

        public override string ToString()
        {
            return string.Format("Type:{0} Value:{1}", type, value);
        }
    }

    public class Configuration : Singleton<Configuration>
    {
        string dbhost, dbuser, dbpass, dbname, language, loginhost, host;
        int dbport, port, loglevel, loginport, dbType, warehouse = 100, firstlevelLimit;
        string encoding;
        string loginPass = "saga";
        List<uint> hostedMaps = new List<uint>();
        SagaLib.Version version = SagaLib.Version.Saga6;
        uint jobSwitchReduceItem = 10024500;
        uint maxFurnitureCount = 100;
        string clientversion;
        bool sqlLog = true;
        Dictionary<SagaDB.Actor.PC_RACE, SagaLogin.Configurations.StartupSetting> startupSetting = new Dictionary<SagaDB.Actor.PC_RACE, SagaLogin.Configurations.StartupSetting>();
        List<string> motd = new List<string>();
        List<string> reference = new List<string>();
        List<string> monitorAccounts = new List<string>();
        Dictionary<RateOverrideType, Dictionary<int, RateOverrideItem>> rateOverride = new Dictionary<RateOverrideType, Dictionary<int, RateOverrideItem>>();

        int exprate, questrate, questGoldRate = 100, questUpdateTime = 24, questUpdateAmount = 5, questPointsMax = 15;
        int stampDropRate = 100;
        int itemFusionRate = 80;
        int ringfameemblem = 300;
        int mobamount = 1;
        ushort speed = 410;
        float deathBaseRateEmil = 0.1f, deathJobRateEmil = 0.02f, deathBaseRateDom = 0.1f, deathJobRateDom = 0.02f;
        float pvpDmgRatePhysic = 1f;
        float pvpDmgRateMagic = 1f;
        float payloadRate = 1f;
        float volumeRate = 1f;
        bool onlineStatic = true;
        string statisticsPage = "index.htm";
        bool multipleDrop = false;
        float globalDropRate = 1f, specialDropRate = 1f;
        int maxLvDiffForExp = 99;
        bool bossSlash = false;
        bool atkMastery = true;
        int bossSlashRate = 10;
        ushort basePhysicDef = 50;
        ushort baseMagicDef = 50;
        ushort maxPhysicDef = 90;
        ushort maxMagicDef = 90;
        bool activespecialloot = false;
        int _BossSpecialLootRate = 0;
        uint _BossSpecialLootID = 0;
        byte _BossSpecialLootNum = 0;
        int _NomalMobSpecialLootRate = 0;
        uint _NomalMobSpecialLootID = 0;
        byte _NomalMobSpecialLootNum = 0;
        bool _ActiveQuestSpecialReward = false;
        int _QuestSpecialRewardRate = 0;
        uint _QuestSpecialRewardID = 0;
        bool ajiMode = false;
        bool enhanceMatsuri = false;
        
        //API
        string apiPass = "saga";
        string whitelist = "127.0.0.1";
        string prefixes, apikey;
        int? apiport;

        //VShop

        bool vshopclosed = false;

        int _maxcharinmapsrv = 2;


        public int MaxCharacterInMapServer { get { return _maxcharinmapsrv;  } set { _maxcharinmapsrv = value;  } }
        public bool AJIMode { get { return this.ajiMode; } set { this.ajiMode = value; } }
        public bool VShopClosed { get { return this.vshopclosed; } set { this.vshopclosed = value; } }
        public string Host { get { return this.host; } set { this.host = value; } }
        public string DBHost { get { return this.dbhost; } set { this.dbhost = value; } }
        public string DBUser { get { return this.dbuser; } set { this.dbuser = value; } }
        public string DBPass { get { return this.dbpass; } set { this.dbpass = value; } }
        public string DBName { get { return this.dbname; } set { this.dbname = value; } }
        public string LoginPass { get { return this.loginPass; } set { this.loginPass = value; } }
        public string ClientVersion { get { return this.clientversion; } set { this.clientversion = value; } }
        public string APIPass { get { return this.apiPass; } set { this.apiPass = value; } }
        public string APIKey { get { return this.apikey; } set { this.apikey = value; } }
        public bool EnhanceMatsuri { get { return this.enhanceMatsuri; } set { this.enhanceMatsuri = value; } }
        public int APIPort
        {
            get
            {
                if (this.apiport == null || this.apiport == 0)
                {
                    Logger.ShowWarning("PORT ARE NOT SET.USEING DEFAULT PORT (8080).");
                    this.apiport = 8080;
                }
                return this.apiport.Value;
            }
            set { this.apiport = value; }
        }
        public string Prefixes
        {
            get
            {
                if (this.prefixes == null)
                {
                    Logger.ShowWarning("PREFIXES ARE NOT SET.USEING DEFAULT PREFIXES (localhost).");
                    this.prefixes = "http://localhost";
                }
                return this.prefixes;
            }
            set { this.prefixes = value; }
        }
        public int Port { get { return this.port; } set { this.port = value; } }
        public string LoginHost { get { return this.loginhost; } set { this.loginhost = value; } }
        public int DBPort { get { return this.dbport; } set { this.dbport = value; } }
        public int DBType { get { return this.dbType; } set { this.dbType = value; } }
        public int LoginPort { get { return this.loginport; } set { this.loginport = value; } }
        public int MaxLevelDifferenceForExp { get { return maxLvDiffForExp; } set { this.maxLvDiffForExp = value; } }

        public int FirstLevelLimit { get { return this.firstlevelLimit; } set { this.firstlevelLimit = value; } }
        public int EXPRate { get { return this.exprate; } set { this.exprate = value; } }

        public float CalcEXPRateForPC(ActorPC pc)
        {
            float rate = ((float)exprate / 100);
            /*RateOverrideItem gmlv, lv;
            GetRateOverride(pc, out gmlv, out lv);
            if (gmlv != null)
                rate *= gmlv.ExpRate;
            if (gmlv == null && lv != null)
                rate *= lv.ExpRate;*/
            return rate;
        }

        public int StampDropRate { get { return this.stampDropRate; } }
        public float CalcStampDropRateForPC(ActorPC pc)
        {
            float rate = (float)stampDropRate / 100;
            if (pc != null)
            {
                RateOverrideItem gmlv, lv;
                GetRateOverride(pc, out gmlv, out lv);
                if (gmlv != null)
                    rate *= gmlv.StampDropRate;
                if (gmlv == null && lv != null)
                    rate *= lv.StampDropRate;
            }
            return rate;
        }
        public int ItemFusionRate { get { return this.itemFusionRate; } }
        public int MobAmount { get { return this.mobamount; } }


        public int QuestRate { get { return this.questrate; } set { this.questrate = value; } }
        public float CalcQuestRateForPC(ActorPC pc)
        {
            float rate = (float)questrate / 100;
            RateOverrideItem gmlv, lv;
            GetRateOverride(pc, out gmlv, out lv);
            if (gmlv != null)
                rate *= gmlv.QuestRate;
            if (gmlv == null && lv != null)
                rate *= lv.QuestRate;
            return rate;
        }
        public int QuestGoldRate { get { return this.questGoldRate; } set { this.questGoldRate = value; } }
        public float CalcQuestGoldRateForPC(ActorPC pc)
        {
            float rate = (float)questGoldRate / 100;
            RateOverrideItem gmlv, lv;
            GetRateOverride(pc, out gmlv, out lv);
            if (gmlv != null)
                rate *= gmlv.QuestGoldRate;
            if (gmlv == null && lv != null)
                rate *= lv.QuestGoldRate;
            return rate;
        }
        public int WarehouseLimit { get { return this.warehouse; } }
        public ushort Speed { get { return this.speed; } }
        public SagaLib.Version Version { get { return this.version; } }
        public uint JobSwitchReduceItem { get { return this.jobSwitchReduceItem; } }
        public int RingFameNeededForEmblem { get { return this.ringfameemblem; } }
        public Dictionary<SagaDB.Actor.PC_RACE, SagaLogin.Configurations.StartupSetting> StartupSetting { get { return this.startupSetting; } set { this.startupSetting = value; } }
        public List<string> Motd { get { return this.motd; } }
        public List<string> ScriptReference { get { return this.reference; } }
        public List<string> MonitorAccounts { get { return this.monitorAccounts; } }

        public string Language { get { return this.language; } set { this.language = value; } }
        public List<uint> HostedMaps { get { return this.hostedMaps; } set { this.hostedMaps = value; } }
        public bool SQLLog { get { return this.sqlLog; } }

        public int QuestUpdateTime { get { return this.questUpdateTime; } set { this.questUpdateTime = value; } }

        public int QuestUpdateAmount { get { return this.questUpdateAmount; } set { this.questUpdateAmount = value; } }

        public int QuestPointsMax { get { return this.questPointsMax; } set { this.questPointsMax = value; } }

        public uint MaxFurnitureCount { get { return this.maxFurnitureCount; } set { this.maxFurnitureCount = value; } }

        public int LogLevel { get { return this.loglevel; } set { this.loglevel = value; } }

        public float DeathPenaltyBaseEmil { get { return this.deathBaseRateEmil; } set { this.deathBaseRateEmil = value; } }

        public float DeathPenaltyJobEmil { get { return this.deathJobRateEmil; } set { this.deathJobRateEmil = value; } }

        public float DeathPenaltyBaseDominion { get { return this.deathBaseRateDom; } set { this.deathBaseRateDom = value; } }

        public float DeathPenaltyJobDominion { get { return this.deathJobRateDom; } set { this.deathJobRateDom = value; } }

        public bool OnlineStatistics { get { return this.onlineStatic; } set { this.onlineStatic = value; } }

        public string StatisticsPagePath { get { return this.statisticsPage; } set { this.statisticsPage = value; } }

        public bool MultipleDrop { get { return this.multipleDrop; } set { this.multipleDrop = value; } }

        public int BossSlashRate { get { return this.bossSlashRate; } set { this.bossSlashRate = value; } }

        public bool BossSlash { get { return this.bossSlash; } set { this.bossSlash = value; } }
        public bool AtkMastery { get { return this.atkMastery; } set { this.atkMastery = value; } }

        public ushort BasePhysicDef { get { return this.basePhysicDef; } set { this.basePhysicDef = value; } }

        public ushort BaseMagicDef { get { return this.baseMagicDef; } set { this.baseMagicDef = value; } }

        public ushort MaxPhysicDef { get { return this.maxPhysicDef; } set { this.maxPhysicDef = value; } }

        public ushort MaxMagicDef { get { return this.maxMagicDef; } set { this.maxMagicDef = value; } }

        public float GlobalDropRate { get { return this.globalDropRate; } set { this.globalDropRate = value; } }
        public float CalcGlobalDropRateForPC(ActorPC pc)
        {
            float rate = (float)globalDropRate;
            if (pc != null)
            {
                RateOverrideItem gmlv, lv;
                GetRateOverride(pc, out gmlv, out lv);
                if (gmlv != null)
                    rate *= gmlv.GlobalDropRate;
                if (gmlv ==null && lv != null)
                    rate *= lv.GlobalDropRate;
            }
            return rate;
        }
        
        public float SpecialDropRate { get { return this.specialDropRate; } set { this.specialDropRate = value; } }
        public float CalcSpecialDropRateForPC(ActorPC pc)
        {
            float rate = (float)specialDropRate;
            if (pc != null)
            {
                RateOverrideItem gmlv, lv;
                GetRateOverride(pc, out gmlv, out lv);
                if (gmlv != null)
                    rate *= gmlv.SpecialDropRate;
                if (gmlv == null && lv != null)
                    rate *= lv.SpecialDropRate;
            }
            return rate;
        }

        public bool ActiveSpecialLoot { get { return this.activespecialloot; } set { this.activespecialloot = value; } }
        public int BossSpecialLootRate { get { return this._BossSpecialLootRate; } set { this._BossSpecialLootRate = value; } }
        public uint BossSpecialLootID { get { return this._BossSpecialLootID; } set { this._BossSpecialLootID = value; } }
        public byte BossSpecialLootNum { get { return this._BossSpecialLootNum; } set { this._BossSpecialLootNum = value; } }
        public int NomalMobSpecialLootRate { get { return this._NomalMobSpecialLootRate; } set { this._NomalMobSpecialLootRate = value; } }
        public uint NomalMobSpecialLootID { get { return this._NomalMobSpecialLootID; } set { this._NomalMobSpecialLootID = value; } }
        public byte NomalMobSpecialLootNum { get { return this._NomalMobSpecialLootNum; } set { this._NomalMobSpecialLootNum = value; } }
        public bool ActivceQuestSpecialReward { get { return this._ActiveQuestSpecialReward; } set { this._ActiveQuestSpecialReward = value; } }
        public uint QuestSpecialRewardID { get { return this._QuestSpecialRewardID; } set { this._QuestSpecialRewardID = value; } }
        public int QuestSpecialRewardRate { get { return this._QuestSpecialRewardRate; } set { this._QuestSpecialRewardRate = value; } }

        public string TwitterID { get { return this.TwitterID; } set { this.TwitterID = value; } }

        public string TwitterPasswd { get { return this.TwitterPasswd; } set { this.TwitterPasswd = value; } }

        public float PVPDamageRatePhysic { get { return this.pvpDmgRatePhysic; } set { this.pvpDmgRatePhysic = value; } }

        public float PVPDamageRateMagic { get { return this.pvpDmgRateMagic; } set { this.pvpDmgRateMagic = value; } }

        public float PayloadRate { get { return this.payloadRate; } set { this.payloadRate = value; } }

        public float VolumeRate { get { return this.volumeRate; } set { this.volumeRate = value; } }
        
        public string DBEncoding
        {
            get
            {
                if (this.encoding == null)
                {
                    Logger.ShowDebug("DB Encoding not set, set to default value: UTF-8", Logger.CurrentLogger);
                    this.encoding = "utf-8";
                }
                return this.encoding;
            }
            set { this.encoding = value; }
        }
#if Text
        void InitXML(string path)
        {
            XmlDocument xml = new XmlDocument();
            bool getVersion = false;
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
                        case "host":
                            this.host = i.InnerText;
                            break;
                        case "port":
                            this.port = int.Parse(i.InnerText);
                            break;
                        case "dbtype":
                            this.dbType = int.Parse(i.InnerText);
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
                        case "loginhost":
                            this.loginhost = i.InnerText;
                            break;
                        case "loginport":
                            this.loginport = int.Parse(i.InnerText);
                            break;
                        case "loginpass":
                            this.loginPass = i.InnerText;
                            break;
                        case "prefixes":
                            this.prefixes = i.InnerText;
                            break;
                        case "apikey":
                            this.apikey = i.InnerText;
                            break;
                        case "apiport":
                            this.apiport = int.Parse(i.InnerText);
                            break;
                        case "vshopclosed":
                            this.vshopclosed = bool.Parse(i.InnerText);
                            break;
                        case "loglevel":
                            this.loglevel = int.Parse(i.InnerText);
                            break;
                        case "clientversion":
                            this.clientversion = i.InnerText;
                            break;
                        case "language":
                            this.language = i.InnerText;
                            break;
                        case "dbencoding":
                            this.encoding = i.InnerText;
                            break;
                        case "atkmastery":
                            this.atkMastery = (i.InnerText == "1");
                            break;
                        case "levellimit" :
                            this.FirstLevelLimit = int.Parse(i.InnerText);
                            break;
                        case "maxsameplayerinmapserver":
                            this._maxcharinmapsrv = int.Parse(i.InnerText);
                            break;
                        case "motd":
                            string[] msg = i.InnerText.Split('\n');
                            foreach (string k in msg)
                            {
                                string tmp = k.Replace("\r", "").Replace(" ", "");
                                if (tmp != "")
                                    motd.Add(tmp);
                            }
                            break;
                        case "monitoraccounts":
                            string[] account = i.InnerText.Split('\n');
                            foreach (string k in account)
                            {
                                string tmp = k.Replace("\r", "").Replace(" ", "");
                                if (tmp != "")
                                    monitorAccounts.Add(tmp);
                            }
                            break;
                        case "maxlvdiffforexp":
                            maxLvDiffForExp = int.Parse(i.InnerText);
                            break;
                        case "rateoverride":
                            {
                                string type = i.Attributes["type"].Value;
                                int value = int.Parse(i.Attributes["value"].Value);
                                RateOverrideType rType = RateOverrideType.GMLv;
                                switch(type.ToLower())
                                {
                                    case "gmlv":
                                        rType= RateOverrideType.GMLv;
                                        break;
                                    case "clv":
                                        rType = RateOverrideType.CLevel;
                                        break;
                                }
                                Dictionary<int, RateOverrideItem> list2;
                                if (rateOverride.ContainsKey(rType))
                                    list2 = rateOverride[rType];
                                else
                                {
                                    list2 = new Dictionary<int, RateOverrideItem>();
                                    rateOverride.Add(rType, list2);
                                }
                                if (!list2.ContainsKey(value))
                                {
                                    RateOverrideItem item = new RateOverrideItem();
                                    item.Type = rType;
                                    item.Value = value;
                                    XmlNodeList maps = i.ChildNodes;
                                    foreach (object l in maps)
                                    {
                                        XmlElement k;
                                        if (l.GetType() != typeof(XmlElement)) continue;
                                        k = (XmlElement)l;
                                        switch (k.Name.ToLower())
                                        {
                                            case "exprate":
                                                item.ExpRate = ((float)int.Parse(k.InnerText) / 100f);
                                                break;
                                            case "questrate":
                                                item.QuestRate = ((float)int.Parse(k.InnerText) / 100f);
                                                break;
                                            case "questgoldrate":
                                                item.QuestGoldRate = ((float)int.Parse(k.InnerText) / 100f);
                                                break;
                                            case "stampdroprate":
                                                item.StampDropRate = ((float)int.Parse(k.InnerText) / 100f);
                                                break;
                                            case "globaldroprate":
                                                item.GlobalDropRate = ((float)int.Parse(k.InnerText) / 100f);
                                                break;
                                            case "specialdroprate":
                                                item.SpecialDropRate = ((float)int.Parse(k.InnerText) / 100f);
                                                break;
                                        }
                                    }
                                    list2.Add(value, item);
                                }
                            }
                            break;
                        case "hostedmaps":
                            {
                                XmlNodeList maps = i.ChildNodes;
                                foreach (object l in maps)
                                {
                                    XmlElement k;
                                    if (l.GetType() != typeof(XmlElement)) continue;
                                    k = (XmlElement)l;
                                    switch (k.Name.ToLower())
                                    {
                                        case "mapid":
                                            hostedMaps.Add(uint.Parse(k.InnerText));
                                            break;
                                    }
                                }
                            }
                            break;
                        case "scriptreference":
                            XmlNodeList dlls = i.ChildNodes;
                            foreach (object l in dlls)
                            {
                                XmlElement k;
                                if (l.GetType() != typeof(XmlElement)) continue;
                                k = (XmlElement)l;
                                switch (k.Name.ToLower())
                                {
                                    case "assembly":
                                        reference.Add(k.InnerText);
                                        break;
                                }
                            }
                            break;
                        case "exprate":
                            this.exprate = int.Parse(i.InnerText);
                            break;
                        case "enhancematsuri":
                            this.enhanceMatsuri = bool.Parse(i.InnerText);
                            break;
                        case "stampdroprate":
                            this.stampDropRate = int.Parse(i.InnerText);
                            break;
                        case "itemfusionrate":
                            this.itemFusionRate = int.Parse(i.InnerText);
                            break;
                        case "questrate":
                            this.questrate = int.Parse(i.InnerText);
                            break;
                        case "questgoldrate":
                            this.questGoldRate = int.Parse(i.InnerText);
                            break;
                        case "warehouselimit":
                            this.warehouse = int.Parse(i.InnerText);
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
                        case "jobswitchreduceitem":
                            this.jobSwitchReduceItem = uint.Parse(i.InnerText);
                            break;
                        case "ringfameneededforemblem":
                            this.ringfameemblem = int.Parse(i.InnerText);
                            break;
                        case "maxfurniturecount":
                            this.maxFurnitureCount = uint.Parse(i.InnerText);
                            break;
                        case "deathpenaltybaseemil":
                            this.deathBaseRateEmil = (float)(int.Parse(i.InnerText)) / 100;
                            break;
                        case "deathpenaltyjobemil":
                            this.deathJobRateEmil = (float)(int.Parse(i.InnerText)) / 100;
                            break;
                        case "deathpenaltybasedominion":
                            this.deathBaseRateDom = (float)(int.Parse(i.InnerText)) / 100;
                            break;
                        case "deathpenaltyjobdominion":
                            this.deathJobRateDom = (float)(int.Parse(i.InnerText)) / 100;
                            break;
                        case "sqllog":
                            if (i.InnerText == "1")
                                this.sqlLog = true;
                            else
                                this.sqlLog = false;
                            break;
                        case "questupdatetime":
                            this.questUpdateTime = int.Parse(i.InnerText);
                            break;
                        case "questupdateamount":
                            this.questUpdateAmount = int.Parse(i.InnerText);
                            break;
                        case "questpointsmax":
                            this.questPointsMax = int.Parse(i.InnerText);
                            break;
                        case "onlinestatistic":
                            this.onlineStatic = (int.Parse(i.InnerText) == 1);
                            break;
                        case "statisticpagepath":
                            this.statisticsPage = i.InnerText;
                            break;
                        case "sqlloglevel":
                            Logger.SQLLogLevel.Value = int.Parse(i.InnerText);
                            break;
                        case "multipledrop":
                            this.multipleDrop = (i.InnerText == "1");
                            break;
                        case "bossslash":
                            this.bossSlash = (i.InnerText == "1");
                            break;
                        case "bossslashrate":
                            this.bossSlashRate = int.Parse(i.InnerText);
                            break;
                        case "globaldroprate":
                            this.globalDropRate = ((float)int.Parse(i.InnerText) / 100f);
                            break;
                        case "specialdroprate":
                            this.specialDropRate = ((float)int.Parse(i.InnerText) / 100f);
                            break;
                        case "pvpdamageratephysic":
                            this.pvpDmgRatePhysic = ((float)int.Parse(i.InnerText) / 100f);
                            break;
                        case "pvpdamageratemagic":
                            this.pvpDmgRateMagic = ((float)int.Parse(i.InnerText) / 100f);
                            break;
                        case "payloadrate":
                            this.payloadRate = ((float)int.Parse(i.InnerText) / 100f);
                            break;
                        case "volumerate":
                            this.volumeRate = ((float)int.Parse(i.InnerText) / 100f);
                            break;
                        case "TwitterID":
                            this.TwitterID = i.InnerText;
                            break;
                        case "TwitterPasswd":
                            this.TwitterPasswd = i.InnerText;
                            break;
                        case "speed":
                            this.speed = ushort.Parse(i.InnerText);
                            break;
                        case "mobamount":
                            this.mobamount = int.Parse(i.InnerText);
                            break;
                        case "basePhysicDef":
                            this.basePhysicDef = ushort.Parse(i.InnerText);
                            break;
                        case "basemagicdef":
                            this.baseMagicDef = ushort.Parse(i.InnerText);
                            break;
                        case "maxphysicdef":
                            this.maxPhysicDef = ushort.Parse(i.InnerText);
                            break;
                        case "maxmagicdef":
                            this.maxMagicDef = ushort.Parse(i.InnerText);
                            break;
                        case "activespecialloot":
                            this.ActiveSpecialLoot = bool.Parse(i.InnerText);
                            break;
                        case "bossspeciallootrate":
                            this.BossSpecialLootRate = int.Parse(i.InnerText);
                            break;
                        case "bossspeciallootid":
                            this.BossSpecialLootID = uint.Parse(i.InnerText);
                            break;
                        case "bossspeciallootnum":
                            this.BossSpecialLootNum = byte.Parse(i.InnerText);
                            break;
                        case "nomalmobspeciallootrate":
                            this.NomalMobSpecialLootRate = int.Parse(i.InnerText);
                            break;
                        case "nomalmobspeciallootid":
                            this.NomalMobSpecialLootID = uint.Parse(i.InnerText);
                            break;
                        case "nomalmobspeciallootnum":
                            this.NomalMobSpecialLootNum = byte.Parse(i.InnerText);
                            break;
                        case "activequestspecialreward":
                            this.ActivceQuestSpecialReward = bool.Parse(i.InnerText);
                            break;
                        case "questspecialrewardid":
                            this.QuestSpecialRewardID = uint.Parse(i.InnerText);
                            break;
                        case "questspecialrewardrate":
                            this.QuestSpecialRewardRate = int.Parse(i.InnerText);
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
//#else
        void InitDat(string path)
        {
            System.IO.FileStream fs = new System.IO.FileStream(path, System.IO.FileMode.Open, System.IO.FileAccess.Read);
            System.IO.BinaryReader br = new System.IO.BinaryReader(fs);
            int magic = br.ReadInt32();
            if (magic == 0x12345678)
            {
                int version = br.ReadInt32();
                byte len = br.ReadByte();
                this.host = Global.Unicode.GetString(br.ReadBytes(len));
                this.port = br.ReadInt32();
                len = br.ReadByte();
                this.dbhost = Global.Unicode.GetString(br.ReadBytes(len));
                this.dbport = br.ReadInt32();
                this.loglevel = br.ReadInt32();
                this.sqlLog = br.ReadBoolean();
                Logger.SQLLogLevel.Value = br.ReadInt32();
                len = br.ReadByte();
                this.dbuser = Global.Unicode.GetString(br.ReadBytes(len));
                len = br.ReadByte();
                this.dbpass = Global.Unicode.GetString(br.ReadBytes(len));
                len = br.ReadByte();
                this.loginhost = Global.Unicode.GetString(br.ReadBytes(len));
                this.loginport = br.ReadInt32();
                len = br.ReadByte();
                this.loginPass = Global.Unicode.GetString(br.ReadBytes(len));
                len = br.ReadByte();
                this.language = Global.Unicode.GetString(br.ReadBytes(len));
                this.version = (SagaLib.Version)br.ReadByte();
                len = br.ReadByte();
                this.encoding = Global.Unicode.GetString(br.ReadBytes(len));
                this.exprate = br.ReadInt32();
                this.questrate = br.ReadInt32();
                this.questGoldRate = br.ReadInt32();
                this.stampDropRate = br.ReadInt32();
                this.itemFusionRate = br.ReadInt32();
                this.mobamount = br.ReadInt32();
                this.questUpdateTime = br.ReadInt32();
                this.questUpdateAmount = br.ReadInt32();
                this.questPointsMax = br.ReadInt32();
                this.warehouse = br.ReadInt32();
                this.deathBaseRateEmil = br.ReadSingle();
                this.deathJobRateEmil = br.ReadSingle();
                this.deathBaseRateDom = br.ReadSingle();
                this.deathJobRateDom = br.ReadSingle();
                this.jobSwitchReduceItem = br.ReadUInt32();
                this.ringfameemblem = br.ReadInt32();
                this.maxFurnitureCount = br.ReadUInt32();
                this.onlineStatic = br.ReadBoolean();
                len = br.ReadByte();
                this.statisticsPage = Global.Unicode.GetString(br.ReadBytes(len));
                this.multipleDrop = br.ReadBoolean();
                this.bossSlash = br.ReadBoolean();
                this.bossSlashRate = br.ReadInt32();
                this.globalDropRate = br.ReadSingle();
                this.atkMastery = br.ReadBoolean();

                int count = br.ReadInt32();
                for (int i = 0; i < count; i++)
                {
                    len = br.ReadByte();
                    string txt = Global.Unicode.GetString(br.ReadBytes(len));
                    this.motd.Add(txt);
                }
                count = br.ReadInt32();
                for (int i = 0; i < count; i++)
                {
                    len = br.ReadByte();
                    string txt = Global.Unicode.GetString(br.ReadBytes(len));
                    this.reference.Add(txt);
                }
                count = br.ReadInt32();
                for (int i = 0; i < count; i++)
                {
                    uint mapID = br.ReadUInt32();
                    this.hostedMaps.Add(mapID);
                }

            }
        }
#endif

        public void InitAnnounce(string path)
        {
            System.IO.StreamReader sr = new System.IO.StreamReader(VirtualFileSystemManager.Instance.FileSystem.OpenFile(path), Encoding.GetEncoding(Configuration.Instance.DBEncoding));

            string[] paras;
            byte count = 0;
            while (!sr.EndOfStream)
            {
                string line;
                line = sr.ReadLine();
                try
                {
                    if (line == "") continue;
                    if (line.Substring(0, 1) == "#")
                        continue;
                    paras = line.Split(',');

                    int DueTime = int.Parse(paras[0]);
                    int Period = int.Parse(paras[1]);
                    string Text = paras[2];

                    count++;
                    Tasks.System.TaskAnnounce ta = new Tasks.System.TaskAnnounce("公告" + count, Text, DueTime,Period);
                    ta.Activate();
                }
                catch (Exception ex)
                {
                }
            }

            sr.Close();
        }

        public void Initialization(string path)
        {
            this.hostedMaps.Clear();
#if Text
            InitXML(path);
#else
            InitDat(path);
#endif
        }

        void GetRateOverride(ActorPC pc, out RateOverrideItem gmlv, out RateOverrideItem clv)
        {
            gmlv = null;
            clv = null;
            foreach (RateOverrideType i in rateOverride.Keys)
            {
                switch (i)
                {
                    case RateOverrideType.GMLv:
                        {
                            int maxValue = 0;
                            foreach (int j in rateOverride[i].Keys)
                            {
                                if (j > maxValue && j <= pc.Account.GMLevel)
                                    maxValue = j;
                            }
                            if (maxValue > 0)
                                gmlv = rateOverride[i][maxValue];
                        }
                        break;
                    case RateOverrideType.CLevel:
                        {
                            int maxValue = 0;
                            foreach (int j in rateOverride[i].Keys)
                            {
                                if (j > maxValue && j <= pc.Level)
                                    maxValue = j;
                            }
                            if (maxValue > 0)
                                clv = rateOverride[i][maxValue];
                        }
                        break;
                }
            }
        }
    }
}
