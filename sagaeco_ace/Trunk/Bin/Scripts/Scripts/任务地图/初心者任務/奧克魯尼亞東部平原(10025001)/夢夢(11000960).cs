using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
//所在地圖:奧克魯尼亞東部平原(10025001) NPC基本信息:夢夢(11000960) X:41 Y:136
namespace SagaScript.M10025001
{
    public class S11000960 : Event
    {
        public S11000960()
        {
            this.EventID = 11000960;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 11000959, 131, "哎呀!$R;" +
                                   "不要打我们的梦梦!$R;", "行会商人");

            Say(pc, 11000960, 364, "哞!$R;", "梦梦");

            Say(pc, 11000959, 131, "可爱吧?$R;" +
                                   "$R它是「法伊斯特」，从认识的人那里送来的。$R;", "行会商人");
        }
    }
}
