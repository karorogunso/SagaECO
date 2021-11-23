using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
//所在地圖:泰迪島(10071000) NPC基本信息:送風負責人(13000122) X:226 Y:160
namespace SagaScript.M10071000
{
    public class S13000122 : Event
    {
        public S13000122()
        {
            this.EventID = 13000122;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 13000123, 65535, "シャワー係でございますの。$R;" +
            "砂や汗を落としますわよ。$R;", "シャワー係");
            ShowEffect(pc, 5031);
            Wait(pc, 660);
            ShowEffect(pc, 5031);
            Wait(pc, 660);
            ShowEffect(pc, 5031);
            Wait(pc, 2640);

            Say(pc, 13000120, 65535, "濡れた体を温めよう。$R;", "熱風係");
            ShowEffect(pc, 5101);
            Wait(pc, 2640);

            Say(pc, 65535, "風で、水分を吹き飛ばしますっ！$R;" +
            "え～いっ！$R;", "送風係");
            ShowEffect(pc, 4029);
            Wait(pc, 3300);

            Say(pc, 13000123, 65535, "はい、終了です♪$R;", "マリオネット３姉妹");
        }
    }
}