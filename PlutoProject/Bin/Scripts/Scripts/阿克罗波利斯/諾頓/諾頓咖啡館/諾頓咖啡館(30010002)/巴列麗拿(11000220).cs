using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
using SagaScript.Chinese.Enums;
namespace SagaScript.M30010002
{
    public class S11000220 : Event
    {
        public S11000220()
        {
            this.EventID = 11000220;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<NDFlags> mask = new BitMask<NDFlags>(pc.CMask["ND"]);
            if (CountItem(pc, 10005800) >= 1)
            {
                Say(pc, 131, "呀！$R;" +
                    "$R那个是不是『豌豆』么？$R;" +
                    "只要有它就能做浓汤了$R;");
                switch (Select(pc, "欢迎光临！", "", "买东西", "什么也不做"))
                {
                    case 1:
                        TakeItem(pc, 10005800, 1);
                        OpenShopBuy(pc, 106);
                        break;
                    case 2:
                        break;
                }
                Say(pc, 131, "欢迎再来$R;");
                return;
            }
            if (mask.Test(NDFlags.中央的巴列麗拿))
            {
                Say(pc, 131, "哦？$R;" +
                    "我们在阿克罗波利斯见过吧？$R;" +
                    "$R还有麻烦过您呢？$R;" +
                    "微火中长时间煮沸$R;" +
                    "就能完成特制的瓦雷利亚的汤$R;" +
                    "$R来！请尝一尝吧！$R;" +
                    "$P真想这么说$R;" +
                    "不过没有『豌豆』，做不了啊$R;" +
                    "$R抱歉啦$R;");
                return;
            }
            Say(pc, 131, "给我『豌豆』$R;" +
                "就能做到美味的浓汤哦$R;");
        }
    }
}
