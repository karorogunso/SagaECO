using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaDB.Item;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
using SagaMap.Network.Client;
namespace SagaScript.M30210000
{
    public class S910000084 : Event
    {
        public S910000084()
        {
            this.EventID = 910000084;
        }

        public override void OnEvent(ActorPC pc)
        {
            if (CountItem(pc, 910000084) > 0)
            {
                if (pc.Level >= 45)
                {
                    Say(pc, 0, "45级以上就无法使用经验券啦。");
                    return;
                 }
                //给使用者经验 80000 base  80000jexp
                TakeItem(pc, 910000084, 1);
                SagaMap.Manager.ExperienceManager.Instance.ApplyExp(pc, 80000, 80000, 1f);
                MapClient.FromActorPC(pc).SendEXP();
            }
        }
    }
}