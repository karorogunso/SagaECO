using System;using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Item;
using SagaDB.Map;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript
{
    public class S18030001 : Event
    {
        public S18030001()
        {
            this.EventID = 18030001;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 0, "新手礼包,现在直接在游戏中$R送了哦!", "大泰迪");
        }
    }
}