
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
    public class S60000133 : Event
    {
        public S60000133()
        {
            this.EventID = 60000133;
        }

        public override void OnEvent(ActorPC pc)
        {
if (CountItem(pc, 950000100) >= 3)//检测道具
{
Say(pc, 0, "QAQ喵喵之前好像手滑了喵，$R不好意思呢，我给您换一下吧", "爱躲猫猫的芭丝特");
GiveItem(pc, 950000103, 3);
TakeItem(pc, 950000100, 3);
return;
}

            if (pc.AInt["第一届哭叽叽躲猫猫"] == 1 && pc.AInt["第一届哭叽叽躲猫猫安慰奖"] == 0)
            {
                Say(pc, 0, "恭喜你获得参与奖喵~", "爱躲猫猫的芭丝特");
                GiveItem(pc, 950000103, 3);
                GiveItem(pc, 910000106, 3);
                GiveItem(pc, 950000034, 1000);
                GiveItem(pc, 950000025, 3);
                pc.AInt["第一届哭叽叽躲猫猫安慰奖"] = 1;
            }
            if (pc.AInt["第一届哭叽叽躲猫猫安慰奖"] == 1) Say(pc, 0, "你已经拿过奖励了哦，喵喵喵", "爱躲猫猫的芭丝特");
            return;
        }
    }
}