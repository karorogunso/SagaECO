using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
//所在地圖:南部地牢B3F(20022000) NPC基本信息:汗だくのタタラベ(11001990) X:210 Y:181
namespace SagaScript.M20022000
{
    public class S11001990 : Event
    {
        public S11001990()
        {
            this.EventID = 11001990;
        }

        public override void OnEvent(ActorPC pc)
        {

        }
    }
}