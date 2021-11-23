using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
namespace SagaScript.M31304000
{
    public class S11001863 : Event
    {
        public S11001863()
        {
            this.EventID = 11001863;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 0, "すみませ～ん。$R;" +
            "$Rこれとこれとこれっ！$R;" +
            "預かってくださ～い。$R;", "あずける女");

            Say(pc, 11001737, 131, "これとこれとこれっ！ですね。$R;" +
            "承知しました～。$R;" +
            "$Rごゆるりと温泉をお楽しみください。$R;", "貴重品預かり所");
        }


    }
}


