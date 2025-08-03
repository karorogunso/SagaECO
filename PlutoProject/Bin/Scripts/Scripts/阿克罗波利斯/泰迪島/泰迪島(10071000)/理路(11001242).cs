using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
//所在地圖:泰迪島(10071000) NPC基本信息:理路(11001242) X:28 Y:36
namespace SagaScript.M10071000
{
    public class S11001242 : Event
    {
        public S11001242()
        {
            this.EventID = 11001242;
        }

        public override void OnEvent(ActorPC pc)
        {

        }
    }
}