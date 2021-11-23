using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Item;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M30099001
{
    public class S11000865 : Event
    {
        public S11000865()
        {
            this.EventID = 11000865;
        }

        public override void OnEvent(ActorPC pc)
        {
            /*
            if (_6a69)
            {
                Say(pc, 131, "這些摩根炭…遠遠不夠呀$R;" +
                    "$R只能從正式渠道購買了…$R;");
                return;
            }
            */
            Say(pc, 131, "哈哈哈！$R;" +
                "以便宜的价格弄到了摩戈炭啦$R;");
        }
    }
}