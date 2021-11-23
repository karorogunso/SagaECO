using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Net;
using System.Net.Sockets;

using SagaDB;
using SagaDB.Actor;
using SagaDB.Item;
using SagaDB.Npc;
using SagaDB.ECOShop;
using SagaLib;
using SagaMap;
using SagaMap.Manager;


namespace SagaMap.Network.Client
{
    public partial class MapClient
    {
        public int npcSelectResult;
        public bool npcShopClosed;
        uint currentEventID;
        public Scripting.Event currentEvent;
        public System.Threading.Thread scriptThread;
        public Dictionary<uint, uint> syntheseItem;
        public bool syntheseFinished;
        public Shop currentShop;
        public string inputContent;
        public bool npcJobSwitch;
        public bool npcJobSwitchRes;
        public bool vshopClosed = SagaMap.Configuration.Instance.VShopClosed;
        uint currentVShopCategory = 0;
        public uint selectedPet;

        public void OnNPCPetSelect(Packets.Client.CSMG_NPC_PET_SELECT p)
        {
            this.selectedPet = p.Result;
        }
        string ff() {
            return Environment.CurrentDirectory;
        }

        public void OnVShopBuy(Packets.Client.CSMG_VSHOP_BUY p)
        {
            if (!vshopClosed)
            {
                uint[] items = p.Items;
                uint[] counts = p.Counts;
                uint[] points = new uint[items.Length];
                int[] rental = new int[items.Length];
                int k = 0;
                uint neededPoints = 0;
                for (int i = 0; i < items.Length; i++)
                {
                    var cat = from item in ECOShopFactory.Instance.Items.Values
                              where item.Items.ContainsKey(items[i])
                              select item;

                    if (cat.Count() > 0)
                    {
                        ShopCategory category = cat.First();
                        if (counts[i] > 0)
                        {
                            ShopItem chip = category.Items[items[i]];
                            points[i] = chip.points;
                            rental[i] = chip.rental;
                        }
                    }
                }
                for (k = 0; k < items.Length; k++)
                {
                    neededPoints += points[k] * counts[k];
                }
                if (this.Character.VShopPoints >= neededPoints)
                {
                    this.Character.UsedVShopPoints += neededPoints;
                    this.Character.VShopPoints -= neededPoints;
                    for (k = 0; k < items.Length; k++)
                    {
                        if (counts[k] <= 0)
                            continue;
                        Item item = ItemFactory.Instance.GetItem(items[k]);
                        item.Stack = (ushort)counts[k];
                        if (rental[k] > 0)
                        {
                            item.Rental = true;
                            item.RentalTime = DateTime.Now + new TimeSpan(0, rental[k], 0);
                        }
                        Logger.LogItemGet(Logger.EventType.ItemVShopGet, this.Character.Name + "(" + this.Character.CharID + ")", item.BaseData.name + "(" + item.ItemID + ")",
                            string.Format("VShopBuy Count:{0}", item.Stack), false);
                        AddItem(item, true);
                    }
                }
            }
            
        }
        public void OnNCShopBuy(Packets.Client.CSMG_NCSHOP_BUY p)
        {
            switch (this.Character.UsingShopType)
            {
                case PlayerUsingShopType.None:
                    break;
                case PlayerUsingShopType.GShop:
                    HandleGShopBuy(p);
                    break;
                case PlayerUsingShopType.NCShop:
                    HandleNCShopBuy(p);
                    break;
                default:
                    break;
            }
        }

        public void HandleNCShopBuy(Packets.Client.CSMG_NCSHOP_BUY p)
        {
            uint[] items = p.Items;
            uint[] counts = p.Counts;
            uint[] points = new uint[items.Length];
            int[] rental = new int[items.Length];
            int k = 0;
            uint neededPoints = 0;
            for (int i = 0; i < items.Length; i++)
            {
                var cat = from item in NCShopFactory.Instance.Items.Values
                          where item.Items.ContainsKey(items[i])
                          select item;

                if (cat.Count() > 0)
                {
                    NCShopCategory category = cat.First();
                    if (counts[i] > 0)
                    {
                        ShopItem chip = category.Items[items[i]];
                        points[i] = chip.points;
                        rental[i] = chip.rental;
                    }
                }
            }
            for (k = 0; k < items.Length; k++)
            {
                neededPoints += points[k] * counts[k];
            }
            if (this.Character.CP >= neededPoints)
            {
                this.Character.UsedVShopPoints += neededPoints;
                this.Character.CP -= neededPoints;
                for (k = 0; k < items.Length; k++)
                {
                    if (counts[k] <= 0)
                        continue;
                    Item item = ItemFactory.Instance.GetItem(items[k]);
                    item.Stack = (ushort)counts[k];
                    if (rental[k] > 0)
                    {
                        item.Rental = true;
                        item.RentalTime = DateTime.Now + new TimeSpan(0, rental[k], 0);
                    }
                    Logger.LogItemGet(Logger.EventType.ItemVShopGet, this.Character.Name + "(" + this.Character.CharID + ")", item.BaseData.name + "(" + item.ItemID + ")",
                        string.Format("NCShopBuy Count:{0}", item.Stack), false);
                    AddItem(item, true);
                }
            }
        }

