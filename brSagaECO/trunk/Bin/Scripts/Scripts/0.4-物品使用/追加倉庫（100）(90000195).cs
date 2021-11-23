using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;

//アイテム名：追加倉庫（100）　アイテムID:10069710
namespace SagaScript
{
    public class S90000196 : Event
    {
        public S90000196()
        {
            this.EventID = 90000196;
        }

        public override void OnEvent(ActorPC pc)
        {
            int i = 1;
            while (i <= 100)
            {
                GiveRandomTreasure(pc, "TREASURE_BOX15");
                i++;
            }
        }
    }
}