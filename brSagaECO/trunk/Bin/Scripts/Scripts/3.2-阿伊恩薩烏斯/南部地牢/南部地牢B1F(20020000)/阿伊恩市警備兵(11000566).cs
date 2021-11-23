using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M20020000
{
    public class S11000566 : Event
    {
        public S11000566()
        {
            this.EventID = 11000566;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "數量雖然少了，$R;" +
                "但還是有兇惡的魔物，$R;" +
                "要小心啊$R;");
        }
    }
}