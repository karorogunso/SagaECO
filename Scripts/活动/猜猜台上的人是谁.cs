
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
    public class S66666667 : Event
    {
        public S66666667()
        {
            this.EventID = 66666667;
        }

        public override void OnEvent(ActorPC pc)
        {
            if (Select(pc, "是否发送全服奖励？", "", "是的", "不发") == 1)
            {
                foreach (var item in SagaMap.Manager.MapClientManager.Instance.OnlinePlayer)
                {
                    if (item.Character.MapID == 91000999)
                    {
                        Say(item.Character, 131, "感谢您参与花魁大赛第二届，$R这是您参与本次活动的奖励。");
                        GiveItem(item.Character, 950000010, 2000);
                        GiveItem(item.Character, 910000040, 10);
                        GiveItem(item.Character, 910000036, 3);
                    }
                }
            }
        }
    }
}