        public void HandleGShopBuy(Packets.Client.CSMG_NCSHOP_BUY p)
        {
            uint[] items = p.Items;
            uint[] counts = p.Counts;
            uint[] points = new uint[items.Length];
            int[] rental = new int[items.Length];
            int k = 0;
            uint neededPoints = 0;
            for (int i = 0; i < items.Length; i++)
            {
                var cat = from item in GShopFactory.Instance.Items.Values
                          where item.Items.ContainsKey(items[i])
                          select item;

                if (cat.Count() > 0)
                {
                    GShopCategory category = cat.First();
                    if (counts[i] > 0)
                    {
                        ShopItem chip = category.Items[items[i]];
                        points[i] = chip.points;
                        rental[i] = chip.rental;
                    }
                }
            }
            for (k = 0; k < items.Length; k++)
            {
                neededPoints += points[k] * counts[k];
            }
            if (this.Character.Gold >= neededPoints)
            {
                this.Character.Gold -= neededPoints;
                for (k = 0; k < items.Length; k++)
                {
                    if (counts[k] <= 0)
                        continue;
                    Item item = ItemFactory.Instance.GetItem(items[k]);
                    item.Stack = (ushort)counts[k];
                    if (rental[k] > 0)
                    {
                        item.Rental = true;
                        item.RentalTime = DateTime.Now + new TimeSpan(0, rental[k], 0);
                    }
                    Logger.LogItemGet(Logger.EventType.ItemNPCGet, this.Character.Name + "(" + this.Character.CharID + ")", item.BaseData.name + "(" + item.ItemID + ")",
                        string.Format("GShopBuy Count:{0}", item.Stack), false);
                    AddItem(item, true);
                }
            }
        }

        public void OnNCShopCategoryRequest(Packets.Client.CSMG_NCSHOP_CATEGORY_REQUEST p)
        {
            NCShopCategory category = NCShopFactory.Instance.Items[p.Page + 1];
            Packets.Server.SSMG_NCSHOP_INFO_HEADER p1 = new SagaMap.Packets.Server.SSMG_NCSHOP_INFO_HEADER();
            p1.Page = p.Page;
            this.netIO.SendPacket(p1);
            this.currentVShopCategory = p.Page + 1;
            foreach (uint i in category.Items.Keys)
            {
                Packets.Server.SSMG_NCSHOP_INFO p2 = new SagaMap.Packets.Server.SSMG_NCSHOP_INFO();
                p2.Point = category.Items[i].points;
                p2.ItemID = i;
                p2.Comment = category.Items[i].comment;
                this.netIO.SendPacket(p2);
            }

            Packets.Server.SSMG_NCSHOP_INFO_FOOTER p3 = new SagaMap.Packets.Server.SSMG_NCSHOP_INFO_FOOTER();
            this.netIO.SendPacket(p3);
        }
        public void OnNCShopClose(Packets.Client.CSMG_NCSHOP_CLOSE p)
        {
            this.Character.UsingShopType = PlayerUsingShopType.None;
            vshopClosed = true;
        }
        public void OnVShopClose(Packets.Client.CSMG_VSHOP_CLOSE p)
        {
            vshopClosed = true;
        }

