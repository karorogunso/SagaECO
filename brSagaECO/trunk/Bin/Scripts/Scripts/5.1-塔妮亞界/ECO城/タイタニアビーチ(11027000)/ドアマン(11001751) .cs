using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
namespace SagaScript.M11027000
{
    public class S11001751 : Event
    {
        public S11001751()
        {
            this.EventID = 11001751;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "ようこそ、ＥＣＯタウン……って$R;" +
            "あれ、え、うそ！？$R;" +
            "$Rキミ、どこから来たの？$R;", "ドアマン");
}
}

        
    }


