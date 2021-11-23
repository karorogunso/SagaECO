using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;

namespace SagaScript
{
    public abstract class 魔法の蝶 : Event
    {
        uint mapID;
        byte x, y;
        protected void Init(uint eventID, uint mapID, byte x, byte y)
        {
            this.EventID = eventID;
            this.mapID = mapID;
            this.x = x;
            this.y = y;
        }
        public override void OnEvent(ActorPC pc)
        {
            switch (Select(pc, "どちらを行いますか？", "", "鍵の契約", "たまいれカウンタ―", "やめる"))
            {
                case 1:
                    鍵の契約(pc);
                    break;
                case 2:
                    Say(pc, 11001979, 0, "未実装機能です。");
                    break;
                case 3:
                    return;
            }
        }
        void 鍵の契約(ActorPC pc)
        {
            BitMask<MagicButterfly> MagicButterfly_mask = pc.CMask["MagicButterfly"];
            if (mapID == 10002000)
            {
                if (MagicButterfly_mask.Test(MagicButterfly.已於瑞路斯雪原紀錄))
                {
                    Say(pc, 0, "已紀錄過了。", "魔法の蝶");
                }
                else
                {
                    MagicButterfly_mask.SetValue(MagicButterfly.已於瑞路斯雪原紀錄, true);
                    Wait(pc, 1000);
                    ShowEffect(pc, 10);
                    Wait(pc, 1000);
                    Say(pc, 0, "已紀錄。", "魔法の蝶");
                }
            }

            if (mapID == 10050001)
            {
                if (MagicButterfly_mask.Test(MagicButterfly.已於北方中央山脈北側紀錄))
                {
                    Say(pc, 0, "已紀錄過了。", "魔法の蝶");
                }
                else
                {
                    MagicButterfly_mask.SetValue(MagicButterfly.已於北方中央山脈北側紀錄, true);
                    Wait(pc, 1000);
                    ShowEffect(pc, 10);
                    Wait(pc, 1000);
                    Say(pc, 0, "已紀錄。", "魔法の蝶");
                }
            }

            if (mapID == 10034000)
            {
                if (MagicButterfly_mask.Test(MagicButterfly.已於奧克魯尼亞東海岸紀錄))
                {
                    Say(pc, 0, "已紀錄過了。", "魔法の蝶");
                }
                else
                {
                    MagicButterfly_mask.SetValue(MagicButterfly.已於奧克魯尼亞東海岸紀錄, true);
                    Wait(pc, 1000);
                    ShowEffect(pc, 10);
                    Wait(pc, 1000);
                    Say(pc, 0, "已紀錄。", "魔法の蝶");
                }
            }

            if (mapID == 10059000)
            {
                if (MagicButterfly_mask.Test(MagicButterfly.已於瑪依瑪依島紀錄))
                {
                    Say(pc, 0, "已紀錄過了。", "魔法の蝶");
                }
                else
                {
                    MagicButterfly_mask.SetValue(MagicButterfly.已於瑪依瑪依島紀錄, true);
                    Wait(pc, 1000);
                    ShowEffect(pc, 10);
                    Wait(pc, 1000);
                    Say(pc, 0, "已紀錄。", "魔法の蝶");
                }
            }

            if (mapID == 10035000)
            {
                if (MagicButterfly_mask.Test(MagicButterfly.已於軍艦島紀錄))
                {
                    Say(pc, 0, "已紀錄過了。", "魔法の蝶");
                }
                else
                {
                    MagicButterfly_mask.SetValue(MagicButterfly.已於軍艦島紀錄, true);
                    Wait(pc, 1000);
                    ShowEffect(pc, 10);
                    Wait(pc, 1000);
                    Say(pc, 0, "已紀錄。", "魔法の蝶");
                }
            }

            if (mapID == 10042000)
            {
                if (MagicButterfly_mask.Test(MagicButterfly.已於步伐沙漠紀錄))
                {
                    Say(pc, 0, "已紀錄過了。", "魔法の蝶");
                }
                else
                {
                    MagicButterfly_mask.SetValue(MagicButterfly.已於步伐沙漠紀錄, true);
                    Wait(pc, 1000);
                    ShowEffect(pc, 10);
                    Wait(pc, 1000);
                    Say(pc, 0, "已紀錄。", "魔法の蝶");
                }
            }

            if (mapID == 10066000)
            {
                if (MagicButterfly_mask.Test(MagicButterfly.已於阿伊恩市城下層紀錄))
                {
                    Say(pc, 0, "已紀錄過了。", "魔法の蝶");
                }
                else
                {
                    MagicButterfly_mask.SetValue(MagicButterfly.已於阿伊恩市城下層紀錄, true);
                    Wait(pc, 1000);
                    ShowEffect(pc, 10);
                    Wait(pc, 1000);
                    Say(pc, 0, "已紀錄。", "魔法の蝶");
                }
            }

            if (mapID == 10068000)
            {
                if (MagicButterfly_mask.Test(MagicButterfly.已於穀倉地帶紀錄))
                {
                    Say(pc, 0, "已紀錄過了。", "魔法の蝶");
                }
                else
                {
                    MagicButterfly_mask.SetValue(MagicButterfly.已於穀倉地帶紀錄, true);
                    Wait(pc, 1000);
                    ShowEffect(pc, 10);
                    Wait(pc, 1000);
                    Say(pc, 0, "已紀錄。", "魔法の蝶");
                }
            }

            if (mapID == 20146000)
            {
                if (MagicButterfly_mask.Test(MagicButterfly.已於光之塔下層紀錄))
                {
                    Say(pc, 0, "已紀錄過了。", "魔法の蝶");
                }
                else
                {
                    MagicButterfly_mask.SetValue(MagicButterfly.已於光之塔下層紀錄, true);
                    Wait(pc, 1000);
                    ShowEffect(pc, 10);
                    Wait(pc, 1000);
                    Say(pc, 0, "已紀錄。", "魔法の蝶");
                }
            }

            if (mapID == 20163000)
            {
                if (MagicButterfly_mask.Test(MagicButterfly.已於光之塔上層紀錄))
                {
                    Say(pc, 0, "已紀錄過了。", "魔法の蝶");
                }
                else
                {
                    MagicButterfly_mask.SetValue(MagicButterfly.已於光之塔上層紀錄, true);
                    Wait(pc, 1000);
                    ShowEffect(pc, 10);
                    Wait(pc, 1000);
                    Say(pc, 0, "已紀錄。", "魔法の蝶");
                }
            }

            if (mapID == 10019000)
            {
                if (MagicButterfly_mask.Test(MagicButterfly.已於不死之島大陸側紀錄))
                {
                    Say(pc, 0, "已紀錄過了。", "魔法の蝶");
                }
                else
                {
                    MagicButterfly_mask.SetValue(MagicButterfly.已於不死之島大陸側紀錄, true);
                    Wait(pc, 1000);
                    ShowEffect(pc, 10);
                    Wait(pc, 1000);
                    Say(pc, 0, "已紀錄。", "魔法の蝶");
                }
            }

            if (mapID == 10055000)
            {
                if (MagicButterfly_mask.Test(MagicButterfly.已於緋紅磚城紀錄))
                {
                    Say(pc, 0, "已紀錄過了。", "魔法の蝶");
                }
                else
                {
                    MagicButterfly_mask.SetValue(MagicButterfly.已於緋紅磚城紀錄, true);
                    Wait(pc, 1000);
                    ShowEffect(pc, 10);
                    Wait(pc, 1000);
                    Say(pc, 0, "已紀錄。", "魔法の蝶");
                }
            }
        }
    }
    public class S11001979 : 魔法の蝶
    {
        public S11001979()
        {
            Init(11001979, 10002000, 45, 21);
        }
    }
}
/*
map
10002000
10050001
10034000
10059000
10035000
10042000
10066000
10068000
20146000
20163000
10019000
10055000

npcid
11001979
11001980
11001981
11001982
11002401
11002402
11002403
11002404
11002405
11002406
11002407
11004330
*/