        public void OnVShopCategoryRequest(Packets.Client.CSMG_VSHOP_CATEGORY_REQUEST p)
        {
            if (!vshopClosed)
            {
                ShopCategory category = ECOShopFactory.Instance.Items[p.Page + 1];
                Packets.Server.SSMG_VSHOP_INFO_HEADER p1 = new SagaMap.Packets.Server.SSMG_VSHOP_INFO_HEADER();
                p1.Page = p.Page;
                this.netIO.SendPacket(p1);
                this.currentVShopCategory = p.Page + 1;
                foreach (uint i in category.Items.Keys)
                {
                    Packets.Server.SSMG_VSHOP_INFO p2 = new SagaMap.Packets.Server.SSMG_VSHOP_INFO();
                    p2.Point = category.Items[i].points;
                    p2.ItemID = i;
                    p2.Comment = category.Items[i].comment;
                    this.netIO.SendPacket(p2);
                }

                Packets.Server.SSMG_VSHOP_INFO_FOOTER p3 = new SagaMap.Packets.Server.SSMG_VSHOP_INFO_FOOTER();
                this.netIO.SendPacket(p3);
            }
        }

        public void OnNPCJobSwitch(Packets.Client.CSMG_NPC_JOB_SWITCH p)
        {
            if (!npcJobSwitch)
                return;
            npcJobSwitchRes = false;
            if (p.Unknown != 0)
            {
                npcJobSwitchRes = true;
                Item item = this.Character.Inventory.GetItem(Configuration.Instance.JobSwitchReduceItem, Inventory.SearchType.ITEM_ID);
                if (item != null || p.ItemUseCount == 0)
                {
                    if (item != null)
                    {
                        if (item.Stack >= p.ItemUseCount)
                        {
                            DeleteItem(item.Slot, (ushort)p.ItemUseCount, true);
                        }
                        else
                            return;
                    }
                    this.Character.SkillsReserve.Clear();
                    //check maximal reservalbe skill count
                    int count = 0;
                    if (this.Character.Job == this.Character.Job2X)
                        count = this.Character.JobLevel2X / 10;
                    if (this.Character.Job == this.Character.Job2T)
                        count = this.Character.JobLevel2T / 10;
                    if (count >= p.Skills.Length)
                    {
                        //set reserved skills
                        foreach (ushort i in p.Skills)
                        {
                            if (this.Character.Skills2.ContainsKey(i))
                            {
                                this.Character.SkillsReserve.Add(i, this.Character.Skills2[i]);
                            }
                        }
                    }

                    //clear skills
                    ResetSkill(2);

                    //change job and reduce job level
                    int levelLost = 0;
                    if (this.Character.Job == this.Character.Job2X)
                    {
                        this.Character.Job = this.Character.Job2T;
                        levelLost = (int)(this.Character.JobLevel2T / 5 - p.ItemUseCount);
                        if (levelLost <= 0)
                            levelLost = 0;
                        if (this.Character.SkillPoint2T > levelLost)
                            this.Character.SkillPoint2T -= (ushort)levelLost;
                        else
                            this.Character.SkillPoint2T = 0;
                        this.Character.JobLevel2T -= (byte)levelLost;
                        this.Character.JEXP = ExperienceManager.Instance.GetExpForLevel(this.Character.JobLevel2T, Scripting.LevelType.JLEVEL2T);
                    }
                    else
                    {
                        this.Character.Job = this.Character.Job2X;
                        levelLost = (int)(this.Character.JobLevel2X / 5 - p.ItemUseCount);
                        if (levelLost <= 0)
                            levelLost = 0;
                        if (this.Character.SkillPoint2X > levelLost)
                            this.Character.SkillPoint2X -= (ushort)levelLost;
                        else
                            this.Character.SkillPoint2X = 0;
                        this.Character.JobLevel2X -= (byte)levelLost;
                        this.Character.JEXP = ExperienceManager.Instance.GetExpForLevel(this.Character.JobLevel2X, Scripting.LevelType.JLEVEL2);
                    }

                    PC.StatusFactory.Instance.CalcStatus(this.Character);
                    SendPlayerInfo();
                    
                    EffectArg arg = new EffectArg();
                    arg.effectID = 4131;
                    arg.actorID = this.Character.ActorID;
                    this.map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.SHOW_EFFECT, arg, this.Character, true);
                }
            }
            npcJobSwitch = false;
        }

        public void OnNPCInputBox(Packets.Client.CSMG_NPC_INPUTBOX p)
        {
            inputContent = p.Content;
        }

