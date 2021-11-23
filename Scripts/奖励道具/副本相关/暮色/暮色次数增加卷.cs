
using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaDB.Item;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M30210000
{
    public class S910000009 : Event
    {
        public S910000009()
        {
            this.EventID = 910000009;
        }

        public override void OnEvent(ActorPC pc)
        {
            if (pc.CInt["今天不死皇城次数"] == 0)
            {
                Say(pc, 131, "你今天还没有进入过暮色宴会哦");
                return;
            }
            pc.CInt["今天不死皇城次数"] -= 1;
            TakeItem(pc, 910000009, 1);
            Say(pc, 131, "清除了一次暮色宴会次数");
        }
        void 奖励(ActorPC pc)
        {

        }
    }
}

