using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
using SagaScript.Chinese.Enums;
using SagaMap;
using System.Linq;
namespace SagaScript.M10062000
{
    public class S11002274 : Event
    {
        public S11002274()
        {
            this.EventID = 11002274;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<Arms_110> Arms_quest = new BitMask<Arms_110>(pc.CMask["Arms_110"]);

            if (pc.Quest != null)
            {
                if (pc.Quest.ID == 50000110)
                {
                    Say(pc, 131, "那么就来吧", "母体");
                    WarpToDungeon(pc);
                    return;
                }
            }
            Arms_quest.SetValue(Arms_110.与研究员第一步纠缠中, true);




            if (Arms_quest.Test(Arms_110.与DEM龙第一次对过话))
            {
                Say(pc, 131, "如何？$R;" +
                    "能下决心了吗", "母体");
                switch (Select(pc, "怎么办？", "", "挑战DEM龙", "算了"))
                {
                    case 1:
                        HandleQuest(pc, 110);
                        break;
                }
                return;
            }
            if (Arms_quest.Test(Arms_110.与研究员第一步纠缠中) && !Arms_quest.Test(Arms_110.与DEM龙第一次对过话))
            {
                Say(pc, 131, "什么人？$R;" +
                    ".....$R;" +
                    "莉莉的伙伴们吗？$R;" +
                    "有什么事吗", "母体");
                switch (Select(pc, "什么事呢？", "", "需要龙玉", "没事"))
                {
                    case 1:
                        Say(pc, 131, "龙玉？$R;" +
                                    ".....$R;" +
                                    "原来是这么回事$R;", "母体");
                        break;
                }
                Say(pc, 131, "不过$R;" +
                            "龙玉是很珍贵的,不能随便给你$R;" +
                            "给我展现你的力量$R;" +
                            "否则就夹着尾巴逃走吧", "母体");
                Arms_quest.SetValue(Arms_110.与DEM龙第一次对过话, true);
                return;
            }
        }
        void timer_01_01(Timer timer, ActorPC invoker)
        {
            timer.Deactivate();
            Announce((uint)invoker.TInt["LV110_ARMS"], "这个地图将在30秒后崩溃");
            Wait(invoker, 10000);
            Announce((uint)invoker.TInt["LV110_ARMS"], "这个地图将在20秒后崩溃");
            Wait(invoker, 10000);
            for (int i = 10; i > 0; i--)
            {
                Wait(invoker, 1000);
                Announce((uint)invoker.TInt["LV110_ARMS"], "这个地图将在" + i + "秒后崩溃");
            }
            DeleteMapInstance(invoker.TInt["LV110_ARMS"]);
            invoker.TInt.Remove("LV110_ARMS");
        }
        protected void WarpToDungeon(ActorPC pc)
        {
            //Dungeon.Dungeon dungeon = null;
            List<string[]> dungeons = GetPossibleDungeons(pc);
            if (dungeons.Count > 0)
            {
                if (dungeons.Count == 1)
                {
                    Warp(pc, uint.Parse(dungeons[0][1]), 14, 31);
                }
                else
                {
                    List<string> temp = new List<string>();
                    for (int i = 0; i < dungeons.Count; i++)
                    {
                        temp.Add(dungeons[i][0]);
                    }
                    temp.Add("不去");
                    switch (Select(pc, "要进入哪个副本?", "", temp.ToArray()))
                    {
                        case 1:
                            Warp(pc, uint.Parse(dungeons[0][1]), 14, 31);
                            return;
                        case 2:
                            Warp(pc, uint.Parse(dungeons[1][1]), 14, 31);
                            return;
                        case 3:
                            if (dungeons.Count <= 2)
                                return;
                            Warp(pc, uint.Parse(dungeons[2][1]), 14, 31);
                            return;
                        case 4:
                            if (dungeons.Count <= 3)
                                return;
                            Warp(pc, uint.Parse(dungeons[3][1]), 14, 31);
                            return;
                        case 5:
                            if (dungeons.Count <= 4)
                                return;
                            Warp(pc, uint.Parse(dungeons[4][1]), 14, 31);
                            return;
                        case 6:
                            if (dungeons.Count <= 5)
                                return;
                            Warp(pc, uint.Parse(dungeons[5][1]), 14, 31);
                            return;
                        case 7:
                            if (dungeons.Count <= 6)
                                return;
                            Warp(pc, uint.Parse(dungeons[6][1]), 14, 31);
                            return;
                        case 8:
                            if (dungeons.Count <= 7)
                                return;
                            Warp(pc, uint.Parse(dungeons[7][1]), 14, 31);
                            return;
                    }
                }
            }

            else if (pc.Quest != null)
            {
                if (pc.Quest.ID == 50000110 && pc.Quest.Status == SagaDB.Quests.QuestStatus.OPEN)
                {
                    if (pc.TInt["LV110_ARMS"] == null || pc.TInt["LV110_ARMS"] == 0)
                    {
                        pc.TInt["LV110_ARMS"] = CreateMapInstance(60912000, 22202000, 154, 31);
                        SpawnMob((uint)pc.TInt["LV110_ARMS"], 148, 31, 14720002, 1, new MobCallback(dieEvent));
                        //SpawnMob((uint)pc.TInt["LV99_ARMS_01"], 60, 60, 14520000, 1);

                        //自动崩溃
                        Timer timer_01 = AddTimer("崩溃计时_01", 10000, 7170000, pc, true);
                        timer_01.OnTimerCall += timer_01_01;
                        timer_01.Activate();
                    }
                    Warp(pc, (uint)pc.TInt["LV110_ARMS"], 87, 32);
                }
                else//*/
                    Say(pc, 131, "任务错误!");
            }
            else
                Say(pc, 131, "没有任务!");
        }
        void dieEvent(SagaMap.ActorEventHandlers.MobEventHandler eh, ActorPC lastHit)
        {
            SetMissionCompleted(eh, lastHit);
            ShowPortal(lastHit, 148, 31, 5000000);
            //这里lastHit用的是对怪物最后一击的人，不一定是地图创建者，所以如果是队伍，注意对正确的人取得TInt["副本测试"]
            Announce((uint)lastHit.TInt["LV110_ARMS"], "这个地图将在30秒后崩溃");
            //创建计时器，AddTimer(计时器名字,每次发动间隔，初次启动延迟，绑定玩家，是否需要调用脚本)
            Timer timer_001 = AddTimer("崩溃计时_01", 10000, 10000, lastHit, true);
            timer_001.OnTimerCall += timerCall;//添加计时器处理器
            timer_001.Activate();
        }

