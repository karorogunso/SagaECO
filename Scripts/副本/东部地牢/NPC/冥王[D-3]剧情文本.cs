
using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaDB.Item;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
using WeeklyExploration;
using System.Globalization;
using SagaMap.ActorEventHandlers;
namespace 东部地牢副本
{
    public partial class S100000105 : Event
    {
        public S100000105()
        {
            this.EventID = 100000105;
        }
        public override void OnEvent(ActorPC pc)
        {
            if (pc.TInt["东牢D-3"] == 1)
            {
                ChangeMessageBox(pc);
                Say(pc, 0, "嗯？都准备好了吗？", "沙月");
                Say(pc, 0, "那么我们开始吧，$R幻象的缝隙不能维持太久，你们一定要尽快解决。", "沙月");
                NPCMove(pc, 80001701, 161, 35, 500, 2, 0xb, 366, 10, 0);
                Wait(pc, 1500);
                NPCMove(pc, 80001701, 161, 35, 500, 1, 0xb, 366, 10, 0);
                ShowEffect(pc, 161, 35, 1022);
                Say(pc, 999, "（低声念咒）", "沙月");
                Wait(pc, 2000);
                ShowEffect(pc, 161, 33, 4344);//4344定位
                ShowEffect(pc, 161, 37, 4344);
                ShowEffect(pc, 163, 35, 4344);
                ShowEffect(pc, 159, 35, 4344);
                Wait(pc, 1800);
                ShowEffect(pc, 161, 33, 4406);//4406风刃
                Wait(pc, 300);
                ShowEffect(pc, 161, 37, 4406);
                Wait(pc, 300);
                ShowEffect(pc, 163, 35, 4406);
                Wait(pc, 300);
                ShowEffect(pc, 159, 35, 4406);
                Wait(pc, 1000);
                ShowEffect(pc, 161, 33, 4406);
                Wait(pc, 300);
                ShowEffect(pc, 161, 37, 4406);
                Wait(pc, 300);
                ShowEffect(pc, 163, 35, 4406);
                Wait(pc, 300);
                ShowEffect(pc, 159, 35, 4406);
                Wait(pc, 500);
                ShowEffect(pc, 161, 37, 5447);//5447破碎
                Wait(pc, 1000);
                Say(pc, 0, "……？", "沙月");
                ShowEffect(pc, 159, 35, 5447);
                ShowEffect(pc, 161, 33, 5447);
                ShowEffect(pc, 163, 35, 5447);
                Wait(pc, 1000);
                Say(pc, 0, "结界已经裂开一条缝了，我只能维持这个状态$CR5分钟$CD，$R若是超出时间，我不敢保证会发生什么事。", "沙月");
                Wait(pc, 1000);
                Say(pc, 81000002, 370, "嗷呜！！！", "冥王");
                ShowPictCancel(pc, 81000002);
                NPCMove(pc, 80001701, 170, 30, 500, 5, 0xb, 366, 10, 0);
                Say(pc, 65535, "哇啊！", "沙月");
                Wait(pc, 1800);
                NPCMove(pc, 80001701, 170, 30, 500, 1, 0xb, 366, 10, 0);
                Say(pc, 65535, "呼……呼，$R差点被打乱状态，我在这边专心维持裂缝，加油啊。", "沙月");
               
                if (pc.Party != null)
                {
                    if (pc.Party.Leader == pc)
                    {
                        Select(pc, " ", "", "迎战吧");
                        if (pc.TInt["东牢D-3"] < 2)
                        {
							foreach (var item in pc.Party.Members.Values)
								if (item.Online)
									item.TInt["东牢D-3"] = 2;
                            东部地牢.Difficulty diff = 东部地牢.Difficulty.Hell;
                            if (pc.CInt["东部地牢难度"] == 1)
                                diff = 东部地牢.Difficulty.Easy;
                            if (pc.CInt["东部地牢难度"] == 2)
                                diff = 东部地牢.Difficulty.Normal;
                            if (pc.CInt["东部地牢难度"] == 3)
                                diff = 东部地牢.Difficulty.Hard;
                            if (pc.CInt["东部地牢难度"] == 4)
                                diff = 东部地牢.Difficulty.Hell;
                            SagaMap.Map map = SagaMap.Manager.MapManager.Instance.GetMap((uint)pc.Party.TInt["S20092000"]);
                            ActorMob mobS2 = map.SpawnCustomMob(10000000, map.ID, 10136900, 0, 10010100, 1, 147, 44, 0, 1, 0, 冥王Info(diff), 冥王AI(diff), null, 0)[0];
                            mobS2.TInt["playersize"] = 2500;
                            map.SendEventToAllActorsWhoCanSeeActor(SagaMap.Map.EVENT_TYPE.CHAR_INFO_UPDATE, null, mobS2, false);
                            ((MobEventHandler)mobS2.e).Dying += (s, e) => 
							{
								SInt["冥王死亡次数"]++;
								foreach (var item in pc.Party.Members.Values)
									if (item.Online)
										item.TInt["东牢D-3"] = 3;
							};
                        }
                    }

                }
                else if (pc.Party == null && pc.TInt["副本复活标记"] == 4)
                {
                    Select(pc, " ", "", "迎战吧");
                    if (pc.TInt["东牢D-3"] < 2)
                    {
                        pc.TInt["东牢D-3"] = 2;
                        东部地牢.Difficulty diff = 东部地牢.Difficulty.Single_Normal;
                        if (pc.CInt["东部地牢难度"] == 1)
                            diff = 东部地牢.Difficulty.Single_Easy;
                        if (pc.CInt["东部地牢难度"] == 2)
                            diff = 东部地牢.Difficulty.Single_Normal;
                        if (pc.CInt["东部地牢难度"] == 3)
                            diff = 东部地牢.Difficulty.Single_Hard;
                        if (pc.CInt["东部地牢难度"] == 4)
                            diff = 东部地牢.Difficulty.Single_Hell;
                        SagaMap.Map map = SagaMap.Manager.MapManager.Instance.GetMap((uint)pc.TInt["S20092000"]);
                        ActorMob mobS2 = map.SpawnCustomMob(10000000, map.ID, 10136900, 0, 10010100, 1, 147, 44, 0, 1, 0, 冥王Info(diff), 冥王AI(diff), (m, p) =>
                        {
                            if (m.mob.Status.Additions.ContainsKey("变形术"))
                                TitleProccess(p, 72, 1);
                        }, 1)[0];
                        mobS2.TInt["playersize"] = 2500;
                        map.SendEventToAllActorsWhoCanSeeActor(SagaMap.Map.EVENT_TYPE.CHAR_INFO_UPDATE, null, mobS2, false);
                        ((MobEventHandler)mobS2.e).Dying += (s, e) => 
						{
							SInt["冥王死亡次数"]++;
							pc.TInt["东牢D-3"] = 3;
						};
                    }
                }
                return;
            }
            ShowEffect(pc, 161, 37, 5447);
            Say(pc, 65535, "加油啊！", "沙月");
        }
    }
}   