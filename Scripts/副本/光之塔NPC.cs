
using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaDB.Item;
using System.Text;
using System.Diagnostics;
using SagaMap.Scripting;
using System.Globalization;
using SagaScript.Chinese.Enums;
namespace WeeklyExploration
{
    public partial class GQuest : Event
    {
        public GQuest()
        {
            this.EventID = 50002013;
        }
        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 0, "欢迎回顾上测副本！$R$R请注意：$R复刻副本仅作为回顾用，$R打开副本战利品需要从商城购买要是。$R", "复刻副本接线员");
            switch (Select(pc, "请选择副本", "", "光之塔副本(最大8人)(需要：30任务点)", "不死皇城的吸血鬼(最大4人)(需要任务点：30）","离开"))
            {
                case 1:
                    switch (Select(pc, "请选择难度【光之塔副本】", "", "复刻(复活:0次)", "离开"))
                    {
                        case 1:
                            光之塔困难(pc);
                            break;
                        case 2:
                            break;
                    }
                    break;
                case 2:
                    switch (Select(pc, "请选择难度【不死皇城的吸血鬼】", "", "复刻(复活:0次)", "离开"))
                    {
                        case 1:
                            不死皇城的吸血鬼(pc);
                            break;
                        case 2:
                            break;
                    }
                    break;
            }
        }
        void 不死皇城的吸血鬼(ActorPC pc)
        {
            switch (Select(pc, "不死皇城的吸血鬼(复刻)", "", "从头开始进入副本", "离开"))
            {
                case 1:
                    if (checkpartyfor不死皇城(pc))
                        进入普通(pc);
                    break;
            }
        }
        void 光之塔困难(ActorPC pc)
        {
            switch (Select(pc, "光之塔副本(复刻)", "", "从头开始进入副本", "中途进入副本", "离开"))
            {
                case 1:
                    if (checkpartyforhard(pc))
                        创建困难(pc);
                    break;
                case 2:

                    if (pc.Party == null) return;

                    if ((uint)pc.Party.Leader.TInt["S10070000"] != 0)
                    {
                        foreach (var item in pc.Party.Members.Values)
                        {
                            if (item.MapID == pc.Party.Leader.TInt["S20140000"] || item.MapID == pc.Party.Leader.TInt["S20146000"] || item.MapID == pc.Party.Leader.TInt["S20163000"])
                            {
                                SagaMap.Map m = SagaMap.Manager.MapManager.Instance.GetMap(item.MapID);
                                m.Announce("由于有队员重新进入了副本，BOSS血量重置了。");
                                foreach (var ac in m.Actors.Values)
                                {
                                    if (ac.type == ActorType.MOB)
                                    {
                                        if (ac.HP > 0)
                                            ac.HP = ac.MaxHP;
                                    }
                                }
                            }
                        }
                        pc.AInt["光之塔困难限制测试"] = GetWeekOfYear(DateTime.Now);
                        pc.TInt["副本复活标记"] = 1;
                        pc.Party.Leader.TInt["复活次数"] = 0;
                        pc.Party.Leader.TInt["设定复活次数"] = 0;
                        Warp(pc, (uint)pc.Party.Leader.TInt["S10070000"], 15, 101);
                    }
                    break;
            }
        }
        bool checkpartyfor不死皇城(ActorPC pc)
        {
            if (pc.Party == null)
            {
                Say(pc, 131, "这个地方太危险啦，$R请组队后一起前往吧。", "复刻副本");
                return false;
            }
            if (pc.Party.Leader != pc)
            {
                Say(pc, 131, "对不起，$R你不是队长耶$R$R请由队长来向我申请吧", "复刻副本");
                return false;
            }
            if (pc.Party.MemberCount > 4)
            {
                Say(pc, 131, "人数似乎太多了呢..$R坐不下哦", "复刻副本");
                return false;
            }
            foreach (var item in pc.Party.Members.Values)
            {
                if (item.QuestRemaining < 30)
                {
                    Say(pc, 131, item.Name + " 任务点不足！", "复刻副本");
                    return false;
                }
            }
            foreach (var item in pc.Party.Members.Values)
            {
                if (item.MapID != pc.MapID)
                {
                    Say(pc, 131, item.Name + " 没有在附近", "复刻副本");
                    return false;
                }
            }

            return true;
        }
        bool checkpartyforhard(ActorPC pc)
        {
            if (pc.Party == null)
            {
                Say(pc, 131, "这个地方太危险啦，$R请组队后一起前往吧。", "复刻副本");
                return false;
            }
            if (pc.Party.Leader != pc)
            {
                Say(pc, 131, "对不起，$R你不是队长耶$R$R请由队长来向我申请吧", "复刻副本");
                return false;
            }
            if (pc.Party.MemberCount > 8)
            {
                Say(pc, 131, "人数似乎太多了呢..$R坐不下哦", "复刻副本");
                return false;
            }
            foreach (var item in pc.Party.Members.Values)
            {
                if (item.QuestRemaining < 30)
                {
                    Say(pc, 131, item.Name + " 任务点不足！", "复刻副本");
                    return false;
                }
            }
            foreach (var item in pc.Party.Members.Values)
            {
                if (item.MapID != pc.MapID)
                {
                    Say(pc, 131, item.Name + " 没有在附近", "复刻副本");
                    return false;
                }
            }

            return true;
        }
        private int GetWeekOfYear(DateTime dt)
        {
            GregorianCalendar gc = new GregorianCalendar();
            return gc.GetWeekOfYear(dt, CalendarWeekRule.FirstDay, DayOfWeek.Monday);
        }
        bool checkparty(ActorPC pc)
        {
            if (pc.Party == null)
            {
                Say(pc, 131, "这个地方太危险啦，$R请组队后一起前往吧。", "复刻副本");
                return false;
            }
            if (pc.Party.Leader != pc)
            {
                Say(pc, 131, "对不起，$R你不是队长耶$R$R请由队长来向我申请吧", "复刻副本");
                return false;
            }
            if (pc.Party.MemberCount > 8)
            {
                Say(pc, 131, "人数似乎太多了呢..$R坐不下哦", "复刻副本");
                return false;
            }
            foreach (var item in pc.Party.Members.Values)
            {
                if (item.AStr["光之塔限制"] == DateTime.Now.ToString("yyyy-MM-dd"))
                {
                    Say(pc, 131, item.Name + " 去过了", "复刻副本");
                    return false;
                }
            }

            foreach (var item in pc.Party.Members.Values)
            {
                if (item.QuestRemaining < 30)
                {
                    Say(pc, 131, item.Name + " 任务点不足！", "复刻副本");
                    return false;
                }
            }
            return true;
        }
        void 不死皇城创建困难(ActorPC pc)
        {
            if (pc.Party == null)
                return;
            if (pc != pc.Party.Leader)
                return;
            pc.Party.MaxMember = 4;

        }
        void 创建困难(ActorPC pc)
        {
            if (pc.Party == null)
                return;
            if (pc != pc.Party.Leader)
                return;
            foreach (var item in pc.Party.Members.Values)
            {
                if (item != null)
                {
                    item.AInt["光之塔困难限制测试"] = GetWeekOfYear(DateTime.Now);
                    item.CStr["光之塔队长名"] = item.Party.Leader.Name;
                }
            }
            pc.Party.MaxMember = 8;
            pc.Party.Leader.TInt["S10070000"] = CreateMapInstance(10070000, 10054000, 21, 21, true, 0, true);//光塔周边
            pc.Party.Leader.TInt["S20140000"] = CreateMapInstance(20140000, 10054000, 21, 21, true, 0, true);//主场馆1F
            pc.Party.Leader.TInt["S20141000"] = CreateMapInstance(20141000, 10054000, 21, 21, true, 0, true);//主场馆2F
            pc.Party.Leader.TInt["S20142000"] = CreateMapInstance(20142000, 10054000, 21, 21, true, 0, true);//主场馆3F
            pc.Party.Leader.TInt["S20143000"] = CreateMapInstance(20143000, 10054000, 21, 21, true, 0, true);//主场馆4F
            pc.Party.Leader.TInt["S20146000"] = CreateMapInstance(20146000, 10054000, 21, 21, true, 0, true);//光塔下层
            pc.Party.Leader.TInt["S20144000"] = CreateMapInstance(20144000, 10054000, 21, 21, true, 0, true);//A-5F
            pc.Party.Leader.TInt["S20145000"] = CreateMapInstance(20145000, 10054000, 21, 21, true, 0, true);//B-5F
            pc.Party.Leader.TInt["S20147000"] = CreateMapInstance(20147000, 10054000, 21, 21, true, 0, true);//A-6F
            pc.Party.Leader.TInt["S20148000"] = CreateMapInstance(20148000, 10054000, 21, 21, true, 0, true);//B-6F
            pc.Party.Leader.TInt["S20149000"] = CreateMapInstance(20149000, 10054000, 21, 21, true, 0, true);//A-7F
            pc.Party.Leader.TInt["S20150000"] = CreateMapInstance(20150000, 10054000, 21, 21, true, 0, true);//B-7F
            pc.Party.Leader.TInt["S20151000"] = CreateMapInstance(20151000, 10054000, 21, 21, true, 0, true);//A-8F
            pc.Party.Leader.TInt["S20152000"] = CreateMapInstance(20152000, 10054000, 21, 21, true, 0, true);//B-8F
            pc.Party.Leader.TInt["S20153000"] = CreateMapInstance(20153000, 10054000, 21, 21, true, 0, true);//A-9F
            pc.Party.Leader.TInt["S20154000"] = CreateMapInstance(20154000, 10054000, 21, 21, true, 0, true);//A-10F
            pc.Party.Leader.TInt["S20155000"] = CreateMapInstance(20155000, 10054000, 21, 21, true, 0, true);//A-11F
            pc.Party.Leader.TInt["S20156000"] = CreateMapInstance(20156000, 10054000, 21, 21, true, 0, true);//A-12F
            pc.Party.Leader.TInt["S20157000"] = CreateMapInstance(20157000, 10054000, 21, 21, true, 0, true);//A-13F
            pc.Party.Leader.TInt["S20154001"] = CreateMapInstance(20154001, 10054000, 21, 21, true, 0, true);//A-14F
            pc.Party.Leader.TInt["S20155001"] = CreateMapInstance(20155001, 10054000, 21, 21, true, 0, true);//A-15F
            pc.Party.Leader.TInt["S20158000"] = CreateMapInstance(20158000, 10054000, 21, 21, true, 0, true);//A-16F
            pc.Party.Leader.TInt["S20159000"] = CreateMapInstance(20159000, 10054000, 21, 21, true, 0, true);//A-17F
            pc.Party.Leader.TInt["S20160000"] = CreateMapInstance(20160000, 10054000, 21, 21, true, 0, true);//A-18F
            pc.Party.Leader.TInt["S20155002"] = CreateMapInstance(20155002, 10054000, 21, 21, true, 0, true);//A-19F
            pc.Party.Leader.TInt["S20154002"] = CreateMapInstance(20154002, 10054000, 21, 21, true, 0, true);//A-20F
            pc.Party.Leader.TInt["S20161000"] = CreateMapInstance(20161000, 10054000, 21, 21, true, 0, true);//A-21F
            pc.Party.Leader.TInt["S20162000"] = CreateMapInstance(20162000, 10054000, 21, 21, true, 0, true);//A-22F
            pc.Party.Leader.TInt["S20163000"] = CreateMapInstance(20163000, 10054000, 21, 21, true, 0, true);//光塔上层


            主场馆2F刷怪((uint)pc.Party.Leader.TInt["S20141000"], pc);
            主场馆3F刷怪((uint)pc.Party.Leader.TInt["S20142000"], pc);
            主场馆4F刷怪((uint)pc.Party.Leader.TInt["S20143000"], pc);
            B5F刷怪((uint)pc.Party.Leader.TInt["S20145000"], pc);
            B6F刷怪((uint)pc.Party.Leader.TInt["S20148000"], pc);
            B7F刷怪((uint)pc.Party.Leader.TInt["S20150000"], pc);
            B8F刷怪((uint)pc.Party.Leader.TInt["S20152000"], pc);
            A5F刷怪((uint)pc.Party.Leader.TInt["S20144000"], pc);
            A6F刷怪((uint)pc.Party.Leader.TInt["S20147000"], pc);
            A7F刷怪((uint)pc.Party.Leader.TInt["S20149000"], pc);
            A8F刷怪((uint)pc.Party.Leader.TInt["S20151000"], pc);
            A9F刷怪((uint)pc.Party.Leader.TInt["S20153000"], pc);
            A10F刷怪((uint)pc.Party.Leader.TInt["S20154000"], pc);
            A11F刷怪((uint)pc.Party.Leader.TInt["S20155000"], pc);
            A12F刷怪((uint)pc.Party.Leader.TInt["S20156000"], pc);
            A13F刷怪((uint)pc.Party.Leader.TInt["S20157000"], pc);
            A14F刷怪((uint)pc.Party.Leader.TInt["S20154001"], pc);
            A15F刷怪((uint)pc.Party.Leader.TInt["S20155001"], pc);
            A16F刷怪((uint)pc.Party.Leader.TInt["S20158000"], pc);
            A17F刷怪((uint)pc.Party.Leader.TInt["S20159000"], pc);
            A18F刷怪((uint)pc.Party.Leader.TInt["S20160000"], pc);
            A19F刷怪((uint)pc.Party.Leader.TInt["S20155002"], pc);
            A20F刷怪((uint)pc.Party.Leader.TInt["S20154002"], pc);
            A21F刷怪((uint)pc.Party.Leader.TInt["S20161000"], pc);
            A22F刷怪((uint)pc.Party.Leader.TInt["S20162000"], pc);

            foreach (var item in pc.Party.Members.Values)
            {
                if (item != null)
                {
                    item.TInt["副本复活标记"] = 1;
                    item.Party.Leader.TInt["复活次数"] = 0;
                    item.Party.Leader.TInt["设定复活次数"] = 0;
                    Warp(item, (uint)pc.Party.Leader.TInt["S10070000"], 15, 101);
                    if (item.QuestRemaining > 30)
                        item.QuestRemaining -= 30;
                }
            }
            光之塔老一刷怪困难((uint)pc.Party.Leader.TInt["S20140000"], pc);
            //光塔隐藏BOSS刷怪((uint)pc.Party.Leader.TInt["S20158000"], pc);
        }
    }
}

