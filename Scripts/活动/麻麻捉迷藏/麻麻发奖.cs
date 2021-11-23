
using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaDB.Item;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
using WeeklyExploration;
using System.Globalization;
namespace SagaScript.M30210000
{
    public class S2555 : Event
    {
        public S2555()
        {
            this.EventID = 2555;
        }

        public override void OnEvent(ActorPC pc)
        {
            switch (Select(pc, "麻麻发奖", "", "发", "退出"))
            {
                case 1:
                    foreach (var item in SagaMap.Manager.MapClientManager.Instance.OnlinePlayer)
                    {
                        if (item.Character.CharID == 943 || item.Character.CharID == 838 || item.Character.CharID == 786 || item.Character.CharID == 976 || item.Character.CharID == 972 || item.Character.CharID == 689 || item.Character.CharID == 704 || item.Character.CharID == 738 || item.Character.CharID == 687 && item.Character.AInt["麻麻捉迷藏参与奖"] == 0)
                        {
                        }
                    }
                    break;
                case 2:
                    break;
            }
        }
    }
}