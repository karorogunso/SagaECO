using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
using SagaScript.Chinese.Enums;

using SagaDB.Item;
//所在地圖:下城(10024000) NPC基本信息:東方的露天商店 店員(11000006) X:138 Y:128
namespace SagaScript.M10024000
{
    public class S11000006 : Event
    {
        public S11000006()
        {
            this.EventID = 11000006;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<Halloween_00> Halloween_00_mask = pc.CMask["Halloween_00"];                                                                                         //活動：萬聖節
            BitMask<Acropolisut_01> Acropolisut_01_mask = pc.CMask["Acropolisut_01"];                                                                                   //一般：阿高普路斯市

            Halloween_00_mask.SetValue(Halloween_00.萬聖節活動期間, false);                                                                                             //萬聖節活動期間                開/關

            if (Halloween_00_mask.Test(Halloween_00.萬聖節活動期間))
            {
                萬聖節(pc);
            }

            if (!Acropolisut_01_mask.Test(Acropolisut_01.已經與東方的露天商店店員進行第一次對話))
            {
                初次與東方的露天商店店員進行對話(pc);
            }

            switch (Select(pc, "過來看看吧! 想做什麼啊?", "", "買東西", "賣東西", "委託「木材加工」", "什麼也不做"))
            {
                case 1:
                    if (!Acropolisut_01_mask.Test(Acropolisut_01.已經與東方的露天商店店員進行第一次購買))
                    {
                        Acropolisut_01_mask.SetValue(Acropolisut_01.已經與東方的露天商店店員進行第一次購買, true);

                        Say(pc, 11000006, 131, "想用我們店裡的材料，$R;" +
                                               "試著做一個美味的『薄餅』試試嗎?$R;" +
                                               "$R這裡賣的都是特別高級的材料，$R;" +
                                               "跟專門店的『薄餅材料』$R;" +
                                               "是一模一樣的。$R;" +
                                               "$P買好『麵團』和『餡料』後，$R;" +
                                               "再放其他自己喜歡的材料，$R;" +
                                               "就完成屬於自己風格的『薄餅』了!$R;" +
                                               "$P『薄餅』的種類一共有5種。$R;" +
                                               "$R每種都好吃的不得了啊!!$R;", "東方的露天商店 店員");
                    }

                    OpenShopBuy(pc, 3);

                    Say(pc, 11000006, 131, "下次還要再來啊!!$R;", "東方的露天商店 店員");
                    break;

                case 2:
                    OpenShopSell(pc, 3);

                    Say(pc, 11000006, 131, "下次還要再來啊!!$R;", "東方的露天商店 店員");
                    break;

                case 3:
                    Synthese(pc, 2020, 3);
                    break;

                case 4:
                    Say(pc, 11000006, 131, "下次還要再來啊!!$R;", "東方的露天商店 店員");
                    break;
            }


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
                    Say(pc, 11000006, 131, "鬼怪客人也是客人!!$R;" +
                                           "快點過來看看啊~!!$R;", "東方的露天商店 店員");
                }
            }
        }

        void 初次與東方的露天商店店員進行對話(ActorPC pc)
        {
            BitMask<Acropolisut_01> Acropolisut_01_mask = pc.CMask["Acropolisut_01"];                                                                                   //一般：阿高普路斯市

            Acropolisut_01_mask.SetValue(Acropolisut_01.已經與東方的露天商店店員進行第一次對話, true); 

            Say(pc, 11000006, 131, "想在這裡進行「木材加工」?$R;" +
                                   "$R原來是要裝修飛空庭啊!$R;" +
                                   "$P如果是「木材加工」的話，$R;" +
                                   "我可是很有信心的!$R;", "東方的露天商店 店員");
        }
    }
}