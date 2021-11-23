using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;

using SagaScript.Chinese.Enums;
namespace SagaScript.M20004001
{
    public class S11000142 : Event
    {
        public S11000142()
        {
            this.EventID = 11000142;
        }

        public override void OnEvent(ActorPC pc)
        {
            if (Global.Random.Next(1, 2) == 1)
                Say(pc, 11000142, 131, "……$R;" +
      "$R我现在心情不好$R;" +
      "这家伙看起来很兴奋呢$R;");

            Say(pc, 11000142, 131, "沃特雷亚$R;" +
                "是我们的故乡$R;" +
                "$R很想回去呀…$R;");
        }
    }
}