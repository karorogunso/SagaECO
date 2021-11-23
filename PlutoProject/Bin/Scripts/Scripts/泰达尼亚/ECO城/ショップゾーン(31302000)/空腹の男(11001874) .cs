using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
namespace SagaScript.M31302000
{
    public class S11001874 : Event
    {
        public S11001874()
        {
            this.EventID = 11001874;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 0, "おまえもアトラクションゾーンには$R;" +
            "気をつけろよ……。$R;" +
            "$R俺たちみたいになるぜ……。$R;", "空腹の男");
}
}

        
    }


