using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

namespace SagaScript
{
    public class 石像 : Event
    {
        public 石像()
        {
            this.EventID = 0xFFFFFF33;
        }

        public override void OnEvent(ActorPC pc)
        {
            string job = "";
            switch (pc.Golem.GolemType)
            {
                case GolemType.Sell:
                    job = "看店";
                    break;
                case GolemType.Buy:
                    job = "購買";
                    break;
                case GolemType.Plant:
                    job = "收集[  植物  ]";
                    break;
                case GolemType.Mineral:
                    job = "收集[  礦物  ]";
                    break;
                case GolemType.Food:
                    job = "收集[  食物  ]";
                    break;
                case GolemType.Magic:
                    job = "收集[  魔法物  ]";
                    break;
                case GolemType.TreasureBox:
                    job = "收集[  寶物箱  ]";
                    break;
                case GolemType.Excavation:
                    job = "收集[  出土品  ]";
                    break;
                case GolemType.Any:
                    job = "收集[  各種東西  ]";
                    break;
                case GolemType.Strange:
                    job = "收集[  不知名的東西  ]";
                    break;
                case GolemType.None:
                    job = "停止";
                    break;
            }
            switch (Select(pc, "選擇" + pc.Golem.Item.BaseData.name + "的任務", "",
                "停止 : 使[  停止  ]",
                "看守商店 : 使[ 看店 ]",
                "購買 : 使[  購買  ]",
                "收集 : 收集[  植物  ]",
                "收集 : 收集[  礦物  ]",
                "收集 : 收集[  食物  ]",
                "收集 : 收集[  魔法物  ]",
                "收集 : 收集[  寶物箱  ]",
                "收集 : 收集[  出土品  ]",
                "收集 : 收集[  各種東西  ]",
                "收集 : 收集[  不知名的東西  ]",
                "之前的設定[ " + job +" ]"))
            {
                case 1:
                    SetGolemType(pc, GolemType.None);
                    break;
                case 2:
                    SetGolemType(pc, GolemType.Sell);
                    break;
                case 3:
                    SetGolemType(pc, GolemType.Buy);
                    break;
                case 4:
                    SetGolemType(pc, GolemType.Plant);
                    break;
                case 5:
                    SetGolemType(pc, GolemType.Mineral);
                    break;
                case 6:
                    SetGolemType(pc, GolemType.Food);
                    break;
                case 7:
                    SetGolemType(pc, GolemType.Magic);
                    break;
                case 8:
                    SetGolemType(pc, GolemType.TreasureBox);
                    break;
                case 9:
                    SetGolemType(pc, GolemType.Excavation);
                    break;
                case 10:
                    SetGolemType(pc, GolemType.Any);
                    break;
                case 11:
                    SetGolemType(pc, GolemType.Strange);
                    break;
                case 12:
                    SetGolemType(pc, pc.Golem.GolemType);
                    break;
            }
        }
    }
}
