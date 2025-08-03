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

            switch (Select(pc, "过来看看吧! 想做什么啊?", "", "买东西", "卖东西", "委托「木材加工」", "什么也不做"))
            {
                case 1:
                    if (!Acropolisut_01_mask.Test(Acropolisut_01.已經與東方的露天商店店員進行第一次購買))
                    {
                        Acropolisut_01_mask.SetValue(Acropolisut_01.已經與東方的露天商店店員進行第一次購買, true);

                        Say(pc, 11000006, 131, "想用我们店里的材料，$R;" +
                                               "试着做一个美味的『薄饼』试试吗?$R;" +
                                               "$R这里卖的都是特别高级的材料，$R;" +
                                               "跟专门店的『薄饼材料』$R;" +
                                               "是一模一样的。$R;" +
                                               "$P买好『面团』和『馅料』后，$R;" +
                                               "再放其他自己喜欢的材料，$R;" +
                                               "就完成属于自己风格的『薄饼』了!$R;" +
                                               "$P『薄饼』的种类一共有5种。$R;" +
                                               "$R每种都好吃的不得了啊!!$R;", "东方的露天商店 店员");
                    }

                    OpenShopBuy(pc, 3);

                    Say(pc, 11000006, 131, "下次还要再来啊!!$R;", "东方的露天商店 店员");
                    break;

                case 2:
                    OpenShopSell(pc, 3);

                    Say(pc, 11000006, 131, "下次还要再来啊!!$R;", "东方的露天商店 店员");
                    break;

                case 3:
                    Synthese(pc, 2020, 3);
                    break;

                case 4:
                    Say(pc, 11000006, 131, "下次还要再来啊!!$R;", "东方的露天商店 店员");
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
                                           "快点过来看看啊~!!$R;", "东方的露天商店 店员");
                }
            }
        }

        void 初次與東方的露天商店店員進行對話(ActorPC pc)
        {
            BitMask<Acropolisut_01> Acropolisut_01_mask = pc.CMask["Acropolisut_01"];                                                                                   //一般：阿高普路斯市

            Acropolisut_01_mask.SetValue(Acropolisut_01.已經與東方的露天商店店員進行第一次對話, true); 

            Say(pc, 11000006, 131, "想在这里进行「木材加工」?$R;" +
                                   "$R原来是要装修飞空庭啊!$R;" +
                                   "$P如果是「木材加工」的话，$R;" +
                                   "我可是很有信心的!$R;", "东方的露天商店 店员");
        }
    }
}