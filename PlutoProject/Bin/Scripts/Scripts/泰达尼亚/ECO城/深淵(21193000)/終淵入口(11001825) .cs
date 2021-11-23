using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
using SagaScript.Chinese.Enums;
using SagaMap;
using System.Linq;
namespace SagaScript.M21193000
{
    public class S11001825 : Event
    {
        public S11001825()
        {
            this.EventID = 11001825;
        }

        public override void OnEvent(ActorPC pc)
        {
            WarpToDungeon(pc);
        }

        void dieEvent_01(SagaMap.ActorEventHandlers.MobEventHandler eh, ActorPC lastHit)
        {
            SetMissionCompleted(eh, lastHit);
            ShowPortal(lastHit, 60, 60, 5000000);
            //这里lastHit用的是对怪物最后一击的人，不一定是地图创建者，所以如果是队伍，注意对正确的人取得TInt["副本测试"]
            Announce((uint)lastHit.TInt["LV99_ARMS_01"], "这个地图将在30秒后崩溃");
            //创建计时器，AddTimer(计时器名字,每次发动间隔，初次启动延迟，绑定玩家，是否需要调用脚本)
            Timer timer_001 = AddTimer("崩溃_01", 10000, 10000, lastHit, true);
            timer_001.OnTimerCall += timerCall_01;//添加计时器处理器
            timer_001.Activate();
        }

        void dieEvent_02(SagaMap.ActorEventHandlers.MobEventHandler eh, ActorPC lastHit)
        {
            SetMissionCompleted(eh, lastHit);
            ShowPortal(lastHit, 60, 60, 5000000);
            //这里lastHit用的是对怪物最后一击的人，不一定是地图创建者，所以如果是队伍，注意对正确的人取得TInt["副本测试"]
            Announce((uint)lastHit.TInt["LV99_ARMS_02"], "这个地图将在30秒后崩溃");
            //创建计时器，AddTimer(计时器名字,每次发动间隔，初次启动延迟，绑定玩家，是否需要调用脚本)
            Timer timer_002 = AddTimer("崩溃_02", 10000, 10000, lastHit, true);
            timer_002.OnTimerCall += timerCall_02;//添加计时器处理器
            timer_002.Activate();
        }

        /// <summary>
        /// 计时器处理器
        /// </summary>
        /// <param name="timer"></param>
        /// <param name="invoker"></param>
        void timerCall_01(Timer timer, ActorPC invoker)
        {
            timer.Deactivate();
            Announce((uint)invoker.TInt["LV99_ARMS_01"], "这个地图将在20秒后崩溃");
            Wait(invoker, 10000);
            for (int i = 10; i > 0; i--)
            {
                Wait(invoker, 1000);
                Announce((uint)invoker.TInt["LV99_ARMS_01"], "这个地图将在" + i + "秒后崩溃");
            }
            DeleteMapInstance(invoker.TInt["LV99_ARMS_01"]);
            invoker.TInt.Remove("LV99_ARMS_01");
            /*
            Announce((uint)invoker.TInt["LV99_ARMS_01"], "这个地图将崩溃");
            DeleteMapInstance(invoker.TInt["LV99_ARMS_01"]);
            invoker.TInt.Remove("LV99_ARMS_01");//*/
        }

        void timerCall_02(Timer timer, ActorPC invoker)
        {
            timer.Deactivate();
            Announce((uint)invoker.TInt["LV99_ARMS_02"], "这个地图将在20秒后崩溃");
            Wait(invoker, 10000);
            for (int i = 10; i > 0; i--)
            {
                Wait(invoker, 1000);
                Announce((uint)invoker.TInt["LV99_ARMS_02"], "这个地图将在" + i + "秒后崩溃");
            }
            DeleteMapInstance(invoker.TInt["LV99_ARMS_02"]);
            invoker.TInt.Remove("LV99_ARMS_02");
            /*
            Announce((uint)invoker.TInt["LV99_ARMS_02"], "这个地图将崩溃");
            DeleteMapInstance(invoker.TInt["LV99_ARMS_02"]);
            invoker.TInt.Remove("LV99_ARMS_02");//*/
        }

        void timer_01_01(Timer timer, ActorPC invoker)
        {
            timer.Deactivate();
            Announce((uint)invoker.TInt["LV99_ARMS_01"], "这个地图将在30秒后崩溃");
            Wait(invoker, 10000);
            Announce((uint)invoker.TInt["LV99_ARMS_01"], "这个地图将在20秒后崩溃");
            Wait(invoker, 10000);
            for (int i = 10; i > 0; i--)
            {
                Wait(invoker, 1000);
                Announce((uint)invoker.TInt["LV99_ARMS_01"], "这个地图将在" + i + "秒后崩溃");
            }
            DeleteMapInstance(invoker.TInt["LV99_ARMS_01"]);
            invoker.TInt.Remove("LV99_ARMS_01");
        }