        public void OnNPCShopBuy(Packets.Client.CSMG_NPC_SHOP_BUY p)
        {
            uint[] goods = p.Goods;
            uint[] counts = p.Counts;
            if (Character.HP == 0) return;
            if (this.currentShop != null && goods.Length > 0)
            {
                uint gold = 0;
                switch (this.currentShop.ShopType)
                {
                    case ShopType.None:
                        gold = (uint)this.Character.Gold;
                        break;
                    case ShopType.CP:
                        gold = this.Character.CP;
                        break;
                    case ShopType.ECoin:
                        gold = this.Character.ECoin;
                        break;
                }
                for (int i = 0; i < goods.Length; i++)
                {
                    if (this.currentShop.Goods.Contains(goods[i]))
                    {
                        Item item = ItemFactory.Instance.GetItem(goods[i]);
                        item.Stack = (ushort)counts[i];
                        short buyrate = 0;
                        if (this.currentShop.ShopType == ShopType.None)
                            buyrate = this.chara.Status.buy_rate;
                        uint price = (uint)(item.BaseData.price * (((float)(this.currentShop.SellRate + buyrate)) / 200));
                        if (price == 0) price = 1;
                        price = price * item.Stack;
                        if (gold >= price)
                        {
                            ushort stack = item.Stack;
                            gold -= price;
                            Logger.LogItemGet(Logger.EventType.ItemNPCGet, this.Character.Name + "(" + this.Character.CharID + ")", item.BaseData.name + "(" + item.ItemID + ")",
                                string.Format("ShopBuy Count:{0}", item.Stack), false);
                            this.AddItem(item, true);
                        }
                    }
                }
                switch (this.currentShop.ShopType)
                {
                    case ShopType.None:
                        this.Character.Gold = (int)gold;
                        break;
                    case ShopType.CP:
                        this.Character.CP = gold;
                        break;
                    case ShopType.ECoin:
                        this.Character.ECoin = gold;
                        break;
                }
                this.Character.Inventory.CalcPayloadVolume();
                this.SendCapacity();
            }
            else
            {
                if (this.currentEvent != null)
                {
                    long gold = this.Character.Gold;

                    switch (Character.TInt["ShopType"])
                    {
                        case 0:
                            gold = (uint)this.Character.Gold;
                            break;
                        case 1:
                            gold = this.Character.CP;
                            break;
                        case 2:
                            gold = this.Character.ECoin;
                            break;
                    }

                    for (int i = 0; i < goods.Length; i++)
                    {
                        if (this.currentEvent.Goods.Contains(goods[i]))
                        {
                            Item item = ItemFactory.Instance.GetItem(goods[i]);
                            item.Stack = (ushort)counts[i];
                            int price = (int)(item.BaseData.price * (((float)(this.Character.Status.buy_rate)) / 1000));
                            if (price == 0) price = 1;
                            price = price * item.Stack;
                            if (gold >= price)
                            {
                                ushort stack = item.Stack;
                                gold -= price;
                                Logger.LogItemGet(Logger.EventType.ItemNPCGet, this.Character.Name + "(" + this.Character.CharID + ")", item.BaseData.name + "(" + item.ItemID + ")",
                                    string.Format("AddItem Count:{0}", item.Stack), false);
                                this.AddItem(item, true);
                            }
                        }
                    }
                    //this.Character.Gold = gold;

                    switch (Character.TInt["ShopType"])
                    {
                        case 0:
                            Character.Gold = gold;
                            break;
                        case 1:
                            Character.CP = (uint)gold;
                            break;
                        case 2:
                            Character.ECoin = (uint)gold;
                            break;
                    }

                    this.Character.Inventory.CalcPayloadVolume();
                    this.SendCapacity();
                }
            }
        }

        public void OnNPCShopSell(Packets.Client.CSMG_NPC_SHOP_SELL p)
        {
            uint[] goods = p.Goods;
            uint[] counts = p.Counts;

            if (this.currentShop != null)            
            {
                uint total = 0;
                for (int i = 0; i < goods.Length; i++)
                {

                    Item itemDroped = this.Character.Inventory.GetItem(goods[i]);
                    if (itemDroped == null)
                        return;
                    if (counts[i] > itemDroped.Stack)
                        counts[i] = itemDroped.Stack;
                    Logger.LogItemLost(Logger.EventType.ItemNPCLost, this.Character.Name + "(" + this.Character.CharID + ")", itemDroped.BaseData.name + "(" + itemDroped.ItemID + ")",
                    string.Format("NPCShopSell Count:{0}", counts[i]), false);

                    this.DeleteItem(goods[i], (ushort)counts[i], true);

                    uint price = (uint)(itemDroped.BaseData.price * counts[i] * (((float)(10 + this.Character.Status.sell_rate)) / 100));
                    total += price;
                }
                this.Character.Gold += (int)total;
                this.Character.Inventory.CalcPayloadVolume();
                this.SendCapacity();
            }
            else
            {
                if (this.currentEvent != null)
                {
                    uint total = 0;
                    for (int i = 0; i < goods.Length; i++)
                    {

                        Item itemDroped = this.Character.Inventory.GetItem(goods[i]);
                        if (itemDroped == null)
                            return;                    
                        Logger.LogItemLost(Logger.EventType.ItemNPCLost, this.Character.Name + "(" + this.Character.CharID + ")", itemDroped.BaseData.name + "(" + itemDroped.ItemID + ")",
                            string.Format("NPCShopSell Count:{0}", counts[i]), false);
                    
                        this.DeleteItem(goods[i], (ushort)counts[i], true);

                        uint price = (uint)(itemDroped.BaseData.price * counts[i] * (((float)(10 + this.Character.Status.sell_rate)) / 100));

                        total += price;                       
                    }
                    this.Character.Gold += (int)total;
                    this.Character.Inventory.CalcPayloadVolume();
                    this.SendCapacity();
                }
            }
        }

