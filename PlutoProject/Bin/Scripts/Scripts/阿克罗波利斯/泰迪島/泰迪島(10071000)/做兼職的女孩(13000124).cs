using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
//所在地圖:泰迪島(10071000) NPC基本信息:做兼職的女孩(13000124) X:227 Y:168
namespace SagaScript.M10071000
{
    public class S13000124 : Event
    {
        public S13000124()
        {
            this.EventID = 13000124;
        }

        public override void OnEvent(ActorPC pc)
        {
            ShowEffect(pc, 13000124, 4505);

            Say(pc, 65535, "タイニー焼き作るの$R;" +
            "だいぶ上手になってきたかな♪$R;", "バイトの女の子");
        }
    }
}