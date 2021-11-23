using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
namespace SagaScript.M21003000
{
    public class S11001494 : Event
    {
        public S11001494()
        {
            this.EventID = 11001494;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "知道吗？埃米尔的世界$R;" +
            "许多种族和平的生活着$R;" +
            "看得到吗。$R;", "美人鱼");

            Say(pc, 11001495, 132, "和平？$R;" +
            "那真的是和平么？$R;", "美人鱼");

            Say(pc, 131, "那样的话、很期待到处看看呢。$R;", "美人鱼");

        }


    }
}


