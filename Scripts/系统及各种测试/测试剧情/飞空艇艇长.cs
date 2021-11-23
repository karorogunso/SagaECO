
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
    public class S50005000 : Event
    {
        public S50005000()
        {
            this.EventID = 50005000;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 0, "事情办完了吗？","飞空艇艇长");
            if(Select(pc," ","","送我回飞空城吧","我还想再待会")== 1)
            {
                Warp(pc, 90001999, 35, 15);
            }
        }
    }
}

