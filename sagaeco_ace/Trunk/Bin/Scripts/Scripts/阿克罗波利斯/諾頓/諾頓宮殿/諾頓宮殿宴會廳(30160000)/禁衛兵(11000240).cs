using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M30160000
{
    public class S11000240 : Event
    {
        public S11000240()
        {
            this.EventID = 11000240;
        }

        public override void OnEvent(ActorPC pc)
        {
            /*
            if (!_5a72 && _5a71 && a)
            {
                Call(EVT1100024000);
                return;
            }
            */

            Say(pc, 131, "…$R;" +
                "$R守护女王陛下是我们的荣耀，$R;" +
                "也是我们的幸福$R;");
        }

        void 向塔尼亚致敬(ActorPC pc)
        {
            //EVT1100024000
            Say(pc, 131, "…$R;" +
                "$R在等您喔$R;" +
                "请收下这封信$R;");
            if (CheckInventory(pc, 10043101, 1))
            {
                GiveItem(pc, 10043101, 1);
                Say(pc, 131, "在宫殿书库发现的信笺$R;" +
                    "得到了女王陛下的允许啰$R;");
                PlaySound(pc, 2040, false, 100, 50);
                Say(pc, 131, "拿到了『玩家的信（活动）』$R;");
                return;
            }
            Say(pc, 131, "行李太多了，不能给您啊$R;");
            //EVENTEND
        }
    }
}