
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
    public partial class 暗鸣 : Event
    {
        void 鱼缸后岛的异变普单人(ActorPC pc)
        {
            if (Select(pc, "确定要进入单人模式吗？", "", "是的", "算了！") == 1)
            {
                if (pc.QuestRemaining >= 20)
                {
                    SagaMap.Network.Client.MapClient.FromActorPC(pc).SendSystemMessage("消耗了20点任务点。");
                    pc.QuestRemaining -= 20;
                    SagaMap.Network.Client.MapClient.FromActorPC(pc).SendQuestPoints();
                    设置单人复活次数(pc);
                    创建单人(pc);
                    pc.Buff.单枪匹马 = true;
                    Warp(pc, (uint)pc.TInt["S10054100"], 225, 87);
                    SetNextMoveEvent(pc, 87000000);//AAA剧情
                    清记录(pc);
                }
                else
                {
                    SagaMap.Network.Client.MapClient.FromActorPC(pc).SendSystemMessage("任务点不足。");
                }
            }
        }
        void 设置单人复活次数(ActorPC pc)
        {
            pc.TInt["副本复活标记"] = 4;
            pc.TInt["单人复活次数记录"] = 3;
            pc.TInt["单人复活次数"] = 3;
        }

        bool 创建单人(ActorPC pc)
        {
            pc.TInt["S10054100"] = CreateMapInstance(10054100, 30131001, 6, 8, true, 0, true);//后岛
            pc.TInt["S20004000"] = CreateMapInstance(20004000, 30131001, 6, 8, true, 0, true);//大陆B5F
            pc.TInt["S20003000"] = CreateMapInstance(20003000, 30131001, 6, 8, true, 0, true);//大陆B4F
            pc.TInt["S20002000"] = CreateMapInstance(20002000, 30131001, 6, 8, true, 0, true);//大陆B3F
            pc.TInt["S20001000"] = CreateMapInstance(20001000, 30131001, 6, 8, true, 0, true);//大陆B2F
            pc.TInt["S20000000"] = CreateMapInstance(20000000, 30131001, 6, 8, true, 0, true);//大陆B1F
            pc.TInt["S30131002"] = CreateMapInstance(30131002, 30131001, 6, 8, true, 0, true);//屋子

            右岛刷怪((uint)pc.TInt["S10054100"], pc);
            大陆B5F刷怪((uint)pc.TInt["S20004000"], pc,true);
            大陆B4F刷怪((uint)pc.TInt["S20003000"], pc);
            大陆B3F刷怪((uint)pc.TInt["S20002000"], pc);
            大陆B2F刷怪((uint)pc.TInt["S20001000"], pc);
            大陆B1F刷怪((uint)pc.TInt["S20000000"], pc, true);
            左岛刷怪((uint)pc.TInt["S10054100"], pc, true);
            return true;
        }
    }
}