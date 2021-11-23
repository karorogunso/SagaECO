using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
namespace SagaScript.M12035001
{
    public class S11001632 : Event
    {
        public S11001632()
        {
            this.EventID = 11001632;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 136, "おねえちゃん、いつまでここにいるの？$R;", "難民の少女");

            Say(pc, 11001631, 135, "……そうね。$R;" +
            "$Rもう少し……かな。$R;", "難民の女性");

        }
    }
}


        
   


