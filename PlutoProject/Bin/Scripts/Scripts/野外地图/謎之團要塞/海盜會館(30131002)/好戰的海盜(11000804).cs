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
    public class S11000804 : Event
    {
        public S11000804()
        {
            this.EventID = 11000804;
        }

        public override void OnEvent(ActorPC pc)
        {
            if (pc.Inventory.Equipments.ContainsKey(EnumEquipSlot.HEAD))
            {
                if (pc.Inventory.Equipments[EnumEquipSlot.HEAD].ItemID == 50025000 ||
                    pc.Inventory.Equipments[EnumEquipSlot.HEAD].ItemID == 50026800)
                {
                    Say(pc, 331, "啊，谜之团!$R;" +
                        "真是一群讨厌的傢伙！$R;" +
                        "$R咩~$R;");
                    return;
                }
            }
            Say(pc, 341, "扔的那么轻的话可不行$R;" +
                "来，给你来个狠的瞧瞧!$R;");
            Wait(pc, 0);
            Say(pc, 11000805, 362, "遭暗算了！！$R;");
        }
    }
}