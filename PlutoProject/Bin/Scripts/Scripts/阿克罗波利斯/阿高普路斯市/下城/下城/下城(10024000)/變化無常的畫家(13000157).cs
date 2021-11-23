using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
//所在地圖:下城(10024000) NPC基本信息:變化無常的畫家(13000157) X:145 Y:114
namespace SagaScript.M10024000
{
    public class S13000157 : Event
    {
        public S13000157()
        {
            this.EventID = 13000157;
        }

        public override void OnEvent(ActorPC pc)
        {

        }
    }
}
