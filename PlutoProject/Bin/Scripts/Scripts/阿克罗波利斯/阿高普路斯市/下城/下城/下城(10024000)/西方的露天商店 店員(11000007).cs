using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
using SagaScript.Chinese.Enums;

using SagaDB.Item;
//所在地圖:下城(10024000) NPC基本信息:西方的露天商店 店員(11000007) X:117 Y:127
namespace SagaScript.M10024000
{
    public class S11000007 : Event
    {
        public S11000007()
        {
            this.EventID = 11000007;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<Halloween_00> Halloween_00_mask = pc.CMask["Halloween_00"];                                                                                         //活動：萬聖節

            Halloween_00_mask.SetValue(Halloween_00.萬聖節活動期間, false);                                                                                             //萬聖節活動期間                開/關

            if (Halloween_00_mask.Test(Halloween_00.萬聖節活動期間))
            {
                萬聖節(pc);
            }

            switch (Select(pc, "过来看看吧! 想做什么啊?", "", "买东西", "卖东西", "什么也不做"))
            {
                case 1:
                    OpenShopBuy(pc, 5);
                    break;

                case 2:
                    OpenShopSell(pc, 5);
                    break;

                case 3:
                    break;
            }

            Say(pc, 11000007, 131, "下次還要再來啊!!$R;", "西方的露天商店 店員");
        }

        void 萬聖節(ActorPC pc)
        {
            if (pc.Inventory.Equipments.ContainsKey(EnumEquipSlot.HEAD))
            {
                if (pc.Inventory.Equipments[EnumEquipSlot.HEAD].ItemID == 50025800 ||
                    pc.Inventory.Equipments[EnumEquipSlot.HEAD].ItemID == 50024350 ||
                    pc.Inventory.Equipments[EnumEquipSlot.HEAD].ItemID == 50024351 ||
                    pc.Inventory.Equipments[EnumEquipSlot.HEAD].ItemID == 50024352 ||
                    pc.Inventory.Equipments[EnumEquipSlot.HEAD].ItemID == 50024353 ||
                    pc.Inventory.Equipments[EnumEquipSlot.HEAD].ItemID == 50024354 ||
                    pc.Inventory.Equipments[EnumEquipSlot.HEAD].ItemID == 50024355 ||
                    pc.Inventory.Equipments[EnumEquipSlot.HEAD].ItemID == 50024356 ||
                    pc.Inventory.Equipments[EnumEquipSlot.HEAD].ItemID == 50024357 ||
                    pc.Inventory.Equipments[EnumEquipSlot.HEAD].ItemID == 50024358 ||
                    pc.Inventory.Equipments[EnumEquipSlot.HEAD].ItemID == 50022500 ||
                    pc.Inventory.Equipments[EnumEquipSlot.HEAD].ItemID == 50022600 ||
                    pc.Inventory.Equipments[EnumEquipSlot.HEAD].ItemID == 50022700 ||
                    pc.Inventory.Equipments[EnumEquipSlot.HEAD].ItemID == 50022800)
                {
                    Say(pc, 11000007, 131, "鬼怪世界的生活是怎么样的?$R;", "西方的露天商店 店员");
                }
            }
        }
    }
}