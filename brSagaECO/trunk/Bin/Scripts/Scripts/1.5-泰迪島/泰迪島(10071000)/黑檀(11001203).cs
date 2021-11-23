using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
//所在地圖:泰迪島(10071000) NPC基本信息:黑檀(11001203) X:188 Y:163
namespace SagaScript.M10071000
{
    public class S11001203 : Event
    {
        public S11001203()
        {
            this.EventID = 11001203;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 11000722, 131, "這小子身上穿的，$R;" +
                                   "是從「泰迪」那裡拿的衣服唷!$R;", "小鬼嘉布利");

            ShowEffect(pc, 10005, 4516);
        }
    }
}
