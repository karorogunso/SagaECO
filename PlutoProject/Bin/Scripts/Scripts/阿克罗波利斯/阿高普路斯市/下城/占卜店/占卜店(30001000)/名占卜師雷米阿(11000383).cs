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
                Say(pc, 131, "「猫灵」是不会害主人的$R;" +
                    "$R只是稍微捣乱而已$R;" +
                    "$P如果想处理那个的时候$R寻找使用光之力量的人$P就可能可以把它赶出去$R;");
                return;
            }
            if (!Neko_01_amask.Test(Neko_01.桃子任務完成) &&
                Neko_01_cmask.Test(Neko_01.與瑪歐斯對話) &&
                !Neko_01_cmask.Test(Neko_01.與雷米阿對話))
            {
                Say(pc, 131, "好像有什么凭依在您身上了$R;" +
                    "$R好像说想要跟我相谈?$R知道了$R;" +
                    "$P闭上眼睛后$R把心情平复下来$R;");
                Say(pc, 131, "……$R;" +
                    "$P嗯…$R里面有猫灵啊$P是猫灵$R不想听听看吗?$R;");
                switch (Select(pc, "猫灵是什么?", "", "问问看", "放弃"))
                {
                    case 1:
                        Say(pc, 131, "猫灵是从前机械文明时代$R叫「猫」的动物魂魄$R;" +
                            "$R「猫」是因为战争时产生的毒$R最先绝种了$R;" +
                            "$P现在在街上或洞窟里$R猫的魂魄附上人族或其他种族身上$R的情况也有$R;" +
                            "$P进入您的，就是那个猫的魂魄$R;");
                        Say(pc, 0, 131, "喵$R;", " ");
                        break;
                }
                Neko_01_cmask.SetValue(Neko_01.與雷米阿對話, true);
                Say(pc, 131, "「猫灵」是不会害主人的$R;" +
                    "$R只是稍微捣乱而已$R;" +
                    "$P如果想处理那个的时候$R寻找使用光之力量的人$P就可能可以把它赶出去$R;");
                return;
            }
            if (Crystal_mask.Test(Crystal.索取魔杖) && Crystal_mask.Test(Crystal.光之精靈))
            {
                if (CheckInventory(pc, 60072054, 1))
                {
                    Crystal_mask.SetValue(Crystal.光之精靈, false);
                    Crystal_mask.SetValue(Crystal.光之精靈, false);
                    GiveItem(pc, 60072054, 1);
                    Say(pc, 131, "得到『时空魔杖·光』$R;");
                    Say(pc, 131, "下次再给你占卜吧$R;");
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
                    Say(pc, 131, "得到『时空魔杖·暗』$R;");
                    Say(pc, 131, "下次再给你占卜吧$R;");
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
                    Say(pc, 131, "得到『时空魔杖·火』$R;");
                    Say(pc, 131, "下次再给你占卜吧$R;");
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
                    Say(pc, 131, "得到『时空魔杖·水』$R;");
                    Say(pc, 131, "下次再给你占卜吧$R;");
                    return;
                }
                Say(pc, 131, "行李太多了$R;");
                return;
            }
            if (Crystal_mask.Test(Crystal.土之精靈) && Crystal_mask.Test(Crystal.索取魔杖))
            {
                if (CheckInventory(pc, 60072053, 1))
                {
                    Crystal_mask.SetValue(Crystal.光之精靈, false);
                    Crystal_mask.SetValue(Crystal.土之精靈, false);
                    GiveItem(pc, 60072053, 1);
                    Say(pc, 131, "得到『时空魔杖·土』$R;");
                    Say(pc, 131, "下次再给你占卜吧$R;");
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
                    Say(pc, 131, "得到『时空魔杖·风』$R;");
                    Say(pc, 131, "下次再给你占卜吧$R;");
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
                    Say(pc, 131, "把精灵的力量注入『水晶』里$R;" +
                        "再给我拿来吧$R;" +
                        "那麻烦您了…$R;");
                    return;
                }
                if (Crystal_mask.Test(Crystal.開始收集) &&
                    Crystal_mask.Test(Crystal.第一個水晶) &&
                    CountItem(pc, 10014300) >= 1)
                {
                    TakeItem(pc, 10014300, 1);
                    Say(pc, 131, "给他『水晶』$R;");
                    Say(pc, 131, "……$R;" +
                        "要试到成功为止喔！$R;" +
                        "$R只要一次……求求您喔!!$R;" +
                        "$P……$R;" +
                        "$R…………$R;" +
                        "$R………………$R;" +
                        "$P这是!?$R;" +
                        "$P……$R;" +
                        "$R还是不行啊………$R;" +
                        "$R但是我不会放弃的……$R;" +
                        "一定要找出能看到自己未来的方法…$R;" +
                        "$R真是万分感谢您啊$R;" +
                        pc.Name + "……真是谢谢啊$R;");
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
                    Say(pc, 131, "技能点数上升了1$R;");
                    Say(pc, 131, "这是表示谢意的$R;");
                    return;
                }
                Say(pc, 131, "把精灵的力量注入『水晶』里$R;" +
                    "再给我拿来吧$R;" +
                    "那麻烦您了…$R;");
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
                        Say(pc, 131, "给他『水晶』$R;");
                        Say(pc, 131, "真是谢谢喔$R;");
                        return;
                    }
                    Say(pc, 131, "$R这次也拿普通『水晶』过来就可以了$R;" +
                        "$R那麻烦您了…$R;");
                    return;
                }
                Say(pc, 131, "这不是" + pc.Name + "吗？$R;" +
                    "$R今天有什么事吗?$R;");
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
                    Say(pc, 131, "得到『时空魔杖·光』$R;");
                    Say(pc, 131, "下次再给你占卜吧$R;");
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
                    "$R如果是您的话，$R;" +
                    "我就可以放心拜托啊……$R;" +
                    "$P没问题的话，可以接受我的拜托嘛？$R;");
                switch (Select(pc, "想怎样做呢?", "", "听听看", "拒绝"))
                {
                    case 1:
                        Crystal_mask.SetValue(Crystal.開始收集, true);
                        Say(pc, 131, "谢谢$R;" +
                            "$R我想请您帮我找回『水晶』$R;" +
                            "但不是普通的『水晶』$R;" +
                            "$P我想要的是$R;" +
                            "据说位于阿克罗尼亚的六个地方$R;" +
                            "注入了精灵力量的『水晶』$R;" +
                            "$R我一定要得到那强力的『水晶』$R;" +
                            "$P那拜托您了…$R;");
                        break;
                    case 2:
                        Say(pc, 131, "是吗?这个委托太突然$R;" +
                            "所以有点困难吧$R;" +
                            "$R那今天来这边有什么事吗？$R;");
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
                Say(pc, 131, "把精灵的力量注入『水晶』里$R;" +
                    "再给我拿来吧$R;" +
                    "那麻烦您了…$R;");
                return;
            }
            if (Crystal_mask.Test(Crystal.開始收集) && CountItem(pc, 10014300) >= 1)
            {
                TakeItem(pc, 10014300, 1);
                Say(pc, 131, "给他『水晶』$R;");
                Say(pc, 131, "安然无恙的回来了$R;" +
                    "$R……$R;" +
                    "$R至今我已经为很多人看了未来$R;" +
                    "但是我自己的未来，我无法看到呢$R;" +
                    "$P……$R;" +
                    "$R不过我对自己的未来很好奇呢…$R;" +
                    "所以需要注入了精灵力量的水晶$R;");
                水晶(pc);
                return;
            }
            Say(pc, 131, "把精灵的力量注入『水晶』里$R;" +
                "再给我拿来吧$R;" +
                "那麻烦您了…$R;");

        }

        void 普通商店(ActorPC pc)
        {
           switch (Select(pc, "欢迎光临…… ", "", "买东西", "卖东西", "占卜", "买提示卡", "什么都不做"))
            {
                case 1:
                    OpenShopBuy(pc, 10);
                    Say(pc, 131, "下次要再来玩喔$R;");
                    break;
                case 2:
                    OpenShopSell(pc, 10);
                    Say(pc, 131, "下次要再来玩喔$R;");
                    break;
                case 3:
                    //GOTO EVT1100011619;
                    break;
                case 4:
                    Say(pc, 131, "看到了您的未来……$R;" +
                        "$R不知道做什么好的时候$R;" +
                        "打开看看吧$R;");
                    OpenShopBuy(pc, 152);
                    break;
                case 5:
                    Say(pc, 131, "下次要再来玩喔$R;");
                    break;
            }
        }

        void 商店(ActorPC pc)
        {
            BitMask<Crystal> Crystal_mask = pc.CMask["Crystal"];

            switch (Select(pc, "欢迎来到占卜店…", "", "买东西", "卖东西", "占卜", "人生相谈(期间限定)", "去拿『水晶』", "买入新商品", "买提示卡", "什么都不做"))
            {
                case 1:
                    OpenShopBuy(pc, 10);
                    Say(pc, 131, "下次要再来玩喔$R;");
                    break;
                case 2:
                    OpenShopSell(pc, 10);
                    Say(pc, 131, "下次要再来玩喔$R;");
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
                        Say(pc, 131, "您会再拿『水晶』过来吗？$R;" +
                            "$R这次也拿普通『水晶』过来就可以了$R;" +
                            "$R因为我在找新的东西呢$R;");
                        return;
                    }
                    Crystal_mask.SetValue(Crystal.開始收集, true);
                    Say(pc, 131, "您会再拿『水晶』过来吗？$R;" +
                        "$R但是已经失败过一次了$R;" +
                        "$R知道了。那再做一次试试看吧$R;");
                    break;
                case 6:
                    OpenShopBuy(pc, 108);
                    Say(pc, 131, "下次要再来玩喔$R;");
                    break;
                case 7:
                    Say(pc, 131, "看到了您的未来……$R;" +
                        "$R不知道做什么好的时候$R;" +
                        "打开看看吧$R;");
                    OpenShopBuy(pc, 152);
                    break;
                case 8:
                    Say(pc, 131, "下次要再来玩喔$R;");
                    break;
            }
        }

        void 水晶(ActorPC pc)
        {
            BitMask<Crystal> Crystal_mask = pc.CMask["Crystal"];

            Crystal_mask.SetValue(Crystal.第一個水晶, true);
            Crystal_mask.SetValue(Crystal.開始收集, false);
            Say(pc, 131, "用它可以看到我的未来呢$R;" +
                "$P……$R;" +
                "$R…………$R;" +
                "$R………………$R;" +
                "$P！?$R;");
            PlaySound(pc, 2235, false, 100, 50);
            Say(pc, 131, "『水晶』碎了$R;");
            Say(pc, 131, "唉…还是看不见啊$R;" +
                "$R好不容易拿过来的$R;" +
                "真的对不起$R;" +
                "$R我以后到底会变成怎么样呢…$R;" +
                "$P……$R;" +
                "该向您道谢呢$R;" +
                "这是我的心意喔$R;");
            if (Crystal_mask.Test(Crystal.光之精靈))
            {
                Crystal_mask.SetValue(Crystal.光之精靈, true);
                if (CheckInventory(pc, 60072054, 1))
                {
                    Crystal_mask.SetValue(Crystal.光之精靈, false);
                    Crystal_mask.SetValue(Crystal.光之精靈, false);
                    GiveItem(pc, 60072054, 1);
                    Say(pc, 131, "得到『时空魔杖·光』$R;");
                    Say(pc, 131, "下次再给你占卜吧$R;"); 
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
                    Say(pc, 131, "得到『时空魔杖·暗』$R;");
                    Say(pc, 131, "下次再给你占卜吧$R;"); 
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
                    Say(pc, 131, "得到『时空魔杖·火』$R;");
                    Say(pc, 131, "下次再给你占卜吧$R;"); 
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
                    Say(pc, 131, "得到『时空魔杖·水』$R;");
                    Say(pc, 131, "下次再给你占卜吧$R;"); 
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
                    Say(pc, 131, "得到『时空魔杖·土』$R;");
                    Say(pc, 131, "下次再给你占卜吧$R;"); 
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
                    Say(pc, 131, "得到『时空魔杖·风』$R;");
                    Say(pc, 131, "下次再给你占卜吧$R;"); 
                    return;
                }
                Say(pc, 131, "行李太多了$R;");
                return;
            } 
        }
    }
}
