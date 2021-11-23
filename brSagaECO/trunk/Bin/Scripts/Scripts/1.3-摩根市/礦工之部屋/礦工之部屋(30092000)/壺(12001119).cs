using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Item;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M30092000
{
    public class S12001119 : Event
    {
        public S12001119()
        {
            this.EventID = 12001119;
        }

        public override void OnEvent(ActorPC pc)
        {
            switch (Select(pc, "要查看裡面嗎？", "", "查看", "不查看"))
            {
                case 1:
                    Say(pc, 255, "裡面什麼都沒有$R;");
                    break;
                case 2:
                    break;
            }
        }
    }
}