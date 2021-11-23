
using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaDB.Item;
using SagaMap;
using SagaMap.Skill;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M30210000
{
    public class S9100000301 : Event
    {
        public S9100000301()
        {
            this.EventID = 910000555;
        }

        public override void OnEvent(ActorPC pc)
        {
            foreach (var item in SagaMap.Manager.MapClientManager.Instance.OnlinePlayer)
            {
                Say(item.Character, 131, "拿到了被光头抢走的战利品","");
                GiveItem(item.Character, 910000027, 1);

            }

        }
    }
}

