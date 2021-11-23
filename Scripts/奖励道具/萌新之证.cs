
using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaDB.Item;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M30210000
{
    public class S950000040 : Event
    {
        public S950000040()
        {
            this.EventID = 950000040;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 0, "上面写着：$R$R    感谢您加入Yggdrasil ECO，请在升到65级后输入 /praise 角色名 来感谢一直以来帮助你的人，还会得到礼品哦！$R$R这是只有萌新才享有的权利。");
        }
    }
}

