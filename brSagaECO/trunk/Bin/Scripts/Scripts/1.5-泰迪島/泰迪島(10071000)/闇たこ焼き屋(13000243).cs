using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
namespace SagaScript.M10071000
{
    public class S13000243 : Event
    {
        public S13000243()
        {
            this.EventID = 13000243;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "まいど～$R;" +
            "たこは入ってないけど、たこ焼きやで～$R;" +
            "というのは嘘やで～、ほんまやで～$R;", "闇たこ焼き屋");
            OpenShopBuy(pc, 254);

        }
    }
}