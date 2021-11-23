using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M20081000
{
    public class S11001338 : Event
    {
        public S11001338()
        {
            this.EventID = 11001338;
        }

        public override void OnEvent(ActorPC pc)
        {
           if(Select(pc, "有什么事么", "", "帮我打开寶物箱15", "没事")==1)
            {
                if (CountItem(pc, 10022115) > 0)
                {
                    GiveRandomTreasure(pc, "TREASURE_BOX15");
                    TakeItem(pc, 10022115, 1);
                    return;
                }
                Say(pc, 190, "你手上没有寶物箱15$R;");
            }
        }
    }
}
    
