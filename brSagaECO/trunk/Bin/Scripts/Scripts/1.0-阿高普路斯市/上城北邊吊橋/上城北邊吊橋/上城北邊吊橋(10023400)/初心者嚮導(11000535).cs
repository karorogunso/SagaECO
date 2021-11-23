using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
//所在地圖:上城北邊吊橋(10023400) NPC基本信息:初心者嚮導(11000535) X:124 Y:8
namespace SagaScript.M10023400
{
    public class S11000535 : Event
    {
        public S11000535()
        {
            this.EventID = 11000535;
        }

        public override void OnEvent(ActorPC pc)
        {
            switch (Select(pc, "要做甚麼?", "", "二次職轉職", "聽取二次職轉職說明事項", "買東西及傳送"))
            {
                case 1:
                    if (pc.JobLevel1 >= 30)
                    {
                        if (pc.Inventory.Equipments.Count == 0)
                        {
                            if (pc.Job == PC_JOB.SWORDMAN)
                            {
                                BitMask<Job2X_01> Job2X_01_mask = pc.CMask["Job2X_01"];
                                switch (Select(pc, "請問要轉職為二次職嗎?", "", "是", "不是"))
                                {
                                    case 1:
                                        Job2X_01_mask.SetValue(Job2X_01.進階轉職開始, true);
                                        Job2X_01_mask.SetValue(Job2X_01.收集黑醋, true);
                                        Job2X_01_mask.SetValue(Job2X_01.給予黑醋, true);
                                        Job2X_01_mask.SetValue(Job2X_01.轉職完成, true);
                                        Say(pc, 131, "給您象徵『劍客』的$R;" +
                                            "『劍客紋章』$R;");
                                        pc.JEXP = 0;
                                        ChangePlayerJob(pc, PC_JOB.BLADEMASTER);
                                        PlaySound(pc, 3087, false, 100, 50);
                                        ShowEffect(pc, 4131);
                                        Wait(pc, 4000);
                                        PlaySound(pc, 4012, false, 100, 50);
                                        Say(pc, 131, "轉職成『劍客』!$R;");
                                        break;
                                    case 2:
                                        return;
                                }
                            }
                            if (pc.Job == PC_JOB.FENCER)
                            {
                                BitMask<Job2X_02> Job2X_02_mask = pc.CMask["Job2X_02"];
                                switch (Select(pc, "請問要轉職為二次職嗎?", "", "是", "不是"))
                                {
                                    case 1:
                                        Job2X_02_mask.SetValue(Job2X_02.進階轉職開始, true);
                                        Job2X_02_mask.SetValue(Job2X_02.收集冰罐頭, true);
                                        Job2X_02_mask.SetValue(Job2X_02.給予冰罐頭, true);
                                        Job2X_02_mask.SetValue(Job2X_02.轉職完成, true);
                                        Say(pc, 131, "給您象徵『聖騎士』的$R;" +
                                            "『聖騎士紋章』$R;");
                                        pc.JEXP = 0;
                                        ChangePlayerJob(pc, PC_JOB.KNIGHT);
                                        PlaySound(pc, 3087, false, 100, 50);
                                        ShowEffect(pc, 4131);
                                        Wait(pc, 4000);
                                        PlaySound(pc, 4012, false, 100, 50);
                                        Say(pc, 131, "轉職成『聖騎士』!$R;");
                                        break;
                                    case 2:
                                        return;
                                }
                            }
                            if (pc.Job == PC_JOB.SCOUT)
                            {
                                BitMask<Job2X_03> Job2X_03_mask = pc.CMask["Job2X_03"];
                                switch (Select(pc, "請問要轉職為二次職嗎?", "", "是", "不是"))
                                {
                                    case 1:
                                        Job2X_03_mask.SetValue(Job2X_03.刺客轉職開始, true);
                                        Job2X_03_mask.SetValue(Job2X_03.轉職結束, true);
                                        Say(pc, 131, "給您象徵『刺客』的$R;" +
                                            "『刺客紋章』$R;");
                                        pc.JEXP = 0;
                                        ChangePlayerJob(pc, PC_JOB.ASSASSIN);
                                        PlaySound(pc, 3087, false, 100, 50);
                                        ShowEffect(pc, 4131);
                                        Wait(pc, 4000);
                                        PlaySound(pc, 4012, false, 100, 50);
                                        Say(pc, 131, "轉職成『刺客』!$R;");
                                        break;
                                    case 2:
                                        return;
                                }
                            }
                            if (pc.Job == PC_JOB.ARCHER)
                            {
                                BitMask<Job2X_04> Job2X_04_mask = pc.CMask["Job2X_04"];
                                switch (Select(pc, "請問要轉職為二次職嗎?", "", "是", "不是"))
                                {
                                    case 1:
                                        Job2X_04_mask.SetValue(Job2X_04.進階轉職開始, true);
                                        Job2X_04_mask.SetValue(Job2X_04.進階轉職結束, true);
                                        Say(pc, 131, "給您象徵『獵人』的$R;" +
                                            "『獵人紋章』$R;");
                                        pc.JEXP = 0;
                                        ChangePlayerJob(pc, PC_JOB.STRIKER);
                                        PlaySound(pc, 3087, false, 100, 50);
                                        ShowEffect(pc, 4131);
                                        Wait(pc, 4000);
                                        PlaySound(pc, 4012, false, 100, 50);
                                        Say(pc, 131, "轉職成『獵人』!$R;");
                                        break;
                                    case 2:
                                        return;
                                }
                            }
                            if (pc.Job == PC_JOB.WIZARD)
                            {
                                BitMask<Job2X_05> Job2X_05_mask = pc.CMask["Job2X_05"];
                                switch (Select(pc, "請問要轉職為二次職嗎?", "", "是", "不是"))
                                {
                                    case 1:
                                        Job2X_05_mask.SetValue(Job2X_05.轉職開始, true);
                                        Job2X_05_mask.SetValue(Job2X_05.轉職完成, true);
                                        Say(pc, 131, "給您象徵『魔導士』的$R;" +
                                            "『魔導士紋章』$R;");
                                        pc.JEXP = 0;
                                        ChangePlayerJob(pc, PC_JOB.SORCERER);
                                        PlaySound(pc, 3087, false, 100, 50);
                                        ShowEffect(pc, 4131);
                                        Wait(pc, 4000);
                                        PlaySound(pc, 4012, false, 100, 50);
                                        Say(pc, 131, "轉職成『魔導士』!$R;");
                                        break;
                                    case 2:
                                        return;
                                }
                            }
                            if (pc.Job == PC_JOB.SHAMAN)
                            {
                                BitMask<Job2X_06> Job2X_06_mask = pc.CMask["Job2X_06"];
                                switch (Select(pc, "請問要轉職為二次職嗎?", "", "是", "不是"))
                                {
                                    case 1:
                                        Job2X_06_mask.SetValue(Job2X_06.進階轉職開始, true);
                                        Job2X_06_mask.SetValue(Job2X_06.轉職完成, true);
                                        Say(pc, 131, "給您象徵『精靈使』的$R;" +
                                            "『精靈使紋章』$R;");
                                        pc.JEXP = 0;
                                        ChangePlayerJob(pc, PC_JOB.ELEMENTER);
                                        PlaySound(pc, 3087, false, 100, 50);
                                        ShowEffect(pc, 4131);
                                        Wait(pc, 4000);
                                        PlaySound(pc, 4012, false, 100, 50);
                                        Say(pc, 131, "轉職成『精靈使』!$R;");
                                        break;
                                    case 2:
                                        return;
                                }
                            }
                            if (pc.Job == PC_JOB.VATES)
                            {
                                BitMask<Job2X_07> Job2X_07_mask = pc.CMask["Job2X_07"];
                                switch (Select(pc, "請問要轉職為二次職嗎?", "", "是", "不是"))
                                {
                                    case 1:
                                        Job2X_07_mask.SetValue(Job2X_07.轉職開始, true);
                                        Job2X_07_mask.SetValue(Job2X_07.轉職結束, true);
                                        Say(pc, 131, "給您象徵『神官』的$R;" +
                                            "『神官紋章』$R;");
                                        pc.JEXP = 0;
                                        ChangePlayerJob(pc, PC_JOB.DRUID);
                                        PlaySound(pc, 3087, false, 100, 50);
                                        ShowEffect(pc, 4131);
                                        Wait(pc, 4000);
                                        PlaySound(pc, 4012, false, 100, 50);
                                        Say(pc, 131, "轉職成『神官』!$R;");
                                        break;
                                    case 2:
                                        return;
                                }
                            }
                            if (pc.Job == PC_JOB.WARLOCK)
                            {
                                BitMask<Job2X_08> Job2X_08_mask = pc.CMask["Job2X_08"];
                                switch (Select(pc, "請問要轉職為二次職嗎?", "", "是", "不是"))
                                {
                                    case 1:
                                        Job2X_08_mask.SetValue(Job2X_08.轉職開始, true);
                                        Job2X_08_mask.SetValue(Job2X_08.轉職結束, true);
                                        Say(pc, 131, "給您象徵『秘法師』的$R;" +
                                            "『秘法師紋章』$R;");
                                        pc.JEXP = 0;
                                        ChangePlayerJob(pc, PC_JOB.CABALIST);
                                        PlaySound(pc, 3087, false, 100, 50);
                                        ShowEffect(pc, 4131);
                                        Wait(pc, 4000);
                                        PlaySound(pc, 4012, false, 100, 50);
                                        Say(pc, 131, "轉職成『秘法師』!$R;");
                                        break;
                                    case 2:
                                        return;
                                }
                            }
                            if (pc.Job == PC_JOB.TATARABE)
                            {
                                BitMask<Job2X_09> Job2X_09_mask = pc.CMask["Job2X_09"];
                                switch (Select(pc, "請問要轉職為二次職嗎?", "", "是", "不是"))
                                {
                                    case 1:
                                        Job2X_09_mask.SetValue(Job2X_09.轉職開始, true);
                                        Job2X_09_mask.SetValue(Job2X_09.使用塞爾曼德的心臟, true);
                                        Say(pc, 131, "給您象徵『鐵匠』的$R;" +
                                            "『鐵匠紋章』$R;");
                                        pc.JEXP = 0;
                                        ChangePlayerJob(pc, PC_JOB.BLACKSMITH);
                                        PlaySound(pc, 3087, false, 100, 50);
                                        ShowEffect(pc, 4131);
                                        Wait(pc, 4000);
                                        PlaySound(pc, 4012, false, 100, 50);
                                        Say(pc, 131, "轉職成『鐵匠』!$R;");
                                        break;
                                    case 2:
                                        return;
                                }
                            }
                            if (pc.Job == PC_JOB.FARMASIST)
                            {
                                BitMask<Job2X_10> Job2X_10_mask = pc.CMask["Job2X_10"];
                                switch (Select(pc, "請問要轉職為二次職嗎?", "", "是", "不是"))
                                {
                                    case 1:
                                        Job2X_10_mask.SetValue(Job2X_10.轉職開始, true);
                                        Job2X_10_mask.SetValue(Job2X_10.所有問題回答正確, true);
                                        Say(pc, 131, "給您象徵『鍊金術師』的$R;" +
                                            "『鍊金術師紋章』$R;");
                                        pc.JEXP = 0;
                                        ChangePlayerJob(pc, PC_JOB.ALCHEMIST);
                                        PlaySound(pc, 3087, false, 100, 50);
                                        ShowEffect(pc, 4131);
                                        Wait(pc, 4000);
                                        PlaySound(pc, 4012, false, 100, 50);
                                        Say(pc, 131, "轉職成『鍊金術師』!$R;");
                                        break;
                                    case 2:
                                        return;
                                }
                            }
                            if (pc.Job == PC_JOB.RANGER)
                            {
                                BitMask<Job2X_11> Job2X_11_mask = pc.CMask["Job2X_11"];
                                switch (Select(pc, "請問要轉職為二次職嗎?", "", "是", "不是"))
                                {
                                    case 1:
                                        Job2X_11_mask.SetValue(Job2X_11.轉職開始, true);
                                        Job2X_11_mask.SetValue(Job2X_11.買花, true);
                                        Say(pc, 131, "給您象徵『探險家』的$R;" +
                                            "『探險家紋章』$R;");
                                        pc.JEXP = 0;
                                        ChangePlayerJob(pc, PC_JOB.EXPLORER);
                                        PlaySound(pc, 3087, false, 100, 50);
                                        ShowEffect(pc, 4131);
                                        Wait(pc, 4000);
                                        PlaySound(pc, 4012, false, 100, 50);
                                        Say(pc, 131, "轉職成『探險家』!$R;");
                                        break;
                                    case 2:
                                        return;
                                }
                            }
                            if (pc.Job == PC_JOB.MERCHANT)
                            {
                                BitMask<Job2X_12> Job2X_12_mask = pc.CMask["Job2X_12"];
                                switch (Select(pc, "請問要轉職為二次職嗎?", "", "是", "不是"))
                                {
                                    case 1:
                                        Job2X_12_mask.SetValue(Job2X_12.轉職開始, true);
                                        Job2X_12_mask.SetValue(Job2X_12.轉職成功, true);
                                        Say(pc, 131, "給您象徵『貿易商』的$R;" +
                                            "『貿易商紋章』$R;");
                                        pc.JEXP = 0;
                                        ChangePlayerJob(pc, PC_JOB.TRADER);
                                        PlaySound(pc, 3087, false, 100, 50);
                                        ShowEffect(pc, 4131);
                                        Wait(pc, 4000);
                                        PlaySound(pc, 4012, false, 100, 50);
                                        Say(pc, 131, "轉職成『貿易商』!$R;");
                                        break;
                                    case 2:
                                        return;
                                }
                            }
                            Say(pc, 131, "你已經成為二次職或者更高職業了$R;" +
                                    "怒我無法幫你$R;");
                        }
                        else
                        Say(pc, 131, "紋章會烙印在皮膚上的$R;" +
                            "把裝備脫掉後再來吧$R;");
                        /*
                        PC_JOB[] srcJob = { PC_JOB.SWORDMAN, PC_JOB.FENCER, PC_JOB.SCOUT, PC_JOB.ARCHER, PC_JOB.WIZARD, PC_JOB.SHAMAN, PC_JOB.VATES, PC_JOB.WARLOCK, PC_JOB.TATARABE, PC_JOB.FARMASIST, PC_JOB.RANGER, PC_JOB.MERCHANT };
                        PC_JOB[] destJOB = { PC_JOB.BLADEMASTER, PC_JOB.KNIGHT, PC_JOB.ASSASSIN, PC_JOB.STRIKER, PC_JOB.SORCERER, PC_JOB.ELEMENTER, PC_JOB.DRUID, PC_JOB.CABALIST, PC_JOB.BLACKSMITH, PC_JOB.ALCHEMIST, PC_JOB.EXPLORER, PC_JOB.TRADER };
                        PC_JOB.Bounty Hunter,PC_JOB.Dark Stalker,PC_JOB.Command,PC_JOB.Gunner,PC_JOB.Saga,PC_JOB.Enchanter,PC_JOB.Bard,PC_JOB.Necromancer,PC_JOB.Machinery,PC_JOB.Marionest,PC_JOB.Treasurehunter,PC_JOB.Gambler
                        int srcJindex = -1;
                        for (int i = 0; i < srcJob.Length; i++)
                        {
                            if (srcJob[i] == pc.Job)
                            {
                                srcJindex = i;
                                break;
                            }
                        }
                        if (srcJindex >= 0)
                        {
                            if (Select(pc, "請問要轉職為二次職嗎?", "", "是", "不是") == 1)
                            {
                                pc.JEXP = 0;
                                ChangePlayerJob(pc, destJOB[srcJindex]);
                            }
                        }
                        */
                    }
                    else
                    {
                        Say(pc, 133, "您的職業等级不足。$R");
                    }
                    break;
                case 2:
                    Say(pc, 131, "轉職到『二次職業』的話$R;" +
                        "LV會成為1…$R;" +
                        "$R但是轉職的之後擁有的$R;" +
                        "技能和技能點數是不會變的$R;" +
                        "$P而且…轉職之前沒有練熟的技能$R;" +
                        "一旦轉職了就無法練下去$R;" +
                        "$R比如說，職業LV30時轉職的話$R;" +
                        "本來的職業LV30以上的技能$R;" +
                        "轉職後就無法在練下去了…$R;" +
                        "$P好好想想吧…$R;");
                    break;
                case 3:
                    switch (Select(pc, "要買東西嗎?", "", "第一商品栏", "第二商品栏", "傳送到東吊橋"))
                    {
                        case 1:
                            OpenShopBuy(pc, 413);
                            break;
                        case 2:
                            OpenShopBuy(pc, 415);
                            break;
                        case 3:
                            Warp(pc, 10023100, 239, 127);
                            break;
                    }
                    break;
            }
        }
    }
}