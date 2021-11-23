using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
//所在地圖:泰迪島(10071000) NPC基本信息:爬爬蟲凱莉娥(11000735) X:129 Y:221
namespace SagaScript.M10071000
{
    public class S11000735 : Event
    {
        public S11000735()
        {
            this.EventID = 11000735;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 11000735, 131, "哞…哞…$R;", "爬爬蟲凱莉娥");
        }
    }
}




