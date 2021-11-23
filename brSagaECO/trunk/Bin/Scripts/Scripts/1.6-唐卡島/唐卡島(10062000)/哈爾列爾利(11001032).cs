using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaDB.Item;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M10062000
{
    public class S11001032 : Event
    {
        public S11001032()
        {
            this.EventID = 11001032;
        }

        public override void OnEvent(ActorPC pc)
        {
            /*
            if (_7a77)
            {
                Say(pc, 0, 131, "停止了$R;");
            }
            */
            Say(pc, 131, "阿，$R;" +
                "$R這裡是進行重要研究的地方$R不能進去呀。$R;");
        }
    }
}