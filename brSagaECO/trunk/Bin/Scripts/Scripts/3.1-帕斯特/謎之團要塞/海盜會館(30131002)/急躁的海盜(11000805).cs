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
                    Say(pc, 255, "謎語團，回去吧！$R;" +
                        "這裡是我們的地盤！$R;");
                    return;
                }
            }
            int a = Global.Random.Next(1, 2);
            if (a == 1)
            {
                Say(pc, 255, "打倒謎語團！$R;" +
                    "是打我們島主意的壞傢伙！$R;");
                return;
            }
            Say(pc, 255, "跟謎語團的戰事$R;" +
                "許勝不許敗!許勝不許敗!$R;");
        }
    }
}