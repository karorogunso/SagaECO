using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
using SagaScript.Chinese.Enums;
namespace SagaScript.M21193000
{
    public class S11001784 : Event
    {
        public S11001784()
        {
            this.EventID = 11001784;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<ECOchen> ECOchen_mask = new BitMask<ECOchen>(pc.CMask["ECOchen"]);
            //int selection;
            if (ECOchen_mask.Test(ECOchen.打到放水龙后))
            {
                HandleQuest(pc, 70);
                return;
            }
            Say(pc, 0, "…雪ダルマを作っているのだけど、$R;" +
            "どうにも何かが足りない気がするのだ。$R;" +
            "$R何が足りないのだろうか…？$R;", "コール");
        }
    }
}