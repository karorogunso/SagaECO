using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaDB.Item;
using SagaMap.Scripting;

using SagaLib;
using SagaScript.Chinese.Enums;
namespace SagaScript.M50008000
{
    public class S11001110 : Event
    {
        public S11001110()
        {
            this.EventID = 11001110;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<Neko_03> Neko_03_cmask = pc.CMask["Neko_03"];
            Say(pc, 111, "……$R;");



            if (Neko_03_cmask.Test(Neko_03.發現機器人))
            {
                return;
            }
            Neko_03_cmask.SetValue(Neko_03.發現機器人, true);

            if (pc.Inventory.Equipments.ContainsKey(EnumEquipSlot.PET))
            {
                if (pc.Inventory.Equipments[EnumEquipSlot.PET].ItemID == 10017905)
                {
                    Say(pc, 0, 111, "什麼？不就是光之塔的機械人嗎？$R;", "凱堤（山吹）");
                }
            }
            if (CountItem(pc, 10017905) >= 1)
            {
                Say(pc, 0, 111, "什麼？不就是光之塔的機械人嗎？$R;", "凱堤（山吹）");
            }
            if (pc.Inventory.Equipments.ContainsKey(EnumEquipSlot.PET))
            {
                if (pc.Inventory.Equipments[EnumEquipSlot.PET].ItemID == 10017900)
                {
                    Say(pc, 0, 111, "阿高普路斯下城裡$R為什麼會有這樣的東西呀？……$R;", "凱堤（桃）");
                }
            }
            if (CountItem(pc, 10017900) >= 1)
            {
                Say(pc, 0, 111, "阿高普路斯下城裡$R為什麼會有這樣的東西呀？……$R;", "凱堤（桃）");
            }
            if (pc.Inventory.Equipments.ContainsKey(EnumEquipSlot.PET))
            {
                if (pc.Inventory.Equipments[EnumEquipSlot.PET].ItemID == 10017902)
                {
                    Say(pc, 0, 111, "……$R;", "凱堤（緑）");
                }
            }
            if (CountItem(pc, 10017902) >= 1)
            {
                Say(pc, 0, 111, "……$R;", "凱堤（緑）");
            }
            if (pc.Inventory.Equipments.ContainsKey(EnumEquipSlot.PET))
            {
                if (pc.Inventory.Equipments[EnumEquipSlot.PET].ItemID == 10017903)
                {
                    Say(pc, 0, 111, "不知為何有點恐怖呀…….$R;", "凱堤（藍）");
                    return;
                }
            }
            if (CountItem(pc, 10017903) >= 1)
            {
                Say(pc, 0, 111, "不知為何有點恐怖呀…….$R;", "凱堤（藍）");
                return;
            }
        }
    }
}