using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaDB.Item;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M10062000
{
    public class S11001055 : Event
    {
        public S11001055()
        {
            this.EventID = 11001055;
        }

        public override void OnEvent(ActorPC pc)
        {
            /*
            if (_5a84)
            {
                Say(pc, 131, "那麼，再見~$R;");
                return;
            }
            if (pc.Inventory.Equipments.ContainsKey(EnumEquipSlot.PET))
            {
                if (pc.Inventory.Equipments[EnumEquipSlot.PET].ItemID == 10048900 ||
                    pc.Inventory.Equipments[EnumEquipSlot.PET].ItemID == 10048901 ||
                    pc.Inventory.Equipments[EnumEquipSlot.PET].ItemID == 10048902 ||
                    pc.Inventory.Equipments[EnumEquipSlot.PET].ItemID == 10048950 ||
                    pc.Inventory.Equipments[EnumEquipSlot.PET].ItemID == 10048951 ||
                    pc.Inventory.Equipments[EnumEquipSlot.PET].ItemID == 10048952 ||
                    pc.Inventory.Equipments[EnumEquipSlot.PET].ItemID == 10006650 ||
                    pc.Inventory.Equipments[EnumEquipSlot.PET].ItemID == 10006651)
                {
                    Say(pc, 131, "嗚哇~~$R;" +
                        "狗阿！這裡有狗！！$R;" +
                        "$R哇，那條狗很可怕，$R;" +
                        "$P好可怕啊$R;" +
                        "$R讓您乘飛空庭，$R;" +
                        "快到別的地方去吧。求您了。$R;");
                    switch (Select(pc, "怎麼辦呢?", "", "什麼也不做", "去馬克特碼頭"))
                    {
                        case 1:
                            break;
                        case 2:
                            _5a84 = true;
                            Say(pc, 131, "那麼，再見~$R;");
                            break;
                    }
                }
            }
            switch (Select(pc, "歡迎來到唐卡機場！", "", "什麼也不做", "辦理搭乘手續"))
            {
                case 1:
                    break;
                case 2:
                    if (pc.Skills2.ContainsKey(706) || pc.SkillsReserve.ContainsKey(706))
                    {
                        Say(pc, 131, "哎呀，$R;" +
                            "您不能收錢。$R;");
                        return;
                    }
                    switch (Select(pc, "支付機場費用100金幣嗎？", "", "什麼也不做", "付100金幣"))
                    {
                        case 1:
                            break;
                        case 2:
                            if (pc.Gold > 99)
                            {
                                pc.Gold -= 100;
                                PlaySound(pc, 2030, false, 100, 50);
                                Say(pc, 131, "付了100金$R;");
                                _5a84 = true;
                                Say(pc, 131, "那麼，再見~$R;");
                                return;
                            }
                            Say(pc, 131, "錢不夠呀$R;");
                            break;
                    }
                    break;
            }
            */
        }
    }
}