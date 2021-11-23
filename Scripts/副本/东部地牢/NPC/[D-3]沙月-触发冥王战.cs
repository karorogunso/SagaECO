
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
    public partial class S80001701 : Event
    {
        public S80001701()
        {
            this.EventID = 80001701;
        }
        public override void OnEvent(ActorPC pc)
        {
			if (pc.Party != null)
			{
				if (pc.Party.TInt["击杀boss1"]==0 || pc.Party.TInt["击杀boss2"]==0 || pc.Party.TInt["击杀boss3"]==0)
				{
					Say(pc, 0, "东牢的最深处…大概就是这里了。但是…这周围被浓密的幻象所笼罩，所以我们恐怕什么都看不到。", "沙月");
					Say(pc, 0, "看来只能像那个家伙说的，先去寻找$CR三个幻象源头$CD了…", "沙月");
					return;
				}
			}
			else
			{
				if (pc.TInt["击杀boss1"]==0 || pc.TInt["击杀boss2"]==0 || pc.TInt["击杀boss3"]==0)
				{
					Say(pc, 0, "东牢的最深处…大概就是这里了。但是…这周围被浓密的幻象所笼罩，所以我们恐怕什么都看不到。", "沙月");
					Say(pc, 0, "看来只能像那个家伙说的，先去寻找$CR三个幻象源头$CD了…", "沙月");
					return;
				}
			}


            if (pc.TInt["东牢D-3"] == 0)
            {
				Timer timer = new Timer("脚本触发100000101", 0, 1000);
				timer.AttachedPC = pc;
				pc.TInt["东牢D-3"] = 1;
				timer.OnTimerCall += (s, e) =>
				{
					SagaMap.Network.Client.MapClient.FromActorPC(e).EventActivate(100000101);
					s.Deactivate();
				};
				timer.Activate();
                return;
            }

            if (pc.TInt["东牢D-3"] == 1)
            {
                if (pc.Party != null)
                {
                    if (pc.Party != null && pc != pc.Party.Leader)
                    {
                        Say(pc, 0, "你们的队长呢？", "沙月");
                        return;
                    }

                    foreach (var item in pc.Party.Members.Values)
                    {
                        if (!item.Online && item.MapID != pc.Party.Leader.MapID)
                        {
                            foreach (var item2 in pc.Party.Members.Values)
                                SagaMap.Network.Client.MapClient.FromActorPC(item2).SendSystemMessage("有队员不在地图内，请先集合所有队员。");
                            return;
                        }

                        if (item.Online && item.HP > 0)
                        {
                            if (item.TInt["东牢D-3"] != 1)
                            {
                                Say(pc, 0, "有玩家尚未完成对话");
                                Timer timer = new Timer("脚本触发100000101", 0, 1000);
                                timer.AttachedPC = item;
                                timer.OnTimerCall += (s, e) =>
                                {
                                    SagaMap.Network.Client.MapClient.FromActorPC(e).EventActivate(100000101);
                                    s.Deactivate();
                                };
                                timer.Activate();
                                return;
                            }
                        }

                        foreach (var item2 in pc.Party.Members.Values)
                            SagaMap.Network.Client.MapClient.FromActorPC(item2).SendSystemMessage("等待玩家" + item.Name + " 接受中..");
                        if (Select(item, "已经准备好了吗？", "", "好了", "再等等") == 1)
                        {
                            foreach (var item2 in pc.Party.Members.Values)
                                SagaMap.Network.Client.MapClient.FromActorPC(item2).SendSystemMessage(item.Name + "已经准备好了");
                        }
                        else
                        {
                            foreach (var item2 in pc.Party.Members.Values)
                                SagaMap.Network.Client.MapClient.FromActorPC(item2).SendSystemMessage(item.Name + "还没有准备好");
                            return;
                        }
                    }
                    foreach (var item in pc.Party.Members.Values)
                    {
                        if (item.Buff.Dead || item == null || !item.Online)
                        {
                            foreach (var item2 in pc.Party.Members.Values)
                                SagaMap.Network.Client.MapClient.FromActorPC(item2).SendSystemMessage(item.Name + "状态异常，请先确认队员状态！");
                            return;
                        }
                    }

                    foreach (var item in pc.Party.Members.Values)
                    {
                        if (item.Online && item.HP > 0)
                        {
                            Timer timer = new Timer("脚本触发100000105", 0, 1000);
                            timer.AttachedPC = item;
                            timer.OnTimerCall += (s, e) =>
                            {
                                SagaMap.Network.Client.MapClient.FromActorPC(e).EventActivate(100000105);
                                s.Deactivate();
                            };
                            timer.Activate();
                        }
                    }
                }
                else
                {
                    Timer timer = new Timer("脚本触发100000105", 0, 1000);
                    timer.AttachedPC = pc;
                    timer.OnTimerCall += (s, e) =>
                    {
                        SagaMap.Network.Client.MapClient.FromActorPC(e).EventActivate(100000105);
                        s.Deactivate();
                    };
                    timer.Activate();
                }
                return;
            }
            if (pc.TInt["东牢D-3"] == 2)
            {
                Say(pc, 65535, "加油啊！", "沙月");
            }

            if (pc.TInt["东牢D-3"] == 3)
            {
				Timer timer = new Timer("脚本触发100000102", 0, 1000);
				timer.AttachedPC = pc;
				timer.OnTimerCall += (s, e) =>
				{
					SagaMap.Network.Client.MapClient.FromActorPC(e).EventActivate(100000102);
					s.Deactivate();
				};
				timer.Activate();
                return;
            }

            if (pc.TInt["东牢D-3"] == 4)
            {
                if (Select(pc, " ", "", "带我离开吧", "我要睡在这里") == 1)
                {
					if(pc.TInt["东牢幻觉"] >= 4)
                    {
						Wait(pc, 1500);
						Say(pc, 0, "（你正要开口，忽然一阵强烈的眩晕与反胃感向你袭来！）", "");
						Timer timer = new Timer("脚本触发10000826", 0, 1000);
						timer.AttachedPC = pc;
						timer.OnTimerCall += (s, e) =>
						{
							SagaMap.Network.Client.MapClient.FromActorPC(e).EventActivate(10000826);
							s.Deactivate();
						};
						timer.Activate();
                    }
					else
					{
						ShowEffect(pc, 4083);
						Wait(pc, 1500);
						ShowEffect(pc, 40, 62, 4299);
						Warp(pc, 10057002, 246, 116);
					}
                }
            }
        }
    }
}
