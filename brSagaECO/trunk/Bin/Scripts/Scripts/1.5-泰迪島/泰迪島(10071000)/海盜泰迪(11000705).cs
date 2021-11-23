using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
//所在地圖:泰迪島(10071000) NPC基本信息:海盜泰迪(11000705) X:50 Y:94
namespace SagaScript.M10071000
{
    public class S11000705 : Event
    {
        public S11000705()
        {
            this.EventID = 11000705;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 11000705, 131, "再靠近，大炮會開火的!!$R;" + 
                                   "$R轟! 一聲。$R;" +
                                   "那可嚇人了。$R;", "海盜泰迪");
        }
    }
}




