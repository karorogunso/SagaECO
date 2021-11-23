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
      "$R我現在心情不好$R;" +
      "這傢伙看起來很興奮呢$R;");

            Say(pc, 11000142, 131, "沃特雷德$R;" +
                "是我們的故鄉$R;" +
                "$R很想回去呀…$R;");
        }
    }
}