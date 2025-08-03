using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
namespace SagaScript.M10061000
{
    public class S11000547 : Event
    {
        public S11000547()
        {
            this.EventID = 11000547;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "沿着这个路走$R就是火山地带$R;" +
                "这里的魔物都很凶猛的$R;" +
                "$R不要太勉强了$R;");
        }
    }
}