using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
//所在地圖:冰結的坑道B1F(20010000) NPC基本信息:厚着嫌いのタタラベ(11001989) X:21 Y:106
namespace SagaScript.M20010000
{
    public class S11001989 : Event
    {
        public S11001989()
        {
            this.EventID = 11001989;
        }

        public override void OnEvent(ActorPC pc)
        {

        }
    }
}
