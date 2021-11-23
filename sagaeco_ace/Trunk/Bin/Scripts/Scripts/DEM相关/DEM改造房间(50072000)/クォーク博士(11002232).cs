using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;

using SagaLib;
namespace SagaScript.M50072000
{
    public class S11002232 : Event
    {
        public S11002232()
        {
            this.EventID = 11002232;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<DEMNewbie> newbie = new BitMask<DEMNewbie>(pc.CMask["DEMNewbie"]);

            Say(pc, 131, "啊，你来了。$R;" + 
            "要什么吗？$R;", "クォーク博士");
            switch (Select(pc, "想怎么办？", "", "成本限制提升", "部件定制", "ＤＥＭＩＣ", "购买部件", "购买晶片", "EP电池(未实装)", "零件强化(未实装)", "零件融合(未实装)", "什么都没有"))
            {
                case 1:
                    DEMCL(pc);
                    break;
                case 2:
                    DEMParts(pc);
                    break;
                case 3:
                    DEMIC(pc);
                    break;
                case 4:
                    Say(pc, 131, "购买零件。$R;" +
                    "$R因为资金困难的缘故..$R;" +
                    "因此会贵一点$R;" +
                    "那么。$R;" +
                    "$P你想购买、$R;" +
                    "什么部件？$R;", "クォーク博士");
                    switch (Select(pc, "……。", "", "购买手臂部件", "购买其他部件", "不购入"))
                    {
                        case 1:
                            OpenShopBuy(pc, 292);
                            break;
                        case 2:
                            OpenShopBuy(pc, 293);
                            break;
                    }
                    break;
                case 5:
                    DEMChipShop(pc);
                    break;
            }
        }
    }
}
