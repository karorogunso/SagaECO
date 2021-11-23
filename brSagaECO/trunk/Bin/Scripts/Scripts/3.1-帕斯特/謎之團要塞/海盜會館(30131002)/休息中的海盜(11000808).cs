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
    public class S11000808 : Event
    {
        public S11000808()
        {
            this.EventID = 11000808;
        }

        public override void OnEvent(ActorPC pc)
        {
            if (pc.Inventory.Equipments.ContainsKey(EnumEquipSlot.HEAD))
            {
                if (pc.Inventory.Equipments[EnumEquipSlot.HEAD].ItemID == 50025000 ||
                    pc.Inventory.Equipments[EnumEquipSlot.HEAD].ItemID == 50026800)
                {
                    Say(pc, 255, "謎語團，在幹什麼呢？$R;" +
                        "$R出去！出去！$R;" +
                        "出去！出去！出去！$R;");
                    return;
                }
            }
            Say(pc, 255, "這附近的島是我們的！$R;" +
                "換句話說，上空也是我們的，$R須得許可才可以使用飛空庭！$R;");
        }
    }
}