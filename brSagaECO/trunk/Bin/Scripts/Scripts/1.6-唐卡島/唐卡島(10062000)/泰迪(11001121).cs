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
    public class S11001121 : Event
    {
        public S11001121()
        {
            this.EventID = 11001121;
        }

        public override void OnEvent(ActorPC pc)
        {
            if (pc.Marionette != null)
            {
                Say(pc, 255, "有時間要清潔一下，$R不然會有異味兒的唷$R;");
                return;
            }
            Say(pc, 255, "把我洗得乾乾淨淨的$R;");
        }
    }
}