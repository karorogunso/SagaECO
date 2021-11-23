
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
    public class S12003010 : Event
    {
        public S12003010()
        {
            this.EventID = 12003010;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 0, "这里是沙漠入口$R$R请小心不要走丢哦！");
        }
    }
}

