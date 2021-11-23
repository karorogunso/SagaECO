using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Item;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M10054000
{
    public class S11000659 : Event
    {
        public S11000659()
        {
            this.EventID = 11000659;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "这裡是谜之团的本部，$R;" +
                "一但进来的话…就完了！嘿嘿嘿…$R;");
        }
    }
}