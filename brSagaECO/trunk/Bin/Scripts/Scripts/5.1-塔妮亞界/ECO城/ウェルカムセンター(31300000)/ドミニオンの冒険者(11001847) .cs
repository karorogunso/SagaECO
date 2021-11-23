using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
namespace SagaScript.M31300000
{
    public class S11001847 : Event
    {
        public S11001847()
        {
            this.EventID = 11001847;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 0, "……くそっ！$R;" +
            "とくに欲しい景品はないんだが$R;" +
            "$R大富豪にはまってしまって$R;" +
            "抜けられねぇっ！！$R;", "ドミニオンの冒険者");

            //
            /*
            Say(pc, 0, "……くそっ！$R;" +
            "とくに欲しい景品はないんだが$R;" +
            "$R大富豪にはまってしまって$R;" +
            "抜けられねぇっ！！$R;", "ドミニオンの冒険者"); ;
            */
        
    }


    }
}


