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
    public class ACEKUJI2 : SagaMap.Scripting.Item

    {
        public ACEKUJI2()
        {
            //ITEM = 22001010 - 22001019
            //EVENT = 82202010 - 82202019
            Init(82202010, delegate(ActorPC pc)
            {
                GiveRandomTreasure(pc, "KUJIBOX_11");
                TakeItem(pc, 22001010, 1);
                PlaySound(pc, 2040, false, 100, 50);
                Say(pc, 0, "出现了随机的礼物!", "激赏礼券");
            });
            Init(82202011, delegate(ActorPC pc)
            {
                GiveRandomTreasure(pc, "KUJIBOX_12");
                TakeItem(pc, 22001011, 1);
                PlaySound(pc, 2040, false, 100, 50);
                Say(pc, 0, "出现了随机的礼物!", "激赏礼券");
            });
            Init(82202012, delegate(ActorPC pc)
            {
                GiveRandomTreasure(pc, "KUJIBOX_13");
                TakeItem(pc, 22001012, 1);
                PlaySound(pc, 2040, false, 100, 50);
                Say(pc, 0, "出现了随机的礼物!", "激赏礼券");
            });
            Init(82202013, delegate(ActorPC pc)
            {
                GiveRandomTreasure(pc, "KUJIBOX_14");
                TakeItem(pc, 22001013, 1);
                PlaySound(pc, 2040, false, 100, 50);
                Say(pc, 0, "出现了随机的礼物!", "激赏礼券");
            });
            Init(82202014, delegate(ActorPC pc)
            {
                GiveRandomTreasure(pc, "KUJIBOX_15");
                TakeItem(pc, 22001014, 1);
                PlaySound(pc, 2040, false, 100, 50);
                Say(pc, 0, "出现了随机的礼物!", "激赏礼券");
            });
            Init(82202015, delegate(ActorPC pc)
            {
                GiveRandomTreasure(pc, "KUJIBOX_16");
                TakeItem(pc, 22001015, 1);
                PlaySound(pc, 2040, false, 100, 50);
                Say(pc, 0, "出现了随机的礼物!", "激赏礼券");
            });
            Init(82202016, delegate(ActorPC pc)
            {
                GiveRandomTreasure(pc, "KUJIBOX_17");
                TakeItem(pc, 22001016, 1);
                PlaySound(pc, 2040, false, 100, 50);
                Say(pc, 0, "出现了随机的礼物!", "激赏礼券");
            });
            Init(82202017, delegate(ActorPC pc)
            {
                GiveRandomTreasure(pc, "KUJIBOX_18");
                TakeItem(pc, 22001017, 1);
                PlaySound(pc, 2040, false, 100, 50);
                Say(pc, 0, "出现了随机的礼物!", "激赏礼券");
            });
            Init(82202018, delegate(ActorPC pc)
            {
                GiveRandomTreasure(pc, "KUJIBOX_19");
                TakeItem(pc, 22001018, 1);
                PlaySound(pc, 2040, false, 100, 50);
                Say(pc, 0, "出现了随机的礼物!", "激赏礼券");
            });
            Init(82202019, delegate(ActorPC pc)
            {
                GiveRandomTreasure(pc, "KUJIBOX_20");
                TakeItem(pc, 22001019, 1);
                PlaySound(pc, 2040, false, 100, 50);
                Say(pc, 0, "出现了随机的礼物!", "激赏礼券");
            });
        }
    }
}
