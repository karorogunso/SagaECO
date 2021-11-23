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
    public partial class PVPTEST2 : Event
    {
        public PVPTEST2()
        {
            this.EventID = 500150033;
        }
        public override void OnEvent(ActorPC pc)
        {
            if (pc.Account.GMLevel >= 250)
            {
                switch (Select(pc, "【管理员模式】朋朋控制", "", "无效果", "传送所有玩家回城", "让所有玩家得到朋朋战利品", "离开"))
                {
                    case 1:
                        return;
                    case 2:
                        foreach (var item in SagaMap.Manager.MapClientManager.Instance.OnlinePlayer)
                        {
                            Warp(item.Character, 91000999, 1, 1);
                        }
                        break;
                    case 3:
                        foreach (var item in SagaMap.Manager.MapClientManager.Instance.OnlinePlayer)
                        {
                            GiveItem(item.Character, 910000041, 1);
                            Say(pc, 131, "得到了朋朋战利品");
                        }
                        return;
                    case 4:
                        break;
                }
            }
        }
    }
}

