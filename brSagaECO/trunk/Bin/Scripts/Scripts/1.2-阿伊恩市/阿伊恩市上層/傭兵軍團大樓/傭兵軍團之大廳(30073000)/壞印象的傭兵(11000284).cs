using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M30073000
{
    public class S11000284 : Event
    {
        public S11000284()
        {
            this.EventID = 11000284;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "最近阿高普路斯怎麼樣啊？$R;");
            Say(pc, 11000285, 131, "好像沒有什麼特別的事阿。$R;");
        }
    }
}