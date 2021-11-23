using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Item;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M10054001
{
    public class S11000906 : Event
    {
        public S11000906()
        {
            this.EventID = 11000906;
        }

        public override void OnEvent(ActorPC pc)
        {
            byte x, y;
            x = (byte)Global.Random.Next(153, 154);
            y = (byte)Global.Random.Next(159, 160);
            /*
            if (a//ME.POS_Y < 163
            )
            {
                Say(pc, 131, "不要妨礙監視。$R;");
                return;
            }
            */
            if (pc.Account.GMLevel >= 100)
            {
                Warp(pc, 10054000, x, y);
                return;
            }

            if (pc.Inventory.Equipments.ContainsKey(EnumEquipSlot.HEAD))
            {
                if (pc.Inventory.Equipments[EnumEquipSlot.HEAD].ItemID == 50025000 ||
                    pc.Inventory.Equipments[EnumEquipSlot.HEAD].ItemID == 50026800)
                {
                    Say(pc, 131, "同志，欢迎您光临$R;" +
                        "快点进去吧！$R;");
                    Warp(pc, 10054000, x, y);
                    return;
                }
            }
            if (CountItem(pc, 10048001) > 0)
            {
                Say(pc, 131, "这是『谜之团通行证』啊！$R;" +
                    "好，进去吧……$R;");
                switch (Select(pc, "怎么办呢？", "", "进入要塞", "不进去"))
                {
                    case 1:
                        TakeItem(pc, 10048001, 1);
                        Warp(pc, 10054000, x, y);
                        break;
                }
                return;
            }

            Say(pc, 131, "这里开始，闲人勿进$R;" +
                "$R如要进入，请出示许可证$R;");

        }
    }
}