using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;

using SagaScript.Chinese.Enums;
namespace SagaScript.M10018000
{
    public class S11000109 : Event
    {
        public S11000109()
        {
            this.EventID = 11000109;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<DFHJFlags> mask = new BitMask<DFHJFlags>(pc.CMask["DFHJ"]);
            BitMask<Job2X_06> Job2X_06_mask = pc.CMask["Job2X_06"];
            BitMask<Crystal> Crystal_mask = pc.CMask["Crystal"];

            if (Crystal_mask.Test(Crystal.開始收集) &&
                !Crystal_mask.Test(Crystal.土之精靈) &&
                CountItem(pc, 10014300) >= 1)
            {
                if (!Crystal_mask.Test(Crystal.光之精靈) &&
                    !Crystal_mask.Test(Crystal.暗之精靈) &&
                    !Crystal_mask.Test(Crystal.炎之精靈) &&
                    !Crystal_mask.Test(Crystal.水之精靈) &&
                    !Crystal_mask.Test(Crystal.風之精靈) &&
                    CountItem(pc, 10014300) >= 1)
                {
                    Crystal_mask.SetValue(Crystal.土之精靈, true);
                    Say(pc, 131, "您好！$R;" +
                        "$R我的名字叫尼布！是大地精靈$R;" +
                        "$R有什麼事呢？$R;" +
                        "$P…$R;" +
                        "$R什麼？在『水晶』上注入力量？$R;" +
                        "$R這樣做有什麼意義嗎？$R;" +
                        "雖然我也不知道，但好像挺好玩的$R;" +
                        "那麼就幫您吧$R;" +
                        "$P準備好了嗎？開始吧~$R;");
                    PlaySound(pc, 3120, false, 100, 50);
                    ShowEffect(pc, 4035);
                    Say(pc, 131, "在『水晶』裡注入力量了$R;" +
                        "…?$R;" +
                        "好像已經不能$R;" +
                        "再增加力量了$R;");
                    return;
                }
                Say(pc, 131, "您好！$R;" +
                    "$R我的名字叫尼布！是大地精靈$R;" +
                    "$R有什麼事呢？$R;" +
                    "$P…$R;" +
                    "$R什麼？在『水晶』上注入力量？$R;" +
                    "$R…?$R;" +
                    "好像已經有別的精靈力量了$R;" +
                    "$P不能再在這裡$R;" +
                    "注入我的力量了$R;");
                return;
            }

            if (!mask.Test(DFHJFlags.大地精靈第一次對話))
            {
                Say(pc, 131, "您好$R;" +
                "$R我叫尼布！是大地精靈$R;" +
                "$R什麼事呢？$R;");
                mask.SetValue(DFHJFlags.大地精靈第一次對話, true);
            }
            else
            {
                Say(pc, 131, "您好$R;" +
                "找我什麼事呢$R;");
            }

            if (Job2X_06_mask.Test(Job2X_06.朝拜大地精靈))
            {
                Say(pc, 131, "加油！$R;");
                return;
            }

            if (Job2X_06_mask.Test(Job2X_06.進階轉職開始))
            {
                Job2X_06_mask.SetValue(Job2X_06.朝拜大地精靈, true);
                //_3A98 = true;
                Say(pc, 131, "哇~太神奇了$R;" +
                    "沒想到現在還有做這種事的人…$R;" +
                    "$P反正都到這來了，沒理由不做吧？$R;" +
                    "$P希望您能得到大地的保護唷$R;");
                ShowEffect(pc, 4035);
                return;
            }

            if (CountItem(pc, 60091051) >= 1 && CountItem(pc, 10012400) >= 1)
            {

                Say(pc, 131, "啊！是『祖母綠』呀$R;" +
                    "$R啊…為了交換祖母綠$R;" +
                    "在『獵弓』上$R;" +
                    "注入地之力是嗎？$R;");
                switch (Select(pc, "怎麼辦呢?", "", "要求注入地之力", "放棄"))
                {
                    case 1:
                        if (CheckInventory(pc, 60091054, 1))
                        {
                            PlaySound(pc, 3068, false, 100, 50);
                            Wait(pc, 2000);
                            TakeItem(pc, 60091051, 1);
                            TakeItem(pc, 10012400, 1);
                            GiveItem(pc, 60091054, 1);
                            Say(pc, 131, "得到了『獵弓(大地)』$R;");
                            Say(pc, 131, "謝謝您的『祖母綠』！$R;" +
                                "請再來玩呀！$R;");
                            return;
                        }
                        Say(pc, 131, "行李太多了，不能給您呀$R;" +
                            "我不喜歡不整理的人！$R;");
                        break;
                    case 2:
                        Say(pc, 131, "哎呀…我誤會了$R;");
                        break;
                }
                return;
            }
            if (CountItem(pc, 10011205) >= 1 && CountItem(pc, 10026400) >= 1)
            {

                Say(pc, 131, "哎！這是『大地召喚石』呀$R;" +
                    "$R啊！原來是想用召喚石$R;" +
                    "在『木箭』注入地之力阿$R;" +
                    "是這樣嗎？$R;");
                switch (Select(pc, "怎麼辦呢?", "", "注入地之力", "放棄"))
                {
                    case 1:

                        PlaySound(pc, 3068, false, 100, 50);
                        Wait(pc, 2000);
                        int MJ;
                        MJ = 0;
                        while (CountItem(pc, 10026400) > MJ)
                        {
                            MJ++;
                        }
                        if (MJ > 200)
                        {

                            if (CheckInventory(pc, 10026505, 200))
                            {

                                TakeItem(pc, 10011205, 1);
                                TakeItem(pc, 10026400, 200);
                                GiveItem(pc, 10026505, 200);
                                Say(pc, 131, "『木箭』變成了『大地之箭』！$R;");
                                Say(pc, 131, "哎…太累了$R;" +
                                    "$R今天就到這兒嗎？$R;" +
                                    "還想再做的話$R;" +
                                    "就把大地召喚石拿來吧！$R;");
                                return;
                            }
                            Say(pc, 131, "行李太多了，不能給您阿$R;" +
                                "真討厭不喜歡整理的人呀！$R;");
                            return;
                        }
                        Say(pc, 131, "『木箭』變成了『大地之箭』！$R;");
                        TakeItem(pc, 10011205, 1);
                        TakeItem(pc, 10026400, (ushort)MJ);
                        GiveItem(pc, 10026505, (ushort)MJ);
                        Say(pc, 131, "竟然給我『大地召喚石』$R太感謝了$R;" +
                            "下次再來吧！$R;");
                        break;
                    case 2:
                        Say(pc, 131, "哎呀…我誤會了$R;");
                        break;
                }
                return;
            }
        }
    }
}