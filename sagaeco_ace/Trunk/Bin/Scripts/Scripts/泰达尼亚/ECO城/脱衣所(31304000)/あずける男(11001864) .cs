using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
namespace SagaScript.M31304000
{
    public class S11001864 : Event
    {
        public S11001864()
        {
            this.EventID = 11001864;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 0, "すんませ～ん。$R;" +
            "$Rわいの大事なモン$R;" +
            "預かってくだ～さい。$R;", "あずける男");

            Say(pc, 11001737, 131, "わいの大事なモン！ですね。$R;" +
            "承知しました～。$R;" +
            "$Rごゆるりと温泉をお楽しみください。$R;", "貴重品預かり所");
        }


    }
}


