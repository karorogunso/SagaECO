using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
//所在地圖:飛空庭的房間(30202000) NPC基本信息:操作盤(11000940) X:8 Y:14
namespace SagaScript.M30202000
{
    public class S11000940 : Event
    {
        public S11000940()
        {
            this.EventID = 11000940;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 0, 0, "这是飞空庭的「操作盘」呀!$R;" +
                          "$R在飞空庭上利用这个「操作盘」$R;" +
                          "和外边的「舵轮」，$R;" +
                          "可以换墙壁或地面的花纹哦!$R;" +
                          "$P唐卡的工匠有卖$R;" +
                          "地面、壁纸、家具等道具呢!$R;" +
                          "$R还有，有一些家具，$R;" +
                          "是某些特定职业拥有的技能生產的。$R;" +
                          "$P有飞空庭的话，$R;" +
                          "就请制作独特有个性的飞空庭吧!$R;", " ");
        }
    }
}
