using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
namespace SagaScript.M21193000
{
    public class S11001794 : Event
    {
        public S11001794()
        {
            this.EventID = 11001794;
        }

        public override void OnEvent(ActorPC pc)
        {

            Say(pc, 0, "ぐーぐー…$R;" +
            "$P…$R;" +
            "$P……$R;" +
            "$P………ふがっ！$R;", "居眠りしてる男");
        }
    }
}