
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
    public class S2666 : Event
    {
        public S2666()
        {
            this.EventID = 2666;
        }

        public override void OnEvent(ActorPC pc)
        {
            switch (Select(pc, "麻麻想做啥？！憋过来", "", "把所有报名者都拉到地图里", "把参赛者打晕拖回鱼缸岛", "退出"))
            {
                case 1:
                    foreach (var item in SagaMap.Manager.MapClientManager.Instance.OnlinePlayer)
                    {
                        if (item.Character.AInt["第一届哭叽叽躲猫猫"] == 1)
                        {
                            Warp(item.Character, 10034000, 4, 211);
                        }
                    }
                    break;
                case 2:
                    foreach (var item in SagaMap.Manager.MapClientManager.Instance.OnlinePlayer)
                    {
                        if (item.Character.AInt["第一届哭叽叽躲猫猫"] == 1)
                        {
                            Warp(item.Character, 10054000, 157, 145);
                        }
                    }
                    break;
                case 3:
                    break;
            }
        }
    }
}