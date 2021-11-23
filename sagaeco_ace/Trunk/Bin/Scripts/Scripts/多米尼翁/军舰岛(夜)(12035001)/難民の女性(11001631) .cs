using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
namespace SagaScript.M12035001
{
    public class S11001631 : Event
    {
        public S11001631()
        {
            this.EventID = 11001631;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 135, "……。$R;" +
            "$Pこの子はわたしが$R;" +
            "守ってみせる、必ず……。$R;", "難民の女性");

        }
    }
}


        
   


