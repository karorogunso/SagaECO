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
    public class S11000806 : Event
    {
        public S11000806()
        {
            this.EventID = 11000806;
        }

        public override void OnEvent(ActorPC pc)
        {
            if (pc.Inventory.Equipments.ContainsKey(EnumEquipSlot.HEAD))
            {
                if (pc.Inventory.Equipments[EnumEquipSlot.HEAD].ItemID == 50025000 ||
                    pc.Inventory.Equipments[EnumEquipSlot.HEAD].ItemID == 50026800)
                {
                    Say(pc, 131, "你這傢伙，原來是謎語團的！$R;" +
                        "來這裡做什麼？$R;" +
                        "$P我是受僱的$R;" +
                        "$R要是泰迪下了命令$R;" +
                        "我就非殺你不可$R;" +
                        "$P在被發現前，快點回去吧！$R;");
                    return;
                }
            }
            int a = Global.Random.Next(1, 2);
            if (a == 1)
            {
                Say(pc, 131, "我是那泰迪聘請的傭兵$R;" +
                    "報酬挺不錯呢$R;" +
                    "短時間內，還是會待在這裡的$R;");
                return;
            }
            Say(pc, 131, "這些傢伙全部都是棉花娃娃，$R;" +
                "$R聽說是泰迪們連夜趕製的呢$R;");
        }
    }
}