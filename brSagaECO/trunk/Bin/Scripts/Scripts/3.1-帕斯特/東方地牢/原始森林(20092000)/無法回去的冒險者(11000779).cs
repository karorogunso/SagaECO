using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M20092000
{
    public class S11000779 : Event
    {
        public S11000779()
        {
            this.EventID = 11000779;
        }

        public override void OnEvent(ActorPC pc)
        {
            int a;
            a = Global.Random.Next(1, 2);
            switch (a)
            {
                case 1:
                    Say(pc, 131, "嗖嗖嗖…$R;" +
                        "$R來這裡的路上，聽到了慘叫聲！$R;" +
                        "$R到底發生什麽事情了？$R;");
                    break;
                case 2:
                    Say(pc, 131, "嗖嗖嗖…$R;" +
                        "$R魔物太强了，無法回去阿!$R;" +
                        "所以在等其他隊員過來接我！$R;");
                    break;
            }
        }
    }
}