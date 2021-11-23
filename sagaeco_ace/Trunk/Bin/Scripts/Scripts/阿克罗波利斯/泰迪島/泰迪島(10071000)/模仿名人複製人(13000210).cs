using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
//所在地圖:泰迪島(10071000) NPC基本信息:模仿名人複製人(13000210) X:227 Y:173
namespace SagaScript.M10071000
{
    public class S13000210 : Event
    {
        public S13000210()
        {
            this.EventID = 13000210;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "やあ、私はクーロン。$R;" +
            "モノマネ名人だよ！$R;", "ものまね名人クーロン");
        }
    }
}