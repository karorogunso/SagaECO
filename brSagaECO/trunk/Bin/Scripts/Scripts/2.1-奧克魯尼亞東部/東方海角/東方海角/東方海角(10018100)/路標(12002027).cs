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
            switch (Select(pc, "後面寫了些什麽…", "", "看看吧！", "不看！"))
            {
                case 1:
                    Say(pc, 131, "把肉和蔬菜放在麵包樹果實$R;" +
                        "放鹽後三明治就完成了$R;" +
                        "$R誰都可以容易做到的$R;");
                    break;
                case 2:
                    break;
            }
        }
    }
}