        void timerCall(Timer timer, ActorPC invoker)
        {
            timer.Deactivate();
            Announce((uint)invoker.TInt["LV110_ARMS"], "这个地图将在20秒后崩溃");
            Wait(invoker, 10000);
            for (int i = 10; i > 0; i--)
            {
                Wait(invoker, 1000);
                Announce((uint)invoker.TInt["LV110_ARMS"], "这个地图将在" + i + "秒后崩溃");
            }
            DeleteMapInstance(invoker.TInt["LV110_ARMS"]);
            invoker.TInt.Remove("LV110_ARMS");
            /*
            Announce((uint)invoker.TInt["LV99_ARMS_01"], "这个地图将崩溃");
            DeleteMapInstance(invoker.TInt["LV99_ARMS_01"]);
            invoker.TInt.Remove("LV99_ARMS_01");//*/
        }





        private void SetMissionCompleted(SagaMap.ActorEventHandlers.MobEventHandler eh, ActorPC lastHit)
        {
            Map map = SagaMap.Manager.MapManager.Instance.GetMap(eh.mob.MapID);
            List<Actor> list = map.GetActorsArea(eh.mob, 12700, false);
            list = list.Where(x => x.type == ActorType.PC && (x as ActorPC).Online).ToList();
            foreach (var ac in list)
            {
                SagaMap.ActorEventHandlers.PCEventHandler ieh = (SagaMap.ActorEventHandlers.PCEventHandler)(ac as ActorPC).e;
                ieh.Client.SendAnnounce("正在分配击杀...");
                int XDiff = 0, YDiff = 0;
                SagaMap.Skill.SkillHandler.Instance.GetXYDiff(map, eh.mob, ac, out XDiff, out YDiff);
                int range = (int)Math.Sqrt((double)(Math.Pow(XDiff, 2) + Math.Pow(YDiff, 2)));
                ieh.Client.SendAnnounce("你与BOSS的距离: " + range);
                ieh.Client.SendAnnounce("正在设置击杀.....");
                ieh.Client.EventMobKilled(eh.mob);
                ieh.Client.QuestMobKilled(eh.mob, false);
                ieh.Client.SendAnnounce("击杀设置已完成");
            }
        }

        protected List<string[]> GetPossibleDungeons(ActorPC pc)
        {
            List<string[]> list = new List<string[]>();
            if (pc.TInt["LV110_ARMS"] != 0)
            {
                string[] tem = { pc.Name, pc.TInt["LV110_ARMS"].ToString() };
                list.Add(tem);
            }
            if (pc.Party != null)
            {
                foreach (ActorPC i in pc.Party.Members.Values)
                {
                    if (!i.Online)
                        continue;
                    if (i == pc)
                        continue;
                    if (i.TInt["LV110_ARMS"] != 0)
                    {
                        string[] tem = { i.Name, i.TInt["LV110_ARMS"].ToString() };
                        list.Add(tem);
                    }
                }
            }
            return list;
        }

        public class SEXIT : Event
        {
            public SEXIT()
            {
                this.EventID = 9000009;
            }

            public override void OnEvent(ActorPC pc)
            {
                Warp(pc, 22202000, 148, 31);
            }
        }
    }


}