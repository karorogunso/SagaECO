using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M30133000
{
    public class S11000697 : Event
    {
        public S11000697()
        {
            this.EventID = 11000697;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 190, "這裡不僅是農夫行會$R;" +
                "其他生產家也經常過來，$R;" +
                "所以也算是各行會的支會$R;");
        }
    }
}