using System;
using System.Collections.Generic;
using System.Text;
using SagaDB.Item;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
using SagaLib;
//using MySql.Data.MySqlClient;
using System.Data;
using SagaScript.Chinese;
//所在地圖:上城東邊吊橋(10023100) NPC基本信息:初心者嚮導(11000532) X:247 Y:124
namespace SagaScript.M10023100
{
    /*
    public class S11000532 : Event
    {
        public S11000532()
        {
            this.EventID = 11000532;
        }

        public override void OnEvent(ActorPC pc)
        {

            //ForumMysql sql = new ForumMysql("127.0.0.1", 3306, "ancof", "root", "aaa123456bbb");
            //string[] forumID = sql.GetForumID(pc.Account.Name);
            //string adf = string.Format("你的论坛UID是：{1}$R你的论坛用户名是：{2}$R现在是声望值: {0}", pc.Fame, forumID[0].ToString(), forumID[1].ToString());
            //Say(pc, 0, adf);

            switch (Select(pc, "选择", "", "任务列表", "进入副本"))
            {
                case 1:
                    HandleQuest(pc, 300);
                    break;
                case 2:
                    //Say(pc, 131, "开始进入");
                    WarpToDungeon(pc);
                    break;
            }
        }

        /// <summary>
        /// 怪物死亡处理器
        /// </summary>
        /// <param name="eh">怪物的事件处理器</param>
        /// <param name="lastHit">最后一击的玩家</param>
        void dieEvent(SagaMap.ActorEventHandlers.MobEventHandler eh, ActorPC lastHit)
        {
            ShowPortal(lastHit, 33, 33, 12345678);
            //这里lastHit用的是对怪物最后一击的人，不一定是地图创建者，所以如果是队伍，注意对正确的人取得TInt["副本测试"]
            Announce((uint)lastHit.TInt["副本测试"], "这个地图将崩溃");
            //创建计时器，AddTimer(计时器名字,每次发动间隔，初次启动延迟，绑定玩家，是否需要调用脚本)
            Timer timer = AddTimer("演示计时器", 10000, 10000, lastHit, true);
            timer.OnTimerCall += timerCall;//添加计时器处理器
            timer.Activate();
        }

        /// <summary>
        /// 计时器处理器
        /// </summary>
        /// <param name="timer"></param>
        /// <param name="invoker"></param>
        void timerCall(Timer timer, ActorPC invoker)
        {
            timer.Deactivate();
            Announce((uint)invoker.TInt["副本测试"], "这个地图将崩溃");
            DeleteMapInstance(invoker.TInt["副本测试"]);
            invoker.TInt.Remove("副本测试");
            invoker.TInt["副本测试"] = 0;
        }

        void dieEvent_01(SagaMap.ActorEventHandlers.MobEventHandler eh, ActorPC lastHit)
        {
            ShowPortal(lastHit, 60, 60, 12345678);
            //这里lastHit用的是对怪物最后一击的人，不一定是地图创建者，所以如果是队伍，注意对正确的人取得TInt["副本测试"]
            Announce((uint)lastHit.TInt["LV99_ARMS_01"], "这个地图将在30秒后崩溃");
            //创建计时器，AddTimer(计时器名字,每次发动间隔，初次启动延迟，绑定玩家，是否需要调用脚本)
            Timer timer_001 = AddTimer("演示_01", 10000, 10000, lastHit, true);
            timer_001.OnTimerCall += timerCall_01;//添加计时器处理器
            timer_001.Activate();
            //
        }

        void dieEvent_02(SagaMap.ActorEventHandlers.MobEventHandler eh, ActorPC lastHit)
        {
            ShowPortal(lastHit, 60, 60, 12345678);
            //这里lastHit用的是对怪物最后一击的人，不一定是地图创建者，所以如果是队伍，注意对正确的人取得TInt["副本测试"]
            Announce((uint)lastHit.TInt["LV99_ARMS_02"], "这个地图将在30秒后崩溃");
            //创建计时器，AddTimer(计时器名字,每次发动间隔，初次启动延迟，绑定玩家，是否需要调用脚本)
            Timer timer_002 = AddTimer("演示_02", 10000, 10000, lastHit, true);
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
        protected new void WarpToDungeon(ActorPC pc)
        {
            Say(pc, 131, "新建队伍列表");
            List<string[]> dungeons = GetPossibleDungeons(pc);

            Say(pc, 131, "list 计数");
            if (dungeons.Count > 0)
            {
                Say(pc, 131, "计数等于1否?");
                if (dungeons.Count == 1)
                {
                    Say(pc, 131, "进入01");
                    Warp(pc, uint.Parse(dungeons[0][1]), 87, 32);
                }
                else
                {
                    Say(pc, 131, "计数大于1");
                    List<string> temp = new List<string>();
                    Say(pc, 131, "生成列表");
                    for (int i = 0; i < dungeons.Count; i++)
                    {
                        Say(pc, 131, "生成列表"+dungeons[i][0]);
                        temp.Add(string.Format("{0}", dungeons[i][0]));
                    }
                    temp.Add("不去");
                    Say(pc, 131, "选择列表");
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
                return;
            }

            Say(pc, 131, "list 计数 = 0");
            if (pc.Quest != null)
            {
                Say(pc, 131, "任务判断");
                if (pc.Quest.ID == 1654654 && pc.Quest.Status == SagaDB.Quests.QuestStatus.OPEN)
                {

                    Say(pc, 131, "任务判断 = 1");
                    if (pc.TInt["LV99_ARMS_01"] == 0)
                    {
                        Say(pc, 131, "任务地图生成01");
                        pc.TInt["LV99_ARMS_01"] = CreateMapInstance(60908000, 10023100, 250, 132);
                        SpawnMob((uint)pc.TInt["LV99_ARMS_01"], 60, 60, 14520000, 1, new MobCallback(dieEvent_01));
                        //自动崩溃
                        Say(pc, 131, "任务地图计时01");
                        Timer timer_01 = AddTimer("崩溃计时_01", 10000, 7170000, pc, true);
                        timer_01.OnTimerCall += timer_01_01;
                        timer_01.Activate();
                        Say(pc, 131, "任务地图进入1");
                        Warp(pc, (uint)pc.TInt["LV99_ARMS_01"], 87, 32);
                    }
                }
                else
                    if (pc.Quest.ID == 1654655 && pc.Quest.Status == SagaDB.Quests.QuestStatus.OPEN)
                    {
                        Say(pc, 131, "任务判断 = 2");
                        if (pc.TInt["LV99_ARMS_02"] == 0)
                        {
                            Say(pc, 131, "任务地图生成02");
                            pc.TInt["LV99_ARMS_02"] = CreateMapInstance(60908000, 10023100, 250, 132);
                            SpawnMob((uint)pc.TInt["LV99_ARMS_02"], 60, 60, 14520001, 1, new MobCallback(dieEvent_02));
                            //自动崩溃
                            Say(pc, 131, "任务地图计时02");
                            Timer timer_02 = AddTimer("崩溃计时_02", 10000, 7170000, pc, true);
                            timer_02.OnTimerCall += timer_02_01;
                            timer_02.Activate();
                        }
                        Say(pc, 131, "任务地图进入2");
                        Warp(pc, (uint)pc.TInt["LV99_ARMS_02"], 87, 32);
                    }
                    else//
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
        protected new List<string[]> GetPossibleDungeons(ActorPC pc)
        {
            Say(pc, 131, "开始读取");
            List<string[]> list = new List<string[]>();
            Say(pc, 131, "检查自己01");
            if (pc.TInt["LV99_ARMS_01"] != 0)
            {
                Say(pc, 131, "自己有");
                string[] tem = { pc.Name, pc.TInt["LV99_ARMS_01"].ToString() };
                list.Add(tem);
            }
            Say(pc, 131, "检查自己02");
            if (pc.TInt["LV99_ARMS_02"] != 0)
            {
                Say(pc, 131, "自己有02");
                string[] tem = { pc.Name, pc.TInt["LV99_ARMS_02"].ToString() };
                list.Add(tem);
            }
            if (pc.Party != null)
            {
                Say(pc, 131, "队伍开始");
                foreach (ActorPC i in pc.Party.Members.Values)
                {
                    Say(pc, 131, string.Format("队伍{0}检查开始",i.Name));
                    if (i == pc)
                    {
                        Say(pc, 131, "队伍到自己");
                        continue;
                    }
                    Say(pc, 131, string.Format("队伍{0}是否在线", i.Name));
                    if (!i.Online)
                    {
                        Say(pc, 131, string.Format("队伍{0}不在在线", i.Name));
                        continue;
                    }
                    Say(pc, 131, string.Format("队伍{0}副本检查是否存在01", i.Name));
                    if (i.TInt["LV99_ARMS_01"] != 0)
                    {
                        Say(pc, 131, string.Format("队伍{0}检查存在01", i.Name));
                        string[] tem = { i.Name, i.TInt["LV99_ARMS_01"].ToString() };
                        list.Add(tem);
                    }
                    Say(pc, 131, string.Format("队伍{0}副本检查是否存在02", i.Name));
                    if (i.TInt["LV99_ARMS_02"] != 0)
                    {
                        Say(pc, 131, string.Format("队伍{0}检查存在02", i.Name));
                        string[] tem = { i.Name, i.TInt["LV99_ARMS_02"].ToString() };
                        list.Add(tem);
                    }
                    Say(pc, 131, string.Format("队伍{0}检查结束", i.Name));
                }
                Say(pc, 131, "队伍检查完成");
            }
            Say(pc, 131, "返回列表");
            return list;
        }
    }

    public class P12345678 : Event
    {
        public P12345678()
        {
            this.EventID = 12345678;
        }

        public override void OnEvent(ActorPC pc)
        {
            Warp(pc, 10023100, 250, 132);

        }
    }
    */
}
