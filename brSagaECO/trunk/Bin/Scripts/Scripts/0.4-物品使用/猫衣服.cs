using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
using SagaScript.Chinese.Enums;
using SagaDB.Item;

namespace SagaScript
{
    public class ネコマタ用 : SagaMap.Scripting.Item
    {
        public ネコマタ用()
        {
            //ネコマタ（杏）用セーラー
            Init(90000339, delegate(ActorPC pc)
            {
                if (PetShow(pc, 10017908, 10260056))
                    TakeItem(pc, 20050130, 1);
            });
            //ネコマタ（黒）用騎士服
            Init(90000343, delegate(ActorPC pc)
            {

                if (PetShow(pc, 10017914, 14370001))
                    TakeItem(pc, 20050131, 1);
            });
            //ネコマタ（黒）用巫女服
            Init(90000343, delegate(ActorPC pc)
            {

                if (PetShow(pc, 10017914, 14370002))
                    TakeItem(pc, 20050131, 1);
            });
            //ネコマタ（胡桃・若菜）用水着
            Init(90000357, delegate(ActorPC pc)
            {

                if (PetShow(pc, 10017912, 14240001))
                    TakeItem(pc, 20050134, 1);
            });

            //ネコマタ（胡桃・若菜）メイド
            Init(90000357, delegate(ActorPC pc)
            {

                if (PetShow(pc, 10017912, 14240002))
                    TakeItem(pc, 20050134, 1);
            });
            //ネコマタ（藍）用羽衣
            Init(90000362, delegate(ActorPC pc)
            {

                if (PetShow(pc, 10017903, 10260057))
                    TakeItem(pc, 20050136, 1);
            });
            //ネコマタ（桃）キティラー服
            Init(90000367, delegate(ActorPC pc)
            {

                if (PetShow(pc, 10017900, 10260058))
                    TakeItem(pc, 20050138, 1);
            });
            //ネコマタ（緑）用カントリー服
            Init(90000390, delegate(ActorPC pc)
            {

                if (PetShow(pc, 10017918, 10260059))
                    TakeItem(pc, 20050141, 1);
            });
            //ネコマタ（白）用トップモデル服
            Init(90000391, delegate(ActorPC pc)
            {

                if (PetShow(pc, 10017923, 10260060))
                    TakeItem(pc, 20050142, 1);
            });
            //ネコマタ（山吹）用和装
            Init(90000403, delegate(ActorPC pc)
            {

                if (PetShow(pc, 10017905, 10260061))
                    TakeItem(pc, 20050143, 1);
            });
            //ネコマタ（茜）用ハロウィン服
            Init(90000404, delegate(ActorPC pc)
            {

                if (PetShow(pc, 10017907, 10260062))
                    TakeItem(pc, 20050144, 1);
            });
            //ネコマタ（空）用怪盗服
            Init(90000410, delegate(ActorPC pc)
            {

                if (PetShow(pc, 10017910, 10260063))
                    TakeItem(pc, 20050145, 1);
            });
            //ネコマタ（菫）用おめかしワンピ
            Init(90000418, delegate(ActorPC pc)
            {

                if (PetShow(pc, 10017906, 10260064))
                    TakeItem(pc, 20050146, 1);
            });
            //ネコマタ（桃）用おでかけパーカー
            Init(90000424, delegate(ActorPC pc)
            {

                if (PetShow(pc, 10017900, 10260065))
                    TakeItem(pc, 20050147, 1);
            });


        }

        bool PetShow(ActorPC pc, uint ItemID, uint pictID)
        {
            if (pc.Inventory.Equipments[EnumEquipSlot.PET] != null)
            {
                if (pc.Inventory.Equipments[EnumEquipSlot.PET].ItemID == ItemID)
                {
                    if (pc.Inventory.Equipments[EnumEquipSlot.PET].PictID == 0)
                    {
                        PetShowReplace(pc, pictID);
                        Say(pc, 0, 0, pc.Inventory.Equipments[EnumEquipSlot.PET].BaseData.ToString() + "を装備していないと$R;" +
                        "使用することができません。$R;", "");
                        return true;

                    }
                    else
                    {
                        Say(pc, 131, "该猫灵已经装备过道具了$R;");
                    }
                }
                else
                {
                    Say(pc, 131, "该猫灵无法装备该道具$R;");
                }
            }
            else
            {
                Say(pc, 131, "未装备猫灵$R;");
            }
            return false;
        }
    }
}