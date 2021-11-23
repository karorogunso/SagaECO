using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
using SagaDB.Item;
//所在地圖:裁縫阿姨的家(30020000) NPC基本信息:熊熊(11000357) X:0 Y:2
namespace SagaScript.M30020000
{
    public class S11000357 : Event
    {
        public S11000357()
        {
            this.EventID = 11000357;
        }

        public override void OnEvent(ActorPC pc)
        {
            PetShow(pc);
        }

        uint[] itemID = { 20050130, 20050131, 20050134, 20050136, 20050138, 20050141, 20050142, 20050143, 20050144, 20050145, 20050146, 20050147 };
        uint[] petPictID = { 10260056, 14370001, 14240001, 10260057, 10260058, 10260059, 10260060, 10260061, 10260062, 10260063, 10260064, 10260065 };

        void PetShow(ActorPC pc)
        {
            if (pc.Inventory.Equipments[EnumEquipSlot.PET] != null)
            {
                if (pc.Inventory.Equipments[EnumEquipSlot.PET].BaseData.itemType == ItemType.PET_NEKOMATA)
                {
                    if (pc.Inventory.Equipments[EnumEquipSlot.PET].PictID != 0)
                    {
                        Say(pc, 131, "おっ！$R;" +
                            "ネコマタ着せ替えたんだねー！$R;" +
                            "似合ってるね♪$R;" +
                            "$Pネコマタの服を元に$R;" +
                            "戻しにきたのかな？$R;", "くまちゃん");
                        switch (Select(pc, "見た目を元に戻しますか？", "", "はい", "いいえ"))
                        {
                            case 1:
                                for (int i = 0; i < 12; i++)
                                {
                                    if (pc.Inventory.Equipments[EnumEquipSlot.PET].PictID != petPictID[i])
                                        continue;
                                    GiveItem(pc, itemID[i], 1);
                                    PetShowReplace(pc, 0);
                                    break;
                                }
                                break;
                            case 2:
                                break;
                        }
                    }
                }
            }
        }
    }
}
