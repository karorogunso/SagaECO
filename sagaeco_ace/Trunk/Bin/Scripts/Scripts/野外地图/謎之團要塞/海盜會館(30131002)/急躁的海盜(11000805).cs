using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Item;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M30131002
{
    public class S11000805 : Event
    {
        public S11000805()
        {
            this.EventID = 11000805;
        }

        public override void OnEvent(ActorPC pc)
        {
            if (pc.Inventory.Equipments.ContainsKey(EnumEquipSlot.HEAD))
            {
                if (pc.Inventory.Equipments[EnumEquipSlot.HEAD].ItemID == 50025000 ||
                    pc.Inventory.Equipments[EnumEquipSlot.HEAD].ItemID == 50026800)
                {
                    Say(pc, 255, "谜之团，回去吧！$R;" +
                        "这里是我们的地盘！$R;");
                    return;
                }
            }
            int a = Global.Random.Next(1, 2);
            if (a == 1)
            {
                Say(pc, 255, "打倒谜之团！$R;" +
                    "是打我们岛主意的坏家伙！$R;");
                return;
            }
            Say(pc, 255, "跟谜之团的战事$R;" +
                "只准胜利!不准失败!$R;");
        }
    }
}