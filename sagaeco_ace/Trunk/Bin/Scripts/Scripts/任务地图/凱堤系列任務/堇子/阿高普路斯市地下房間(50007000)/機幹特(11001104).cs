using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaDB.Item;
using SagaMap.Scripting;

using SagaLib;
using SagaScript.Chinese.Enums;
namespace SagaScript.M50007000
{
    public class S11001104 : Event
    {
        public S11001104()
        {
            this.EventID = 11001104;
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
                    Say(pc, 0, 111, "什么？不就是光之塔的机器人吗？$R;", "猫灵（山吹）");
                }
            }
            if (CountItem(pc, 10017905) >= 1)
            {
                Say(pc, 0, 111, "什么？不就是光之塔的机器人吗？$R;", "猫灵（山吹）");
            }
            if (pc.Inventory.Equipments.ContainsKey(EnumEquipSlot.PET))
            {
                if (pc.Inventory.Equipments[EnumEquipSlot.PET].ItemID == 10017900)
                {
                    Say(pc, 0, 111, "阿克罗波利斯下城里$R为什么会有这样的东西呀？……$R;", "猫灵（桃子）");
                }
            }
            if (CountItem(pc, 10017900) >= 1)
            {
                Say(pc, 0, 111, "阿克罗波利斯下城里$R为什么会有这样的东西呀？……$R;", "猫灵（桃子）");
            }
            if (pc.Inventory.Equipments.ContainsKey(EnumEquipSlot.PET))
            {
                if (pc.Inventory.Equipments[EnumEquipSlot.PET].ItemID == 10017902)
                {
                    Say(pc, 0, 111, "……$R;", "猫灵（绿）");
                }
            }
            if (CountItem(pc, 10017902) >= 1)
            {
                Say(pc, 0, 111, "……$R;", "猫灵（绿）");
            }
            if (pc.Inventory.Equipments.ContainsKey(EnumEquipSlot.PET))
            {
                if (pc.Inventory.Equipments[EnumEquipSlot.PET].ItemID == 10017903)
                {
                    Say(pc, 0, 111, "不知为何有点恐怖呀…….$R;", "猫灵（蓝子）");
                    return;
                }
            }
            if (CountItem(pc, 10017903) >= 1)
            {
                Say(pc, 0, 111, "不知为何有点恐怖呀…….$R;", "猫灵（蓝子）");
                return;
            }
        }
    }
}