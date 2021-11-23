using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB;
using SagaDB.Item;
using SagaDB.Actor;
using SagaMap.Network.Client;
using SagaMap.Scripting;
using SagaMap.Manager;
using SagaScript.Chinese.Enums;
namespace SagaScript
{
    public class ACEKUJI3 : SagaMap.Scripting.Item

    {
        public ACEKUJI3()
        {
            //ITEM = 22001020 - 22001029
            //EVENT = 82202020 - 82202029
            Init(82202020, delegate(ActorPC pc)
            {
                GiveRandomTreasure(pc, "KUJIBOX_21");
                TakeItem(pc, 22001020, 1);
                PlaySound(pc, 2040, false, 100, 50);
                Say(pc, 0, "出现了随机的礼物!", "激赏礼券");
            });
            Init(82202021, delegate(ActorPC pc)
            {
                GiveRandomTreasure(pc, "KUJIBOX_22");
                TakeItem(pc, 22001021, 1);
                PlaySound(pc, 2040, false, 100, 50);
                Say(pc, 0, "出现了随机的礼物!", "激赏礼券");
            });
            Init(82202022, delegate(ActorPC pc)
            {
                GiveRandomTreasure(pc, "KUJIBOX_23");
                TakeItem(pc, 22001022, 1);
                PlaySound(pc, 2040, false, 100, 50);
                Say(pc, 0, "出现了随机的礼物!", "激赏礼券");
            });
            Init(82202023, delegate(ActorPC pc)
            {
                GiveRandomTreasure(pc, "KUJIBOX_24");
                TakeItem(pc, 22001023, 1);
                PlaySound(pc, 2040, false, 100, 50);
                Say(pc, 0, "出现了随机的礼物!", "激赏礼券");
            });
            Init(82202024, delegate(ActorPC pc)
            {
                GiveRandomTreasure(pc, "KUJIBOX_25");
                TakeItem(pc, 22001024, 1);
                PlaySound(pc, 2040, false, 100, 50);
                Say(pc, 0, "出现了随机的礼物!", "激赏礼券");
            });
            Init(82202025, delegate(ActorPC pc)
            {
                GiveRandomTreasure(pc, "KUJIBOX_26");
                TakeItem(pc, 22001025, 1);
                PlaySound(pc, 2040, false, 100, 50);
                Say(pc, 0, "出现了随机的礼物!", "激赏礼券");
            });
            Init(82202026, delegate(ActorPC pc)
            {
                GiveRandomTreasure(pc, "KUJIBOX_27");
                TakeItem(pc, 22001026, 1);
                PlaySound(pc, 2040, false, 100, 50);
                Say(pc, 0, "出现了随机的礼物!", "激赏礼券");
            });
            Init(82202027, delegate(ActorPC pc)
            {
                GiveRandomTreasure(pc, "KUJIBOX_28");
                TakeItem(pc, 22001027, 1);
                PlaySound(pc, 2040, false, 100, 50);
                Say(pc, 0, "出现了随机的礼物!", "激赏礼券");
            });
            Init(82202028, delegate(ActorPC pc)
            {
                GiveRandomTreasure(pc, "KUJIBOX_29");
                TakeItem(pc, 22001028, 1);
                PlaySound(pc, 2040, false, 100, 50);
                Say(pc, 0, "出现了随机的礼物!", "激赏礼券");
            });
            Init(82202029, delegate(ActorPC pc)
            {
                GiveRandomTreasure(pc, "KUJIBOX_30");
                TakeItem(pc, 22001029, 1);
                PlaySound(pc, 2040, false, 100, 50);
                Say(pc, 0, "出现了随机的礼物!", "激赏礼券");
            });
        }
    }
}
