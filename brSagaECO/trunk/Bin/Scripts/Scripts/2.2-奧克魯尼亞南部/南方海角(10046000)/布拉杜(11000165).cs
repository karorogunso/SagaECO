using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
namespace SagaScript.M10046000
{
    public class S11000165 : Event
    {
        public S11000165()
        {
            this.EventID = 11000165;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "這裡是奧克魯尼亞大陸的$R最南邊的村子$R;" +
    "$R過就在下面的南邊$R大橋的話就可以到達$R阿伊恩薩烏斯島上$R;" +
    "$P因爲阿伊恩薩烏斯島上有活火山$R所以是非常危險的島$R;" +
    "$R想要去的話爲了防熱和火山碳$R要做充分的準備啊$R;");
        }
    }
}
