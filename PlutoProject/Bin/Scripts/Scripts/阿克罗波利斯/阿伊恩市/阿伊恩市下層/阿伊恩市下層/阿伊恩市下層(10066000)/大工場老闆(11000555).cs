using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M10066000
{
    public class S11000555 : Event
    {
        public S11000555()
        {
            this.EventID = 11000555;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "您也是来打魔物的吗？$R;" +
                "$P如果没有魔物就好了$R;" +
                "$R还能舒服一点$R;");
        }
    }
}