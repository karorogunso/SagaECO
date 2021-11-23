using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Item;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M30094000
{
    public class S11000840 : Event
    {
        public S11000840()
        {
            this.EventID = 11000840;

        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "$R光之塔好像又出現了傷者$R;" +
                "$P迪澳曼特明知道危險$R;" +
                "$R還硬把機票賣給$R;" +
                "水準低的冒險者…$R;" +
                "真是壞傢伙阿！$R;");
        }
    }
}