        public void OnNPCShopClose(Packets.Client.CSMG_NPC_SHOP_CLOSE p)
        {
            npcShopClosed = true;
        }

        public void OnNPCSelect(Packets.Client.CSMG_NPC_SELECT p)
        {
            npcSelectResult = p.Result;
        }

        public void OnNPCSynthese(Packets.Client.CSMG_NPC_SYNTHESE p)
        {
            Dictionary<uint, ushort> ids = p.SynIDs;
            foreach (var item in ids)
            {
                if (!this.syntheseItem.ContainsKey(ids[item.Key]))
                    this.syntheseItem.Add(item.Key, item.Value);
            }
        }

        public void OnNPCSyntheseFinish(Packets.Client.CSMG_NPC_SYNTHESE_FINISH p)
        {
            this.syntheseFinished = true;
        }

        public void OnNPCEventStart(Packets.Client.CSMG_NPC_EVENT_START p)
        {
            if (this.scriptThread == null)
            {
                if (this.tradingTarget != null || trading || this.chara.Buff.GetReadyPossession)
                {
                    SendEventStart(p.EventID);
                    SendCurrentEvent(p.EventID);
                    SendEventEnd();
                    return;
                }
                //if (p.EventID < 20000000 || p.EventID >= 0xF0000000)Unknow为啥要限制编号
                if (true)
                {
                    if (p.EventID >= 11000000)
                    {
                        if (NPCFactory.Instance.Items.ContainsKey(p.EventID))
                        {
                            NPC npc = NPCFactory.Instance.Items[p.EventID];
                            uint mapid;
                            if (this.map.IsMapInstance)
                            {
                                if (map.OriID != 0)
                                    mapid = this.map.OriID;
                                else
                                    mapid = map.ID * 100 / 1000;
                            }
                            else
                                mapid = this.map.ID;
                            if (npc.MapID == mapid)
                            {
                                if (Math.Abs(this.Character.X - Global.PosX8to16(npc.X, map.Width)) > 700 ||
                                    Math.Abs(this.Character.Y - Global.PosY8to16(npc.Y, map.Height)) > 700)
                                {
                                    SendEventStart(p.EventID);
                                    SendCurrentEvent(p.EventID);
                                    SendEventEnd();
                                    return;
                                }
                            }
                            else
                            {
                                SendEventStart(p.EventID);
                                SendCurrentEvent(p.EventID);
                                SendEventEnd();
                                return;
                            }
                        }
                    }
                    else
                    {
                        if (p.EventID != 10000315 && p.EventID != 10000316)//Exception for flying garden events
                        {
                            if (this.map.Info.events.ContainsKey(p.EventID))
                            {
                                byte[] pos = this.map.Info.events[p.EventID];
                                byte x, y;
                                x = Global.PosX16to8(this.chara.X, map.Width);
                                y = Global.PosY16to8(this.chara.Y, map.Height);
                                bool valid = false;
                                for (int i = 0; i < pos.Length / 2; i++)
                                {
                                    if (Math.Abs((int)pos[i*2] - x) <= 3 && Math.Abs((int)pos[i*2 + 1] - y) <= 3)
                                    {
                                        valid = true;
                                        break;
                                    }
                                }
                                if (!valid)
                                {
                                    SendHack();
                                    SendEventStart(p.EventID);
                                    SendCurrentEvent(p.EventID);
                                    SendEventEnd();
                                    return;
                                }
                            }
                            else
                            {
                                SendHack();
                                SendEventStart(p.EventID);
                                SendCurrentEvent(p.EventID);
                                SendEventEnd();
                                return;
                            }
                        }
                    }
                    EventActivate(p.EventID);
                }
                else
                {
                    SendEventStart(p.EventID);
                    SendCurrentEvent(p.EventID);
                    SendEventEnd();
                }
            }
            else
            {
                SendEventStart(p.EventID);
                SendCurrentEvent(p.EventID);
                SendEventEnd();
            }
        }

