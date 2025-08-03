using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
namespace SagaScript.M11001448
{
    public class S11001448 : Event
    {
        public S11001448()
        {
            this.EventID = 11001448;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "啦啦〜、啦啦啦啦〜♪$R;" + 
            "啦啦啦〜♪$R;", "歌姫娜娜");
        }
    }
}