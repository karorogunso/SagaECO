using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
namespace SagaScript.M31304000
{
    public class S11001852 : Event
    {
        public S11001852()
        {
            this.EventID = 11001852;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 0, "お湯に浸かった後は$R;" +
            "冷たいものを顔に当てるといいわ。$R;" +
            "$R引き締まって$R;" +
            "毛穴が目立たない肌になるわよ。$R;", "美肌の女");
        }


    }
}