        public void EventActivate(uint EventID)
        {
            if(this.Character.Account.GMLevel > 100)
            this.SendSystemMessage("触发ID:" + EventID.ToString());
            if (ScriptManager.Instance.Events.ContainsKey(EventID))
            {
                System.Threading.Thread thread = new System.Threading.Thread(RunScript);
                thread.Name = string.Format("ScriptThread({0}) of player:{1}", thread.ManagedThreadId, this.Character.Name);
                ClientManager.AddThread(thread);
                if (this.scriptThread != null)
                {
                    Logger.ShowDebug("current script thread != null, currently running:" + currentEventID.ToString(), Logger.defaultlogger);
                    this.scriptThread.Abort();
                }
                currentEventID = EventID;
                this.scriptThread = thread;
                thread.Start();
            }
            else
            {
                SendEventStart(EventID);

                SendCurrentEvent(EventID);

                SendNPCMessageStart();
                if (this.account.GMLevel > 0)
                {
                    SendNPCMessage(EventID, string.Format(LocalManager.Instance.Strings.NPC_EventID_NotFound, EventID), 131, "System Error");
                }
                else
                {
                    SendNPCMessage(EventID, string.Format(LocalManager.Instance.Strings.NPC_EventID_NotFound_Msg, EventID), 131, "");
                }
                SendNPCMessageEnd();
                SendEventEnd();
                Logger.ShowWarning("No script loaded for EventID:" + EventID);

            }
        }

        void RunScript()
        {
            ClientManager.EnterCriticalArea();
            Scripting.Event evnt = null;
            try
            {
                evnt = ScriptManager.Instance.Events[currentEventID];
                if (currentEventID < 0xFFFF0000)
                {
                    SendEventStart(currentEventID);
                    SendCurrentEvent(currentEventID);
                }
                this.currentEvent = evnt;
                this.currentEvent.CurrentPC = this.Character;
                bool runscript = true;
                if (this.Character.Quest != null)
                {
                    if (this.Character.Quest.Detail.NPCSource == evnt.EventID)
                    {
                        if (this.Character.Quest.CurrentCount1 == 0 && this.Character.Quest.Status == SagaDB.Quests.QuestStatus.OPEN)
                        {                           
                            this.Character.Quest.CurrentCount1 = 1;
                            evnt.OnTransportSource(this.Character);
                            evnt.OnQuestUpdate(this.Character, this.Character.Quest);
                            runscript = false;
                        }
                        else
                        {
                            if (this.Character.Quest.CurrentCount2 == 1)
                            {
                                evnt.OnTransportCompleteSrc(this.Character);
                                runscript = false;
                            }
                        }
                    }
                    if (this.Character.Quest.Detail.NPCDestination == evnt.EventID)
                    {
                        if (this.Character.Quest.CurrentCount2 == 0 && this.Character.Quest.Status == SagaDB.Quests.QuestStatus.OPEN)
                        {
                            evnt.OnTransportDest(this.Character);
                            if (this.Character.Quest.CurrentCount3 == 0)
                            {
                                this.Character.Quest.CurrentCount2 = 1;
                                this.Character.Quest.Status = SagaDB.Quests.QuestStatus.COMPLETED;
                                evnt.OnQuestUpdate(this.Character, this.Character.Quest);
                                SendQuestStatus();
                                runscript = false;
                            }
                        }
                        else
                        {
                            evnt.OnTransportCompleteDest(this.Character);
                            runscript = false;
                        }
                    }
                }
                if (runscript)
                {

                    this.currentEvent.OnEvent(this.Character);
                }
                if (currentEventID < 0xFFFF0000)
                    SendEventEnd();
            }
            catch (System.Threading.ThreadAbortException)
            {
                try
                {
                    ClientManager.RemoveThread(this.scriptThread.Name);
                    ClientManager.LeaveCriticalArea(this.scriptThread);
                    if (evnt != null)
                        evnt.CurrentPC = null;
                    this.currentEvent = null;
                    if (this.Character != null)
                        Logger.ShowWarning(string.Format("Player:{0} logged out while script thread is still running, terminating the script thread!", this.Character.Name));
                }
                catch { }
                this.scriptThread = null;
            }
            catch (Exception ex)
            {
                try
                {
                    if (this.Character.Online)
                    {
                        if (this.Character.Account.GMLevel > 2)
                        {
                            SendNPCMessageStart();
                            SendNPCMessage(currentEventID, "Script Error(" + ScriptManager.Instance.Events[currentEventID].ToString() + "):" + ex.Message, 131, "System Error");
                            SendNPCMessageEnd();
                        }
                        SendEventEnd();
                    }

                    Logger.ShowWarning("Script Error(" + ScriptManager.Instance.Events[currentEventID].ToString() + "):" + ex.Message + "\r\n" + ex.StackTrace);
                }
                catch { }
            }
            if (evnt != null)
                evnt.CurrentPC = null;
            this.scriptThread = null;
            this.currentEvent = null;
            ClientManager.RemoveThread(System.Threading.Thread.CurrentThread.Name);
            ClientManager.LeaveCriticalArea();
        }

