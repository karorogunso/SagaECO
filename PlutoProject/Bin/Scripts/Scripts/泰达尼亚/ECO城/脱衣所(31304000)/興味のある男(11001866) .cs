using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
namespace SagaScript.M31304000
{
    public class S11001866 : Event
    {
        public S11001866()
        {
            this.EventID = 11001866;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 0, "あああっ！！$R;" +
            "$Rもう我慢がぁッ！$R;", "興味のある男");
        }


    }
}


