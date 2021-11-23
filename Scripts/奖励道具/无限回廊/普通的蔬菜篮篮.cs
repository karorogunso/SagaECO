
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
    public class S910000053 : Event
    {
        public S910000053()
        {
            this.EventID = 910000053;
        }

        public override void OnEvent(ActorPC pc)
        {
            if (CountItem(pc, 910000053) >= 1)
            {
                TakeItem(pc, 910000053, 1);
                奖励(pc);
            }
        }
        void 奖励(ActorPC pc)
        {
            List<uint> list = getitems();
            uint id = list[Global.Random.Next(0, list.Count - 1)];
            GiveItem(pc, id, 1);
        }
        List<uint> getitems()
        {
            List<uint> ids = new List<uint>();
            ids.Add(10007700);
            ids.Add(10001800);
            ids.Add(10099600);
            ids.Add(10095300);
            ids.Add(10007900);
            ids.Add(10004100);
            ids.Add(10006400);
            ids.Add(10004000);
            ids.Add(10007003);
            ids.Add(10122300);
            ids.Add(10095700);
            return ids;
        }
    }
}