        public void SendEventStart(uint id)
        {
            if (!this.Character.Online)
                return;
            Packets.Server.SSMG_NPC_EVENT_START p = new SagaMap.Packets.Server.SSMG_NPC_EVENT_START();
            this.netIO.SendPacket(p);
            Packets.Server.SSMG_NPC_EVENT_START_RESULT p2 = new Packets.Server.SSMG_NPC_EVENT_START_RESULT();
            p2.NPCID = id;
            this.netIO.SendPacket(p2);

        }

        public void SendEventEnd()
        {
            if (!this.Character.Online)
                return;
            /*string args = "05 F4 00";
            byte[] buf = Conversions.HexStr2Bytes(args.Replace(" ", ""));
            Packet ps1 = new Packet();
            ps1.data = buf;*/
            //this.netIO.SendPacket(ps1);
            Packets.Server.SSMG_NPC_EVENT_END p = new SagaMap.Packets.Server.SSMG_NPC_EVENT_END();
            this.netIO.SendPacket(p);            
        }

        public void SendCurrentEvent(uint eventid)
        {
            Packets.Server.SSMG_NPC_CURRENT_EVENT p = new SagaMap.Packets.Server.SSMG_NPC_CURRENT_EVENT();
            p.EventID = eventid;
            this.netIO.SendPacket(p);
        }

        public void SendNPCMessageStart()
        {
            if (!this.Character.Online)
                return;
            Packets.Server.SSMG_NPC_MESSAGE_START p = new SagaMap.Packets.Server.SSMG_NPC_MESSAGE_START();
            this.netIO.SendPacket(p);
        }

        public void SendNPCMessageEnd()
        {
            if (!this.Character.Online)
                return;
            Packets.Server.SSMG_NPC_MESSAGE_END p = new SagaMap.Packets.Server.SSMG_NPC_MESSAGE_END();
            this.netIO.SendPacket(p);
        }

        public void SendNPCMessage(uint npcID, string message, ushort motion, string title)
        {
            try
            {
                if (!this.Character.Online)
                    return;
                Packets.Server.SSMG_NPC_MESSAGE p = new SagaMap.Packets.Server.SSMG_NPC_MESSAGE();
                if(message.Contains('%'))
                {
                    string newmessage = "";
                    string temp = "";
                    string[] paras = message.Split('%');
                    for (int i = 0; i < paras.Length; i++)
                    {
                        temp = temp + paras[i];
                        temp = temp.Replace("$P", "");
                        if (i != paras.Length -1)
                            temp = temp + "$P";
                        newmessage += temp;
                    }
                    message = newmessage;
                }
                if (message.Length > 50)
                {
                    int count = message.Length / 50;
                    List<string> messages = new List<string>();
                    for (int i = 0; i < count; i++)
                        messages.Add(message.Substring(50 * i, 50));
                    if (message.Length != count * 50)
                        messages.Add(message.Substring(count * 50, message.Length - count * 50));
                    foreach (string item in messages)
                    {
                        p = new SagaMap.Packets.Server.SSMG_NPC_MESSAGE();
                        p.SetMessage(npcID, 1, item, motion, title);
                        this.netIO.SendPacket(p);
                    }
                }
                else
                {
                    p.SetMessage(npcID, 1, message, motion, title);
                    this.netIO.SendPacket(p);
                }
            }
            catch(Exception ex)
            {
                SagaLib.Logger.ShowError(ex);
            }
        }