        void timer_02_01(Timer timer, ActorPC invoker)
        {
            timer.Deactivate();
            Announce((uint)invoker.TInt["LV99_ARMS_02"], "这个地图将在30秒后崩溃");
            Wait(invoker, 10000);
            Announce((uint)invoker.TInt["LV99_ARMS_02"], "这个地图将在20秒后崩溃");
            Wait(invoker, 10000);
            for (int i = 10; i > 0; i--)
            {
                Wait(invoker, 1000);
                Announce((uint)invoker.TInt["LV99_ARMS_02"], "这个地图将在" + i + "秒后崩溃");
            }
            DeleteMapInstance(invoker.TInt["LV99_ARMS_02"]);
            invoker.TInt.Remove("LV99_ARMS_02");
        }
        /// <summary>
        /// 传送玩家到副本
        /// </summary>
        /// <param name="pc">玩家</param>
        protected void WarpToDungeon(ActorPC pc)
        {
            //Dungeon.Dungeon dungeon = null;
            List<string[]> dungeons = GetPossibleDungeons(pc);
            if (dungeons.Count > 0)
            {
                if (dungeons.Count == 1)
                {
                    Warp(pc, uint.Parse(dungeons[0][1]), 87, 32);
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
                            Warp(pc, uint.Parse(dungeons[0][1]), 87, 32);
                            return;
                        case 2:
                            Warp(pc, uint.Parse(dungeons[1][1]), 87, 32);
                            return;
                        case 3:
                            if (dungeons.Count <= 2)
                                return;
                            Warp(pc, uint.Parse(dungeons[2][1]), 87, 32);
                            return;
                        case 4:
                            if (dungeons.Count <= 3)
                                return;
                            Warp(pc, uint.Parse(dungeons[3][1]), 87, 32);
                            return;
                        case 5:
                            if (dungeons.Count <= 4)
                                return;
                            Warp(pc, uint.Parse(dungeons[4][1]), 87, 32);
                            return;
                        case 6:
                            if (dungeons.Count <= 5)
                                return;
                            Warp(pc, uint.Parse(dungeons[5][1]), 87, 32);
                            return;
                        case 7:
                            if (dungeons.Count <= 6)
                                return;
                            Warp(pc, uint.Parse(dungeons[6][1]), 87, 32);
                            return;
                        case 8:
                            if (dungeons.Count <= 7)
                                return;
                            Warp(pc, uint.Parse(dungeons[7][1]), 87, 32);
                            return;
                    }
                }
            }

            else if (pc.Quest != null)
            {
                if (pc.Quest.ID == 25200003 && pc.Quest.Status == SagaDB.Quests.QuestStatus.OPEN)
                {
                    if (pc.TInt["LV99_ARMS_01"] == null || pc.TInt["LV99_ARMS_01"] == 0)
                    {
                        pc.TInt["LV99_ARMS_01"] = CreateMapInstance(60908000, 21193000, 126, 28);
                        SpawnMob((uint)pc.TInt["LV99_ARMS_01"], 60, 60, 14520000, 1, new MobCallback(dieEvent_01));
                        //SpawnMob((uint)pc.TInt["LV99_ARMS_01"], 60, 60, 14520000, 1);

                        //自动崩溃
                        Timer timer_01 = AddTimer("崩溃计时_01", 10000, 7170000, pc, true);
                        timer_01.OnTimerCall += timer_01_01;
                        timer_01.Activate();
                    }
                    Warp(pc, (uint)pc.TInt["LV99_ARMS_01"], 87, 32);
                }
                else if (pc.Quest.ID == 25200004 && pc.Quest.Status == SagaDB.Quests.QuestStatus.OPEN)
                {
                    if (pc.TInt["LV99_ARMS_02"] == null || pc.TInt["LV99_ARMS_02"] == 0)
                    {
                        pc.TInt["LV99_ARMS_02"] = CreateMapInstance(60908000, 21193000, 126, 28);
                        SpawnMob((uint)pc.TInt["LV99_ARMS_02"], 60, 60, 14520001, 1, new MobCallback(dieEvent_02));
                        //自动崩溃
                        Timer timer_02 = AddTimer("崩溃计时_02", 10000, 7170000, pc, true);
                        timer_02.OnTimerCall += timer_02_01;
                        timer_02.Activate();
                    }
                    Warp(pc, (uint)pc.TInt["LV99_ARMS_02"], 87, 32);
                }
                else//*/
                    Say(pc, 131, "任务错误!");
            }
            else
                Say(pc, 131, "没有任务!");
        }

        /// <summary>
        /// 取得可能的副本
        /// </summary>
        /// <param name="pc"></param>
        /// <returns></returns>
        protected List<string[]> GetPossibleDungeons(ActorPC pc)
        {
            List<string[]> list = new List<string[]>();
            if (pc.TInt["LV99_ARMS_01"] != 0)
            {
                string[] tem = { pc.Name, pc.TInt["LV99_ARMS_01"].ToString() };
                list.Add(tem);
            }
            if (pc.TInt["LV99_ARMS_02"] != 0)
            {
                string[] tem = { pc.Name, pc.TInt["LV99_ARMS_02"].ToString() };
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
                    if (i.TInt["LV99_ARMS_01"] != 0)
                    {
                        string[] tem = { i.Name, i.TInt["LV99_ARMS_01"].ToString() };
                        list.Add(tem);
                    }
                    if (i.TInt["LV99_ARMS_02"] != 0)
                    {
                        string[] tem = { i.Name, i.TInt["LV99_ARMS_02"].ToString() };
                        list.Add(tem);
                    }
                }
            }
            return list;
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
    }

    public class SEXIT : Event
    {
        public SEXIT()
        {
            this.EventID = 5000000;
        }

        public override void OnEvent(ActorPC pc)
        {
            Warp(pc, 21193000, 126, 28);
        }
    }
}