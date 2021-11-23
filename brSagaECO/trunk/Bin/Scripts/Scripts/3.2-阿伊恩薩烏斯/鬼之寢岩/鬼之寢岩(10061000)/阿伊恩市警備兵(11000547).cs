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
            Say(pc, 131, "沿著這個路走$R就是火山地帶$R;" +
                "這裡的魔物都很兇猛的$R;" +
                "$R不要太勉強了$R;");
        }
    }
}