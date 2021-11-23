using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
namespace SagaScript.M11053000
{
    public class S11001513 : Event
    {
        public S11001513()
        {
            this.EventID = 11001513;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 0, "斯瑟斯瑟……。$R;", "五古亀");
        }


    }
}


