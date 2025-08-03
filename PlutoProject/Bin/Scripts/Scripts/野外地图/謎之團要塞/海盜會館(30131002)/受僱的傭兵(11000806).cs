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
                    Say(pc, 131, "你这傢伙，原来是谜之团的！$R;" +
                        "来这里做什么？$R;" +
                        "$P我是受雇的$R;" +
                        "$R要是泰迪下了命令$R;" +
                        "我就非杀你不可$R;" +
                        "$P在被发现前，快点回去吧！$R;");
                    return;
                }
            }
            int a = Global.Random.Next(1, 2);
            if (a == 1)
            {
                Say(pc, 131, "我是那泰迪聘请的佣兵$R;" +
                    "报酬挺不错呢$R;" +
                    "短时间内，还是会待在这里的$R;");
                return;
            }
            Say(pc, 131, "这些家伙全部都是棉花娃娃，$R;" +
                "$R听说是泰迪们连夜赶制的呢$R;");
        }
    }
}