        public void SendNPCWait(uint wait)
        {
            Packets.Server.SSMG_NPC_WAIT p = new SagaMap.Packets.Server.SSMG_NPC_WAIT();
            p.Wait = wait;
            this.netIO.SendPacket(p);
        }
        public void SendNPCPlaySound(uint soundID, byte loop, uint volume, byte balance)
        {
            SendNPCPlaySound(soundID, loop, volume, balance, false);
        }
        public void SendNPCPlaySound(uint soundID, byte loop, uint volume, byte balance ,bool stopBGM)
        {
            Packets.Server.SSMG_NPC_PLAY_SOUND p = new SagaMap.Packets.Server.SSMG_NPC_PLAY_SOUND();
            if (stopBGM) p.ID = 0x05EE;
            p.SoundID = soundID;
            p.Loop = loop;
            p.Volume = volume;
            p.Balance = balance;
            this.netIO.SendPacket(p);
        }
        public void SendChangeBGM(uint soundID, byte loop, uint volume, byte balance)
        {
            Packets.Server.SSMG_NPC_CHANGE_BGM p = new SagaMap.Packets.Server.SSMG_NPC_CHANGE_BGM();
            p.SoundID = soundID;
            p.Loop = loop;
            p.Volume = volume;
            p.Balance = balance;
            this.netIO.SendPacket(p);
        }
        public void SendNPCShowEffect(uint actorID, byte x, byte y, ushort height, uint effectID, bool oneTime)
        {
            Packets.Server.SSMG_NPC_SHOW_EFFECT p = new SagaMap.Packets.Server.SSMG_NPC_SHOW_EFFECT();
            p.ActorID = actorID;
            p.EffectID = effectID;
            p.X = x;
            p.Y = y;
            p.height = height;
            p.OneTime = oneTime;
            this.netIO.SendPacket(p);
        }

        public void SendNPCStates()
        {
            Dictionary<uint, bool> AllInvolvedNPCStates = (from npc in NPCFactory.Instance.Items.Values where npc.MapID == this.Character.MapID select npc).ToDictionary(i => i.ID, i=>false);
            for (int i=0;i<AllInvolvedNPCStates.Count;i++)
            {
                uint npcid = AllInvolvedNPCStates.Keys.ElementAt(i);
                if (this.Character.NPCStates.ContainsKey(npcid))
                    AllInvolvedNPCStates[npcid] = this.Character.NPCStates[npcid];
            }

            int unloadedCount = AllInvolvedNPCStates.Count;
            int loadedCount = 0;
            List<Dictionary<uint, bool>> pages = new List<Dictionary<uint, bool>>();
            while (unloadedCount > 0)
            {
                if (unloadedCount > 100)
                {
                    pages.Add(AllInvolvedNPCStates.Skip(loadedCount).Take(100).ToDictionary(i => i.Key, i => i.Value));
                    loadedCount += 100;
                    unloadedCount -= 100;
                }
                else
                {
                    pages.Add(AllInvolvedNPCStates.Skip(loadedCount).Take(unloadedCount).ToDictionary(i => i.Key, i => i.Value));
                    loadedCount += unloadedCount;
                    unloadedCount = 0;
                }
            }
            foreach (Dictionary<uint, bool> subpage in pages)
            {
                Packets.Server.SSMG_NPC_STATES p = new SagaMap.Packets.Server.SSMG_NPC_STATES();
                p.PutNPCStates(subpage);
                this.netIO.SendPacket(p);
            }
            /*
            if (this.Character.NPCStates.ContainsKey(this.map.ID))
            {
                foreach (uint i in this.chara.NPCStates[this.map.ID].Keys)
                {
                    if (this.chara.NPCStates[this.map.ID][i])
                    {
                        Packets.Server.SSMG_NPC_SHOW p = new SagaMap.Packets.Server.SSMG_NPC_SHOW();
                        p.NPCID = i;
                        this.netIO.SendPacket(p);
                    }
                    else
                    {
                        Packets.Server.SSMG_NPC_HIDE p = new SagaMap.Packets.Server.SSMG_NPC_HIDE();
                        p.NPCID = i;
                        this.netIO.SendPacket(p);
                    }
                }
            } 
            */
        }
    }
}
