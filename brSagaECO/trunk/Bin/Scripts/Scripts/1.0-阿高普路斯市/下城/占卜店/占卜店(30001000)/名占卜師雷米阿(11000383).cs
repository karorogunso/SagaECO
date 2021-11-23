using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
using SagaScript.Chinese.Enums;
//所在地圖:占卜店(30001000) NPC基本信息:名占卜師雷米阿(11000383) X:2 Y:1
namespace SagaScript.M30001000
{
    public class S11000383 : Event
    {
        public S11000383()
        {
            this.EventID = 11000383;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<Neko_01> Neko_01_cmask = pc.CMask["Neko_01"];
            BitMask<Neko_01> Neko_01_amask = pc.AMask["Neko_01"];
            BitMask<Crystal> Crystal_mask = pc.CMask["Crystal"];

            if (!Neko_01_amask.Test(Neko_01.桃子任務完成) &&
                Neko_01_cmask.Test(Neko_01.與雷米阿對話) &&
                !Neko_01_cmask.Test(Neko_01.與祭祀對話))
            {
                Say(pc, 131, "「凱堤」是不會害主人的$R;" +
                    "$R只是稍微搗亂而已$R;" +
                    "$P如果想處理那個的時候$R尋找使用光之力量的人$P就可能可以把它趕出去$R;");
                return;
            }
            if (!Neko_01_amask.Test(Neko_01.桃子任務完成) &&
                Neko_01_cmask.Test(Neko_01.與瑪歐斯對話) &&
                !Neko_01_cmask.Test(Neko_01.與雷米阿對話))
            {
                Say(pc, 131, "好像有什麼憑依在您身上了$R;" +
                    "$R好像說想要跟我相談?$R知道了$R;" +
                    "$P閉上眼睛後$R把心情平復下來$R;");
                Say(pc, 131, "……$R;" +
                    "$P嗯…$R裡面有凱堤阿$P是凱堤$R不想聽聽看嗎?$R;");
                switch (Select(pc, "凱堤是什麼?", "", "問問看", "放棄"))
                {
                    case 1:
                        Say(pc, 131, "凱堤是從前機械文明時代$R叫「貓」的動物魂魄$R;" +
                            "$R「貓」是因為戰爭時產生的毒$R最先絕種了$R;" +
                            "$P現在在街上或洞窟裡$R貓的魂魄附上人族或其他種族身上$R的情況也有$R;" +
                            "$P進入您的，就是那個貓的魂魄$R;");
                        Say(pc, 0, 131, "喵$R;", " ");
                        break;
                }
                Neko_01_cmask.SetValue(Neko_01.與雷米阿對話, true);
                Say(pc, 131, "「凱堤」是不會害主人的$R;" +
                    "$R只是稍微搗亂而已$R;" +
                    "$P如果想處理那個的時候$R尋找使用光之力量的人$P就可能可以把它趕出去$R;");
                return;
            }
            if (Crystal_mask.Test(Crystal.索取魔杖) && Crystal_mask.Test(Crystal.光之精靈))
            {
                if (CheckInventory(pc, 60072054, 1))
                {
                    Crystal_mask.SetValue(Crystal.光之精靈, false);
                    Crystal_mask.SetValue(Crystal.光之精靈, false);
                    GiveItem(pc, 60072054, 1);
                    Say(pc, 131, "得到『光明屬性神功魔杖』$R;");
                    Say(pc, 131, "下次再給你占卜吧$R;");
                    return;
                }
                Say(pc, 131, "行李太多了$R;");
                return;
            }
            if (Crystal_mask.Test(Crystal.索取魔杖) && Crystal_mask.Test(Crystal.暗之精靈))
            {
                if (CheckInventory(pc, 60072055, 1))
                {
                    Crystal_mask.SetValue(Crystal.光之精靈, false);
                    Crystal_mask.SetValue(Crystal.暗之精靈, false);
                    GiveItem(pc, 60072055, 1);
                    Say(pc, 131, "得到『黑暗屬性神功魔杖』$R;");
                    Say(pc, 131, "下次再給你占卜吧$R;");
                    return;
                }
                Say(pc, 131, "行李太多了$R;");
                return;
            }
            if (Crystal_mask.Test(Crystal.索取魔杖) && Crystal_mask.Test(Crystal.炎之精靈))
            {
                if (CheckInventory(pc, 60072050, 1))
                {
                    Crystal_mask.SetValue(Crystal.光之精靈, false);
                    Crystal_mask.SetValue(Crystal.炎之精靈, false);
                    GiveItem(pc, 60072050, 1);
                    Say(pc, 131, "得到『火焰屬性神功魔杖』$R;");
                    Say(pc, 131, "下次再給你占卜吧$R;");
                    return;
                }
                Say(pc, 131, "行李太多了$R;");
                return;
            }
            if (Crystal_mask.Test(Crystal.水之精靈) && Crystal_mask.Test(Crystal.索取魔杖))
            {
                if (CheckInventory(pc, 60072051, 1))
                {
                    Crystal_mask.SetValue(Crystal.光之精靈, false);
                    Crystal_mask.SetValue(Crystal.水之精靈, false); 
                    GiveItem(pc, 60072051, 1);
                    Say(pc, 131, "得到『水靈屬性神功魔杖』$R;");
                    Say(pc, 131, "下次再給你占卜吧$R;");
                    return;
                }
                Say(pc, 131, "下次再給你占卜吧$R;");
                return;
            }
            if (Crystal_mask.Test(Crystal.土之精靈) && Crystal_mask.Test(Crystal.索取魔杖))
            {
                if (CheckInventory(pc, 60072053, 1))
                {
                    Crystal_mask.SetValue(Crystal.光之精靈, false);
                    Crystal_mask.SetValue(Crystal.土之精靈, false);
                    GiveItem(pc, 60072053, 1);
                    Say(pc, 131, "得到『大地屬性神功魔杖』$R;");
                    Say(pc, 131, "下次再給你占卜吧$R;");
                    return;
                }
                Say(pc, 131, "行李太多了$R;");
                return;
            }
            if (Crystal_mask.Test(Crystal.風之精靈) && Crystal_mask.Test(Crystal.索取魔杖))
            {
                if (CheckInventory(pc, 60072052, 1))
                {
                    Crystal_mask.SetValue(Crystal.光之精靈, false);
                    Crystal_mask.SetValue(Crystal.風之精靈, false);
                    GiveItem(pc, 60072052, 1);
                    Say(pc, 131, "得到『神風屬性神功魔杖』$R;");
                    Say(pc, 131, "行李太多了$R;");
                    return;
                }
                Say(pc, 131, "行李太多了$R;");
                return;
            }
            if (Crystal_mask.Test(Crystal.第一個水晶) && Crystal_mask.Test(Crystal.開始收集))
            {
                if (!Crystal_mask.Test(Crystal.光之精靈) &&
                    !Crystal_mask.Test(Crystal.暗之精靈) && 
                    !Crystal_mask.Test(Crystal.炎之精靈) &&
                    !Crystal_mask.Test(Crystal.水之精靈) &&
                    !Crystal_mask.Test(Crystal.土之精靈) &&
                    !Crystal_mask.Test(Crystal.風之精靈))
                {
                    Say(pc, 131, "把精靈的力量注入『水晶』裡$R;" +
                        "再給我拿來吧$R;" +
                        "那麻煩您了…$R;");
                    return;
                }
                if (Crystal_mask.Test(Crystal.開始收集) &&
                    Crystal_mask.Test(Crystal.第一個水晶) &&
                    CountItem(pc, 10014300) >= 1)
                {
                    TakeItem(pc, 10014300, 1);
                    Say(pc, 131, "給他『水晶』$R;");
                    Say(pc, 131, "……$R;" +
                        "要試到成功為止喔！$R;" +
                        "$R只要一次……求求您喔!!$R;" +
                        "$P……$R;" +
                        "$R…………$R;" +
                        "$R………………$R;" +
                        "$P這是!?$R;" +
                        "$P……$R;" +
                        "$R還是不行啊………$R;" +
                        "$R但是我不會放棄的……$R;" +
                        "一定要找出能看到自己未來的方法…$R;" +
                        "$R真是萬分感謝您啊$R;" +
                        pc.Name + "……真是謝謝啊$R;");
                    Crystal_mask.SetValue(Crystal.第二個水晶, true);
                    Crystal_mask.SetValue(Crystal.開始收集, false);
                    Crystal_mask.SetValue(Crystal.光之精靈, false);
                    Crystal_mask.SetValue(Crystal.暗之精靈, false);
                    Crystal_mask.SetValue(Crystal.炎之精靈, false);
                    Crystal_mask.SetValue(Crystal.水之精靈, false);
                    Crystal_mask.SetValue(Crystal.土之精靈, false);
                    Crystal_mask.SetValue(Crystal.風之精靈, false);
                    SkillPointBonus(pc, 1);
                    //SKILLBONUS 1
                    Wait(pc, 2000);
                    PlaySound(pc, 3087, false, 100, 50);
                    ShowEffect(pc, 4131);
                    Wait(pc, 2000);
                    Say(pc, 131, "技能點數上升了1$R;");
                    Say(pc, 131, "這是表示謝意的$R;");
                    return;
                }
                Say(pc, 131, "把精靈的力量注入『水晶』裡$R;" +
                    "再給我拿來吧$R;" +
                    "那麻煩您了…$R;");
                return;
            }
            if (Crystal_mask.Test(Crystal.第二個水晶))
            {
                if (Crystal_mask.Test(Crystal.繼續收集水晶))
                {
                    if (Crystal_mask.Test(Crystal.繼續收集水晶) && CountItem(pc, 10014300) >= 1)
                    {
                        Crystal_mask.SetValue(Crystal.繼續收集水晶, false);
                        TakeItem(pc, 10014300, 1);
                        Say(pc, 131, "給他『水晶』$R;");
                        Say(pc, 131, "真是謝謝喔$R;");
                        return;
                    }
                    Say(pc, 131, "$R這次也拿普通『水晶』過來就可以了$R;" +
                        "$R那麻煩您了…$R;");
                    return;
                }
                Say(pc, 131, "這不是" + pc.Name + "嗎？$R;" +
                    "$R今天有什麼事嗎?$R;");
                商店(pc);
                return;
            }
            if (Crystal_mask.Test(Crystal.索取魔杖))
            {
                Crystal_mask.SetValue(Crystal.光之精靈, true);
                if (CheckInventory(pc, 60072054, 1))
                {
                    Crystal_mask.SetValue(Crystal.光之精靈, false);
                    Crystal_mask.SetValue(Crystal.光之精靈, false);
                    GiveItem(pc, 60072054, 1);
                    Say(pc, 131, "得到『光明屬性神功魔杖』$R;");
                    Say(pc, 131, "下次再給你占卜吧$R;");
                    return;
                }
                Say(pc, 131, "行李太多了$R;");
                return;
            }
            if (Crystal_mask.Test(Crystal.第一個水晶))
            {
                商店(pc);
                return;
            }
            if (pc.Fame > 9 && !Crystal_mask.Test(Crystal.開始收集))
            {
                Say(pc, 131, "…看到了看到了$R;" +
                    "$R如果是您的話，$R;" +
                    "我就可以放心拜託啊……$R;" +
                    "$P沒問題的話，可以接受我的拜託嘛？$R;");
                switch (Select(pc, "想怎樣做呢?", "", "聽聽看", "拒絕"))
                {
                    case 1:
                        Crystal_mask.SetValue(Crystal.開始收集, true);
                        Say(pc, 131, "謝謝$R;" +
                            "$R我想請您幫我找回『水晶』$R;" +
                            "但不是普通的『水晶』$R;" +
                            "$P我想要的是$R;" +
                            "據說位於奥克魯尼亞大陸各地$R;" +
                            "注入了精靈力量的『水晶』$R;" +
                            "$R我一定要得到那強力的『水晶』$R;" +
                            "$P那拜託您了…$R;");
                        break;
                    case 2:
                        Say(pc, 131, "是嗎?這個委託太突然$R;" +
                            "所以有點困難吧$R;" +
                            "$R那今天來這邊有什麼事嗎？$R;");
                        普通商店(pc);
                        break;
                }
                return;
            }
            if (!Crystal_mask.Test(Crystal.開始收集))
            {
                普通商店(pc);
                return;
            }
            if (!Crystal_mask.Test(Crystal.光之精靈) &&
                !Crystal_mask.Test(Crystal.暗之精靈) &&
                !Crystal_mask.Test(Crystal.炎之精靈) &&
                !Crystal_mask.Test(Crystal.水之精靈) &&
                !Crystal_mask.Test(Crystal.土之精靈) &&
                !Crystal_mask.Test(Crystal.風之精靈))
            {
                Say(pc, 131, "把精靈的力量注入『水晶』裡$R;" +
                    "再給我拿來吧$R;" +
                    "那麻煩您了…$R;");
                return;
            }
            if (Crystal_mask.Test(Crystal.開始收集) && CountItem(pc, 10014300) >= 1)
            {
                TakeItem(pc, 10014300, 1);
                Say(pc, 131, "給他『水晶』$R;");
                Say(pc, 131, "安然無恙的回來了$R;" +
                    "$R……$R;" +
                    "$R至今我已經為很多人看了未來$R;" +
                    "但是我自己的未來，我無法看到呢$R;" +
                    "$P……$R;" +
                    "$R不過我對自己的未來很好奇呢…$R;" +
                    "所以需要注入了精靈力量的水晶$R;");
                水晶(pc);
                return;
            }
            Say(pc, 131, "把精靈的力量注入『水晶』裡$R;" +
                "再給我拿來吧$R;" +
                "那麻煩您了…$R;");

        }

