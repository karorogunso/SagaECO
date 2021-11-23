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
            Say(pc, 0, 0, "這是飛空庭的「操作盤」呀!$R;" +
                          "$R在飛空庭上利用這個「操作盤」$R;" +
                          "和外邊的「舵輪」，$R;" +
                          "可以換牆壁或地面的花紋唷!$R;" +
                          "$P唐卡的工匠有賣$R;" +
                          "地面、壁紙、傢俱等道具呢!$R;" +
                          "$R還有，有一些傢俱，$R;" +
                          "是某些特定職業擁有的技能生產的。$R;" +
                          "$P有飛空庭的話，$R;" +
                          "就請製作獨特有個性的飛空庭吧!$R;", " ");
        }
    }
}
