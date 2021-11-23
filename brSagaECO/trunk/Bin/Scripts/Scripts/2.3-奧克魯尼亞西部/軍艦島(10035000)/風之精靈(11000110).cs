using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;

using SagaScript.Chinese.Enums;
namespace SagaScript.M10035000
{
    public class S11000110 : Event
    {
        public S11000110()
        {
            this.EventID = 11000110;
        }

        public override void OnEvent(ActorPC pc)
        {

            BitMask<JJDFlags> mask = new BitMask<JJDFlags>(pc.CMask["JJD"]);
            BitMask<Job2X_06> Job2X_06_mask = pc.CMask["Job2X_06"];
            BitMask<Crystal> Crystal_mask = pc.CMask["Crystal"];

            if (Crystal_mask.Test(Crystal.開始收集) &&
                !Crystal_mask.Test(Crystal.風之精靈) &&
                CountItem(pc, 10014300) >= 1)
            {
                if (!Crystal_mask.Test(Crystal.光之精靈) &&
                    !Crystal_mask.Test(Crystal.暗之精靈) &&
                    !Crystal_mask.Test(Crystal.炎之精靈) &&
                    !Crystal_mask.Test(Crystal.土之精靈) &&
                    !Crystal_mask.Test(Crystal.水之精靈) &&
                    CountItem(pc, 10014300) >= 1)
                {
                    Crystal_mask.SetValue(Crystal.風之精靈, true); 
                    Say(pc, 131, "您好$R;" +
                        "$R我叫菲爾！是神風精靈$R;" +
                        "$R什麼事情呢？$R;" +
                        "$P…$R;" +
                        "什麼？在『水晶』上注入力量？$R;" +
                        "$R不知道是什麼，$R;" +
                        "但只要我能做到的，一定會幫您唷$R;" +
                        "$P那麼～開始吧$R;");
                    PlaySound(pc, 3120, false, 100, 50);
                    ShowEffect(pc, 4033);
                    Say(pc, 131, "『水晶』裡注入力量了$R;" +
                        "…?$R;" +
                        "再增加力量$R;" +
                        "已經不可能了$R;");
                    return;
                }
                Say(pc, 131, "您好$R;" +
                    "$R我的名字叫菲爾！是神風精靈$R;" +
                    "$R什麼事情呢？$R;" +
                    "$P…$R;" +
                    "在『水晶』上注入力量？$R;" +
                    "$R…?$R;" +
                    "已經有別的精靈力量注入了$R;" +
                    "$P水晶現在不能注入我的力量唷$R;");
                return;
            }

            if (!mask.Test(JJDFlags.神風精靈第一次對話))
            {
                Say(pc, 131, "您好$R;" +
                    "$R我叫菲爾！是神風精靈$R;" +
                    "$R有什麼事情嗎？$R;");
                mask.SetValue(JJDFlags.神風精靈第一次對話, true);
            }
            else
            {
                Say(pc, 131, "您好$R;" +
                    "找我有什麼事情呢？$R;");
            }

            if (Job2X_06_mask.Test(Job2X_06.朝拜神風精靈))
            {
                Say(pc, 131, "再見$R;");
                return;
            }

            if (Job2X_06_mask.Test(Job2X_06.進階轉職開始))
            {
                Job2X_06_mask.SetValue(Job2X_06.朝拜神風精靈, true);
                Say(pc, 131, "您真是一個奇怪的客人$R;" +
                    "$P元素術師是一個$R;" +
                    "很累的職業，請加油唷$R;" +
                    "$P希望您能得神風保護呀$R;");
                ShowEffect(pc, 4033);
                return;
            }

            if (CountItem(pc, 60091051) >= 1 && CountItem(pc, 10012000) >= 1)
            {

                Say(pc, 131, "啊，『方型黃寶石』呀$R;" +
                    "為了交換方型黃寶石$R;" +
                    "在『獵弓』裡$R;" +
                    "注入風之力是嗎？$R;");
                switch (Select(pc, "怎麼辦呢?", "", "拜託他注入風之力", "放棄"))
                {
                    case 1:
                        if (CheckInventory(pc, 60091055, 1))
                        {

                            PlaySound(pc, 3068, false, 100, 50);
                            Wait(pc, 2000);
                            TakeItem(pc, 60091051, 1);
                            TakeItem(pc, 10012000, 1);
                            GiveItem(pc, 60091055, 1);
                            Say(pc, 131, "得到了『獵弓（神風）』$R;");
                            Say(pc, 131, "給了我『方型黃寶石』$R;" +
                                "太感謝了$R;" +
                                "請再來玩呀！$R;");
                            return;
                        }
                        Say(pc, 131, "行李太多了，不能給您呀$R;" +
                            "減少行李後，再來吧$R;");
                        break;
                    case 2:
                        Say(pc, 131, "哎呀…我誤會您了$R;");
                        break;
                }
                return;
            }
            if (CountItem(pc, 10011207) >= 1 && CountItem(pc, 10026400) >= 1)
            {

                Say(pc, 131, "啊！$R『神風召喚石』$R;" +
                    "$R嘻嘻！想用召喚石在$R;" +
                    "『木箭』上注入風之力是嗎？$R;");
                switch (Select(pc, "怎麼辦呢?", "", "注入風之力", "放棄"))
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

                            if (CheckInventory(pc, 10026507, 200))
                            {

                                TakeItem(pc, 10011207, 1);
                                TakeItem(pc, 10026400, 200);
                                GiveItem(pc, 10026507, 200);
                                Say(pc, 131, "『木箭』變成了『神風之箭』$R;");
                                Say(pc, 131, "哎…好睏呀$R;" +
                                    "$R今天到這裡嗎？$R;" +
                                    "還想繼續製作$R;" +
                                    "就把神風召喚石拿來吧！$R;");
                                return;
                            }
                            Say(pc, 131, "行李太多了，不能給您呀$R;" +
                                "減少道具後，再來吧$R;");
                            return;
                        }
                        TakeItem(pc, 10011207, 1);
                        TakeItem(pc, 10026400, (ushort)MJ);
                        GiveItem(pc, 10026507, (ushort)MJ);
                        Say(pc, 131, "『木箭』變成了『神風之箭』$R;");
                        Say(pc, 131, "給我『神風召喚石』太感謝您了$R;" +
                            "請再來玩呀！$R;");

                        break;
                    case 2:
                        Say(pc, 131, "哎呀…我誤會您了$R;");
                        break;
                }
                return;
            }
            Say(pc, 131, "…?$R;" +
                "什麼也沒有呀$R;");
        }
        //未知对话
        

        
    }
}