        void 普通商店(ActorPC pc)
        {
            switch (Select(pc, "歡迎光臨…… ", "", "買東西", "賣東西", "占卜", "買提示卡", "什麼都不做"))
            {
                case 1:
                    OpenShopBuy(pc, 10);
                    Say(pc, 131, "下次要再來玩喔$R;");
                    break;
                case 2:
                    OpenShopSell(pc, 10);
                    Say(pc, 131, "下次要再來玩喔$R;");
                    break;
                case 3:
                    //GOTO EVT1100011619;
                    break;
                case 4:
                    Say(pc, 131, "看到了您的未來……$R;" +
                        "$R不知道做什麼好的時候$R;" +
                        "打開看看吧$R;");
                    OpenShopBuy(pc, 152);
                    break;
                case 5:
                    Say(pc, 131, "下次要再來玩喔$R;");
                    break;
            }
        }

        void 商店(ActorPC pc)
        {
            BitMask<Crystal> Crystal_mask = pc.CMask["Crystal"];

            switch (Select(pc, "歡迎來到占卜店…", "", "買東西", "賣東西", "占卜", "人生相談(期間限定)", "去拿『水晶』", "買入新商品", "買提示卡", "什麼都不做"))
            {
                case 1:
                    OpenShopBuy(pc, 10);
                    Say(pc, 131, "下次要再來玩喔$R;");
                    break;
                case 2:
                    OpenShopSell(pc, 10);
                    Say(pc, 131, "下次要再來玩喔$R;");
                    break;
                case 3:
                    //GOTO EVT1100011619;
                    break;
                case 4:
                    //GOTO EVT11000005002;
                    break;
                case 5:
                    if (Crystal_mask.Test(Crystal.第二個水晶))
                    {
                        Crystal_mask.SetValue(Crystal.繼續收集水晶, true);
                        Say(pc, 131, "您會再拿『水晶』過來嗎？$R;" +
                            "$R這次也拿普通『水晶』過來就可以了$R;" +
                            "$R因為我在找新的東西呢$R;");
                        return;
                    }
                    Crystal_mask.SetValue(Crystal.開始收集, true);
                    Say(pc, 131, "您會再拿『水晶』過來嗎？$R;" +
                        "$R但是已經失敗過一次了$R;" +
                        "$R知道了。那再做一次試試看吧$R;");
                    break;
                case 6:
                    OpenShopBuy(pc, 108);
                    Say(pc, 131, "下次要再來玩喔$R;");
                    break;
                case 7:
                    Say(pc, 131, "看到了您的未來……$R;" +
                        "$R不知道做什麼好的時候$R;" +
                        "打開看看吧$R;");
                    OpenShopBuy(pc, 152);
                    break;
                case 8:
                    Say(pc, 131, "下次要再來玩喔$R;");
                    break;
            }
        }

