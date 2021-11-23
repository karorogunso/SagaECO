using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaDB.Item;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M10062000
{
    public class S11001123 : Event
    {
        public S11001123()
        {
            this.EventID = 11001123;
        }

        public override void OnEvent(ActorPC pc)
        {
            if (pc.Marionette != null)
            {
                Say(pc, 255, "被敵人抓住的$R;" +
                    "活動木偶怎麼樣了呢？$R;" +
                    "$R鳴鳴……想想都怕。$R;");
                return;
            }
            Say(pc, 255, "訓練中$R;");
        }
    }
}