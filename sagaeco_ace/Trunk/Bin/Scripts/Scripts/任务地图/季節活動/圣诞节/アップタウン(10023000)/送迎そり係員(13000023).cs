using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
namespace SagaScript.M10023000
{
    public class S13000023 : Event
    {
        public S13000023()
        {
            this.EventID = 13000023;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "こちらからのそりは$R;" +
            "地方に住む子どもたちのために$R;" +
            "プレゼント箱を運ぶ$R;" +
            "運搬者専用となっております。$R;" +
            "$Rご利用の方は酒屋でクエストを$R;" +
            "受けてきてくださいませ。$R;", "送迎そり係員");
        }
    }
}
