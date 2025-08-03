using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;

using SagaScript.Chinese.Enums;
namespace SagaScript.M10018100
{
    public class S12002027 : Event
    {
        public S12002027()
        {
            this.EventID = 12002027;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "商人的商店$R;");
            switch (Select(pc, "后面写了些什么…", "", "看看吧！", "不看！"))
            {
                case 1:
                    Say(pc, 131, "把肉和蔬菜放在面包树果实$R;" +
                        "放盐后三明治就完成了$R;" +
                        "$R谁都可以容易做到的$R;");
                    break;
                case 2:
                    break;
            }
        }
    }
}