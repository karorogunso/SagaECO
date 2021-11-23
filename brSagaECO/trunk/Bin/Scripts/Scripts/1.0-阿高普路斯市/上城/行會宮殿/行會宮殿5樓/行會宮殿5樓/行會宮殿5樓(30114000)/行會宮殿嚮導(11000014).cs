using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
//所在地圖:行會宮殿5樓(30114000) NPC基本信息:行會宮殿嚮導(11000014) X:10 Y:16
namespace SagaScript.M30114000
{
    public class S11000014 : Event
    {
        public S11000014()
        {
            this.EventID = 11000014;
        }

        public override void OnEvent(ActorPC pc)
        {

        }
    }
}
