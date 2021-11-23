using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
using SagaScript.Chinese.Enums;
namespace SagaScript.M50011000
{
    public class S11001205 : Event
    {
        public S11001205()
        {
            this.EventID = 11001205;
        }

        public override void OnEvent(ActorPC pc)
        {

            if (pc.Quest != null)
            {
                if (pc.Quest.ID == 10031701)
                {
                    if (pc.Quest.Status == SagaDB.Quests.QuestStatus.OPEN)
                    {
                        Say(pc, 131, "火鳳凰！來！$R;" +
                            "讓草鱷魚看看您的真面目吧！$R;");
                        return;
                    }
                    if (pc.Quest.Status == SagaDB.Quests.QuestStatus.COMPLETED)
                    {
                        Say(pc, 131, "打倒了！？哇！$R;" +
                            "$P哇…一定是說謊！一定是說謊！$R;" +
                            "這怎麼可能呢！$R;" +
                            "那是好像很困難的任務呢$R;" +
                            "是最強的魔人喔！$R;" +
                            "$R我重新做呢！$R;" +
                            "這一次已經是重新做的嗎？$R;");

                        HandleQuest(pc, 31);

                        Warp(pc, 10023000, 95, 165);
                        return;
                    }
                    Say(pc, 131, "♪$R;" +
                        "草員的火鳳凰，連出手都還沒有呢！$R;" +
                        "$R是阿！是阿！$R;");

                    HandleQuest(pc, 31);

                    Say(pc, 131, "雖然好像是打倒了999隻魔物$R;" +
                        "但反正在樸魯附近打怪，$R;" +
                        "也挺有趣的吧！$R;");
                    ShowEffect(pc, 11001205, 8013);
                    PlaySound(pc, 2430, false, 100, 50);
                    Say(pc, 131, "有嗎！？是誰？$R;");

                    pc.CInt["LV75_Clothes_Map_02"] = CreateMapInstance(50012000, 10023000, 95, 165);

                    Warp(pc, (uint)pc.CInt["LV75_Clothes_Map_02"], 7, 11);
                    return;
                    //EVENTMAP_IN 12 1 7 4 4
                    //SWITCH START
                    //ME.WORK0 = -1 EVT1100120501a
                    //SWITCH END
                    //EVENTEND
                    //EVT1100120501a
                    //Say(pc, 131, "……可能是心情問題?$R;" +
                    //    "不論如何，任務都失敗了！$R;");
                    //EVENTEND

                }
            }
            Say(pc, 131, "戻るがよい。$R;");

            Warp(pc, 10023000, 95, 165);
        }
    }
}