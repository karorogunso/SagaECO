using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
using SagaScript.Chinese.Enums;
//所在地圖:泰迪島(10071000) NPC基本信息:行會商人(11000734) X:128 Y:220
namespace SagaScript.M10071000
{
    public class S11000734 : Event
    {
        public S11000734()
        {
            this.EventID = 11000734;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<Tinyis_Land_01> Tinyis_Land_01_mask = new BitMask<Tinyis_Land_01>(pc.CMask["Tinyis_Land_01"]);

            if (!Tinyis_Land_01_mask.Test(Tinyis_Land_01.已經與行會商人進行第一次對話))
            {
                初次與行會商人進行對話(pc);
            }

            Say(pc, 11000734, 131, "欢迎光临!!$R;", "行会商人");

            switch (Select(pc, "欢迎光临!!", "", "买东西", "卖东西", "什么也不做"))
            {
                case 1:
                    OpenShopBuy(pc, 148);
                    break;

                case 2:
                    OpenShopSell(pc, 148);
                    break;

                case 3:
                    break;
            }
        }

        void 初次與行會商人進行對話(ActorPC pc)
        {
            BitMask<Tinyis_Land_01> Tinyis_Land_01_mask = new BitMask<Tinyis_Land_01>(pc.CMask["Tinyis_Land_01"]);

            Tinyis_Land_01_mask.SetValue(Tinyis_Land_01.已經與行會商人進行第一次對話, true);

            Say(pc, 11000734, 131, "您好，$R;" +
                                   "我是属于「商人行会」的「行会商人」哦!$R;" +
                                   "$R以冒险者为贩卖东西的对象呢。$R;" +
                                   "$P听说这里有可以做买卖的好地方，$R;" +
                                   "所以就乘坐飞空庭飞到了这座岛。$R;" +
                                   "$P商人是只要生意好，$R;" +
                                   "哪儿都可以去的。$R;" +
                                   "$R哈哈!!$R;", "行会商人");

            Say(pc, 11000734, 131, "什么? 什么叫「飞空庭」?$R;" +
                                   "$R「飞空庭」，故名思义，$R;" +
                                   "是指飞上天空的『庭院』的意思。$R;" +
                                   "$P我们商人主要用它来运东西哦。$R;" +
                                   "$R但用来栽培农作物，$R;" +
                                   "或安装大的帆到处旅行也是可以的。$R;" +
                                   "$P最近，$R;" +
                                   "流行在庭院上，$R;" +
                                   "盖自己的『房子』呢。$R;" +
                                   "$P您也去赶紧准备一艘吧。$R;", "行会商人");

            Say(pc, 11000734, 131, "哎呀，说了一些废话。$R;" +
                                   "$R在飞行途中遇到了暴风雨，$R;" +
                                   "失去了意识，$R;" +
                                   "醒来一看，就在这座岛了。$R;" +
                                   "$P为了减少「飞空庭」的东西，$R;" +
                                   "现在特价大拍卖。$R;" +
                                   "$R一定要光临呀!!$R;", "行会商人");

            Say(pc, 11000734, 131, "钱不够的话，$R;" +
                                   "可以在这座岛上，$R;" +
                                   "搜集各种物品卖给我哦。$R;" +
                                   "$R我会算你便宜一点的。$R;", "行会商人");
        }
    }
}




