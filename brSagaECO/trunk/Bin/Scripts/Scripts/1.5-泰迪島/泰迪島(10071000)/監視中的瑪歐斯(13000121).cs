using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
//所在地圖:泰迪島(10071000) NPC基本信息:監視中的瑪歐斯(13000121) X:249 Y:160
namespace SagaScript.M10071000
{
    public class S13000121 : Event
    {
        public S13000121()
        {
            this.EventID = 13000121;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 65535, "俺、見張り。$R;" +
            "$P昔、人間と約束した。$R;" +
            "$P陸、お前らのもの。$R;" +
            "$P海、おれらのもの。$R;" +
            "$Pでも、人間約束破ろうとする。$R;" +
            "$Pだから、見張る。$R;", "見張りのインスマウス");
        }
    }
}