        void 水晶(ActorPC pc)
        {
            BitMask<Crystal> Crystal_mask = pc.CMask["Crystal"];

            Crystal_mask.SetValue(Crystal.第一個水晶, true);
            Crystal_mask.SetValue(Crystal.開始收集, false);
            Say(pc, 131, "用它可以看到我的未來呢$R;" +
                "$P……$R;" +
                "$R…………$R;" +
                "$R………………$R;" +
                "$P！?$R;");
            PlaySound(pc, 2235, false, 100, 50);
            Say(pc, 131, "『水晶』碎了$R;");
            Say(pc, 131, "唉…還是看不見啊$R;" +
                "$R好不容易拿過來的$R;" +
                "真的對不起$R;" +
                "$R我以後到底會變成怎麼樣呢…$R;" +
                "$P……$R;" +
                "該向您道謝呢$R;" +
                "這是我的心意喔$R;");
            if (Crystal_mask.Test(Crystal.光之精靈))
            {
                Crystal_mask.SetValue(Crystal.光之精靈, true);
                if (CheckInventory(pc, 60072054, 1))
                {
                    Crystal_mask.SetValue(Crystal.光之精靈, false);
                    Crystal_mask.SetValue(Crystal.光之精靈, false);
                    GiveItem(pc, 60072054, 1);
                    Say(pc, 131, "得到『光明屬性神功魔杖』$R;");
                    Say(pc, 131, "下次再給你占卜吧$R;");
                    return;
                }
                Say(pc, 131, "行李太多了$R;");
                return;
            }
            if (Crystal_mask.Test(Crystal.暗之精靈))
            {
                Crystal_mask.SetValue(Crystal.光之精靈, true);
                if (CheckInventory(pc, 60072055, 1))
                {
                    Crystal_mask.SetValue(Crystal.光之精靈, false);
                    Crystal_mask.SetValue(Crystal.暗之精靈, false);
                    GiveItem(pc, 60072055, 1);
                    Say(pc, 131, "得到『黑暗屬性神功魔杖』$R;");
                    Say(pc, 131, "下次再給你占卜吧$R;");
                    return;
                }
                Say(pc, 131, "行李太多了$R;");
                return;
            }
            if (Crystal_mask.Test(Crystal.炎之精靈))
            {
                Crystal_mask.SetValue(Crystal.光之精靈, true);
                if (CheckInventory(pc, 60072050, 1))
                {
                    Crystal_mask.SetValue(Crystal.光之精靈, false);
                    Crystal_mask.SetValue(Crystal.炎之精靈, false);
                    GiveItem(pc, 60072050, 1);
                    Say(pc, 131, "得到『火焰屬性神功魔杖』$R;");
                    Say(pc, 131, "下次再給你占卜吧$R;");
                    return;
                }
                Say(pc, 131, "行李太多了$R;");
                return;
            }
            if (Crystal_mask.Test(Crystal.水之精靈))
            {
                Crystal_mask.SetValue(Crystal.光之精靈, true);
                if (CheckInventory(pc, 60072051, 1))
                {
                    Crystal_mask.SetValue(Crystal.光之精靈, false);
                    Crystal_mask.SetValue(Crystal.水之精靈, false); 
                    GiveItem(pc, 60072051, 1);
                    Say(pc, 131, "得到『水靈屬性神功魔杖』$R;");
                    Say(pc, 131, "下次再給你占卜吧$R;");
                    return;
                }
                Say(pc, 131, "下次再給你占卜吧$R;");
                return;
            }
            if (Crystal_mask.Test(Crystal.土之精靈))
            {
                Crystal_mask.SetValue(Crystal.光之精靈, true);
                if (CheckInventory(pc, 60072053, 1))
                {
                    Crystal_mask.SetValue(Crystal.光之精靈, false);
                    Crystal_mask.SetValue(Crystal.土之精靈, false);
                    GiveItem(pc, 60072053, 1);
                    Say(pc, 131, "得到『大地屬性神功魔杖』$R;");
                    Say(pc, 131, "下次再給你占卜吧$R;");
                    return;
                }
                Say(pc, 131, "行李太多了$R;");
                return;
            }
            if (Crystal_mask.Test(Crystal.風之精靈))
            {
                Crystal_mask.SetValue(Crystal.光之精靈, true);
                if (CheckInventory(pc, 60072052, 1))
                {
                    Crystal_mask.SetValue(Crystal.光之精靈, false);
                    Crystal_mask.SetValue(Crystal.風之精靈, false);
                    GiveItem(pc, 60072052, 1);
                    Say(pc, 131, "得到『神風屬性神功魔杖』$R;");
                    Say(pc, 131, "行李太多了$R;");
                    return;
                }
                Say(pc, 131, "行李太多了$R;");
                return;
            } 
        }
    }
}
