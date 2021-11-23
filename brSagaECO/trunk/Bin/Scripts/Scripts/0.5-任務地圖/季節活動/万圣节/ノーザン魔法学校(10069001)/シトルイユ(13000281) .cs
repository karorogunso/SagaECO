using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
using SagaScript.Chinese.Enums;
namespace SagaScript.M10069001
{
    public class S13000281 : Event
    {
        public S13000281()
        {
            this.EventID = 13000281;
        }

        public override void OnEvent(ActorPC pc)
        {
            ShowEffect(pc, 13000281, 4510);

            Say(pc, 131, "……こまりましたわ。$R;" +
            "$Rキュルビスお姉さまとの$R;" +
            "晩餐にと色々準備しましたのに。$R;" +
            "$P一番肝心な料理の事を$R;" +
            "忘れていたなんて……。$R;", "シトルイユ");

            Say(pc, 131, "はぁ〜。$R;" +
            "そんな都合よく$R;" +
            "『ハロウィンのごちそう』なんて$R;" +
            "手に入りませんわよね……。$R;", "シトルイユ");
        }
    }
}