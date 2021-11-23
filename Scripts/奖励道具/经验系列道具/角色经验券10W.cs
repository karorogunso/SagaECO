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
    public class S910000085 : Event
    {
        public S910000085()
        {
            this.EventID = 910000085;
        }

        public override void OnEvent(ActorPC pc)
        {
            if (CountItem(pc, 910000085) > 0)
            {
                if (pc.Level >= 45)
                {
                    Say(pc, 0, "45级以上就无法使用经验券啦。");
                    return;
                 }
                //给使用者经验 100000 base  100000jexp
                TakeItem(pc, 910000085, 1);
                SagaMap.Manager.ExperienceManager.Instance.ApplyExp(pc, 100000, 100000, 1f);
                MapClient.FromActorPC(pc).SendEXP();
            }
        }
    }
}