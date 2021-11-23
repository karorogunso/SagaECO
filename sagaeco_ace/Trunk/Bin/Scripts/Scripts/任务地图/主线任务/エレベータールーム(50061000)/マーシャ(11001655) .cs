using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;

using SagaScript.Chinese.Enums;
namespace SagaScript.M50061000
{
    public class S11001655 : Event
    {
        public S11001655()
        {
            this.EventID = 11001655;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "なんだかワクワクするわね、エミル！$R;", "マーシャ");

            Say(pc, 11001654, 131, "僕はちょっと不安かな……。$R;", "エミル");

            Say(pc, 131, "んもぉ、男ならシャキっとしなさいよ！$R;", "マーシャ");

            Say(pc, 11001654, 131, "ご、ごめん……。$R;", "エミル");

        }

    }

}
            
            
        
     
    