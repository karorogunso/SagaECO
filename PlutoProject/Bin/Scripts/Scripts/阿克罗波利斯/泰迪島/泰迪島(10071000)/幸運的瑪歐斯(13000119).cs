using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
//所在地圖:泰迪島(10071000) NPC基本信息:幸運的瑪歐斯(13000119) X:240 Y:223
namespace SagaScript.M10071000
{
    public class S13000119 : Event
    {
        public S13000119()
        {
            this.EventID = 13000119;
        }

        public override void OnEvent(ActorPC pc)
        {

            Say(pc, 65535, "……みっ、みずっぅ！$R;", "幸運のインスマウス");
        }
    }
}