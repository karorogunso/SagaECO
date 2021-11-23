
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
    public partial class S110003500 : Event
    {
        public S110003500()
        {
            EventID = 110003500;
        }
        public override void OnEvent(ActorPC pc)
        {
            if (CountItem(pc, 110003500) < 1) return;
            if (pc.Party != null)
            {
                if (pc.MapID == pc.Party.TInt["S20090000"] || pc.MapID == pc.Party.TInt["S20091000"] || pc.MapID == pc.Party.TInt["S20092000"])
                {
                    SagaMap.Map map = SagaMap.Manager.MapManager.Instance.GetMap(pc.MapID);
                    pc.HP = 10;
                    pc.MP = 0;
                    pc.SP = 0;
                    map.SendEventToAllActorsWhoCanSeeActor(SagaMap.Map.EVENT_TYPE.HPMPSP_UPDATE, null, pc, true);
                    SagaMap.Network.Client.MapClient.FromActorPC(pc).SendSystemMessage("你吃掉了一个快要腐烂的果实，感觉不太好。");
                    pc.TInt["东牢幻觉"]++;
                    TakeItem(pc, 110003500, 1);
                    /* if(pc.TInt["东牢幻觉"] >= 4)
                    {
                        Say(pc, 0, "你似乎看到了幻觉..？");
                        Warp(pc, (uint)pc.Party.TInt["S60903000"], 49, 4);
                    } */
                    return;
                }
            }
            else if(pc.TInt["副本复活标记"] == 4)
            {
                if (pc.MapID == pc.TInt["S20090000"] || pc.MapID == pc.TInt["S20091000"] || pc.MapID == pc.TInt["S20092000"])
                {
                    SagaMap.Map map = SagaMap.Manager.MapManager.Instance.GetMap(pc.MapID);
                    pc.HP = 10;
                    pc.MP = 0;
                    pc.SP = 0;
                    map.SendEventToAllActorsWhoCanSeeActor(SagaMap.Map.EVENT_TYPE.HPMPSP_UPDATE, null, pc, true);
                    SagaMap.Network.Client.MapClient.FromActorPC(pc).SendSystemMessage("你吃掉了一个快要腐烂的果实，感觉不太好。");
                    pc.TInt["东牢幻觉"]++;
                    TakeItem(pc, 110003500, 1);
                    /* if (pc.TInt["东牢幻觉"] >= 4)
                    {
                        Say(pc, 0, "你似乎看到了幻觉..？");
                        Warp(pc, (uint)pc.TInt["S60903000"], 49, 4);
                    } */
                    return;
                }
            }
            else
            {
                int count = CountItem(pc, 110003500);
                if (count >= 1)
                {
                    TakeItem(pc, 110003500, (ushort)count);
                    GiveItem(pc, 110003600, (ushort)count);
                    Say(pc, 0, "果实发生了变化！！");
                }
            }
        }
    }
}