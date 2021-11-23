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
                    Say(pc, 131, "同志，歡迎您光臨$R;" +
                        "快點進去吧！$R;");
                    Warp(pc, 10054000, x, y);
                    return;
                }
            }
            if (CountItem(pc, 10048001) > 0)
            {
                Say(pc, 131, "這是『謎語團的柏斯卡』啊！$R;" +
                    "好，進去也好……$R;");
                switch (Select(pc, "怎麼辦呢？", "", "進入要塞", "不進去"))
                {
                    case 1:
                        TakeItem(pc, 10048001, 1);
                        Warp(pc, 10054000, x, y);
                        break;
                }
                return;
            }

            Say(pc, 131, "這裡開始，閒人勿進$R;" +
                "$R如要進入，請出示許可証$R;");

        }
    }
}