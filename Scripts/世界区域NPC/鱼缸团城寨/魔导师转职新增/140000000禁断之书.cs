
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
namespace SagaScript.M30210000
{
    public partial class S140000000 : Event
    {
        public S140000000()
        {
            EventID = 140000000;
        }
        public override void OnEvent(ActorPC pc)
        {
            if (CountItem(pc, 140000000) < 1) return;
            if (pc.CInt["任务技能解锁幻术系"] !=1)
            {
                if (pc.CInt["灾祸的见证者任务"] >= 4 && pc.CInt["禁断之书的记忆-夺魂者"] == 1)
                {
                    pc.CInt["任务技能解锁幻术系"] = 1;
                    SagaMap.Network.Client.MapClient.FromActorPC(pc).SendPlayerInfo();
                    Say(pc, 0, "禁断之书上记录了大量关于幻术的知识…$R似乎对你有所启发！", "禁断之书");
                    SagaMap.Network.Client.MapClient.FromActorPC(pc).SendSystemMessage("幻术系技能已解放！");
                }
				else if (pc.CInt["灾祸的见证者任务"] >= 5 || pc.CInt["禁断之书的记忆-夺魂者"] == 1)
				{
					Say(pc, 0, "禁断之书上记录了一些关于幻术的知识$R但是零零碎碎的，很难理解。$R$R也许还需要进一步的记录…", "禁断之书");
				}
				else
				{
					Say(pc, 0, "禁断之书上没有什么有意思的东西。", "禁断之书");
				}
            }
            else
            {
				Say(pc, 0, "禁断之书上没有什么有意思的新东西。", "禁断之书");
            }
        }
    }
}