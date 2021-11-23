using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
//所在地圖:泰迪島(10071000) NPC基本信息:破碎的電路機械(11001239) X:59 Y:28
namespace SagaScript.M10071000
{
    public class S11001239 : Event
    {
        public S11001239()
        {
            this.EventID = 11001239;
        }

        public override void OnEvent(ActorPC pc)
        {

        }
    }
}