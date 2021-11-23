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
            Say(pc, 190, "这里不仅是农夫行会$R;" +
                "其他生产系也经常过来，$R;" +
                "所以也算是各行会的支会$R;");
        }
    }
}