using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
//所在地圖:泰迪島(10071000) NPC基本信息:夸克博士(18000170) X:48 Y:193
namespace SagaScript.M10071000
{
    public class S18000170 : Event
    {
        public S18000170()
        {
            this.EventID = 18000170;
        }

        public override void OnEvent(ActorPC pc)
        {

        }
    }
}