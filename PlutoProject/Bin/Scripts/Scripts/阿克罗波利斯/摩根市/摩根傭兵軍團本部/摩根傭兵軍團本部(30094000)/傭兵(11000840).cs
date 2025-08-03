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
            Say(pc, 131, "$R光之塔好像又出现了伤者$R;" +
                "$P迪奥曼特明知道危险$R;" +
                "$R还硬把机票卖给$R;" +
                "水准低的冒险者…$R;" +
                "真是坏家伙啊！$R;");
        }
    }
}