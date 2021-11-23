using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaDB.Actor;
using SagaDB.Item;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
using SagaMap.Network.Client;

namespace SagaScript.M30069005
{
    public class S20010025 : Event
    {
        public S20010025()
        {
            this.EventID = 20010025;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 11002282, 131, "作为一个优秀的挖煤的$R;" +
                                   "我要给你推荐生活必需品!$R;" +
                                   "『粘土』$R;"+
                                   "我这里可是物美价廉,可别错过", "领域");
            Say(pc, 11002282, 131, "当然$R;" +
                                   "还有一些光之塔的出土物品$R;", "领域");
            Say(pc, 20010014, 131, ".....", "破坏RX1·阿鲁玛");
            OpenShopBuy(pc, 2013);
            if (pc.Gender == PC_GENDER.MALE) 
            {
                Say(pc, 20010014, 131, "那个"+
                                       "亚拉那一卡?", "领域");
            }
        }
    }
}