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
    public class ACEKUJI : SagaMap.Scripting.Item

    {
        public ACEKUJI()
        {
            //ITEM = 22001000 - 22001010
            //EVENT = 82202000 - 82202010
            Init(82202000, delegate(ActorPC pc)
            {
                GiveRandomTreasure(pc, "KUJIBOX_1");
                TakeItem(pc, 22001000, 1);
                PlaySound(pc, 2040, false, 100, 50);
                Say(pc, 0, "出现了随机的礼物!", "激赏礼券");
            });
            Init(82202001, delegate(ActorPC pc)
            {
                GiveRandomTreasure(pc, "KUJIBOX_2");
                TakeItem(pc, 22001001, 1);
                PlaySound(pc, 2040, false, 100, 50);
                Say(pc, 0, "出现了随机的礼物!", "激赏礼券");
            });
            Init(82202002, delegate(ActorPC pc)
            {
                GiveRandomTreasure(pc, "KUJIBOX_3");
                TakeItem(pc, 22001002, 1);
                PlaySound(pc, 2040, false, 100, 50);
                Say(pc, 0, "出现了随机的礼物!", "激赏礼券");
            });
            Init(82202003, delegate(ActorPC pc)
            {
                GiveRandomTreasure(pc, "KUJIBOX_4");
                TakeItem(pc, 22001003, 1);
                PlaySound(pc, 2040, false, 100, 50);
                Say(pc, 0, "出现了随机的礼物!", "激赏礼券");
            });
            Init(82202004, delegate(ActorPC pc)
            {
                GiveRandomTreasure(pc, "KUJIBOX_5");
                TakeItem(pc, 22001004, 1);
                PlaySound(pc, 2040, false, 100, 50);
                Say(pc, 0, "出现了随机的礼物!", "激赏礼券");
            });
            Init(82202005, delegate(ActorPC pc)
            {
                GiveRandomTreasure(pc, "KUJIBOX_6");
                TakeItem(pc, 22001005, 1);
                PlaySound(pc, 2040, false, 100, 50);
                Say(pc, 0, "出现了随机的礼物!", "激赏礼券");
            });
            Init(82202006, delegate(ActorPC pc)
            {
                GiveRandomTreasure(pc, "KUJIBOX_7");
                TakeItem(pc, 22001006, 1);
                PlaySound(pc, 2040, false, 100, 50);
                Say(pc, 0, "出现了随机的礼物!", "激赏礼券");
            });
            Init(82202007, delegate(ActorPC pc)
            {
                GiveRandomTreasure(pc, "KUJIBOX_8");
                TakeItem(pc, 22001007, 1);
                PlaySound(pc, 2040, false, 100, 50);
                Say(pc, 0, "出现了随机的礼物!", "激赏礼券");
            });
            Init(82202008, delegate(ActorPC pc)
            {
                GiveRandomTreasure(pc, "KUJIBOX_9");
                TakeItem(pc, 22001008, 1);
                PlaySound(pc, 2040, false, 100, 50);
                Say(pc, 0, "出现了随机的礼物!", "激赏礼券");
            });
            Init(82202009, delegate(ActorPC pc)
            {
                GiveRandomTreasure(pc, "KUJIBOX_10");
                TakeItem(pc, 22001009, 1);
                PlaySound(pc, 2040, false, 100, 50);
                Say(pc, 0, "出现了随机的礼物!", "激赏礼券");
            });
        }
    }
}
