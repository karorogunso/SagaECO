using System;
using System.Collections.Generic;
using System.Text;
using SagaDB.Actor;
using SagaMap.Scripting;
namespace SagaScript
{
    public class 空中之咖啡室輪舵 : Event
    {
        public 空中之咖啡室輪舵()
        {
            this.EventID = 11001018;
        }

        public override void OnEvent(ActorPC pc)
        {
            switch (Select(pc, "要回去了吗？", "", "回阿克罗尼亚东部平原", "回阿克罗尼亚西部平原", "回阿克罗尼亚南部平原", "回阿克罗尼亚北部平原", "不了"))
           {
               case 1:
               Warp(pc, 10025000, 117, 131);
               break;

               case 2:
               Warp(pc, 10022000, 143, 122);
               break;

               case 3:
               Warp(pc, 10031000, 123, 131);
               break;

               case 4:
               Warp(pc, 10014000, 133, 137);
               break;
           }
        }
    }
}