using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
namespace SagaScript.M31302000
{
    public class S11001873 : Event
    {
        public S11001873()
        {
            this.EventID = 11001873;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 0, "あ～……。$R;" +
            "おなかすいた～……。$R;" +
            "$Rあんた、なんか買ってきてよ～。$R;", "空腹の女");

            Say(pc, 11001874, 0, "自分でいけよ～……。$R;" +
            "ついでに俺のも頼むわ……。$R;", "空腹の男");
}
}

        
    }


