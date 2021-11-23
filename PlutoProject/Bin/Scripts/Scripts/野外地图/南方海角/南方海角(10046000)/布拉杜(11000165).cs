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
            Say(pc, 131, "这里是阿克罗尼亚大陆$R最南边的村子$R;" +
                        "$R过了在南边$R大桥的话就可以到达$R艾恩萨乌斯岛上$R;" +
                        "$P因为艾恩萨乌斯岛上有活火山$R所以是非常危险的岛$R;" +
                        "$R想要去的话为了防热和火山灰$R要做充分的准备啊$R;");
        }
    }
}
