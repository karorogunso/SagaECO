using System;
using System.Collections.Generic;
using System.Text;
using SagaDB.Item;
using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
namespace SagaScript.M12058000
{
    public class S10001627 : Event
    {
        public S10001627()
        {
            this.EventID = 10001627;
        }

        public override void OnEvent(ActorPC pc)
        {

            Say(pc, 0, 0, "这是去「军舰岛」的飞空艇。$R;" +
            "$R前面是$CO可以ＰＶＰ的区域$CD。$R;" +
            "$R$COＬＶ５以上$CD可以ＰＶＰ。$R;" +
            "注意下周围小心点。$R;", "飛空庭係員");
            switch (Select(pc, "乘坐飞空艇去军舰岛", "", "还不想乘坐", "乘坐飞空艇（100G）"))
            {
                case 2:
                    if (pc.Gold >= 100)
                    {
                        pc.Gold -= 100;
                        PlaySound(pc, 2040, false, 100, 50);
                        Say(pc, 0, 0, "支付了100G！$R;", "");

                        Say(pc, 0, 0, "……摇摆不定。$R;" +
                        "$R不要掉下去了？$R;", "飛空庭係員");
                        PlaySound(pc, 2426, false, 100, 50);
                        Say(pc, 0, 0, "……出发去军舰岛！$R;", "飛空庭係員");
                        PlaySound(pc, 2438, false, 100, 50);
                        ShowEffect(pc, 128, 252, 8066);
                        Wait(pc, 1980);
                        Warp(pc, 12035000, 48, 159);
                        if (pc.Inventory.Equipments.ContainsKey(EnumEquipSlot.BACK))
                        {
                            if (pc.Inventory.Equipments[EnumEquipSlot.BACK].BaseData.possibleLv > pc.DominionLevel)
                                RemoveEquipment(pc, EnumEquipSlot.BACK);
                        }
                        if (pc.Inventory.Equipments.ContainsKey(EnumEquipSlot.CHEST_ACCE))
                        {
                            if (pc.Inventory.Equipments[EnumEquipSlot.CHEST_ACCE].BaseData.possibleLv > pc.DominionLevel)
                                RemoveEquipment(pc, EnumEquipSlot.CHEST_ACCE);
                        }
                        if (pc.Inventory.Equipments.ContainsKey(EnumEquipSlot.FACE))
                        {
                            if (pc.Inventory.Equipments[EnumEquipSlot.FACE].BaseData.possibleLv > pc.DominionLevel)
                                RemoveEquipment(pc, EnumEquipSlot.FACE);
                        }
                        if (pc.Inventory.Equipments.ContainsKey(EnumEquipSlot.FACE_ACCE))
                        {
                            if (pc.Inventory.Equipments[EnumEquipSlot.FACE_ACCE].BaseData.possibleLv > pc.DominionLevel)
                                RemoveEquipment(pc, EnumEquipSlot.FACE_ACCE);
                        }
                        if (pc.Inventory.Equipments.ContainsKey(EnumEquipSlot.HEAD))
                        {
                            if (pc.Inventory.Equipments[EnumEquipSlot.HEAD].BaseData.possibleLv > pc.DominionLevel)
                                RemoveEquipment(pc, EnumEquipSlot.HEAD);
                        }
                        if (pc.Inventory.Equipments.ContainsKey(EnumEquipSlot.HEAD_ACCE))
                        {
                            if (pc.Inventory.Equipments[EnumEquipSlot.HEAD_ACCE].BaseData.possibleLv > pc.DominionLevel)
                                RemoveEquipment(pc, EnumEquipSlot.HEAD_ACCE);
                        }
                        if (pc.Inventory.Equipments.ContainsKey(EnumEquipSlot.LEFT_HAND))
                        {
                            if (pc.Inventory.Equipments[EnumEquipSlot.LEFT_HAND].BaseData.possibleLv > pc.DominionLevel)
                                RemoveEquipment(pc, EnumEquipSlot.LEFT_HAND);
                        }
                        if (pc.Inventory.Equipments.ContainsKey(EnumEquipSlot.LOWER_BODY))
                        {
                            if (pc.Inventory.Equipments[EnumEquipSlot.LOWER_BODY].BaseData.possibleLv > pc.DominionLevel)
                                RemoveEquipment(pc, EnumEquipSlot.LOWER_BODY);
                        }
                        if (pc.Inventory.Equipments.ContainsKey(EnumEquipSlot.RIGHT_HAND))
                        {
                            if (pc.Inventory.Equipments[EnumEquipSlot.RIGHT_HAND].BaseData.possibleLv > pc.DominionLevel)
                                RemoveEquipment(pc, EnumEquipSlot.RIGHT_HAND);
                        }
                        if (pc.Inventory.Equipments.ContainsKey(EnumEquipSlot.SHOES))
                        {
                            if (pc.Inventory.Equipments[EnumEquipSlot.SHOES].BaseData.possibleLv > pc.DominionLevel)
                                RemoveEquipment(pc, EnumEquipSlot.SHOES);
                        }
                        if (pc.Inventory.Equipments.ContainsKey(EnumEquipSlot.SOCKS))
                        {
                            if (pc.Inventory.Equipments[EnumEquipSlot.SOCKS].BaseData.possibleLv > pc.DominionLevel)
                                RemoveEquipment(pc, EnumEquipSlot.SOCKS);
                        }
                        if (pc.Inventory.Equipments.ContainsKey(EnumEquipSlot.UPPER_BODY))
                        {
                            if (pc.Inventory.Equipments[EnumEquipSlot.UPPER_BODY].BaseData.possibleLv > pc.DominionLevel)
                                RemoveEquipment(pc, EnumEquipSlot.UPPER_BODY);
                        }
                    }
                    else
                    {
                        Say(pc, 10000023, 131, "什么…$R;" +
                                               "没钱吗！$R;" +
                                               "$R走开$R;", "飛空庭係員");
                    }
                    break;
            }

        }
    }


}


