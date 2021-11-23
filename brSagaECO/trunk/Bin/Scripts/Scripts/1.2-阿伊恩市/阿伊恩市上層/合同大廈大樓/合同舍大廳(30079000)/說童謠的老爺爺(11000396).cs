using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M30079000
{
    public class S11000396 : Event
    {
        public S11000396()
        {
            this.EventID = 11000396;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<AYEFlags> mask = pc.CMask["AYE"];
            if (mask.Test(AYEFlags.職業裝任務結束))//_4a44)
            {
                Say(pc, 131, "哦哦哦！這個呀！$R;" +
                    "哦，您的武器在說話呢。$R;" +
                    "$R訴說您流的汗水和激烈的戰鬥$R;" +
                    "$P真了不起吧！$R;" +
                    "您說是不是啊，小姑娘?$R;");
                Say(pc, 11000294, 131, "是，老爺爺，$R;" +
                    "我也是這麼想的$R;" +
                    "$R下一位。$R;");
                return;
            }
            if (pc.Job == PC_JOB.SWORDMAN
              || pc.Job == PC_JOB.BLADEMASTER
              || pc.Job == PC_JOB.BOUNTYHUNTER
              || pc.Job == PC_JOB.FENCER
              || pc.Job == PC_JOB.KNIGHT
              || pc.Job == PC_JOB.DARKSTALKER
              || pc.Job == PC_JOB.SCOUT
              || pc.Job == PC_JOB.ASSASSIN
              || pc.Job == PC_JOB.COMMAND
              || pc.Job == PC_JOB.ARCHER
              || pc.Job == PC_JOB.STRIKER
              || pc.Job == PC_JOB.GUNNER)
            {
                战士系职业分支判断(pc);
                return;
            }
            if (pc.Job == PC_JOB.WIZARD
                || pc.Job == PC_JOB.SORCERER
                || pc.Job == PC_JOB.SAGE
                || pc.Job == PC_JOB.SHAMAN
                || pc.Job == PC_JOB.ELEMENTER
                || pc.Job == PC_JOB.ENCHANTER
                || pc.Job == PC_JOB.VATES
                || pc.Job == PC_JOB.DRUID
                || pc.Job == PC_JOB.BARD
                || pc.Job == PC_JOB.WARLOCK
                || pc.Job == PC_JOB.GAMBLER
                || pc.Job == PC_JOB.NECROMANCER)
            {
                法师系开始(pc);
                return;
            }
            Say(pc, 131, "現在的戰士真是不行啊，$R;" +
                "練一會兒就說累，$R;" +
                "$R真是看不下去了$R;" +
                "我以前也沒有這樣啊$R;" +
                "$P以前好好的……$R;" +
                "您說是不是啊，小姑娘?$R;");
            Say(pc, 11000294, 131, "是，老爺爺，$R;" +
                "我也是這麼想的$R;" +
                "$R下一位。$R;");
        }
        void 战士系职业分支判断(ActorPC pc)
        {
            BitMask<AYEFlags> mask = pc.CMask["AYE"];
            if (mask.Test(AYEFlags.劍士系職業裝))//_2a96)
            {
                /*
                if (_4a43)
                {
                    剑士系完成(pc);
                    return;
                }
                //60010411, 1);
                if (a//ME.WORK0 > 0
                )
                {
                    _4a43 = true;
                    TakeItem(pc, 60010411, 1);
                    Say(pc, 131, "呵呵，您看起來經歷了很多呢。$R;" +
                        "給我武器讓我看一看吧$R;" +
                        "$R那我看看啊。$R;" +
                        "$P呵呵，武器告訴我$R;" +
                        "$R您是個真正的戰士呢。$R;" +
                        "$P真的很厲害.$R;" +
                        "最近像您這樣的可真少見呢。$R;" +
                        "$R合我的心意$R;");
                    剑士系完成(pc);
                    return;
                }
                */
                Say(pc, 131, "$R用它證明您的力量和能力吧$R;" +
                    "$P這個武器會記錄您的戰鬥，$R;" +
                    "$R它會告訴我您的能力唷$R;");
                return;
            }

            if (mask.Test(AYEFlags.騎士系職業裝))//_2a97)
            {
                /*
                if (_4a43)
                {
                    骑士系完成(pc);
                    return;
                }
                //60060250, 1);
                if (a//ME.WORK0 > 0
                )
                {
                    _4a43 = true;
                    TakeItem(pc, 60060250, 1);
                    Say(pc, 131, "呵呵，您看起來經歷了很多呢。$R;" +
                        "給我武器讓我看一看吧$R;" +
                        "$R那我看看啊。$R;" +
                        "$P呵呵，武器告訴我$R;" +
                        "$R您是個真正的戰士呢。$R;" +
                        "$P真的很厲害$R;" +
                        "最近像您這樣的可真少見呢。$R;" +
                        "$R合我的心意$R;");
                    骑士系完成(pc);
                    return;
                }
                */
                Say(pc, 131, "用這個證明您的力量和能力吧$R;" +
                    "$P這個武器會記錄您的戰鬥，$R;" +
                    "$R它會告訴我您的能力唷$R;");
                return;
            }
            if (mask.Test(AYEFlags.盜賊系職業裝))//_2a98)
            {
                /*
                if (_4a43)
                {
                    盗贼系完成(pc);
                    return;
                }
                //60010050, 1);
                if (a//ME.WORK0 > 0
                )
                {
                    _4a43 = true;
                    TakeItem(pc, 60010050, 1);
                    Say(pc, 131, "呵呵，您看起來經歷了很多呢。$R;" +
                        "給我武器讓我看一看吧$R;" +
                        "$R那我看看啊。$R;" +
                        "$P呵呵，武器告訴我$R;" +
                        "$R您是個真正的戰士呢。$R;" +
                        "$P真的很厲害.$R;" +
                        "最近像您這樣的可真少見呢。$R;" +
                        "$R合我的心意$R;");
                    盗贼系完成(pc);
                    return;
                }
                 */
                Say(pc, 131, "用這個證明您的力量和能力吧$R;" +
                    "$P這個武器會記錄您的戰鬥，$R;" +
                    "$R它會告訴我您的能力唷$R;");
                return;
            }
            if (mask.Test(AYEFlags.弓箭手系職業裝))//_2a99)
            {
                /*
                if (_4a43)
                {
                    弓箭手系完成(pc);
                    return;
                }
                //60090150, 1);
                if (a//ME.WORK0 > 0
                )
                {
                    _4a43 = true;
                    TakeItem(pc, 60090150, 1);
                    Say(pc, 131, "呵呵，您看起來經歷了很多呢。$R;" +
                        "給我武器讓我看一看吧$R;" +
                        "$R那我看看啊。$R;" +
                        "$P呵呵，武器告訴我$R;" +
                        "$R您是個真正的戰士呢。$R;" +
                        "$P真的很厲害.$R;" +
                        "最近像您這樣的可真少見呢。$R;" +
                        "$R合我的心意$R;");
                    弓箭手系完成(pc);
                    return;
                }
                */
                Say(pc, 131, "用這個證明您的力量和能力吧$R;" +
                    "$P這個武器會記錄您的戰鬥，$R;" +
                    "$R它會告訴我您的能力唷$R;");
                return;
            }
            Say(pc, 131, "現在的戰士真是不行啊，$R;" +
                "練一會兒就說累，$R;" +
                "$R真是看不下去了$R;" +
                "我以前也沒有這樣啊$R;" +
                "$P還是以前好啊$R;");
            switch (Select(pc, "是不是以前好呢？", "", "不是", "是的"))
            {
                case 1:
                    if (pc.Level < 45)
                    {
                        Say(pc, 131, "您這個等級低的小鬼，$R哪有資格說這樣的話阿?$R;");
                        return;
                    }
                    if (pc.Job == PC_JOB.SWORDMAN
                        || pc.Job == PC_JOB.BLADEMASTER
                        || pc.Job == PC_JOB.BOUNTYHUNTER)
                    {
                        Say(pc, 131, "您是真正的戰士嗎？$R;" +
                            "那就拿著這個$R;" +
                            "$R用它證明您的力量和能力吧$R;" +
                            "$P這個武器會記錄您的戰鬥，$R;" +
                            "$R它會告訴我您的能力唷$R;");
                        if (CheckInventory(pc, 60010411, 1))
                        {
                            mask.SetValue(AYEFlags.劍士系職業裝, true);
                            //_2a96 = true;
                            GiveItem(pc, 60010411, 1);
                            return;
                        }
                        Say(pc, 131, "行李太多了$R;" +
                            "去整理一下，再來吧$R;");
                        return;
                    }
                    if (pc.Job == PC_JOB.FENCER
                        || pc.Job == PC_JOB.KNIGHT
                        || pc.Job == PC_JOB.DARKSTALKER)
                    {
                        Say(pc, 131, "您是真正的戰士嗎？$R;" +
                            "那就拿著這個$R;" +
                            "$R用它證明您的力量和能力吧$R;" +
                            "$P這個武器會記錄您的戰鬥，$R;" +
                            "$R它會告訴我您的能力唷$R;");
                        if (CheckInventory(pc, 60060250, 1))
                        {
                            mask.SetValue(AYEFlags.騎士系職業裝, true);
                            //_2a97 = true;
                            GiveItem(pc, 60060250, 1);
                            return;
                        }
                        Say(pc, 131, "行李太多了$R;" +
                            "去整理一下，再來吧$R;");
                        return;
                    }
                    if (pc.Job == PC_JOB.SCOUT
                        || pc.Job == PC_JOB.ASSASSIN
                        || pc.Job == PC_JOB.COMMAND)
                    {
                        Say(pc, 131, "您是真正的戰士嗎？$R;" +
                            "那就拿著這個$R;" +
                            "$R用它證明您的力量和能力吧$R;" +
                            "$P這個武器會記錄您的戰鬥，$R;" +
                            "$R它會告訴我您的能力唷$R;");
                        if (CheckInventory(pc, 60010050, 1))
                        {
                            mask.SetValue(AYEFlags.盜賊系職業裝, true);
                            //_2a98 = true;
                            GiveItem(pc, 60010050, 1);
                            return;
                        }
                        Say(pc, 131, "行李太多了$R;" +
                            "去整理一下，再來吧$R;");
                        return;
                    }
                    if (pc.Job == PC_JOB.ARCHER
                        || pc.Job == PC_JOB.STRIKER
                        || pc.Job == PC_JOB.GUNNER)
                    {
                        Say(pc, 131, "您是真正的戰士嗎？$R;" +
                            "那就拿著這個$R;" +
                            "$R用它證明您的力量和能力吧$R;" +
                            "$P這個武器會記錄您的戰鬥，$R;" +
                            "$R它會告訴我您的能力唷$R;");
                        if (CheckInventory(pc, 60090150, 1))
                        {
                            mask.SetValue(AYEFlags.弓箭手系職業裝, true);
                            //_2a99 = true;
                            GiveItem(pc, 60090150, 1);
                            return;
                        }
                        Say(pc, 131, "行李太多了$R;" +
                            "去整理一下，再來吧$R;");
                        return;
                    }
                    Say(pc, 131, "您又不是戰士，裝懂！$R;");
                    break;
                case 2:
                    Say(pc, 131, "真是可憐啊！$R;" +
                        "現在的年輕人太弱了。$R;" +
                        "真想遇見個強悍得$R;" +
                        "可以一手把我的武器$R;" +
                        "弄斷的傢伙呢。$R;" +
                        "$P那我就能送他$R;" +
                        "我那個悲壯的……$R;");
                    break;
            }
        }
        void 剑士系完成(ActorPC pc)
        {
            BitMask<AYEFlags> mask = pc.CMask["AYE"];
            if (pc.Gender == PC_GENDER.FEMALE)
            {
                if (CheckInventory(pc, 50061850, 1))
                {
                    Say(pc, 131, "這是我以前使用的$R;" +
                        "悲壯的防具$R;" +
                        "$R送給您吧！$R;");
                    mask.SetValue(AYEFlags.職業裝任務結束, true);
                    GiveItem(pc, 50061850, 1);
                    return;
                }
                Say(pc, 131, "這是我以前使用的$R;" +
                    "悲壯的防具$R;" +
                    "$R送給您吧！$R;" +
                    "去把行李整理一下，再來吧！$R;");
                return;
            }
            if (CheckInventory(pc, 50013050, 1))
            {
                Say(pc, 131, "這是我以前使用的$R;" +
                    "悲壯的防具$R;" +
                    "$R送給您吧！$R;");
                mask.SetValue(AYEFlags.職業裝任務結束, true);
                GiveItem(pc, 50013050, 1);
                return;
            }
            Say(pc, 131, "這是我以前使用的$R;" +
                "悲壯的防具$R;" +
                "$R送給您吧！$R;" +
                "去把行李整理一下，再來吧！$R;");
        }
        void 骑士系完成(ActorPC pc)
        {
            BitMask<AYEFlags> mask = pc.CMask["AYE"];
            if (pc.Gender == PC_GENDER.FEMALE)
            {
                if (CheckInventory(pc, 50061950, 1))
                {
                    Say(pc, 131, "這是我以前使用的$R;" +
                        "悲壯的防具$R;" +
                        "$R送給您吧！$R;");
                    mask.SetValue(AYEFlags.職業裝任務結束, true);
                    GiveItem(pc, 50061950, 1);
                    return;
                }
                Say(pc, 131, "這是我以前使用的$R;" +
                    "悲壯的防具$R;" +
                    "$R送給您吧！$R;" +
                    "去把行李整理一下，再來吧！$R;");
                return;
            }
            if (CheckInventory(pc, 50013150, 1))
            {
                Say(pc, 131, "這是我以前使用的$R;" +
                    "悲壯的防具$R;" +
                    "$R送給您吧！$R;");
                mask.SetValue(AYEFlags.職業裝任務結束, true);
                GiveItem(pc, 50013150, 1);
                return;
            }
            Say(pc, 131, "這是我以前使用的$R;" +
                "悲壯的防具$R;" +
                "$R送給您吧！$R;" +
                "去把行李整理一下，再來吧！$R;");
        }
        void 盗贼系完成(ActorPC pc)
        {
            BitMask<AYEFlags> mask = pc.CMask["AYE"];
            if (CheckInventory(pc, 50025350, 1))
            {
                Say(pc, 131, "這是我以前使用的$R;" +
                    "悲壯的防具$R;" +
                    "$R送給您吧！$R;");
                mask.SetValue(AYEFlags.職業裝任務結束, true);
                GiveItem(pc, 50025350, 1);
                return;
            }
            Say(pc, 131, "這是我以前使用的$R;" +
                "悲壯的防具$R;" +
                "$R送給您吧！$R;" +
                "去把行李整理一下，再來吧！$R;");
        }
        void 弓箭手系完成(ActorPC pc)
        {
            BitMask<AYEFlags> mask = pc.CMask["AYE"];
            if (pc.Gender == PC_GENDER.FEMALE)
            {
                if (CheckInventory(pc, 50023252, 1))
                {
                    Say(pc, 131, "這是我以前使用的$R;" +
                        "悲壯的防具$R;" +
                        "$R送給您吧！$R;");
                    mask.SetValue(AYEFlags.職業裝任務結束, true);
                    GiveItem(pc, 50023252, 1);
                    return;
                }
                Say(pc, 131, "這是我以前使用的$R;" +
                    "悲壯的防具$R;" +
                    "$R送給您吧！$R;" +
                    "去把行李整理一下，再來吧！$R;");
                return;
            }
            if (CheckInventory(pc, 50023251, 1))
            {
                Say(pc, 131, "這是我以前使用的$R;" +
                    "悲壯的防具$R;" +
                    "$R送給您吧！$R;");
                mask.SetValue(AYEFlags.職業裝任務結束, true);
                GiveItem(pc, 50023251, 1);
                return;
            }
            Say(pc, 131, "這是我以前使用的$R;" +
                "悲壯的防具$R;" +
                "$R送給您吧！$R;" +
                "去把行李整理一下，再來吧！$R;");
        }
        void 法师系开始(ActorPC pc)
        {
            BitMask<AYEFlags> mask = pc.CMask["AYE"];
            if (mask.Test(AYEFlags.法師系職業裝))//_4a45)
            {
                /*
                if (_4a43)
                {
                    法师系职业判断(pc);
                    return;
                }
                //60070164, 1);
                if (a//ME.WORK0 > 0
                )
                {
                    _4a43 = true;
                    TakeItem(pc, 60070164, 1);
                    Say(pc, 131, "呵呵，您看起來經歷了很多呢。$R;" +
                        "給我武器讓我看一看吧$R;" +
                        "$R那我看看啊。$R;" +
                        "$P呵呵，武器告訴我$R;" +
                        "$R您是個真正的戰士呢。$R;" +
                        "$P真的很厲害$R;" +
                        "最近像您這樣的可真少見呢。$R;" +
                        "$R合我的心意$R;");
                    法师系职业判断(pc);
                    return;
                }
                */
                Say(pc, 131, "用這個證明您的力量和能力吧$R;" +
                    "$P這個武器會記錄您的戰鬥，$R;" +
                    "$R它會告訴我您的能力唷$R;");
                return;
            }
            Say(pc, 131, "現在的魔法師真是不行啊，$R;" +
                "練一會兒就說累，$R;" +
                "$R真是看不下去阿$R;" +
                "我以前也沒有這樣啊$R;" +
                "還是以前好啊$R;");
            switch (Select(pc, "是不是以前好呢？", "", "不是", "是的"))
            {
                case 1:
                    if (pc.Level < 45)
                    {
                        Say(pc, 131, "您這個等級低的小鬼，$R哪有資格說這樣的話阿?$R;");
                        return;
                    }
                    Say(pc, 131, "您說您是真正的魔法師嗎？$R;" +
                        "那就拿著這個$R;" +
                        "$R用它證明您的力量和能力吧$R;" +
                        "$P這個武器會記錄您的戰鬥，$R;" +
                        "$R它會告訴我您的能力唷$R;");
                    if (CheckInventory(pc, 60070164, 1))
                    {
                        mask.SetValue(AYEFlags.法師系職業裝, true);
                        //_4a45 = true;
                        GiveItem(pc, 60070164, 1);
                        return;
                    }
                    Say(pc, 131, "想給您禮物，但是行李太多了，$R;" +
                        "去整理一下，再回來吧。$R;");
                    break;
                case 2:
                    Say(pc, 131, "真是可憐啊！$R;" +
                        "現在的年輕人太弱了。$R;" +
                        "真想遇見個強悍得$R;" +
                        "可以一手把我的武器$R;" +
                        "弄斷的傢伙呢。$R;" +
                        "$P那我就能送他$R;" +
                        "我那個悲壯的……$R;");
                    break;
            }
        }

        void 法师系职业判断(ActorPC pc)
        {
            BitMask<AYEFlags> mask = pc.CMask["AYE"];
            if (pc.Job == PC_JOB.WIZARD
               || pc.Job == PC_JOB.SORCERER
               || pc.Job == PC_JOB.SAGE)
            {
                if (pc.Gender == PC_GENDER.FEMALE)
                {
                    if (CheckInventory(pc, 50012550, 1))
                    {
                        Say(pc, 131, "這是我以前使用的$R;" +
                            "悲壯的防具$R;" +
                            "$R送給您吧！$R;");
                        mask.SetValue(AYEFlags.職業裝任務結束, true);
                        GiveItem(pc, 50012550, 1);
                        return;
                    }
                    Say(pc, 131, "這是我以前使用的$R;" +
                        "悲壯的防具$R;" +
                        "$R送給您吧！$R;" +
                        "去把行李整理一下，再來吧！$R;");
                    return;
                }
                if (CheckInventory(pc, 50062250, 1))
                {
                    Say(pc, 131, "這是我以前使用的$R;" +
                        "悲壯的防具$R;" +
                        "$R送給您吧！$R;");
                    mask.SetValue(AYEFlags.職業裝任務結束, true);
                    GiveItem(pc, 50062250, 1);
                    return;
                }
                Say(pc, 131, "這是我以前使用的$R;" +
                    "悲壯的防具$R;" +
                    "$R送給您吧！$R;" +
                    "去把行李整理一下，再來吧！$R;");
                return;
            }
            if (pc.Job == PC_JOB.SHAMAN
               || pc.Job == PC_JOB.ELEMENTER
               || pc.Job == PC_JOB.ENCHANTER)
            {
                if (pc.Gender == PC_GENDER.FEMALE)
                {
                    if (CheckInventory(pc, 50012750, 1))
                    {
                        Say(pc, 131, "這是我以前使用的$R;" +
                            "悲壯的防具$R;" +
                            "$R送給您吧！$R;");
                        mask.SetValue(AYEFlags.職業裝任務結束, true);
                        GiveItem(pc, 50012750, 1);
                        return;
                    }
                    Say(pc, 131, "這是我以前使用的$R;" +
                        "悲壯的防具$R;" +
                        "$R送給您吧！$R;" +
                        "去把行李整理一下，再來吧！$R;");
                    return;
                }
                if (CheckInventory(pc, 50062350, 1))
                {
                    Say(pc, 131, "這是我以前使用的$R;" +
                        "悲壯的防具$R;" +
                        "$R送給您吧！$R;");
                    GiveItem(pc, 50062350, 1);
                    return;
                }
                Say(pc, 131, "這是我以前使用的$R;" +
                    "悲壯的防具$R;" +
                    "$R送給您吧！$R;" +
                    "去把行李整理一下，再來吧！$R;");
                return;
            }
            if (pc.Job == PC_JOB.VATES
              || pc.Job == PC_JOB.DRUID
              || pc.Job == PC_JOB.BARD)
            {
                if (pc.Gender == PC_GENDER.FEMALE)
                {
                    if (CheckInventory(pc, 50013450, 1))
                    {
                        Say(pc, 131, "這是我以前使用的$R;" +
                            "悲壯的防具$R;" +
                            "$R送給您吧！$R;");
                        mask.SetValue(AYEFlags.職業裝任務結束, true);
                        GiveItem(pc, 50013450, 1);
                        return;
                    }
                    Say(pc, 131, "這是我以前使用的$R;" +
                        "悲壯的防具$R;" +
                        "$R送給您吧！$R;" +
                        "去把行李整理一下，再來吧！$R;");
                    return;
                }
                if (CheckInventory(pc, 50062450, 1))
                {
                    Say(pc, 131, "這是我以前使用的$R;" +
                        "悲壯的防具$R;" +
                        "$R送給您吧！$R;");
                    mask.SetValue(AYEFlags.職業裝任務結束, true);
                    GiveItem(pc, 50062450, 1);
                    return;
                }
                Say(pc, 131, "這是我以前使用的$R;" +
                    "悲壯的防具$R;" +
                    "$R送給您吧！$R;" +
                    "去把行李整理一下，再來吧！$R;");
                return;
            }
            if (pc.Job == PC_JOB.WARLOCK
               || pc.Job == PC_JOB.GAMBLER
               || pc.Job == PC_JOB.NECROMANCER)
            {
                if (pc.Gender == PC_GENDER.FEMALE)
                {
                    if (CheckInventory(pc, 50062551, 1))
                    {
                        Say(pc, 131, "這是我以前使用的$R;" +
                            "悲壯的防具$R;" +
                            "$R送給您吧！$R;");
                        mask.SetValue(AYEFlags.職業裝任務結束, true);
                        GiveItem(pc, 50062551, 1);
                        return;
                    }
                    Say(pc, 131, "這是我以前使用的$R;" +
                        "悲壯的防具$R;" +
                        "$R送給您吧！$R;" +
                        "去把行李整理一下，再來吧！$R;");
                    return;
                }
                if (CheckInventory(pc, 50013551, 1))
                {
                    Say(pc, 131, "這是我以前使用的$R;" +
                        "悲壯的防具$R;" +
                        "$R送給您吧！$R;");
                    mask.SetValue(AYEFlags.職業裝任務結束, true);
                    GiveItem(pc, 50013551, 1);
                    return;
                }
                Say(pc, 131, "這是我以前使用的$R;" +
                    "悲壯的防具$R;" +
                    "$R送給您吧！$R;" +
                    "去把行李整理一下，再來吧！$R;");
                return;
            }
        }
    }
}